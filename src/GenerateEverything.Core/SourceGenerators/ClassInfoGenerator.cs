using GenerateEverything.Attributes;
using GenerateEverything.Extensions;
using GenerateEverything.Nodes;
using GenerateEverything.Nodes.Interfaces;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Linq;
using System.Threading;

namespace GenerateEverything.SourceGenerators
{
    [Generator]
    public class ClassInfoGenerator : IIncrementalGenerator 
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
            SyntaxNode stytaxNode = node;
            if (node is FieldDeclarationSyntax field)
                stytaxNode = field.Declaration.Variables.FirstOrDefault();
            else if(node is PropertyDeclarationSyntax property)
                stytaxNode = property;
            else if(node is MethodDeclarationSyntax method)
                stytaxNode = method;
            else if(node is ClassDeclarationSyntax @class)
                stytaxNode = @class;
            return context.SemanticModel.GetDeclaredSymbol(stytaxNode, cancellationToken: _);
        }
        static bool Where(ISymbol node)
        {
            return node != null &&
            node.GetAttributes().Any(attribute =>
                attribute.AttributeClass?.ToDisplayString() 
                == typeof(GetClassInfo).FullName);
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
                    attr.AttributeClass?.ToDisplayString() == typeof(GetClassInfo).FullName);
                if (attribute is null) return;
                var typeArgument = attribute.ConstructorArguments.First();
                if (typeArgument.Kind == TypedConstantKind.Type 
                && typeArgument.Value is INamedTypeSymbol typeSymbol
                && typeSymbol.TypeKind == TypeKind.Class)
                {
                    GenerateClassInfo(new Class(typeSymbol), spc);
                }  

            });
        }

        public static void GenerateClassInfo(Class @class, SourceProductionContext context)
        {
            var fields = @class.Fields;
            var properties = @class.Properties;
            var source = $@"
using {GeneratorInfo.Namespace};
using {GeneratorInfo.Namespace}.Nodes;
using {GeneratorInfo.Namespace}.Nodes.Interfaces;
namespace {@class.Namespace}
{{
    {@class.Accessibility.ToAccessibilityString()} static class {@class.Name}Info
    {{
        public static List<{nameof(IField)}> Fields {{ get; }} = new List<{nameof(IField)}>()
        {{
            {string.Join(",\n            ", fields.Select(field => $"new {nameof(Field)} {{ {nameof(Field.Name)} = \"{field.Name}\" }}"))}
        }};

        public static List<{nameof(IProperty)}> Properties {{ get; }} = new List<{nameof(IProperty)}>()
        {{
            {string.Join(",\n            ", properties.Select(property => $"new {nameof(Property)} {{ {nameof(Property.Name)} = \"{property.Name}\" }}"))}
        }};
    }}
}}";
            context.AddSource($"{@class.Name}Info.g.cs", source);
        }
    }
}
