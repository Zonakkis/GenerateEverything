using Microsoft.CodeAnalysis;
using System;

namespace GenerateEverything.Extensions
{
    public static class TypeKindExtension
    {
        public static string ToTypeKindString(this TypeKind typeKind)
        {
            switch (typeKind)
            {
                case TypeKind.Class:
                    return "class";
                case TypeKind.Struct:
                    return "struct";
                case TypeKind.Interface:
                    return "interface";
                case TypeKind.Enum:
                    return "enum";
                case TypeKind.Delegate:
                    return "delegate";
                default:
                    throw new ArgumentOutOfRangeException(typeKind.ToString(), typeKind, "Unknown type kind");
            }
        }
    }
}
