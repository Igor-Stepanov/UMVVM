using System;
using System.Linq;
using System.Reflection;
using ViewModels.Commands;
using ViewModels.Members;
using static System.Reflection.BindingFlags;

namespace ViewModels.Types
{
  public class ViewModelType
  {
    public int MembersCount => 
      Fields.Length + Methods.Length;

    // TODO: Profile and cache
    public int ViewModelsCount =>
      Fields.Where(Implements<IViewModel>)
        .Count();

    public readonly FieldInfo[] Fields;
    public readonly MethodInfo[] Methods;

    public ViewModelType(Type viewModelType)
    {
      Fields = viewModelType
        .GetFields(Instance | Public | NonPublic)
        .Where(Implements<IViewModelMember>)
        .ToArray();

      Methods = viewModelType
        .GetMethods(Instance | Public | NonPublic)
        .Where(Has<CommandAttribute>)
        .ToArray();
    }

    private static bool Implements<TInterface>(FieldInfo x) =>
      typeof(TInterface).IsAssignableFrom(x.FieldType);

    private static bool Has<TAttribute>(MethodInfo x) where TAttribute : Attribute =>
      x.GetCustomAttribute<TAttribute>(false) != null;

    public ViewModelMembers BuildMembersOf(IViewModel viewModel) => 
      new ViewModelMembers(viewModel, this);
  }
}