using System.Collections.Generic;
using UnityEngine;

public class GetObjectsWithTagRequest
{
    public ExampleTags RequiredTags { get; set; }
    public HashSet<GameObject> TaggedObjects { get; set; } 

    public GetObjectsWithTagRequest(ExampleTags requiredTags)
    {
        TaggedObjects = new HashSet<GameObject>();
        RequiredTags = requiredTags;
    }
}