/// <summary>
/// This is our extended tag, the normal unity tag system has trouble with assigning several tags to the same game object.
/// This script solves that problem by using a flag enum. We can also do operations on the tags using requests in the manager,
/// such as finding all objects implementing a certain tag etc.
/// </summary>
public class ExtendedTag : MonoBehaviourEx, IHandle<GetObjectsWithTagRequest>
{
    [BitMask(typeof(ExampleTags))]
    public ExampleTags Tags;

    public void Handle(GetObjectsWithTagRequest message)
    {
        //Check if our tags contains all the required tags, in that case, add us to the result!!
        if ((Tags & message.RequiredTags) == message.RequiredTags)
            message.TaggedObjects.Add(gameObject);
    }
}