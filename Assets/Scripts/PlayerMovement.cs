using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    float moveSpeed = 5f;
    private Rigidbody playerRb;
    private Camera cam;


    Vector3 movement;
    Vector3 mousePos;

    private void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        cam = FindObjectOfType<Camera>();
    }
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.z = Input.GetAxisRaw("Vertical");
        // Create a Ray from the main camera to where the mouse is pointing
        Ray cameraRay = cam.ScreenPointToRay(Input.mousePosition);
        // the ray need to stop at a mathematical point which is our ground
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
        // how far it is from the camera
        float rayLength;
        // ray cast true is the ray from the camera intersect with something
        if(groundPlane.Raycast(cameraRay, out rayLength))
        {
            Vector3 pointToLook = cameraRay.GetPoint(rayLength);
            Debug.DrawLine(cameraRay.origin, pointToLook, Color.red);

            // if we just type this below, the player will look below him, so he will rotate in a weird way, thats because he is slightly above the ground
            // transform.LookAt(pointToLook); so we do create a new vector3 to set the y value, so it's at his level
            transform.LookAt(new Vector3(pointToLook.x, transform.position.y, pointToLook.z));
        }

        
    }

    private void FixedUpdate()
    {
        playerRb.MovePosition(playerRb.position + movement * moveSpeed * Time.fixedDeltaTime);

        // if I take 2 vectors and subtrack them I'll get one pointing to the other!
        Vector3 lookDir = mousePos - playerRb.position;
        float angle = Mathf.Atan2(lookDir.x, lookDir.y) * Mathf.Rad2Deg;
        playerRb.transform.Rotate(0, angle, 0);
    }
}
