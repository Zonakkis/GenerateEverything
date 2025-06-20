using GenerateEverything.Nodes.Interfaces;
using Microsoft.CodeAnalysis;

namespace GenerateEverything.Nodes
{
    public class Field : NodeBase, IConstField
    {
        public Field()
        {

        }

        public Field(IFieldSymbol field) : base(field)
        {
            IsConst = field.IsConst;
            ConstantValue = field.ConstantValue;
        }

        public bool IsConst { get; }
        public object ConstantValue { get; }
    }
}
