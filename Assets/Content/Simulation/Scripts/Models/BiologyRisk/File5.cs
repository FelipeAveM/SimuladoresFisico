using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[System.Serializable]
public class File5 : System.Object {

	[SerializeField]
	string risk;

	[SerializeField]
	string riskFactor;

	[SerializeField]
	string control;

	[SerializeField]
	string classification;

	[SerializeField]
	string feedback;

	[SerializeField]
	float score;

	public string Risk {
		get{ return risk;}
		set{ risk = value;}
	}

	public string RiskFactor {
		get{ return riskFactor;}
		set{ riskFactor = value;}
	}

	public string Control {
		get{ return control;}
		set{ control = value;}
	}

	public string Classification {
		get{ return classification;}
		set{ classification = value;}
	}

	public string Feedback {
		get{ return feedback;}
		set{ feedback = value;}
	}

	public float Score {
		get{ return score;}
		set{ score = value;}
	}

}
