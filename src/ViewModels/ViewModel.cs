using ViewModels.Members;
using ViewModels.Types;

namespace ViewModels
{
  public class ViewModel : IViewModel
  {
    private ViewModelMembers _members;

    private readonly ViewModelType _type;

    public ViewModel(IViewModelTypes types) =>
      _type = types[GetType()];

    void IViewModel.Enable()
    {
      Enable();
      EnableMembers();
    }

    void IViewModel.Disable()
    {
      DisableMembers();
      Disable();
    }

    public bool TryGetMember<T>(string name, out T member) where T : IViewModelMember =>
      _members.TryGetMember(name, out member);

    protected virtual void Enable() { }
    protected virtual void Disable() { }

    private void EnableMembers()
    {
      if (_members == null)
        _members = _type.BuildMembersOf(this);

      _members.Enable();
    }

    private void DisableMembers() =>
      _members.Disable();
  }
}