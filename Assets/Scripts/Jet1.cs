using System;
using System.IO;
using UnityEngine;
using UnityEngine.UIElements.Experimental;

public class Jet1 : MonoBehaviour
{
    public JetSO jetSO;

    // center point adalah Atlas sebagai target serangan jet
    // khususnya ketika terbang melingkari target
    public Transform centerPoint;
    private Rigidbody rb;
    private readonly float LEFT = 1;
    private readonly float RIGHT = -1;
    private readonly float UP = -1;
    private readonly float DOWN = 1;
    private long timer;
    private long now;
    private float initialHeading;
    private float currentHeading;
    private bool[] flagList;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // memastikan scene tidak freezed
        Time.timeScale = 1;
        rb = GetComponent<Rigidbody>();
        rb.maxLinearVelocity = jetSO.maxSpeed;
        rb.linearVelocity = new Vector3(-jetSO.initialSpeed, 0f, 0f);

        timer = new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds();
        now = timer;

        initialHeading = rb.transform.rotation.eulerAngles.y;
        currentHeading = initialHeading;

        flagList = new bool[6];
        ResetFlag();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        //ForwardMovement();
        //pitchMovement();
        //RollMovement();
        //TurnLeft();

        currentHeading = rb.transform.rotation.eulerAngles.y - initialHeading;

        //MovementPath();
        ForwardMovement();
        PitchMovement();
    }

    private void ResetFlag()
    {
        for (int x = 0; x < flagList.Length; x++)
        {
            flagList[x] = true;
        }
    }

    private void MovementPath()
    {
        ForwardMovement();

        now = new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds();
        bool[] returnFlag = new bool[3];

        if (flagList[2] && now - timer >= 10)
        {
            returnFlag = TurnLeft(180, flagList[0], flagList[1], flagList[2]);
            flagList[0] = returnFlag[0];
            flagList[1] = returnFlag[1];
            flagList[2] = returnFlag[2];
            /*
            if (!flagList[2])
            {
                timer = now;
            }*/
        }
        /*
        if (!flagList[2] && now - timer >= 20)
        {
            returnFlag = TurnLeft(180, flagList[3], flagList[4], flagList[5]);
            flagList[3] = returnFlag[0];
            flagList[4] = returnFlag[1];
            flagList[5] = returnFlag[2];
        }

        if (flagList[2] == false && flagList[5] == false)
        {
            timer = now;
            ResetFlag();
        }
        */
    }

    private void ForwardMovement()
    {
        rb.AddRelativeForce(new Vector3(0f, 0f, jetSO.initialSpeed) * jetSO.acceleration, ForceMode.Force);
    }

    private void RollMovement(float direction = 1)
    {
        float roll = jetSO.rollSPeed * direction * Time.fixedDeltaTime;
        Quaternion rotation = Quaternion.Euler(0f, 0f, roll);
        rb.MoveRotation(rb.rotation * rotation);
    }

    private bool RollLeft(float rollAngle, bool flag)
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
                //rb.transform.rotation = fixedRotation;
            }
            flag = false;
        }
        //print($"RollLeft z:{eulerZ}; rollAngle:{rollAngle}");
        return flag;
        /*if (rb.transform.eulerAngles.z > rollAngle)
        {
            RollMovement(RIGHT);
        }*/
    }

    private bool RollRight(float rollAngle, bool flag)
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
        //print($"RollRight z:{eulerZ}; rollAngle:{rollAngle}");
        return flag;
        /*if (rb.transform.eulerAngles.z < rollAngle)
        {
            RollMovement(LEFT);
        }*/
    }

    private bool[] TurnLeft(float heading, bool rollLeft, bool rollRight, bool flag)
    {
        if (rollLeft)
        {
            rollLeft = RollLeft(90, rollLeft);
        }

        float eulerZ = MathHelper.HumanizeRotationAngle(rb.transform.eulerAngles.z);
        bool standardHeading = Math.Abs(currentHeading) < Math.Abs(heading);
        //bool halfCircleHeading = | heading != 180;
        if (eulerZ > 89 && eulerZ < 91 && standardHeading)
        {
            PitchMovement(UP);
        }

        if (Math.Abs(currentHeading) >= Math.Abs(heading))
        {
            if (rollRight)
            {
                rollRight = RollRight(0, rollRight);
            }
        }

        if (rollLeft == false && rollRight == false)
        {
            flag = false;
        }
        print($"currentHeading:{currentHeading}; heading:{heading}");
        bool[] returnFlag = { rollLeft, rollRight , flag };
        return returnFlag;
    }

    private bool[] TurnRight(float heading, bool rollLeft, bool rollRight, bool flag)
    {
        if (rollRight)
        {
            rollRight = RollRight(90, rollRight);
        }

        float eulerZ = MathHelper.HumanizeRotationAngle(rb.transform.eulerAngles.z);
        if (eulerZ < -89 && eulerZ > -91 && Math.Abs(currentHeading) < Math.Abs(heading))
        {
            PitchMovement(UP);
        }

        if (Math.Abs(currentHeading) >= Math.Abs(heading))
        {
            if (rollLeft)
            {
                rollLeft = RollLeft(0, rollLeft);
            }
        }

        if (rollLeft == false && rollRight == false)
        {
            flag = false;
        }

        bool[] returnFlag = { rollLeft, rollRight, flag };
        return returnFlag;
    }

    private void PitchMovement(float direction = -1)
    {
        float pith = jetSO.pitchSpeed * direction * Time.fixedDeltaTime;
        Quaternion rotation = Quaternion.Euler(pith, 0f, 0f);
        rb.MoveRotation(rb.rotation * rotation);
    }

    private void UpdateCircularMovement()
    {
        float pith = jetSO.pitchSpeed * Time.fixedDeltaTime;
        Quaternion rotation = Quaternion.Euler(pith, 0f, 0f);
        rb.MoveRotation(rb.rotation * rotation);
    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
        //Destroy(other.gameObject);
        Debug.Log("collision occured");
    }
}