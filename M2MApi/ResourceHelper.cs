using System.Threading;

namespace M2MApi
{
  public static class ResourceHelper
  {
    private static long currentSequenceId;

    public static string GetNextId()
    {
      return Interlocked.Increment(ref currentSequenceId).ToString("X");
    }
  }
}