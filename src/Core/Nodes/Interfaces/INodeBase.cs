using Microsoft.CodeAnalysis;

namespace GenerateEverything.Nodes.Interfaces
{
    public interface INodeBase
    {
        Accessibility Accessibility { get; }
        string Name { get; }
        string Namespace { get; }
    }
}