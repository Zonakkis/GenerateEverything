using Microsoft.CodeAnalysis;
using System;

namespace GenerateEverything.Extensions
{
    public static class AccessiblityExtension
    {
        public static string ToAccessibilityString(this Accessibility accessibility)
        {

            switch (accessibility)
            {
                case Accessibility.Public:
                    return "public";
                case Accessibility.Internal:
                    return "internal";
                case Accessibility.Private:
                    return "private";
                case Accessibility.Protected:
                    return "protected";
                case Accessibility.ProtectedAndInternal:
                    return "protected internal";
                case Accessibility.ProtectedOrInternal:
                    return "protected or internal";
                case Accessibility.NotApplicable:
                    return "";
                default:
                    throw new ArgumentOutOfRangeException(accessibility.ToString(), accessibility, "Unknown accessibility type");
            }
        }
    }
}
