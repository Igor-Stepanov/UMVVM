using ViewModels;
using ViewModels.Commands;
using ViewModels.Properties;

namespace Tests.ViewModelsTests
{
  public static class Fake
  {
    public static IViewModel ViewModel() =>
      new FakeViewModel();
  }
  
  public class FakeViewModel : ViewModel
  {
    public bool MethodCalled { get; private set; }

    public readonly Property<int> FakeProperty = new Property<int>();
    public readonly EmptyViewModel FakeMember = new EmptyViewModel();

    [Command]
    public void FakeMethod() =>
      MethodCalled = true;

  }

  public class EmptyViewModel : ViewModel
  {
  }
}