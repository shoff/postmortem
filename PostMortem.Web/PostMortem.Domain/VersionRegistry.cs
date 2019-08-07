namespace PostMortem.Domain
{
    using System.Linq;

    internal static class VersionRegistry
    {
        private static readonly string[] versions = { "v0", "v1" };

        public static string GetLatestVersionInformation()
        {
            return versions.Last();
        }
    }
}