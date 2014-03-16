using UnityEngine;

/// <summary>
/// You can send global messenges like this, everyone who registered for the message in the current scene will get it.
/// There are many reasons why you would want to use this over the normal iterate over all objects and do broadcast message.
/// 1. It is MUCH faster, since almost everything here is cached, and it is only ever bothering objects that are interested.
/// 2. It is type safe, when you refactor code with unity's standard interface, and you miss to change one method name string, you might not notice it for some time.
///    Since we're using real types here, the compiler will throw complaints right away.
/// 3. It's easier, only one line of code to publish, and you don't have to explicitly remember to subscribe and unsubscribe for clients
/// 4. It is more useful, since you can also use it for much more than just sending plain messages (see for example the tagging example or the request example)
/// </summary>
public class GlobalExamplePublisher : MonoBehaviourEx
{
    public GameObject LolPrefab;

    public void OnGUI()
    {
        GUI.TextField(new Rect(0, 0, 700, 600), @"
 (see console for output)

You can send global messenges like this, everyone who registered for 
the message in the current scene will get it. There are many reasons 
why you would want to use this over the normal iterate over all objects 
and do broadcast message. 
1. It is MUCH faster, since almost everything here is cached, 
   and it is only ever bothering objects that are interested.
2. It is type safe, when you refactor code with unity's standard interface, 
   and you miss to change one method name string, you might not notice it for some time.
   Since we're using real types here, the compiler will throw complaints right away.
3. It's easier, only one line of code to publish, and you don't have to 
   explicitly remember to subscribe and unsubscribe for clients
4. It is more useful, since you can also use it for much more than just 
   sending plain messages (see for example the tagging example or the request example)
5. Thread safety (although you're free to remove this if it doesn't suit you)

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
        if (GUI.Button(new Rect(710, 0, 300, 50), "Add new listener!"))
            Instantiate(LolPrefab);
        if (GUI.Button(new Rect(710, 60, 300, 50), "Destroy all extra listeners!"))
            Messenger.Publish(new KillMessage());
    }

    public void Update()
    {
        Messenger.Publish(new ExampleMessage("OMG THIS JUST HAPPENED", renderer));
    }
}