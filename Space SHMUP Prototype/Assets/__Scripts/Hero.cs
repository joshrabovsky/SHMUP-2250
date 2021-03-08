using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : MonoBehaviour
{
    //Singleton means the class should only be instantiated one time
    static public Hero S; //Singleton

    [Header("Set in Inspector")]
    //These field control the movement of the ship
    public float speed = 30;
    public float rollMult = -45;
    public float pitchMult = 30;
    public float gameRestartDelay = 2f;
    public GameObject projectilePrefab;
    public float projectileSpeed = 40;

    [Header("Set Dynamically")]
    //This variable holds a reference to the last triggering GameObject
    private GameObject lastTriggerGo = null;

    // Called when the script is being loaded
    void Awake()
    {
        if (S == null)
        {
            S = this; //Set the Singleton to the GameObject that calls the script
        } else
        {
            Debug.LogError("Hero.Awake() - Attempted to assign second Hero.S!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Get information from the input class
        //Did not use GetAxisRaw to give the ship a more natural feel
        float xAxis = Input.GetAxis("Horizontal");
        float yAxis = Input.GetAxis("Vertical");

        //Change the tranform.position based on the axes
        Vector3 pos = transform.position;
        pos.x += xAxis * speed * Time.deltaTime;
        pos.y += yAxis * speed * Time.deltaTime;
        transform.position = pos;

        //Rotate the ship to make it feel more dynamic
        //Rotates the ship based on the way it is moving
        //Since Y-axis and X-axis always have a value from 0 to 1
        //The Ship can be rotated in the X or Y directions up to the multipliers degrees
        //If moving in X-Axis want to rotate along Y-Axis
        //If moving in Y-Axis want to rotate along X-Axis
        transform.rotation = Quaternion.Euler(yAxis * pitchMult, xAxis * rollMult, 0);

        //Allow the ship to fire every time space bar is pressed
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TempFire();
        }
    }

    //Make a Projectile Prefav
    void TempFire()
    {
        GameObject projGO = Instantiate<GameObject>(projectilePrefab);
        //Set the initial position to the GO position
        projGO.transform.position = transform.position;
        //Get the rigidBody component
        Rigidbody rigidB = projGO.GetComponent<Rigidbody>();
        //Add a constant Up Vector to the velocity until off screen
        rigidB.velocity = Vector3.up * projectileSpeed;

    }

    void OnTriggerEnter(Collider other)
    {
        //Get the other GO
        Transform rootT = other.gameObject.transform.root;
        GameObject go = rootT.gameObject;
        //Make sure its not the same triggering GO as last time
        //This can happen if two child GameObjects of the same enemy
        //both trigger the hero collider in the same single frame
        if(go == lastTriggerGo)
        {
            return;
        }
        //assign enemy to last triggered enemy
        lastTriggerGo = go;

        if (go.tag == "Enemy") //Ensure the other is the enemy
        {
            Destroy(go); //Destroy the enemy + its children
            Destroy(gameObject); //Destory the player
            Main.S.DelayedRestart(3); //Restart the game
        }
        else
        {
            //Check that should never run
            print("Triggered by non-enemy: " + go.name);
        }
    }

}
