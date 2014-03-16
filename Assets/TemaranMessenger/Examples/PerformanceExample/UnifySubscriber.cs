using UnityEngine;

public class UnifySubscriber : MonoBehaviour
{
    public float Lol;

    public void OnSpeedChanged(float speed)
    {
        Lol = speed;
    }

    public void OnEnable()
    {
        Messenger<float>.AddListener("speed changed", OnSpeedChanged);
    }

    public void OnDisable()
    {
        Messenger<float>.RemoveListener("speed changed", OnSpeedChanged);
    }
}