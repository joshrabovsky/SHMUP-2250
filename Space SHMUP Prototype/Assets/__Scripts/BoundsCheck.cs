using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Keeps a GameObject on screen.
/// Note that this only works for an orthographic Main Camera at [ 0, 0, 0 ]
/// </summary>

public class BoundsCheck : MonoBehaviour
{
    [Header("Set in Inspector")]
    public float radius = 1f;
    public bool keepOnScreen = true; // Allows you to force GameObject to stay on green

    [Header("Set Dynamically")]
    public bool isOnScreen = true; //false if gamebject exits the screen
    public float camWidth;
    public float camHeight;

    [HideInInspector] //means it does not appear in inspector, but public
    public bool offRight, offLeft, offUp, offDown;

    // Awake is called as soon as the script is opened
    void Awake()
    {
        //Camera.main gives you access to the first camera with the tag MainCamera in your scene
        //If the camera is orthographic, orthographicSize gives you the Size number from the camera
        //inspector
        camHeight = Camera.main.orthographicSize;
        //.aspect gives the ratio of the camera in width/height
        //therefore by multipling by height we get the width
        //(distance from origin to left/right of screen)
        camWidth = camHeight * Camera.main.aspect; 

    }

    // LateUpdate is called once per frame after Update has been called on every GameObject
    //This ensures that the heroes Update function moves the object to the new position
    //before checking the boundary conditions
    //Avoids Race conditions (do not know which function will happen first)
    void LateUpdate()
    {
        Vector3 pos = transform.position;
        isOnScreen = true;//Turn is on screen to false if off screen
        offRight = offLeft = offUp = offDown = false; //all directions can be off screen

        if (pos.x > camWidth - radius)
        {
            pos.x = camWidth - radius;
            offRight = true;
        }
        if(pos.x < -camWidth + radius)
        {
            pos.x = -camWidth + radius;
            offLeft = true;
        }
        if (pos.y > camHeight - radius)
        {
            pos.y = camHeight - radius;
            offUp = true;

        }
        if (pos.y < -camHeight + radius)
        {
            pos.y = -camHeight + radius;
            offDown = true;
        }

        //only true if completely on screen
        isOnScreen = !(offRight || offLeft || offUp || offDown);

        //if keepOnScreen is true, object (GO) will be forced to stay on screen
        if (keepOnScreen && !isOnScreen)
        {
            //Restoring the object to be back on the screen
            transform.position = pos;
            isOnScreen = true;
        }
    }

    //Draw the bounds in the Scene using OnDrawGizmos()
    //Built in MonoBehaviour method that can draw to the Scene pane
    void OnDrawGizmos()
    {
        if (!Application.isPlaying) return;
        Vector3 boundSize = new Vector3(camWidth * 2, camHeight * 2, 0.1f);
        //Wire is placed around the center
        Gizmos.DrawWireCube(Vector3.zero, boundSize);
    }
}
