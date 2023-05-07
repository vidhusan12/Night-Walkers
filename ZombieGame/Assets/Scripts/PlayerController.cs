using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This script controls the movement and rotation of the player character
public class PlayerController : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float rotationSpeed = 500f;

    //The rotation of the player character
    Quaternion playerRotation;

    //The camera controller for the player character
    PlayerCamera cameraController;

    //Get the camera controller for the player character when the script starts
    private void Awake()
    {
        cameraController = Camera.main.GetComponent<PlayerCamera>();
    }

    //Update teh player charcter's movement and rotation based on user input
    private void Update()
    {
        //Get teh horizontal and vertical movement inputs from the user
        float hor = Input.GetAxis("Horizontal");
        float ver = Input.GetAxis("Vertical");
        //Calculate the total amount of movement input
        float moveAmount = Mathf.Abs(hor) + Mathf.Abs(ver);

        //Normaluze the movement input to get a direction vector
        var moveInput = (new Vector3(hor, 0, ver)).normalized;
        //Calculate teh movement durection based on the camera's rotation
        var moveDir = cameraController.PlannerRotation * moveInput;

        //If the player is moving, move the player charcter and rotate it towards the movement direction
        if(moveAmount > 0)
        {
            transform.position += moveDir * moveSpeed * Time.deltaTime;
            playerRotation = Quaternion.LookRotation(moveDir);
        }

        //Rotate the player character towards the desried rotaion
        transform.rotation = Quaternion.RotateTowards(transform.rotation, playerRotation, rotationSpeed * Time.deltaTime);

    }

}
