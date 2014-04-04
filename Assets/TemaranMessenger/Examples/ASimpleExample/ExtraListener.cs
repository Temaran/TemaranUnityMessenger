/// <summary>
/// This is a script that simply also listens to the ExampleMessage
/// If a kill message is sent, it kills itself
/// </summary>
public class ExtraListener : MonoBehaviourEx, IHandle<KillMessage>, IHandle<ExampleMessage>
{
    /// <summary>
    /// When we get the kill message, destroy ourself
    /// </summary>
    public void Handle(KillMessage message)
    {
        Destroy(gameObject);
    }

    /// <summary>
    /// When we get the example message, happily report it!
    /// </summary>
    public void Handle(ExampleMessage message)
    {
        print("Hello from extra listener!\n" + message.CrazyReason);
    }
}