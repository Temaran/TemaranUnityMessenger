using UnityEngine;

/// <summary>
/// The point here is that we can decide which clients get the message depending on where they are in an object hierarchy
/// Only clients that are decendants of the transform you specify will be eligable.
/// </summary>
public class ExtendedTagPublisher : MonoBehaviourEx
{
    public ExampleTags TagToGet = ExampleTags.Enemy;

    public void OnGUI()
    {
        if (!GUI.Button(new Rect(0, 60, 400, 50), "Get objects with tag! (see console for output)"))
            return;

        var request = new GetObjectsWithTagRequest(TagToGet);
        Messenger.Publish(request);

        print("I found: \n");
        foreach (var obj in request.TaggedObjects)
            print(obj.name + "\n");
    }
}