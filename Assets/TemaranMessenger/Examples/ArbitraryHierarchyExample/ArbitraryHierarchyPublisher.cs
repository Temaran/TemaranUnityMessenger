using UnityEngine;

/// <summary>
/// The point here is that we can decide which clients get the message depending on where they are in an object hierarchy
/// Only clients that are decendants of the transform you specify will be eligable.
/// </summary>
public class ArbitraryHierarchyPublisher : MonoBehaviourEx
{
    public void OnGUI()
    {
        GUI.TextField(new Rect(0, 400, 500, 300), @"
The point here is that we can decide which clients get the message 
depending on where they are in an object hierarchy
Only clients that are decendants of the transform you 
specify will be eligable.");

        if (GUI.Button(new Rect(0, 0, 500, 50), "Fire to own transform from (see console for output): " + name))
            Messenger.Publish(new ExampleMessage("FROM ME? SICK!!!", renderer), transform);
    }
}