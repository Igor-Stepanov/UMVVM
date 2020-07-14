using FluentAssertions;
using NUnit.Framework;
using ViewModels;
using ViewModels.Extensions;
using static Tests.ViewModelsTests.Fake;

namespace Tests.ViewModelsTests
{
  [TestFixture]
  public class ViewModelTests
  {
    [Test]
    public void WhenEnableViewModel_MembersShouldEnable()
    {
      // Arrange
      var viewModel = ViewModel();

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
      var viewModel = ViewModel();

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