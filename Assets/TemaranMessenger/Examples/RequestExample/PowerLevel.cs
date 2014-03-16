public class PowerLevel : MonoBehaviourEx, IHandle<ExampleRequest>
{
    public int Level = 2;
    
    public void Handle(ExampleRequest message)
    {
        if(Level > message.SomeCondition)
            message.RequestResult.Add(gameObject.name);
    }
}