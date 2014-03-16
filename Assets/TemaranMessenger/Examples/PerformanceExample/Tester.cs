using System;
using UnityEngine;

public class Tester : MonoBehaviourEx
{
    public int TestRuns = 10000000;

    public void OnGUI()
    {
        GUI.TextField(new Rect(310, 0, 800, 500), @"
My approach is slightly faster than the solution on the unify wiki for this trivial test, 
and I'm sure it could be optimized even more (for example by caching the invocation lists client side etc. etc.)
but even disregarding that, the added type safety and utility is the main reason why I use this in all my projects
and I hope you will too :)

The implementation can of course be made a LOT faster if we do away with some of the reflection.
But if you really need that extra speed increase, this solution would probably not fit you anyways.

Some more things could be done when it comes to threading as well

Estimated run time for 10.000.000 message publishes:
Unify: 2.18 seconds
Temaran: 1.84 seconds with the extra guard,  1.72 seconds without it *

Not at all representative, but for fun:
Unity broadcast message (THIS IS FOR ONE OBJECT):  6.37 seconds**

The numbers speak for themselves, mine is in the lead in this test,
Unify works well, but lacks features such as type safety and versatility
and NO GAME BEYOND THE MOST SIMPLE SHOULD EVER USE UNITY BROADCASTING.
Do not be fooled by the unity figure (even though it is dead last).
For a project with 100 objects, mine and unifys solution would still be pretty fast, while any dynamic broadcasting
solution would be utterly useless.

* Check EventAggregator.cs on row 82 for more info
** Please note that this is with the TARGET CACHED. This means that it is incredibly inflexible.
   Running it dynamically though (with FindObjectOfType<GameObject>()) is pure stupidity, just for fun I started that test here
   and it ran for 20minutes without completing.
   Please note that the unity broadcast message solution scales HORRIBLY since you need to iterate over all gameobjects in the scene. 
   MULTIPLY THIS TIME BY THE NUMBER OF OBJECTS IN YOUR SCENE TO GET THE TRUE PERFORMANCE
   This is not true for the unify or my solution which cache and call only the needed ones.");

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
        if (GUI.Button(new Rect(0, 120, 300, 50), "Test Unity broadcasting for " + TestRuns + " runs..."))
        {
            var time = DateTime.UtcNow;
            var cachedObject = GameObject.Find("UnityBroadcasting");
            for (var i = 0; i < TestRuns; i++)
            {
                cachedObject.BroadcastMessage("TestMethod", Time.time);
            }

            print("(NOT COMPARABLE TO THE OTHER TWO) It took: " + (DateTime.UtcNow - time).TotalMilliseconds + " ms");
        }

        //This runs in 20+ minutes on 10.000.000 message publishes. Try on your own risk
        //        if (GUI.Button(new Rect(0, 120, 300, 50), "Test Unity broadcasting for " + TestRuns + " runs..."))
        //        {
        //            var time = DateTime.UtcNow;
        //            for (var i = 0; i < TestRuns; i++)
        //            {
        //                var allObjects = FindObjectOfType<GameObject>();
        //                allObjects.BroadcastMessage("TestMethod", Time.time);
        //            }
        //
        //            print("It took: " + (DateTime.UtcNow - time).TotalMilliseconds + " ms");
        //        }
    }
}