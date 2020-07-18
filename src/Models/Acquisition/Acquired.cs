using System;
using Models.Acquisition.Extensions;

namespace Models.Acquisition
{
  public class Acquired<TModel> : IAcquired<TModel>
    where TModel : class, IModel
  {
    public TModel Model { get; private set; }

    [ThreadStatic] private static Acquired<TModel> _acquired;

    public Acquired(TModel model)
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