public class TemaranSubscriber : MonoBehaviourEx, IHandle<PerformanceMessage>
{
    public float Lol;

    public void Handle(PerformanceMessage message)
    {
        Lol = message.Lolol;
    }
}