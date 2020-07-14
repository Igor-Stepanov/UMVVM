using ViewModels.Members;

namespace ViewModels.Types
{
  public static class ViewModelTypes
  {
    // TODO: Implement caching
    public static ViewModelMembers MembersOf(ViewModel viewModel) =>
      new ViewModelType(viewModel.GetType())
        .BuildMembersOf(viewModel);
  }
}