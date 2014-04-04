using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This is a more concrete example of a request, this request will get you all gameobjects that has the extendedtag
/// script and a certain set of tags set
/// </summary>
public class GetObjectsWithTagRequest
{
    /// <summary>
    /// These are the tags that we require our subscriber extended tags have set to be eligable to add themselves
    /// to the return collection
    /// </summary>
    public ExampleTags RequiredTags { get; set; }
    /// <summary>
    /// This is the collection of objects that we expect to be filled by sending the request.
    /// If a subscriber feel they satisfy the requiredtags condition, they will add themselves to this collection and be "returned".
    /// </summary>
    public HashSet<GameObject> TaggedObjects { get; set; } 

    public GetObjectsWithTagRequest(ExampleTags requiredTags)
    {
        TaggedObjects = new HashSet<GameObject>();
        RequiredTags = requiredTags;
    }
}