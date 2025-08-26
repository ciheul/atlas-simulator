public class JetMedium : Jet
{
    protected override void MovementPath(bool[] returnFlag)
    {
        if (flagList[2] && rb.transform.position.z < -1000)
        {
            float offset = MathHelper.OffsetRotationAngle(initialHeading, 180);
            returnFlag = TurnLeft(offset, flagList[0], flagList[1], flagList[2]);
            flagList[0] = returnFlag[0];
            flagList[1] = returnFlag[1];
            flagList[2] = returnFlag[2];

            if (!flagList[2])
            {
                timer = now;
            }
        }

        if (!flagList[2] && rb.transform.position.z > 1000)
        {
            float offset = MathHelper.OffsetRotationAngle(initialHeading, 0);
            returnFlag = TurnLeft(offset, flagList[3], flagList[4], flagList[5]);
            flagList[3] = returnFlag[0];
            flagList[4] = returnFlag[1];
            flagList[5] = returnFlag[2];

            if (!flagList[5])
            {
                timer = now;
            }
        }

        if (flagList[2] == false && flagList[5] == false)
        {
            timer = now;
            ResetFlag();
        }
    }
}
