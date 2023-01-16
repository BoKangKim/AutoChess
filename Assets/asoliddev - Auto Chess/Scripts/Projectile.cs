using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controls Arrows, Spells movement from point A to B
/// </summary>
public class Projectile : MonoBehaviour
{
    ///Movement speed of the projectile
    public float speed;

    ///Time to wait destroying this projectile after reached the target
    public float duration;

    private GameObject target;

    private bool isMoving = false;
    
    /// <summary>
    /// Called when projectile created
    /// </summary>
    /// <param name="_target"></param>
    public void Init(GameObject _target)
    {
        target = _target;
       
        isMoving = true;
    }

    /// Update is called once per frame
    void Update()
    {
      
        if (isMoving)
        {
            if (target == null)
            {
                Destroy(this.gameObject);
                return;
            }

            Vector3 relativePos = target.transform.position - transform.position;

            // the second argument, upwards, defaults to Vector3.up
            Quaternion rotation = Quaternion.LookRotation(relativePos, Vector3.up);
            this.transform.rotation = rotation;


            Vector3 targetPosition = target.transform.position + new Vector3(0, 1, 0);

            float step = speed * Time.deltaTime; // calculate distance to move
            transform.position = Vector3.MoveTowards(this.transform.position, targetPosition, step);


            float distance = Vector3.Distance(this.transform.position, targetPosition);

            if (distance < 0.2f)
            {
               // isMoving = false;

                this.transform.parent = target.transform;


                Destroy(this.gameObject, duration);
            }
           
        }
     

    }
}
