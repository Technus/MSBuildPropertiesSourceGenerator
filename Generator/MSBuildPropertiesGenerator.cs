using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;
using System;
using System.Text;

namespace Generator
{
    [Generator]
    public class MSBuildPropertiesGenerator : ISourceGenerator
    {
        public void Initialize(GeneratorInitializationContext context)
        {

        }

        public void Execute(GeneratorExecutionContext context)
        {
            var source=$@"
namespace {context.GetMSBuildProperty("build_property.rootnamespace")}
{{
    internal static class MSBuildProperties
    {{

{GenerateFields(context)}
    }}
}}
";
            context.AddSource("MSBuildConstants.g.cs",SourceText.From(source, Encoding.UTF8));
        }

        private string GenerateFields(GeneratorExecutionContext context)
        {
            var builder = new StringBuilder();

            foreach (var prop in context.AnalyzerConfigOptions.GlobalOptions.Keys)
            {
                var name = prop.Replace(".", "__");
                var value = context.GetMSBuildProperty(prop).Replace("\"", "\"\"");
                builder.AppendLine($@"        internal const string {name} = @""{value}"";");
            }

            return builder.ToString();
        }
    }
}
