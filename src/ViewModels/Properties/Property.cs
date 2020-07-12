using System;
using ViewModels.Properties.Extensions;

namespace ViewModels.Properties
{
  public class Property<T> : IProperty<T>
  {
    public event Action Changed;

    public T Value { get; private set; }
    private bool _dirty;

    public void Set(T value)
    {
      Value = value;
      _dirty = true;
    }

    void IProperty.NotifyIfChanged()
    {
      Changed.InvokeIf(_dirty);
      _dirty = false;
    }
  }
}