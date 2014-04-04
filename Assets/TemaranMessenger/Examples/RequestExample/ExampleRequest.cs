using System.Collections.Generic;

/// <summary>
/// This is an example request to show how the messenger use can be extended to more useful scenarios in addition to the
/// original one.
/// </summary>
public class ExampleRequest
{
    /// <summary>
    /// This is an example condition, this basic idea is that clients will look at this condition, and do some thinking, and if
    /// they decide that they fulfill the condition, they will make a contribution to the result
    /// </summary>
    public int SomeCondition { get; set; }
    /// <summary>
    /// This is the result list, all clients who feel they satisfy the condition will add their result here
    /// </summary>
    public List<object> RequestResult { get; set; }
 
    public ExampleRequest(int someCondition)
    {
        RequestResult = new List<object>();
        SomeCondition = someCondition;
    }
}