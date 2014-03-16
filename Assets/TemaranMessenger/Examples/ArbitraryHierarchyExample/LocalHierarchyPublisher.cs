using UnityEngine;

/// <summary>
/// A specialization of the hierarchy use, and probably the one you will be using the most often :)
/// Specifying the transform.root will send it to all subscribers in the current hierarchy
/// </summary>
public class LocalHierarchyPublisher : MonoBehaviourEx
{
    public void OnGUI()
    {
        if (GUI.Button(new Rect(0, 60, 500, 50), "Fire to root from  (see console for output): " + name))
            Messenger.Publish(new ExampleMessage("OMG FROM THE ROOT!!!!", renderer), transform.root);
    }
}