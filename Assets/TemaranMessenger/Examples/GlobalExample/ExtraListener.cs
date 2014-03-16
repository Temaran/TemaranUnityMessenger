public class ExtraListener : MonoBehaviourEx, IHandle<KillMessage>, IHandle<ExampleMessage>
{
    public void Handle(KillMessage message)
    {
        Destroy(gameObject);
    }

    public void Handle(ExampleMessage message)
    {
        print("Hello from extra listener!\n" + message.CrazyReason);
    }
}