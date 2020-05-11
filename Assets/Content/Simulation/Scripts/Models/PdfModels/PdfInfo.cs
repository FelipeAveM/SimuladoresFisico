using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[System.Serializable]
public class PdfInfo :  System.Object {

	[SerializeField]
	private string url;

	[SerializeField]
	private string filename;

	[SerializeField]
	private string date;

	[SerializeField]
	private Data data;

	public string Url{
		get { return url;}
		set { url = value;}
	}

	public string Filename{
		get { return filename;}
		set { filename = value;}
	}

	public string Date{
		get { return date;}
		set { date = value;}
	}

	public Data Data{
		get { return data;}
		set { data = value;}
	}
}
