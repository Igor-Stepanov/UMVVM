using ViewModels.Members;

namespace ViewModels.Extensions
{
  public static class ViewModelExtensions
  {
    public static bool HasBy<TMember>(this IViewModel self, string key, out TMember member)
      where TMember : IViewModelMember
    {
      member = default(TMember);

      if (!self.TryGetValue(key, out var value))
        return false;

      if (!(value is TMember castedValue))
        return false;

      member = castedValue;
      return true;
    }
  }
}