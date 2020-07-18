using FluentAssertions;
using NUnit.Framework;
using ViewModels;
using ViewModels.Extensions;
using Zenject;

namespace Tests.ViewModelsTests
{
  [TestFixture]
  public class ViewModelTests
  {
    private DiContainer _container;
    
    [OneTimeSetUp]
    public void SetUp()
    {
      _container = new DiContainer();
      _container
        .Bind<IViewModel>()
        .To<FakeViewModel>()
        .AsSingle();
    }
    
    [Test]
    public void WhenEnableViewModel_MembersShouldEnable()
    {
      // Arrange
      var viewModel = _container.Resolve<IViewModel>();

      // Act
      viewModel.Enable();

      // Assert
      viewModel.Enabled.Should().BeTrue();
      viewModel.HasBy("Member", out IViewModel member).Should().BeTrue();
      member.Enabled.Should().BeTrue();
    }

    [Test]
    public void WhenDisableViewModel_MembersShouldDisable()
    {
      // Arrange
      var viewModel = _container.Resolve<IViewModel>();

      // Act
      viewModel.Enable();
      viewModel.Disable();

      // Assert
      viewModel.Enabled.Should().BeFalse();
      viewModel.HasBy("Member", out IViewModel member).Should().BeTrue();
      member.Enabled.Should().BeFalse();
    }
  }
}