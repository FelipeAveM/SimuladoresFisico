using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[System.Serializable]
public class ImagePdf :  System.Object {

	[SerializeField]
	private string label = null;

	[SerializeField]
	private string url;

	[SerializeField]
	private byte[] byteImage;

	[SerializeField]
	private string typeDocument = "Image";

	public string Label{
		get { return label; }
		set { label = value; }
	}

	public string Url{
		get { return url; }
		set { url = value; }
	}

	public string TypeDocument{
		get { return typeDocument; }
		set { typeDocument = value; }
	}

	public byte[] ByteImage{
		get { return byteImage; }
		set { 
			byteImage = value; 
			Debug.Log (JsonUtility.ToJson(this));
		}
	}

	public ImagePdf(){
		// Starting list
	
	}

}
