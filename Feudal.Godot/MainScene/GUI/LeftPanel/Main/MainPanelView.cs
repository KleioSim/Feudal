using System;
using System.Linq;
using System.Reflection;

public abstract partial class MainPanelView : ViewControl
{
    internal static Type[] DerivedTypes { get; } = Assembly.GetExecutingAssembly().GetTypes()
    .Where(x => x.IsAssignableTo(typeof(MainPanelView))
        && x.IsClass && !x.IsAbstract && !x.IsGenericType)
    .ToArray();
}