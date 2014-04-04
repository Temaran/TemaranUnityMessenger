// THIS CODE AND INFORMATION ARE PROVIDED "AS IS" WITHOUT WARRANTY OF ANY 
// KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE
// IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
// PARTICULAR PURPOSE.
//
// Created by Fredrik Lindh (Temaran)
// Contact me at: temaran (at) gmail (dot) com
// Last changed: 2014-03-15
// You're free to do whatever you want with the code except claiming it's you who wrote it.
// I would also appreciate it if you kept this file header as a thank you for the code :)

using UnityEngine;

/// <summary>
/// This class should become your new MonoBehavior class, EVERYTHING should derive from this to make it easy to use the messenger.
/// Alternatively, if you already have such a class, just smack this stuff in there :)
/// </summary>
public class MonoBehaviourEx : MonoBehaviour
{
    /// <summary>
    /// This messenger is global to the entire game
    /// </summary>
    public static IEventAggregator Messenger = new EventAggregator();

    public virtual void Awake()
    {
        Messenger.Subscribe(this);
    }
}