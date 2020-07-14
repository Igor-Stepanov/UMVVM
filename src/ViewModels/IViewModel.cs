using ViewModels.Members;

namespace ViewModels
{
  public interface IViewModel : IViewModelMember
  {
    bool Enabled { get; }

    void Enable();
    void Disable();

    bool TryGetValue(string name, out IViewModelMember member);
  }
}