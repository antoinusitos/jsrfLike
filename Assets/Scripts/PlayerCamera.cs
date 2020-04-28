using System;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    [SerializeField]
    private float myHorizontalSpeed = 2.0f;

    [SerializeField]
    private float myVerticalSpeed = 2.0f;

    private Transform myTransform = null;

    private void Start()
    {
        myTransform = transform;
    }

    private void Update()
    {
        float x = Input.GetAxis("RS_X");

        if(Mathf.Abs(x) > 0.1f)
            myTransform.Rotate(Vector3.up, myHorizontalSpeed * x * Time.deltaTime);
    }
}
