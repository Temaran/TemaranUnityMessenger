using UnityEngine;

/// <summary>
/// This attribute helps us with displaying the extended tag information in the inspector
/// </summary>
public class BitMaskAttribute : PropertyAttribute
{
    public System.Type propType;
    public BitMaskAttribute(System.Type aType)
    {
        propType = aType;
    }
}