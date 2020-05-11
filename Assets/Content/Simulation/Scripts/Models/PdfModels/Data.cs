using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[System.Serializable]
public class Data : System.Object {

	[SerializeField]
	private string Etag;

	[SerializeField]
	private string Location;

	[SerializeField]
	private string key;

	[SerializeField]
	private string Key;

	[SerializeField]
	private string Bucket;

	public string LocationPdf {
		get{ return Location;}
		set{ Location = value;}
	}
}
