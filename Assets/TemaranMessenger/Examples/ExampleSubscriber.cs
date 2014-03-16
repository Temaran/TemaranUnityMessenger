public class ExampleSubscriber : MonoBehaviourEx, IHandle<ExampleMessage>
{
    public string ExtraInfo = "I like swords";

    public void Handle(ExampleMessage message)
    {
        print(string.Format("{0}\n{1}", ExtraInfo, message.CrazyReason));
    }
}