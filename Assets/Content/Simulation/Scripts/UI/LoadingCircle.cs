using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingCircle : MonoBehaviour {

    private RectTransform rectComponent;
    private float rotateSpeed = 200f;
	public bool rotationActive = true;

    private void Start()
    {
        rectComponent = GetComponent<RectTransform>();
    }

    private void Update()
    {
		if(rotationActive)
       		rectComponent.Rotate(0f, 0f, rotateSpeed * Time.deltaTime);
    }

}
