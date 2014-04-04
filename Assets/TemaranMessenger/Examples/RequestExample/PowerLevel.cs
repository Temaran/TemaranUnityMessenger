/// <summary>
/// An example subscriber script
/// </summary>
public class PowerLevel : MonoBehaviourEx, IHandle<ExampleRequest>
{
    public int Level = 2;
    
    public void Handle(ExampleRequest message)
    {
        //If our power level is above the required power level, add us to the return collection!
        if(Level > message.SomeCondition)
            message.RequestResult.Add(gameObject.name);
    }
}