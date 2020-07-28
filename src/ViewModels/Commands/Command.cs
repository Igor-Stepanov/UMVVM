using System;
using ViewModels.Members;

namespace ViewModels.Commands
{
  public class Command : IViewModelMember
  {
    private readonly string _name;
    private readonly Action _action;
    private readonly IViewModel _viewModel;

    public Command(string name, Action action, IViewModel viewModel) =>
      (_name, _action, _viewModel) =
      (name, action, viewModel);

    public void Invoke()
    {
      if (!_viewModel.Enabled)
        throw new InvalidOperationException($"Cannot invoke [{_name}()] " +
                                            $"when [{_viewModel.GetType().Name}] is disabled.");

      _action.Invoke();
    }
  }

  public class Command<T> : IViewModelMember
  {
    private readonly string _name;
    private readonly Action<T> _action;
    private readonly IViewModel _viewModel;

    public Command(string name, Action<T> action, IViewModel viewModel) =>
      (_name, _action, _viewModel) =
      (name, action, viewModel);

    public void Invoke(T argument)
    {
      if (!_viewModel.Enabled)
        throw new InvalidOperationException($"Cannot invoke [{_name}()] " +
                                            $"when [{_viewModel.GetType().Name}] is disabled.");

      _action.Invoke(argument);
    }
  }
}