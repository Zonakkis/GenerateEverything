namespace GenerateEverything.Nodes.Interfaces
{
    public interface IConstField : IField
    {
        bool IsConst { get; }
        object ConstantValue { get; }
    }
}
