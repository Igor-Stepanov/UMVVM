using FluentAssertions;
using NUnit.Framework;
using ViewModels;
using ViewModels.Commands;
using ViewModels.Extensions;
using ViewModels.Properties;

namespace Tests.ViewModelsTests
{
  [TestFixture]
  public class ViewModelTests
  {
    [Test]
    public void WhenInitializeViewModel_MembersShouldBeAvailable()
    {
      // Arrange
      var viewModel = Fake.ViewModel();

      // Act
      viewModel.Initialize();
      
      // Assert
      viewModel.HasBy("FakeMember", out IViewModel _).Should().BeTrue();
      viewModel.HasBy("FakeProperty", out IProperty<int> _).Should().BeTrue();
    }

    [Test]
    public void WhenEnableViewModel_MembersShouldEnable()
    {
      // Arrange
      var viewModel = Fake.ViewModel();

      // Act
      viewModel.Initialize();
      viewModel.Enable();

      // Assert
      viewModel.Enabled.Should().BeTrue();
      viewModel.HasBy("FakeMember", out IViewModel member).Should().BeTrue();
      member.Enabled.Should().BeTrue();
    }
    
    [Test]
    public void WhenViewModelCommandInvoked_MethodShouldBeCalled()
    {
      // Arrange
      var viewModel = Fake.ViewModel();

      // Act
      viewModel.Initialize();
      viewModel.Enable();

      // Assert
      viewModel.Enabled.Should().BeTrue();
      viewModel.HasBy("FakeMethod", out Command command).Should().BeTrue();
      command.Invoke();

      viewModel.As<FakeViewModel>().MethodCalled.Should().BeTrue();
    }
    
    [Test]
    public void WhenViewModelCommandInvoked_ButViewModelIsDisabled_ExceptionShouldBeThrown()
    {
      // Arrange
      var viewModel = Fake.ViewModel();

      // Act
      viewModel.Initialize();

      // Assert
      viewModel.HasBy("FakeMethod", out Command command).Should().BeTrue();

      command.Invoke();
    }
  }
}