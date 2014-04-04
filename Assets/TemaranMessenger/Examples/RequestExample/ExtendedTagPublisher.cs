using UnityEngine;

/// <summary>
/// The point here is that we can decide which clients get the message depending on where they are in an object hierarchy
/// Only clients that are decendants of the transform you specify will be eligable.
/// </summary>
public class ExtendedTagPublisher : MonoBehaviourEx, IHandle<NewInformationMessage>
{
    public ExampleTags TagToGet = ExampleTags.Enemy;

    private string _additionalInfo = "Press a button to request some objects!";

    public void OnGUI()
    {
        GUI.TextField(new Rect(410, 0, 800, 800),
@"Sometimes, you need to find all the objects in a scene, or a subset of the scene that has
some special property. Maybe they have a special tag, like a category of enemies,
or all objects that have their current property set to a certain value. Or again,
maybe you just want the first object that implements a certain script.

The normal way to do this in unity is very fragile (GameObject.Find()) and EXTREMELY slow.
For example, if you change the name of one of your objects in the hierarchy, you have to remember
to update all places in your code that you do GameObject.Find(Name), or you will get unexpected 
behaviour. Especially in the case of finding certain categories of objects is difficult. If it is a
static collection, you can probably cache this at the start of the scene, but if this group is dynamic,
then you have a big problem. Adding and removing items is another obvious problem to the caching approach.

Good thing then that you can use this messenger to do this dirty work for you too!
The syntax becomes a little bit clunky though for this purpose (three lines) but it's still immensely better
than the alternatives, and you could easily create a wrapper system for this to make it pretty if you were so
inclined... I will probably do it myself some day :D

It works like this:
First create a message like normal, and design the message in such a way that it's construction takes a set of
conditions we want the subscribers to test. Also create a collection which our subscribers will add their result to
if they feel they have satisfied the condition. When you create the message in your publisher, cache the message in
a local variable and then send it. When the Messenger.Publish() method has completed execution, all the clients have then
had a chance to process the message, and all responses can then be found in our collection, ready for your to do interesting
things to :)
");

        GUI.TextField(new Rect(0, 200, 400, 400), _additionalInfo);


        if (!GUI.Button(new Rect(0, 60, 400, 50), "Get objects with tag! (see console for output)"))
            return;

        _additionalInfo = string.Format("You just requested objects with the specified tag \"{0}\"\nThe results are in the console :)", TagToGet);

        var request = new GetObjectsWithTagRequest(TagToGet);
        Messenger.Publish(request);

        print("I found: \n");
        foreach (var obj in request.TaggedObjects)
            print(obj.name + "\n");
    }

    public void Handle(NewInformationMessage message)
    {
        _additionalInfo = message.NewInformation;
    }
}