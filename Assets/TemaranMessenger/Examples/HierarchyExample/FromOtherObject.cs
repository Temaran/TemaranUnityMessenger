using UnityEngine;

/// <summary>
/// The idea here is that you can target certain parts of a hierarchy to notify all of it's children of something.
/// For example, imagine you have a hitscan weapon that just hit some poor sods arm, you can now choose to send the take damage message
/// to all of the target (send to target.transform.root), or only to the arm and it's children (target.transform)
/// </summary>
public class FromOtherObject : MonoBehaviourEx
{
    public GameObject target;

    public void OnGUI()
    {
        if (GUI.Button(new Rect(0, 120, 500, 50), "Fire to target from (see console for output): " + name))
        {
            Messenger.Publish(new ExampleMessage("OMG FROM OTHER OBJECT!!!!", renderer), target.transform);
            Messenger.Publish(new NewInformationMessage(
@"This button triggered a message from the ""Other object""
in the hierarchy directed towards its target transform.

This is useful when you want to tell a specific sub-part of a 
gameobject something. Maybe you just want the right arm of your
enemy monster to receive the take damage message.

The messenger works by looking if the current handle has
the transform you specified as an ancestor. If it does, 
it tells the handle to execute, if it doesn't, it skips the handle"));
        }

        if (GUI.Button(new Rect(0, 180, 500, 50), "Fire to target root from (see console for output): " + name))
        {
            Messenger.Publish(new ExampleMessage("OMG FROM OTHER OBJECT!!!!", renderer), target.transform.root);
            Messenger.Publish(new NewInformationMessage(
@"This button triggered a message from the ""Other object""
in the hierarchy directed towards its target transforms root.

This is useful when you want to tell a specific object hierarchy something.
For example that it should take damage, or maybe transfer ownership
of an item or similar.

The messenger works by looking if the current handle has
the transform you specified as an ancestor. If it does, 
it tells the handle to execute, if it doesn't, it skips the handle"));
        }
    }
}