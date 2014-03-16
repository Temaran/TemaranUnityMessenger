using System;
using UnityEngine;

public class Tester : MonoBehaviourEx
{
    public int TestRuns = 10000000;

    public void OnGUI()
    {
        GUI.TextField(new Rect(0, 150, 600, 300), @"
My approach is slightly faster than the solution on the unify wiki for this trivial test, 
and I'm sure it could be optimized even more (for example by caching the invocation lists client side etc. etc.)
but even disregarding that, the added type safety and utility is the main reason why I use this in all my projects
and I hope you will too :)

The implementation can of course be made a LOT faster if we do away with some of the reflection.
But if you really need that extra speed increase, this solution would probably not fit you anyways.

Some more things could be done when it comes to threading as well");

        if (GUI.Button(new Rect(0, 0, 300, 50), "Test Unify Messenger for " + TestRuns + " runs..."))
        {
            var time = DateTime.UtcNow;
            for (var i = 0; i < TestRuns; i++)
                Messenger<float>.Broadcast("speed changed", Time.time);

            print("It took: " + (DateTime.UtcNow - time).TotalMilliseconds + " ms");
        }

        if (GUI.Button(new Rect(0, 60, 300, 50), "Test Temaran Messenger for " + TestRuns + " runs..."))
        {
            var time = DateTime.UtcNow;
            for (var i = 0; i < TestRuns; i++)
                Messenger.Publish(new PerformanceMessage(Time.time));

            print("It took: " + (DateTime.UtcNow - time).TotalMilliseconds + " ms");
        }
    }
}