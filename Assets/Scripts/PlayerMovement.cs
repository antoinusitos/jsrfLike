using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float mySpeedMax = 10.0f;
    private float mySpeed = 0.0f;

    [SerializeField]
    private Transform myCamera = null;

    [SerializeField]
    private Rigidbody myRigidBody = null;

    [SerializeField]
    private float myAcceleration = 2.0f;

    [SerializeField]
    private float myDecceleration = 2.0f;

    [SerializeField]
    private float myDamp = 1.0f;

    [SerializeField]
    private Transform myBody = null;

    [SerializeField]
    private float myJumpForce = 5;

    [SerializeField]
    private Transform target = null;

    [SerializeField]
    private float myRotationSpeed = 1.0f;

    private Vector3 myNewPos = Vector3.zero;

    private void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        Vector3 camForward = myCamera.forward;
        camForward.y = 0;

        Vector3 camRight = myCamera.right;
        camRight.y = 0;

        target.position = myBody.position + camForward * y + camRight * x;

        // Determine which direction to rotate towards
        Vector3 targetDirection = target.position - myBody.position;

        // The step size is equal to speed times frame time.
        float singleStep = myRotationSpeed * Time.deltaTime;

        // Rotate the forward vector towards the target direction by one step
        Vector3 newDirection = Vector3.RotateTowards(myBody.forward, targetDirection, singleStep, 0.0f);

        // Calculate a rotation a step closer to the target and applies rotation to this object
        myBody.rotation = Quaternion.LookRotation(newDirection);

        Debug.DrawLine(myBody.position, target.position, Color.red);

        if (x != 0 || y != 0)
        {
            mySpeed += myAcceleration * Time.deltaTime;
            if (mySpeed > mySpeedMax)
                mySpeed = mySpeedMax;

        }
        else if (mySpeed > 0)
        {
            mySpeed -= myDecceleration * Time.deltaTime;
            if (mySpeed < 0)
                mySpeed = 0;
        }

        Vector3 velocity = Vector3.zero;
        //myNewPos = Vector3.SmoothDamp(myRigidBody.position, myRigidBody.position + myBody.forward * Time.deltaTime * mySpeed, ref velocity, myDamp);
        myNewPos = myRigidBody.position + myBody.forward * Time.deltaTime * mySpeed;
    }

    private void FixedUpdate()
    {
        myRigidBody.MovePosition(myNewPos);
    }
}
