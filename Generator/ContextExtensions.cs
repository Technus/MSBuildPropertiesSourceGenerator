using Microsoft.CodeAnalysis;
using System.Collections.Generic;
using System.Linq;

namespace Generator
{
    internal static class ContextExtensions
    {
        private const string SourceItemGroupMetadata = "build_metadata.AdditionalFiles.SourceItemGroup";

        internal static string GetMSBuildProperty(
            this GeneratorExecutionContext context,
            string name,
            string defaultValue = null)
        {
            context.AnalyzerConfigOptions.GlobalOptions.TryGetValue($"{name}", out var value);
            return value ?? defaultValue;
        }

        internal static string[] GetMSBuildItems(this GeneratorExecutionContext context, string name)
            => context
                .AdditionalFiles
                .Where(f => context.AnalyzerConfigOptions
                    .GetOptions(f)
                    .TryGetValue(SourceItemGroupMetadata, out var sourceItemGroup)
                    && sourceItemGroup == name)
                .Select(f => f.Path)
                .ToArray();
    }
}
