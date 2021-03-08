using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    //Acts as the superclass for both Enemy_1 and Enemy_2

    [Header("Set in Inspector")]
    public float speed = 10f; //The speed in m/s
    public float fireRate = 0.3f; //Seconds/shot (unused)
    public float health = 10;
    public int score = 100; //Points earned for destroying this


    //This is a Property: A method that acts like a field
    //can get and set pos as if it were a class var of enemy
    //returns the transforms components position or sets it
    public Vector3 pos
    {
        get
        {
            return (this.transform.position);
        }
        set
        {
            this.transform.position = value;
        }
    }

    //Creating a private BoundsCheck so that the enemy stays on screen
    private BoundsCheck bndCheck;

    //bndCheck retrieves the component on awake so that the BoundsCheck
    //script is attached
    void Awake()
    {
        bndCheck = GetComponent<BoundsCheck>();
    }

    // Update is called once per frame
    void Update()
    {
        //Move should always be called as it should always move down
        Move();
        //Getting the current position
        Vector3 tempPos = pos;

        //bndCheck needs to be set and if off screen besides up the GO should
        //be destroyed
        if (bndCheck != null && (bndCheck.offDown || bndCheck.offLeft || bndCheck.offRight))
        {
            //Will destory when completely of the bottom
            Destroy(gameObject);
        }
    }

    //Moves the position of the enemey downwards in the Y-direction
    //by getting and reassigning the pos
    public virtual void Move()
    {
        Vector3 tempPos = pos;
        tempPos.y -= speed * Time.deltaTime;
        pos = tempPos;
    }

    //When a rigidBody and a collider touch each other
    void OnCollisionEnter(Collision collision)
    {
        //retrieve the other game object (presumably projectileHero)
        GameObject otherGO = collision.gameObject;
        if (otherGO.tag == "ProjectileHero")
        {
            Destroy(otherGO);//Destroy the shots
            Destroy(gameObject);//Destroy the enemy
        }
        else
        {
            //This will be called if it collision is triggered and it 
            //is not the tag in the if statement
            print("Enemy hit by non-ProjectileHero: " + otherGO.name);
        }
    }
}
