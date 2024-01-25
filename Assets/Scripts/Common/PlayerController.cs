using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : BaseObject
{
    /*
     * Player Manager Class
     * All player movement/collision logic is stored here
     */


    private void Start()
    {
        
    }


    public override void ObjectUpdate()
    {
        transform.position += new Vector3(0.001f, 0, 0);
    }

    public override void ObjectFixedUpdate()
    {

    }
}
