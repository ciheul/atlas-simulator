using System;
using UnityEngine;

public class Jet : MonoBehaviour
{
    public JetSO jetSO;

    // center point adalah Atlas sebagai target serangan jet
    // khususnya ketika terbang melingkari target
    Rigidbody rb;
    readonly float LEFT = 1;
    readonly float RIGHT = -1;
    readonly float UP = -1;
    readonly float DOWN = 1;
    long timer;
    long now;
    float initialHeading;
    float currentHeading;
    float initialPitchAngle;
    float currentPitchAngle;
    bool[] flagList;
    delegate void MovementPathDelegate(bool[] returnFlag);
    MovementPathDelegate MovementPath;

    private void Awake()
    {
        // difficulty
        // 0 = easy
        // 1 = medium
        // 2 = hard
        int difficulty = PlayerPrefs.GetInt("difficulty", 0);
        switch (difficulty)
        {
            case 0:
                jetSO = Resources.Load<JetSO>("JetEasySO");
                MovementPath = new MovementPathDelegate(MovementPathEasy);
                break;
            case 1:
                jetSO = Resources.Load<JetSO>("JetMediumSO");
                MovementPath = new MovementPathDelegate(MovementPathMedium);
                break;
            case 2:
                jetSO = Resources.Load<JetSO>("JetHardSO");
                MovementPath = new MovementPathDelegate(MovementPathHard);
                break;
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
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
    void Update()
    {

    }

    void FixedUpdate()
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

    void ResetFlag()
    {
        for (int x = 0; x < flagList.Length; x++)
        {
            flagList[x] = true;
        }
    }

    void MovementPathEasy(bool[] returnFlag)
    {
        if (flagList[2] && now - timer >= 20)
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

        if (!flagList[2] && now - timer >= 20)
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

    void MovementPathMedium(bool[] returnFlag)
    {
        if (flagList[2] && now - timer >= 10)
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

        if (!flagList[2] && now - timer >= 10)
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

    void MovementPathHard(bool[] returnFlag)
    {
        if (flagList[2] && now - timer >= 10)
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

        if (!flagList[2] && now - timer >= 10)
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

    void ForwardMovement()
    {
        rb.AddRelativeForce(new Vector3(0f, 0f, jetSO.initialSpeed) * jetSO.acceleration, ForceMode.Force);
    }

    void RollMovement(float direction = 1)
    {
        float roll = jetSO.rollSpeed * direction * Time.fixedDeltaTime;
        Quaternion rotation = Quaternion.Euler(0f, 0f, roll);
        rb.MoveRotation(rb.rotation * rotation);
    }

    bool RollLeft(float rollAngle, bool flag)
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

    bool RollRight(float rollAngle, bool flag)
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

    bool[] TurnLeft(float heading, bool rollLeft, bool rollRight, bool flag)
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

    bool[] TurnRight(float heading, bool rollLeft, bool rollRight, bool flag)
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

    void PitchMovement(float direction = -1)
    {
        float pitch = jetSO.pitchSpeed * direction;
        Quaternion rotation = Quaternion.Euler(pitch, 0f, 0f);
        rb.MoveRotation(rb.rotation * rotation);
    }

    bool PullUp(float pitchAngle, bool flag)
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

    bool PullDown(float pitchAngle, bool flag)
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

    void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
        //Destroy(other.gameObject);
        Debug.Log("collision occured");
    }
}