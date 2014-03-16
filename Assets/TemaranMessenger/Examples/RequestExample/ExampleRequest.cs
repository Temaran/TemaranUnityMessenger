using System.Collections.Generic;

public class ExampleRequest
{
    public int SomeCondition { get; set; }
    public List<object> RequestResult { get; set; }
 
    public ExampleRequest(int someCondition)
    {
        RequestResult = new List<object>();
        SomeCondition = someCondition;
    }
}