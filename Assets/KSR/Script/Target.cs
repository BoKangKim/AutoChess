using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    float RotateSpeed;


    private void Start()
    {
        RotateSpeed = 1000f;
    }
    void Update()
    {
        transform.RotateAround(this.transform.position, Vector3.up, RotateSpeed * Time.deltaTime);
    }
}
