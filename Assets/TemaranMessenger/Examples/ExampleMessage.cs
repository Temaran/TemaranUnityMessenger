public class ExampleMessage
{
    public string CrazyReason { get; set; }
    public object SomeImportantContext { get; set; }

    public ExampleMessage(string crazyReason, object someImportantContext)
    {
        CrazyReason = crazyReason;
        SomeImportantContext = someImportantContext;
    }
}