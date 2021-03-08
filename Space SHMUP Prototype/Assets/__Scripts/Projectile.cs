using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private BoundsCheck bndCheck;

    void Awake()
    {
        //Add a bounds check to the projectile
        bndCheck = GetComponent<BoundsCheck>();    
    }

    void Update()
    {
        //If the bndCheck is offUp, destory the object
        if (bndCheck.offUp)
        {
            Destroy(gameObject);
        }    
    }
}
