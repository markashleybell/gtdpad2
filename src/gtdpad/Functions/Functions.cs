using System;
using Microsoft.AspNetCore.Mvc;
using static gtdpad.Constants;

namespace gtdpad.Functions
{
    public static class Functions
    {
        public static bool IsNonEmptyAndLocal(this IUrlHelper urlHelper, Uri uri) =>
            uri is object && urlHelper.IsLocalUrl(uri.ToString());

#pragma warning disable CA1055 // Uri return values should not be strings
        public static string GetUrl(string controllerName, string actionName) =>
#pragma warning restore CA1055 // Uri return values should not be strings
            $"/{controllerName.Replace(ControllerSuffix, string.Empty, StringComparison.OrdinalIgnoreCase)}/{actionName}";
    }
}
