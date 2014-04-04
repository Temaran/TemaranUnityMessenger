using UnityEngine;

/// <summary>
/// The point here is that we can decide which clients get the message depending on where they are in an object hierarchy
/// Only clients that are decendants of the transform you specify will be eligable.
/// </summary>
public class ArbitraryHierarchyPublisher : MonoBehaviourEx, IHandle<NewInformationMessage>
{
    private string _additionalInfo = "";

    public void OnGUI()
    {
        GUI.TextField(new Rect(520, 0, 500, 800), 
@"The point here is that we can decide which clients get the message 
depending on where they are in an object hierarchy
Only clients that are decendants of the transform you specify 
will be eligable.

You can see which scripts triggered when this message was fired
by looking at the console output.

The advantage of this is of course that you can direct where you
want your messages to go if you have several objects that potentially
might be interested in the message you're sending.

Say for example you would like to send a message that you dealt damage
to something. If you just published that to everyone, all living things
in your scene would take damage!

The proper way to do it would of course be to first single the object
you want to damage out, and then tell that object to take the damage.
Normally you would do this by for example doing a raycast, and then
trying to get the appropriate scripts using GetComponentsInChildren<T>
The problem with this of course is that you are forcing your code to
KNOW WHICH TYPE YOU WANT TO TELL TO TAKE DAMAGE. 
And every time you add more scripts that have to know this, 
you have to add more code.

Using a messenger, you once again decouple the one sending the message
from knowing who needs to see it, and how it should be handled, leaving that
for the client to decide.");

        GUI.TextField(new Rect(0, 300, 500, 300), _additionalInfo);

        if (GUI.Button(new Rect(0, 0, 500, 50), "Fire to own transform from (see console for output): " + name))
        {
            Messenger.Publish(new ExampleMessage("FROM ME? SICK!!!", renderer), transform);
            Messenger.Publish(new NewInformationMessage(
@"This button just sent a message directed at its own transform.
This means that this gameobject itself, as well as 
all of its children will be notified of the message.

This is useful when you want to tell a part of your own
hierarchy something important. Maybe you want all subweapons
of your multipart gun to fire, but only the left hand one.
In that case you should send a message to only yourself, not your root,
which would make the message travel to all hands instead.

The messenger works by looking if the current handle has
the transform you specified as an ancestor. If it does, 
it tells the handle to execute, if it doesn't, it skips the handle"));
        }
    }

    //Here we actually use the messenger to help us write the sample :D
    public void Handle(NewInformationMessage message)
    {
        _additionalInfo = message.NewInformation;
    }
}