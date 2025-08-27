using UnityEngine;
using static UnityEngine.Rendering.DebugUI.Table;

public class JetTest : Jet
{
    protected new void FixedUpdate()
    {
        base.FixedUpdate();
        print($"pitch:{rb.transform.rotation.eulerAngles.x}");
    }
    protected new void MovementPathEasy(bool[] returnFlag)
    {
        if (rb.transform.position.y < 500)
        {
            flagList[0] = PullUp(10, flagList[0]);
        }

        if (rb.transform.position.y >= 200)
        {
            //flagList[1] = PullDown(0, flagList[1]);
        }

        //PitchMovement(UP);
    }
}
