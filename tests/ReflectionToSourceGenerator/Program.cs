using GenerateEverything.Attributes;
using ReflectionToSourceGenerator;

[GetEnumInfo(typeof(StudentType))]
internal class Program
{
    private static void Main(string[] args)
    {
        foreach (var name in studentTypes)
        {
            Console.WriteLine(name);
        }
        Console.ReadLine();
    }

    [GetClassInfo(typeof(Student))]
    static readonly List<string> names = StudentInfo.Properties.Select(property => property.Name).ToList();


    static readonly List<string> studentTypes = StudentTypeInfo.Names;
}