public class ExtendedTag : MonoBehaviourEx, IHandle<GetObjectsWithTagRequest>
{
    [BitMask(typeof(ExampleTags))]
    public ExampleTags Tags;

    public void Handle(GetObjectsWithTagRequest message)
    {
        if ((Tags & message.RequiredTags) == message.RequiredTags)
            message.TaggedObjects.Add(gameObject);
    }
}