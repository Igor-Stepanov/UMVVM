using System;

namespace Models.Acquisition
{
  public interface IAcquired<out TModel> : IDisposable where TModel : IModel
  {
    TModel Model { get; }
  }
}