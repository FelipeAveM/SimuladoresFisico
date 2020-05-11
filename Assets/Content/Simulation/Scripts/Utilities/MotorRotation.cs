using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MotorRotation : MonoBehaviour {

    public float speed = 100;

    Quaternion rot;
    float rotz = 0.0f;

    void Start()
    {
        var rot = transform.rotation;
    }

    void Update () {
       
        rotz += Time.deltaTime * speed;
        transform.eulerAngles = new Vector3(0, 25, rotz);
    }
}
