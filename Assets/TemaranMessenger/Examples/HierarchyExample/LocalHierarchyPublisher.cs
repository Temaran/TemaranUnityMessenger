using UnityEngine;

/// <summary>
/// A specialization of the hierarchy use, and probably the one you will be using the most often :)
/// Specifying the transform.root will send it to all subscribers in the current hierarchy
/// </summary>
public class LocalHierarchyPublisher : MonoBehaviourEx
{
    public void OnGUI()
    {
        if (GUI.Button(new Rect(0, 60, 500, 50), "Fire to root from  (see console for output): " + name))
        {
            Messenger.Publish(new ExampleMessage("OMG FROM THE ROOT!!!!", renderer), transform.root);
            Messenger.Publish(new NewInformationMessage(
@"This button just sent a message directed at its own transform.
This means that this gameobject itself, as well as 
all of its children will be notified of the message.

This is useful when you want to tell a part of your own
hierarchy something important. Maybe you want all your scripts to know
that you just added a new +2 strength item to your inventory.

The messenger works by looking if the current handle has
the transform you specified as an ancestor. If it does, 
it tells the handle to execute, if it doesn't, it skips the handle"));
        }
    }
}