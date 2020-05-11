using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[System.Serializable]
public class File4 : System.Object {

	[SerializeField]
	string risk;

	[SerializeField]
	string interNd;

	[SerializeField]
	string interNe;

	[SerializeField]
	string nP;

	[SerializeField]
	string interNp;

	[SerializeField]
	string interNc;

	[SerializeField]
	int nR;

	[SerializeField]
	string levelOfRisk;	
		
	[SerializeField]
	string meaning;

	[SerializeField]
	string feeback;

	[SerializeField]
	float score;

	public string Risk {
		get{ return risk;}
		set{ risk = value;}
	}

	public string InterNd {
		get{ return interNd;}
		set{ interNd = value;}
	}

	public string InterNe {
		get{ return interNe;}
		set{ interNe = value;}
	}


	public string NP {
		get{ return nP;}
		set{ nP = value;}
	}


	public string InterNp {
		get{ return interNp;}
		set{ interNp = value;}
	}


	public string InterNc {
		get{ return interNc;}
		set{ interNc = value;}
	}


	public int NR {
		get{ return nR;}
		set{ nR = value;}
	}

	public string LevelOfRisk {
		get{ return levelOfRisk;}
		set{ levelOfRisk = value;}
	}		

	public string Meaning {
		get{ return meaning;}
		set{ meaning = value;}
	}	

	public string Feeback {
		get{ return feeback;}
		set{ feeback = value;}
	}	

	public float Score {
		get{ return score;}
		set{ score = value;}
	}	
}
