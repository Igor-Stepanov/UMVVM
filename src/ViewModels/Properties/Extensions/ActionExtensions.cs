using System;

namespace ViewModels.Properties.Extensions
{
  public static class ActionExtensions
  {
    public static void InvokeIf(this Action self, bool condition)
    {
      if (condition)
        self?.Invoke();
    }
  }
}