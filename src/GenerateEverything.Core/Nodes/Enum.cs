using GenerateEverything.Nodes.Interfaces;
using Microsoft.CodeAnalysis;
using System.Collections.Generic;
using System.Linq;

namespace GenerateEverything.Nodes
{
    public class Enum : NodeBase
    {
        public Enum(INamedTypeSymbol type) : base(type)
        {
            Type = type;    
            ValueType = type.EnumUnderlyingType;
            Members = type.GetMembers().OfType<IFieldSymbol>()
                .Select(fieldSymbol => new Field(fieldSymbol) as IConstField).ToList();
        }

        public ITypeSymbol Type { get; }
        public INamedTypeSymbol ValueType { get; }
        public List<IConstField> Members { get; }
    }
}
