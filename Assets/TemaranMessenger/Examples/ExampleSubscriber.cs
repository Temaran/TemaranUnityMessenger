/// <summary>
/// This is an example subscriber, it listens to the example message and prints a greeting when it is received.
/// </summary>
public class ExampleSubscriber : MonoBehaviourEx, IHandle<ExampleMessage>
{
    public string ExtraInfo = "I like swords";

    /// <summary>
    /// This method will trigger if the subscriber is eligable for the message.
    /// To learn more about what constitutes eligability, check out the hierarchy example!
    /// </summary>
    public void Handle(ExampleMessage message)
    {
        print(string.Format("{0}\n{1}", ExtraInfo, message.CrazyReason));
    }
}