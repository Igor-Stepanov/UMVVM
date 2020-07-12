using System;
using Models.Acquisition.Extensions;

namespace Models.Acquisition
{
  public class Acquire<TModel> : IAcquire<TModel>, IDisposable
    where TModel : class, IModel
  {
    public TModel Model { get; private set; }

    [ThreadStatic] private static Acquire<TModel> _acquired;

    public Acquire(TModel model)
    {
      if (_acquired?.Model != model)
      {
        model.EnterMonitor();
        _acquired = this;
      }

      Model = model;
    }

    void IDisposable.Dispose()
    {
      var model = Model;
      Model = null;

      if (_acquired != this)
        return;

      _acquired = null;
      model.ExitMonitor();
    }
  }
}