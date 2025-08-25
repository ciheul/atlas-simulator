using UnityEngine;

public class MathHelper
{
    public static float HumanizeRotationAngle(float rotation)
    {
        if (rotation > 180f)
        {
            rotation -= 360;
        }
        return rotation;
    }
}