using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class Ficha3CGrupalChemistry : MonoBehaviour
{
	public DataBuilder dataBuilder;
	public Transform pickups;
	public UnityEvent saveCard;

	private Transform tableParent;

	void Start()
	{	/*
		for(int i=0; i<pickups.childCount; i++){
			GameObject item = pickups.GetChild(i).gameObject;
			Object prueba = item.GetComponent<MouseAction>().OnMouseClick.GetPersistentTarget(0);
		}*/
		//pickups.gameObject.SetActive(false);
		//zones.gameObject.SetActive(true);
	}
}

