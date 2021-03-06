using System;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Docs.GitHub.Documents;
using Volo.Docs.Projects;

namespace Volo.Docs.GitHub.Projects
{
    public static class ProjectGithubExtensions
    {
        public static string GetGitHubUrl([NotNull] this Project project)
        {
            CheckGitHubProject(project);
            return project.ExtraProperties["GitHubRootUrl"] as string;
        }

        public static string GetGitHubUrl([NotNull] this Project project, string version)
        {
            return project
                .GetGitHubUrl()
                .Replace("{version}", version);
        }

        public static string GetGitHubUrlForCommitHistory([NotNull] this Project project)
        {
            return project
                .GetGitHubUrl()
                .Replace("github.com", "api.github.com/repos")
                .Replace("tree/{version}/", "commits?path=");
        }

        public static void SetGitHubUrl([NotNull] this Project project, string value)
        {
            CheckGitHubProject(project);
            project.ExtraProperties["GitHubRootUrl"] = value;
        }

        public static string GetGitHubAccessTokenOrNull([NotNull] this Project project)
        {
            CheckGitHubProject(project);
            return project.ExtraProperties["GitHubAccessToken"] as string;
        }

        public static void SetGitHubAccessToken([NotNull] this Project project, string value)
        {
            CheckGitHubProject(project);
            project.ExtraProperties["GitHubAccessToken"] = value;
        }

        private static void CheckGitHubProject(Project project)
        {
            Check.NotNull(project, nameof(project));

            if (project.DocumentStoreType != GithubDocumentStore.Type)
            {
                throw new ApplicationException("Given project has not a Github document store!");
            }
        }
    }
}