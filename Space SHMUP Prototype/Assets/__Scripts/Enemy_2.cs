using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_2 : Enemy
{
    //Second Prefab that extends enemy
    //The direction that it goes is either a 0 or 1,
    //Therefore it is a binary decision for left or right
    private int _direction;

    public void Start()
    {
        //Randomly assinging a 0 or 1 to direction
        _direction = Random.Range(0, 2);
    }

    //NOTE
    //This class is able to work at a 45 degree angle as the addition
    //or subtraction in the x-axis is the same of that as the parent class
    //in the y-axis. Inverse of tan (1) = 45 degress and thus behaves
    //Appropiately
    public override void Move()
    {

        //retrieving the pos using the pos property (from Enemy)
        Vector3 tempPos = pos;

        if(_direction == 0)
        {
            //Move right if direction = 0
            tempPos.x += speed * Time.deltaTime;
        }
        else
        {
            //Move left if direction = 1
            tempPos.x -= speed * Time.deltaTime;
        }

        //The pos equals the change
        pos = tempPos;
        //Move down from the parent class
        base.Move();
    }
}
