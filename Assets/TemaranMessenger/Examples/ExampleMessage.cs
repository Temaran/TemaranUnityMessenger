/// <summary>
/// A very simple example message
/// </summary>
public class ExampleMessage
{
    //Some example data...
    public string CrazyReason { get; set; }
    public object SomeImportantContext { get; set; }

    public ExampleMessage(string crazyReason, object someImportantContext)
    {
        CrazyReason = crazyReason;
        SomeImportantContext = someImportantContext;
    }
}