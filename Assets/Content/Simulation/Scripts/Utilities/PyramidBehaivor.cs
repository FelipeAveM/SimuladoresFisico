using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PyramidBehaivor : MonoBehaviour {

	public Material pyramidMaterial;
	public int deltaTimeChange = 1;
	private float timer;
	public const string colorName = "_EmissionColor";

	void Update () {
		
		timer += Time.deltaTime;
		if(timer > deltaTimeChange){
			pyramidMaterial.SetColor (colorName, Random.ColorHSV());
			timer = 0;
		}
	}
}
