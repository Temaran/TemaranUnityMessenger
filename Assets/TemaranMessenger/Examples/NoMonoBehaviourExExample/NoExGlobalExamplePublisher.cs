using System;
using UnityEngine;

/// <summary>
/// This script is responsible for sending the example message each update loop, as well as create additional listeners
/// </summary>
public class NoExGlobalExamplePublisher : MonoBehaviour
{
    public GameObject LolPrefab;
    private string _additionalInfo =
@"Right now, only the example Subscriber is listening to the Example messages
we are sending on each update pass.
Try adding another listener by pressing the button!";

    private Vector2 _scrollPos;
    
    public virtual void Awake()
    {
        MessengerSingleton.Messenger.Subscribe(this);
    }

    public void OnGUI()
    {
        GUI.TextField(new Rect(0, 0, 1000, 800),
@"The only difference in this sample compared to the ""A Simple Example"" example
is that this sample does not use the MonoBehaviourEx base class.

Instead, a singleton that manages the messenger is used, and every class has to 
register themselves on awake as such:

public virtual void Awake()
{
    MessengerSingleton.Messenger.Subscribe(this);
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
            MessengerSingleton.Messenger.Publish(new KillMessage());
            _additionalInfo =
@"You just destroyed all extra listeners, now only the example subscriber
listens to the messages again...";
        }

        GUI.TextField(new Rect(1010, 120, 500, 600), _additionalInfo);
    }

    public void Update()
    {
        MessengerSingleton.Messenger.Publish(new ExampleMessage("This got sent at tick count: " + DateTime.UtcNow.Ticks, renderer));
    }
}