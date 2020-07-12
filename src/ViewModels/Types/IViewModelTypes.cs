using System;

namespace ViewModels.Types
{
  public interface IViewModelTypes
  {
    ViewModelType this[Type type] { get; }
  }
}