using System;
using UnityEngine;

public abstract class Jet : MonoBehaviour
{
    public JetSO jetSO;

    // center point adalah Atlas sebagai target serangan jet
    // khususnya ketika terbang melingkari target
    protected Rigidbody rb;
    protected readonly float LEFT = 1;
    protected readonly float RIGHT = -1;
    protected readonly float UP = -1;
    protected readonly float DOWN = 1;
    protected long timer;
    protected long now;
    protected float initialHeading;
    protected float currentHeading;
    protected float initialPitchAngle;
    protected float currentPitchAngle;
    protected bool[] flagList;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.maxLinearVelocity = jetSO.maxSpeed;
        rb.linearVelocity = new Vector3(-jetSO.initialSpeed, 0f, 0f);

        timer = new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds();
        now = timer;

        initialHeading = rb.transform.rotation.eulerAngles.y;
        currentHeading = initialHeading;
        initialPitchAngle = rb.transform.rotation.eulerAngles.x;
        currentPitchAngle = initialPitchAngle;

        flagList = new bool[6];
        ResetFlag();
    }

    // Update is called once per frame
    protected void Update()
    {

    }

    protected void FixedUpdate()
    {
        currentHeading = rb.transform.rotation.eulerAngles.y;
        currentPitchAngle = rb.transform.rotation.eulerAngles.x;

        ForwardMovement();
        now = new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds();
        bool[] returnFlag = new bool[3];
        MovementPath(returnFlag);

        float offset = MathHelper.OffsetRotationAngle(initialHeading, currentHeading);
        //print($"initialHeading:{initialHeading}; currentHeading:{currentHeading}; offsetHeading:{offset}");
        //print($"pitch:{rb.transform.rotation.eulerAngles.x}");
    }

    protected void ResetFlag()
    {
        for (int x = 0; x < flagList.Length; x++)
        {
            flagList[x] = true;
        }
    }

    protected abstract void MovementPath(bool[] returnFlag);

    protected void ForwardMovement()
    {
        rb.AddRelativeForce(new Vector3(0f, 0f, jetSO.initialSpeed) * jetSO.acceleration, ForceMode.Force);
    }

    protected void RollMovement(float direction = 1)
    {
        float roll = jetSO.rollSpeed * direction * Time.fixedDeltaTime;
        Quaternion rotation = Quaternion.Euler(0f, 0f, roll);
        rb.MoveRotation(rb.rotation * rotation);
    }

    protected bool RollLeft(float rollAngle, bool flag)
    {
        float eulerZ = MathHelper.HumanizeRotationAngle(rb.transform.rotation.eulerAngles.z);
        if (eulerZ < rollAngle)
        {
            RollMovement(LEFT);
        }
        else
        {
            if (flag)
            {
                Vector3 fixedVector = new Vector3(rb.transform.rotation.eulerAngles.x, rb.transform.rotation.eulerAngles.y, rollAngle);
                Quaternion fixedRotation = Quaternion.Euler(fixedVector);
                rb.transform.rotation = fixedRotation;
            }
            flag = false;
        }
        return flag;
    }

    protected bool RollRight(float rollAngle, bool flag)
    {
        float eulerZ = MathHelper.HumanizeRotationAngle(rb.transform.rotation.eulerAngles.z);
        if (eulerZ > rollAngle)
        {
            RollMovement(RIGHT);
        }
        else
        {
            if (flag)
            {
                Vector3 fixedVector = new Vector3(rb.transform.rotation.eulerAngles.x, rb.transform.rotation.eulerAngles.y, rollAngle);
                Quaternion fixedRotation = Quaternion.Euler(fixedVector);
            }
            flag = false;
        }
        return flag;
    }

    protected bool[] TurnLeft(float heading, bool rollLeft, bool rollRight, bool flag)
    {
        bool[] returnFlag;

        if (rollLeft)
        {
            rollLeft = RollLeft(90, rollLeft);
        }

        float eulerZ = MathHelper.HumanizeRotationAngle(rb.transform.eulerAngles.z);
        bool standardHeading = currentHeading > heading;
        bool standardHeadingReached = currentHeading <= heading;

        if (currentHeading == 0)
        {
            standardHeading = currentHeading < heading;
            standardHeadingReached = currentHeading >= heading;
        }

        if (heading == 0)
        {
            standardHeading = !(currentHeading < 360 && currentHeading > 359);
            standardHeadingReached = !standardHeading;
        }

        if (eulerZ > 89 && eulerZ < 91 && standardHeading)
        {
            PitchMovement(UP);
        }

        if (standardHeadingReached)
        {
            if (rollRight)
            {
                rollRight = RollRight(0, rollRight);
            }
        }

        if (rollLeft == false && rollRight == false)
        {
            Vector3 fixedVector = new Vector3(0f, heading, 0f);
            Quaternion fixedRotation = Quaternion.Euler(fixedVector);
            rb.transform.rotation = fixedRotation;
            flag = false;
        }
        returnFlag = new bool[3] { rollLeft, rollRight, flag };
        return returnFlag;
    }

    protected bool[] TurnRight(float heading, bool rollLeft, bool rollRight, bool flag)
    {
        bool[] returnFlag;

        if (rollRight)
        {
            rollRight = RollRight(90, rollLeft);
        }

        float eulerZ = MathHelper.HumanizeRotationAngle(rb.transform.eulerAngles.z);
        bool standardHeading = currentHeading < heading;
        bool standardHeadingReached = currentHeading >= heading;

        if (currentHeading == 0)
        {
            standardHeading = currentHeading > heading;
            standardHeadingReached = currentHeading <= heading;
        }

        if (heading == 0)
        {
            standardHeading = !(currentHeading < 360 && currentHeading > 359);
            standardHeadingReached = !standardHeading;
        }

        if (eulerZ > 89 && eulerZ < 91 && standardHeading)
        {
            PitchMovement(UP);
        }

        if (standardHeadingReached)
        {
            if (rollLeft)
            {
                rollLeft = RollLeft(0, rollLeft);
            }
        }

        if (rollLeft == false && rollRight == false)
        {
            Vector3 fixedVector = new Vector3(0f, heading, 0f);
            Quaternion fixedRotation = Quaternion.Euler(fixedVector);
            rb.transform.rotation = fixedRotation;
            flag = false;
        }
        returnFlag = new bool[3] { rollLeft, rollRight, flag };
        return returnFlag;
    }

    protected void PitchMovement(float direction = -1)
    {
        float pitch = jetSO.pitchSpeed * direction;
        Quaternion rotation = Quaternion.Euler(pitch, 0f, 0f);
        rb.MoveRotation(rb.rotation * rotation);
    }

    protected bool PullUp(float pitchAngle, bool flag)
    {
        if (!flag) return flag;

        bool standardPitch = currentPitchAngle > pitchAngle;
        bool standardPitchReached = currentPitchAngle <= pitchAngle;

        if (currentPitchAngle < pitchAngle)
        {
            standardPitch = currentPitchAngle < pitchAngle;
            standardPitchReached = currentPitchAngle >= pitchAngle;
        }

        if (standardPitchReached)
        {
            flag = false;
        }

        if (standardPitch && flag)
        {
            PitchMovement(UP);
        }

        print($"currentPitchAngle:{currentPitchAngle}; pitchAngle:{pitchAngle}");
        print($"standardPitchReached:{standardPitchReached}");
        return flag;
    }

    protected bool PullDown(float pitchAngle, bool flag)
    {
        if (!flag) return flag;

        bool standardPitch = currentPitchAngle < pitchAngle;
        bool standardPitchReached = currentPitchAngle >= pitchAngle;

        if (currentPitchAngle > pitchAngle)
        {
            standardPitch = currentPitchAngle > pitchAngle;
            standardPitchReached = currentPitchAngle <= pitchAngle;
        }

        if (standardPitchReached)
        {
            flag = false;
        }

        if (standardPitch && flag)
        {
            PitchMovement(DOWN);
        }

        print($"currentPitchAngle:{currentPitchAngle}; pitchAngle:{pitchAngle}");
        print($"standardPitchReached:{standardPitchReached}");
        return flag;
    }

    protected void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
        //Destroy(other.gameObject);
        Debug.Log("collision occured");
    }
}