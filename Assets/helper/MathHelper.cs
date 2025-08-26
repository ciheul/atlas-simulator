using UnityEngine;

public class MathHelper
{
    public static float HumanizeRotationAngle(float rotation)
    {
        // ubah rotation 0 - 360 jadi 180 - 0 - -180 
        if (rotation > 180f)
        {
            rotation -= 360;
        }
        return rotation;
    }

    public static float OffsetRotationAngle(float initialRotationAngle, float currentRotationAngle)
    {
        float finalAngle = currentRotationAngle - initialRotationAngle;
        if (finalAngle < 0) {
            finalAngle = 360 + finalAngle;
        }
        return finalAngle;
    }

    public static float ReverseOffsetRotationAngle(float initialRotationAngle, float offsetRotationAngle)
    {
        float finalAngle = offsetRotationAngle + initialRotationAngle;
        if (finalAngle >= 360)
        {
            finalAngle = finalAngle - 360;
        }
        return finalAngle;
    }
}