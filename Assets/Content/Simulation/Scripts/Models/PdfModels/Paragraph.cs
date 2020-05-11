using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[System.Serializable]
public class Paragraph : System.Object {

	[SerializeField]
	private int fontSize;

	[SerializeField]
	private string content;

	[SerializeField]
	private string typeDocument = "Paragraph";

	public int FontSize{
		get { return fontSize; }
		set { fontSize = value; }
	}

	public string Content{
		get { return content; }
		set { content = value; }
	}

	public string TypeDocument{
		get { return typeDocument; }
		set { typeDocument = value; }
	}

	public Paragraph(){
		// Starting list

	}
}
