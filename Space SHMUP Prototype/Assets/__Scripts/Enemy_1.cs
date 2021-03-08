using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_1 : Enemy
{
    //This class extends Enemy and thus contains its functionality
    //Most importantly the Update function works the same way

    //The Move method needs to be overriden since it is Virtual in the
    //parent class. Since it moves straight down it has the same functionality
    public override void Move()
    {
        base.Move();
    }
}
