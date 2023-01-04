using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExampleAI : ParentBT
{
    protected override Action idle => base.idle;

    protected override Action move 
    {
        get 
        {
            return () => 
            {
                if (Input.GetKey(KeyCode.W))
                {
                    gameObject.transform.Translate(Vector3.forward * 2f);
                }
                if (Input.GetKey(KeyCode.S))
                {
                    gameObject.transform.Translate(-Vector3.forward * 2f);
                }
                if (Input.GetKey(KeyCode.A))
                {
                    gameObject.transform.Translate(-Vector3.right * 2f);
                }
                if (Input.GetKey(KeyCode.D))
                {
                    gameObject.transform.Translate(Vector3.right * 2f);
                }
            };
        }
    }

    protected override Action attack
    {
        get
        {
            return () => { Debug.Log("Attack"); };
        }
    }

    protected override Func<bool> isArangeIn => base.isArangeIn;
}
