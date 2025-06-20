using Core.Tests.Models;
using GenerateEverything.Attributes;
using System.Reflection;
using Xunit.Abstractions;
namespace Core.Tests.SourceGenerators
{
    public class ClassInfoGeneratorTests(ITestOutputHelper output)
    {
        [Fact]
        [GetClassInfo(typeof(Student))]
        public void Fields_ShouldBe_Generated()
        {
            var reflectionFields = typeof(Student)
                .GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            var generatedFields = StudentInfo.Fields;
            var reflectionFieldNames = reflectionFields
                .Select(f => f.Name).ToList();
            var generatedFieldNames = generatedFields
                .Select(f => f.Name).ToList();
            Assert.True(new HashSet<string>(reflectionFieldNames)
                .SetEquals(generatedFieldNames));
        }
        [Fact]
        [GetEnumInfo(typeof(StudentType))]
        public void Members_ShouldBe_Generated()
        {

            var reflectionFields = typeof(StudentType)
                .GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            var reflectionFieldNames = reflectionFields
                .Select(f => f.Name).ToList();
            var generatedFieldNames = StudentTypeInfo.Names;
            Assert.True(new HashSet<string>(reflectionFieldNames)
                .SetEquals(generatedFieldNames));
        }
    }
}
