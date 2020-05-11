﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTarget : MonoBehaviour {

    public Transform target;
    public Vector3 offset;

    // Use this for initialization
    void Start () {
        offset = transform.position - target.transform.position;
    }
	
	// Update is called once per frame
	void Update () {
        transform.position = target.transform.position + offset;
    }
}