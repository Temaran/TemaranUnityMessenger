using UnityEngine;

/// <summary>
/// The idea here is that you can target certain parts of a hierarchy to notify all of it's children of something.
/// For example, imagine you have a hitscan weapon that just hit some poor sods arm, you can now choose to send the take damage message
/// to all of the target (send to target.transform.root), or only to the arm and it's children (target.transform)
/// </summary>
public class FromOtherObject : MonoBehaviourEx
{
    public GameObject target;

    public void OnGUI()
    {
        if (GUI.Button(new Rect(0, 120, 500, 50), "Fire to target from (see console for output): " + name))
            Messenger.Publish(new ExampleMessage("OMG FROM OTHER OBJECT!!!!", renderer), target.transform);

        if (GUI.Button(new Rect(0, 180, 500, 50), "Fire to target root from (see console for output): " + name))
            Messenger.Publish(new ExampleMessage("OMG FROM OTHER OBJECT!!!!", renderer), target.transform.root);
    }
}