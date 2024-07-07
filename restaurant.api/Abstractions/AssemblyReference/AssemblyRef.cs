using System.Reflection;

namespace restaurant.api.Abstractions.AssemblyReference;

public static class AssemblyRef
{
    public static readonly Assembly Reference = typeof(AssemblyRef).Assembly;
}
