using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ParentToggle : MonoBehaviour {

	public int validOpcion = 4;
	List<Toggle> isOnElements;

	// Use this for initialization
	void Start () {
		isOnElements = new List<Toggle> ();
	}
	
	public void toogleChange(Toggle element){
		if (element.isOn) {
			isOnElements.Add (element);
			if (isOnElements.Count > validOpcion) {
				isOnElements [0].isOn = false;
			}
		} else {
			isOnElements.Remove (element);
		}
	}

	public void clearToggles(){
		if (isOnElements != null) {
			if (isOnElements.Count > 0) {
				print (isOnElements.Count);
				foreach (var element in isOnElements) {
					element.isOn = false;
					isOnElements.Remove (element);
				}
			}
		}
	}
}
