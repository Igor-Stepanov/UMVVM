using ViewModels.Members;

namespace ViewModels
{
  public interface IViewModel : IViewModelMember
  {
    bool Enabled { get; }

    void Initialize();
    void Enable();
    void Disable();
    void Terminate();

    bool HasBy(string name, out IViewModelMember member);
  }
}