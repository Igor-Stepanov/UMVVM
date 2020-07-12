using ViewModels.Members;

namespace ViewModels
{
  public interface IViewModel : IViewModelMember
  {
    void Enable();
    void Disable();

    bool TryGetMember<T>(string name, out T member)
      where T : IViewModelMember;
  }
}