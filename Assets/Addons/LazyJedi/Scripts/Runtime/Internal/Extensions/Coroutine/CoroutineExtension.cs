using UnityEngine;

public static class CoroutineExtension
{
    private static void EmptyCallback()
    {
    }

    // public static YieldInstruction AsDelay(this float time)
    // {
    //     return DOVirtual.DelayedCall(time, EmptyCallback, false).WaitForCompletion();
    // }
    //
    // public static YieldInstruction AsDelay(this int time)
    // {
    //     return DOVirtual.DelayedCall(time, EmptyCallback, false).WaitForCompletion();
    // }
}