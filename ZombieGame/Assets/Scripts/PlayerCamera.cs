using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    [SerializeField] Transform playerTarget;
    [SerializeField] float distance = 5;
    [SerializeField] float minVerticalAngle;
    [SerializeField] float maxVerticalAngle;
    [SerializeField] float rotationSpeed = 2f;
    [SerializeField] Vector2 framingOffset;
    

    float rotationX;
    float rotationY;

    private void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }


    private void Update(){
        rotationX += Input.GetAxis("Mouse Y") * rotationSpeed;
        rotationX = Mathf.Clamp(rotationX, minVerticalAngle, maxVerticalAngle);

        rotationY += Input.GetAxis("Mouse X") * rotationSpeed;

        var playerRotation = Quaternion.Euler(rotationX, rotationY, 0);
        var focusPostion = playerTarget.position + new Vector3(framingOffset.x,framingOffset.y);

        transform.position = focusPostion - playerRotation * new Vector3(0, 0, distance);
        transform.rotation = playerRotation;
        
    }

}
