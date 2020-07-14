using ViewModels;

namespace Tests.ViewModelsTests
{
  public static class Fake
  {
    public class FakeViewModel : ViewModel
    {
      public readonly EmptyViewModel Member = new EmptyViewModel();
    }

    public class EmptyViewModel : ViewModel
    {
    }
    
    public static IViewModel ViewModel() =>
      new FakeViewModel();
  }
}