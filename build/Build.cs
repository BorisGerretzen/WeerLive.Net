using System.Linq;
using Nuke.Common;
using Nuke.Common.CI.GitHubActions;
using Nuke.Common.IO;
using Nuke.Common.ProjectModel;
using Nuke.Common.Tooling;
using Nuke.Common.Tools.DotNet;
using Nuke.Common.Tools.GitVersion;
using Nuke.Common.Utilities.Collections;
using static Nuke.Common.Tools.DotNet.DotNetTasks;
using static Nuke.Common.Tools.Git.GitTasks;

[GitHubActions(
    "test",
    GitHubActionsImage.UbuntuLatest,
    On = [GitHubActionsTrigger.PullRequest],
    InvokedTargets = [nameof(Test)],
    FetchDepth = 0
)]
[GitHubActions(
    "publish",
    GitHubActionsImage.UbuntuLatest,
    On = [GitHubActionsTrigger.WorkflowDispatch],
    InvokedTargets = [nameof(Pack), nameof(Push)],
    ImportSecrets = [nameof(NugetApiKey)],
    FetchDepth = 0
)]
class Build : NukeBuild
{
    [Parameter("Configuration to build - Default is 'Debug' (local) or 'Release' (server)")]
    readonly Configuration Configuration = IsLocalBuild ? Configuration.Debug : Configuration.Release;

    [GitVersion] readonly GitVersion GitVersion;

    [Parameter("API Key for the NuGet server.")] [Secret] readonly string NugetApiKey;

    [Parameter("NuGet server URL.")] readonly string NugetSource = "https://api.nuget.org/v3/index.json";

    [Parameter("NuGet package version.")] readonly string PackageVersion;

    [Solution] readonly Solution Solution;

    AbsolutePath SourcesDirectory => RootDirectory / "src";
    AbsolutePath ArtifactsDirectory => RootDirectory / "artifacts";

    Project LibProject => Solution.GetProject("WeerLive.Lib");

    Target Clean => t => t
        .Before(Restore)
        .Executes(() =>
        {
            SourcesDirectory.GlobDirectories("**/bin", "**/obj").ForEach(path => path.DeleteDirectory());
            SourcesDirectory.GlobDirectories("**/bin", "**/obj").ForEach(path => path.DeleteDirectory());
            ArtifactsDirectory.CreateOrCleanDirectory();
        });

    Target Restore => t => t
        .Executes(() =>
        {
            DotNetRestore(s => s
                .SetProjectFile(Solution)
            );
        });

    Target Compile => t => t
        .DependsOn(Restore)
        .Executes(() =>
        {
            DotNetBuild(s => s
                .EnableNoRestore()
                .SetProjectFile(Solution)
                .SetConfiguration(Configuration)
                .SetAssemblyVersion(GitVersion.AssemblySemVer)
                .SetFileVersion(GitVersion.AssemblySemFileVer)
                .SetInformationalVersion(GitVersion.InformationalVersion)
            );

            DotNetPublish(s => s
                .EnableNoRestore()
                .EnableNoBuild()
                .SetConfiguration(Configuration)
                .SetAssemblyVersion(GitVersion.AssemblySemVer)
                .SetFileVersion(GitVersion.AssemblySemFileVer)
                .SetInformationalVersion(GitVersion.InformationalVersion)
                .CombineWith(
                    from project in new[] { LibProject }
                    from framework in project.GetTargetFrameworks()
                    select new { project, framework }, (cs, v) => cs
                        .SetProject(v.project)
                        .SetFramework(v.framework)
                )
            );
        });

    Target Test => t => t
        .DependsOn(Compile)
        .Executes(() =>
        {
            DotNetTest(s => s
                .SetProjectFile(Solution)
                .SetConfiguration(Configuration)
                .SetFramework("net8.0")
                .EnableNoRestore()
                .EnableNoBuild()
            );
        });

    Target Pack => t => t
        .DependsOn(Clean, Test)
        .Requires(() => Configuration == Configuration.Release)
        .Executes(() =>
        {
            DotNetPack(s => s
                .EnableNoRestore()
                .EnableNoBuild()
                .SetProject(LibProject)
                .SetConfiguration(Configuration)
                .SetOutputDirectory(ArtifactsDirectory)
                .SetProperty("PackageVersion", PackageVersion ?? GitVersion.AssemblySemFileVer)
            );
        });

    Target Push => t => t
        .After(Pack)
        .Executes(() =>
        {
            DotNetNuGetPush(s => s
                .SetSource(NugetSource)
                .SetApiKey(NugetApiKey)
                .CombineWith(ArtifactsDirectory.GlobFiles("*.nupkg"), (s, v) => s
                    .SetTargetPath(v)
                )
            );

            var version = PackageVersion ?? GitVersion.AssemblySemFileVer;

            // check if current version is already tagged
            if (Git($"tag --list {version}").Count != 0)
                return;

            Git("config --global user.email \"<>\"");
            Git("config --global user.name \"GitHub Actions\"");
            Git($"tag -a {version} -m \"Created release '{version}'\"");
            Git($"push --tags");
        });

    public static int Main() => Execute<Build>(x => x.Test);
}