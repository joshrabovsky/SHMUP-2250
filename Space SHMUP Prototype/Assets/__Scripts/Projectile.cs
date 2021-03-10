using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private BoundsCheck _bndCheck;

    void Awake()
    {
        //Add a bounds check to the projectile
        _bndCheck = GetComponent<BoundsCheck>();    
    }

    void Update()
    {
        //If the bndCheck is offUp, destory the object
        if (_bndCheck.offUp)
        {
            Destroy(gameObject);
        }    
    }
}
