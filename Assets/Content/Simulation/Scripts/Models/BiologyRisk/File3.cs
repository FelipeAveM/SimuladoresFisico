using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[System.Serializable]
public class File3 : System.Object {

	[SerializeField]
	string risk;

	[SerializeField]
	string nd;

	[SerializeField]
	string ne;

	[SerializeField]
	string nc;

	public string Risk {
		get{ return risk;}
		set{ risk = value;}
	}

	public string Nd {
		get{ return nd;}
		set{ nd = value;}
	}

	public string Ne {
		get{ return ne;}
		set{ ne = value;}
	}

	public string Nc {
		get{ return nc;}
		set{ nc = value;}
	}
}
