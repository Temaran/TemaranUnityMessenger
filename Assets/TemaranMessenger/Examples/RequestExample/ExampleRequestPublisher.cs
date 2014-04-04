using UnityEngine;

/// <summary>
/// This is our publisher, it sends the request to all subscribers, and then presents the results
/// </summary>
public class ExampleRequestPublisher : MonoBehaviourEx
{
    public void OnGUI()
    {
        if (!GUI.Button(new Rect(0, 0, 400, 50), "Get stuff over 9000! (see console for output)"))
            return;

        var stuffOver9000Request = new ExampleRequest(9000);
        Messenger.Publish(stuffOver9000Request);
        Messenger.Publish(new NewInformationMessage("You just requested game objects that are over 9000!\nThe results are in the console :)"));

        print("Stuff over 9000:\n");
        foreach (var res in stuffOver9000Request.RequestResult)
        {
            print(res + "\n");
        }
    }
}