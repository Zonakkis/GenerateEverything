using GenerateEverything.Nodes.Interfaces;
using Microsoft.CodeAnalysis;

namespace GenerateEverything.Nodes
{
    public class NodeBase : INodeBase
    {
        public NodeBase()
        {

        }
        public NodeBase(ISymbol symbol)
        {
            Namespace = symbol.ContainingNamespace.ToDisplayString();
            Accessibility = symbol.DeclaredAccessibility;
            Name = symbol.Name;
        }
        public string Namespace { get; set; }
        public Accessibility Accessibility { get; set; }
        public string Name { get; set; }
    }
}