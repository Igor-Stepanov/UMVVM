using System;
using System.Threading;

namespace Models.Acquisition.Extensions
{
  public static class ModelMonitorExtensions
  {
    private const int LockAcquiringTimeoutMs = 10_000;

    public static void EnterMonitor(this IModel self)
    {
      if (Monitor.TryEnter(self, LockAcquiringTimeoutMs))
        return;

      throw new InvalidOperationException("Lock timeout.");
    }

    public static void ExitMonitor(this IModel self) =>
      Monitor.Exit(self);
  }
}