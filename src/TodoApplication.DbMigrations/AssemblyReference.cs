using System.Reflection;

namespace TodoApplication.DbMigrations;

public static class AssemblyReference
{
    public static readonly Assembly Assembly = typeof(AssemblyReference).Assembly;
}
