using GenerateEverything.Nodes.Interfaces;
using Microsoft.CodeAnalysis;

namespace GenerateEverything.Nodes
{
    public class Property : NodeBase, IProperty
    {
        public Property()
        {

        }
        public Property(IPropertySymbol property) : base(property)
        {

        }

    }
}
