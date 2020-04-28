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

    private Vector3 myLastDir = Vector3.zero;

    [SerializeField]
    private float myJumpForce = 5;

    private void FixedUpdate()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 camForward = myCamera.forward;
        camForward.y = 0;
        camForward *= z;

        Vector3 camRight = myCamera.right;
        camRight.y = 0;
        camRight *= x;

        Vector3 dir = camForward + camRight;

        if (x != 0 || z != 0)
        {
            mySpeed += myAcceleration * Time.deltaTime;
            if (mySpeed > mySpeedMax)
                mySpeed = mySpeedMax;

            myLastDir = dir;
        }
        else if(mySpeed > 0)
        {
            mySpeed -= myDecceleration * Time.deltaTime;
            if (mySpeed < 0)
                mySpeed = 0;
            dir = myLastDir;
        }

        Vector3 velocity = Vector3.zero;
        Vector3 damp = Vector3.SmoothDamp(myRigidBody.position, myRigidBody.position + dir.normalized * Time.fixedDeltaTime * mySpeed, ref velocity, myDamp);

        myRigidBody.MovePosition(damp);

        myBody.LookAt(myRigidBody.position + dir.normalized);

        if(Input.GetKeyDown(KeyCode.Space))
        {
            myRigidBody.velocity += Vector3.up * myJumpForce;
        }
    }
}
