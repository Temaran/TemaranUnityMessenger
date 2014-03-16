using UnityEngine;

public class ExampleRequestPublisher : MonoBehaviourEx
{
    public void OnGUI()
    {
        if (!GUI.Button(new Rect(0, 0, 400, 50), "Get stuff over 9000! (see console for output)"))
            return;

        var stuffOver9000Request = new ExampleRequest(9000);
        Messenger.Publish(stuffOver9000Request);

        print("Stuff over 9000:\n");
        foreach (var res in stuffOver9000Request.RequestResult)
        {
            print(res + "\n");
        }
    }
}