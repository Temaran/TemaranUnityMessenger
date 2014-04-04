using System;
using UnityEngine;

/// <summary>
/// This script is responsible for sending the example message each update loop, as well as create additional listeners
/// </summary>
public class GlobalExamplePublisher : MonoBehaviourEx
{
    public GameObject LolPrefab;
    private string _additionalInfo =
@"Right now, only the example Subscriber is listening to the Example messages
we are sending on each update pass.
Try adding another listener by pressing the button!";

    private Vector2 _scrollPos;

    public void OnGUI()
    {
        GUI.TextField(new Rect(0, 0, 1000, 800), 
@"You can send global messages like this, everyone who registered for the message in the current scene will get it. 
There are many reasons why you would want to use this over the other options you have available:

1. It decouples your sending code from your receiving code and encapsulation of the client code itself. For example;
   A weapon script does not have to know that it needs to look for a health script, and much more, if you add more types of receivers 
   (for example, different types of health scripts) you do not have to remember to modify the weapon script to look for these 
   new types. Everything is done client side (the new health script implements a handle for the damage-message, and you're done). 
   Refactoring communication code has never been this easy. 
   It also promotes reuseability, since your components only need to know their input / output messages, and hold very few direct references. 
   This means it is very easy to move a complete component between projects.
2. Easy to test since you can mock simply by sending dummy messages.
3. It is MUCH faster, since almost everything here is cached, and it is only ever bothering objects that are interested.
4. Thread safety (although you're free to remove this if it doesn't suit you)

It also offers these additional advantages (which it seems that other messengers on the
asset store does not offer, at least not from what I have seen):

1. It is type safe, when you refactor code with unity's standard interface, and you miss to change one method name string, 
   you might not notice it for some time. Since we're using real types here, the compiler will throw complaints right away.
2. It's easier, only one line of code to publish, and you don't have to explicitly remember to subscribe and unsubscribe for clients
3. It's faster than all implementations I've tested. I've only included the tests for free messengers.
4. It is more useful, since you can also use it for much more than just sending plain messages (see for example the tagging example or the request example)
5. It's FREE.

To illustrate the easy of use, below is all code needed to get a sample running :)

Publish:
public class GlobalExamplePublisher : MonoBehaviourEx
{
    public void Update()
    {
        Messenger.Publish(new ExampleMessage(""OMG THIS JUST HAPPENED"", renderer));
    }
}

Subscribe:
public class ExampleSubscriber : MonoBehaviourEx, IHandle<ExampleMessage>
{
    public void Handle(ExampleMessage message)
    {
        //Do something here
    }
}
");

        if (GUI.Button(new Rect(1010, 0, 300, 50), "Add new listener!"))
        {
            Instantiate(LolPrefab);
            _additionalInfo =
@"You just added a new listener, now both the original subscriber,
this new listener, and any other listeners will receive the example messages!";
        }
        if (GUI.Button(new Rect(1010, 60, 300, 50), "Destroy all extra listeners!"))
        {
            Messenger.Publish(new KillMessage());
            _additionalInfo =
@"You just destroyed all extra listeners, now only the example subscriber
listens to the messages again...";
        }

        GUI.TextField(new Rect(1010, 120, 500, 600), _additionalInfo);
    }

    public void Update()
    {
        Messenger.Publish(new ExampleMessage("This got sent at tick count: " + DateTime.UtcNow.Ticks, renderer));
    }
}