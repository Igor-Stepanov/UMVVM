using UnityEngine;
using ViewModels;
using ViewModels.Members;
using ViewModels.Extensions;

namespace Views
{
  public abstract class View : MonoBehaviour
  {
    [SerializeField]
    private IViewModel _viewModel;

    public bool HasBy<TMember>(string key, out TMember member)
      where TMember : IViewModelMember =>
      _viewModel.HasBy(key, out member);
    
    private void Awake()
    {
    }

    private void OnEnable() => 
      _viewModel.Enable();

    private void OnDisable() => 
      _viewModel.Disable();

    private void OnDestroy()
    {
    }
  }
}