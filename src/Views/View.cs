using UnityEngine;
using ViewModels;

namespace Views
{
  public class View : MonoBehaviour, IView
  {
    [SerializeField]
    private IViewModel _viewModel;
    
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