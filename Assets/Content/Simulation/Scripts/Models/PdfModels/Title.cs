using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[System.Serializable]
public class Title : System.Object {

	[SerializeField]
	private int fontSize;

	[SerializeField]
	private string label;

	[SerializeField]
	private string typeDocument = "Title";

	public int FontSize{
		get { return fontSize; }
		set { fontSize = value; }
	}

	public string TypeDocument{
		get { return typeDocument; }
		set { typeDocument = value; }
	}

	public string Label{
		get { return label; }
		set { label = value; }
	}
		
	public Title(){
		// Starting list

	}
}
