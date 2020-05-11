using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpriteAlphaImage : MonoBehaviour {

	private Image image;
	private float delayBetweenTransitions;
	private float elapseTime = 0;
	const int rangeAlpha = 255;
	private float currentAlpha = 0;

	public float timeToAnimation = 10; //seconds
	[Range (0,255)]
	public int startAlpha;
	delegate void Alpha();
	Alpha sumAlpha;

	void Awake()
	{
		image = GetComponent<Image>();
		delayBetweenTransitions = timeToAnimation / rangeAlpha;
		image.color = new Vector4 (image.color.r, image.color.g, image.color.b, 0);
		currentAlpha = startAlpha;
	}

	void Update(){
		
		if(elapseTime > delayBetweenTransitions){
			elapseTime = 0;
		}
	

		if (currentAlpha > 255) {
			sumAlpha = lessAlpha;
		}
		if (currentAlpha <= startAlpha) {
			sumAlpha = addAlpha;
		}

		sumAlpha ();

		image.color = new Vector4 (image.color.r,image.color.g,image.color.b,currentAlpha/rangeAlpha);

		elapseTime += Time.deltaTime;
	}

	void addAlpha(){
		currentAlpha += 1;
	}

	void lessAlpha(){
		currentAlpha -= 1;
	}

}
