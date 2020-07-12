using System;
using ViewModels.Members;

namespace ViewModels.Commands
{
  public class Command : IViewModelMember
  {
    private readonly Action _action;

    public Command(Action action) => 
      _action = action;
  }
  
  public class Command<T> : IViewModelMember
  {
    private readonly Action<T> _action;

    public Command(Action<T> action) => 
      _action = action;
  }
}