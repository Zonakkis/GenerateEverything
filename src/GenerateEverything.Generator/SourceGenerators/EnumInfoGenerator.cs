using GenerateEverything.Attributes;
using GenerateEverything.Extensions;
using GenerateEverything.Nodes;
using GenerateEverything.Nodes.Interfaces;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;

namespace GenerateEverything.SourceGenerators
{
    [Generator]
    internal class EnumInfoGenerator : IIncrementalGenerator
    {
        static bool Predicate(SyntaxNode node, CancellationToken _)
        {
            return (node is FieldDeclarationSyntax field && field.AttributeLists.Any())
               || (node is PropertyDeclarationSyntax property && property.AttributeLists.Any())
               || (node is MethodDeclarationSyntax method && method.AttributeLists.Any())
               || (node is ClassDeclarationSyntax @class && @class.AttributeLists.Any());
        }
        static ISymbol Transform(GeneratorSyntaxContext context, CancellationToken _)
        {
            var node = context.Node;
            SyntaxNode variable = node;
            if (node is FieldDeclarationSyntax field)
                variable = field.Declaration.Variables.FirstOrDefault();
            else if (node is PropertyDeclarationSyntax property)
                variable = property;
            else if (node is MethodDeclarationSyntax method)
                variable = method;
            else if (node is ClassDeclarationSyntax @class)
                variable = @class;
            return context.SemanticModel.GetDeclaredSymbol(variable, cancellationToken: _);
        }
        static bool Where(ISymbol node)
        {
            return node != null &&
            node.GetAttributes().Any(attribute =>
                attribute.AttributeClass?.ToDisplayString()
                == $"{nameof(GenerateEverything)}.{nameof(Attributes)}.{nameof(GetEnumInfo)}");
        }
        public void Initialize(IncrementalGeneratorInitializationContext context)
        {
            var nodes = context.SyntaxProvider
                .CreateSyntaxProvider(
                    predicate: Predicate,
                    transform: Transform)
                .Where(Where);
            context.RegisterSourceOutput(nodes, (spc, node) =>
            {
                if (node is null) return;
                var attribute = node.GetAttributes()
                    .FirstOrDefault(attr =>
                    {
                        return attr.AttributeClass?.ToDisplayString()
                        == $"{nameof(GenerateEverything)}.{nameof(Attributes)}.{nameof(GetEnumInfo)}";
                    });
                if (attribute is null) return;
                var typeArgument = attribute.ConstructorArguments.First();
                if (typeArgument.Kind == TypedConstantKind.Type
                && typeArgument.Value is INamedTypeSymbol enumSymbol
                && enumSymbol.TypeKind == TypeKind.Enum)
                {
                    GenerateEnumInfo(new Enum(enumSymbol), spc);
                }

            });
        }

        public static void GenerateEnumInfo(Enum @enum, SourceProductionContext context)
        {
            var type = @enum.Type.Name;
            var members = @enum.Members ?? new List<IConstField>();
            string valueType = @enum.Type?.ToDisplayString(SymbolDisplayFormat.MinimallyQualifiedFormat) ?? typeof(object).Name;
            var source = $@"
using {nameof(GenerateEverything)};
using {nameof(GenerateEverything)}.{nameof(Nodes)};
using {nameof(GenerateEverything)}.{nameof(Nodes)}.{nameof(Nodes.Interfaces)};
using System.Collections.Generic;
namespace {@enum.Namespace}
{{
    {@enum.Accessibility.ToAccessibilityString()} static class {@enum.Name}Info
    {{
        public static List<string> Names {{ get; }} = new List<string>()
        {{
            {string.Join(",\n            ", members.Select(member => $"\"{member.Name}\""))}
        }};

        public static List<{type}> Members {{ get; }} = new List<{type}>()
        {{
            {string.Join(",\n            ", members.Select(member => $"{type}.{member.Name}"))}
        }};

        public static Dictionary<string, {type}> NameToMember {{ get; }} = new Dictionary<string, {type}>()
        {{
            {string.Join(",\n            ", members.Select(member => $"{{ \"{member.Name}\", {type}.{member.Name} }}"))}
        }};
    }}
}}";
            context.AddSource($"{@enum.Name}Info.g.cs", source);
        }
    }
}
