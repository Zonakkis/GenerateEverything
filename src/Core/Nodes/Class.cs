using GenerateEverything.Nodes.Interfaces;
using Microsoft.CodeAnalysis;
using System.Collections.Generic;
using System.Linq;

namespace GenerateEverything.Nodes
{
    public class Class : NodeBase
    {
        public Class(INamedTypeSymbol type) : base(type)
        {
            Fields = type.GetMembers().OfType<IFieldSymbol>()
                .Select(fieldSymbol => new Field(fieldSymbol) as IField).ToList();
            Properties = type.GetMembers().OfType<IPropertySymbol>()
                .Select(propertySymbol => new Property(propertySymbol) as IProperty).ToList();
        }

        public List<IField> Fields { get; }
        public List<IProperty> Properties { get; }
    }
}
