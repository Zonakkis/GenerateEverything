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
            Type = type.EnumUnderlyingType;
            Members = type.GetMembers().OfType<IFieldSymbol>()
                .Select(fieldSymbol => new Field(fieldSymbol) as IConstField).ToList();
        }

        public INamedTypeSymbol Type { get; }

        public List<IConstField> Members { get; }
    }
}
