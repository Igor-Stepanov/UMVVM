using System;
using ViewModels.Members;

namespace ViewModels.Properties
{
  public interface IProperty : IViewModelMember
  {
    event Action Changed;
    void NotifyIfChanged();
  }

  public interface IProperty<out T> : IProperty
  {
    T Value { get; }
  }
}