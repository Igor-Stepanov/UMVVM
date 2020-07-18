using ViewModels;

namespace Tests.ViewModelsTests
{
  public class FakeViewModel : ViewModel
  {
    public readonly EmptyViewModel Member = new EmptyViewModel();
  }

  public class EmptyViewModel : ViewModel
  {
  }
}