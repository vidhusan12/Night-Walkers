using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    //The transform of the player character that the camera is following
    [SerializeField] Transform playerTarget;
    //The distance between the camera and the player character
    [SerializeField] float distance = 5;
    //The minimum vertical angle that the camera can rotate to
    [SerializeField] float minVerticalAngle;
    //The maximum vertical angel that the camera can rotate to 
    [SerializeField] float maxVerticalAngle;
    //The speed at which the camera rotates
    [SerializeField] float rotationSpeed = 2f;
    //The offset from the player character's position that the camera focuses on
    [SerializeField] Vector2 framingOffset;
    

    float rotationX;
    float rotationY;

    //Set teh cursor settings when the game starts
    private void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    //Updates the camera position and rotation based on user input
    private void Update(){
        //Rotate the camera vertically based on the mouse Input
        rotationX += Input.GetAxis("Mouse Y") * rotationSpeed;
        rotationX = Mathf.Clamp(rotationX, minVerticalAngle, maxVerticalAngle);

        //Rotate teh camera horizontally based on the mouse Input
        rotationY += Input.GetAxis("Mouse X") * rotationSpeed;

        //Calculate the player's rotatin based on the the camera rotation
        var playerRotation = Quaternion.Euler(rotationX, rotationY, 0);
        //Calculate the position that the camera should focus on
        var focusPostion = playerTarget.position + new Vector3(framingOffset.x,framingOffset.y);

        //Set the camera position and rotation based on the player's postion and rotation
        transform.position = focusPostion - playerRotation * new Vector3(0, 0, distance);
        transform.rotation = playerRotation;
        
    }

    //return the player's rotation in the horizontal plane
    public Quaternion PlannerRotation=> Quaternion.Euler(0, rotationY, 0);
    
        

}
