using ViewModels.Members;
using static ViewModels.Types.ViewModelTypes;

namespace ViewModels
{
  public abstract class ViewModel : IViewModel
  {
    public bool Enabled { get; private set; }
    
    private readonly ViewModelMembers _members;

    protected ViewModel() => 
      _members = MembersOf(this);

    void IViewModel.Enable()
    {
      if (Enabled)
        return;
      
      Enable();
      _members.Enable();

      Enabled = true;
    }

    void IViewModel.Disable()
    {
      if (!Enabled)
        return;

      _members.Disable();
      Disable();
      
      Enabled = false;
    }

    public bool TryGetValue(string name, out IViewModelMember member) => 
      _members.HasBy(name, out member);

    protected virtual void Enable() { }
    protected virtual void Disable() { }
  }
}