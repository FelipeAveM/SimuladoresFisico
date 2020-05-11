using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public enum LEVEL_OF_RISK {
	No_es_un_riesgo,
	IV,
	III,
	II,
	I
}

[System.Serializable]
public class File1 : System.Object {

	[SerializeField]
	private string risk;

	[SerializeField]
	private string classification;

    [SerializeField]
    private string characterization;

    [SerializeField]
    private string decriptionGTC;

    [SerializeField]
	private float score = 0.0f;

	[SerializeField]
	private LEVEL_OF_RISK  levelOfRisk; 

	[SerializeField]
	private CONTROL  control = CONTROL.Aumento_de_ventilación; 

	[SerializeField]
	private CONTROL_CLASSIFICATION  controlClassification= CONTROL_CLASSIFICATION.Sustitución; 

	[SerializeField]
	private LEVEL_OF_RISK[] levelOfRiskValid; 

	[SerializeField]
	private CONTROL[] controlsValid; 

	[SerializeField]
	private CONTROL_CLASSIFICATION[]  controlClassificationValids; 

	public string Risk {
		get{ return risk;}
		set{ risk = value;}
	}

	public string Clasification {
		get{ return classification;}
		set{ classification = value;}
	}

    public string Characterization
    {
        get { return characterization; }
        set { characterization = value; }
    }

    public string DecriptionGTC
    {
        get { return decriptionGTC; }
        set { decriptionGTC = value; }
    }

    public float Score {
		get { return score; }
		set { score = value; }	
	}

	public LEVEL_OF_RISK LevelOfRisk {
		get { return levelOfRisk; }
		set { levelOfRisk = value; }
	}

	public CONTROL Control {
		get { return control; }
		set { control = value; }
	}

	public CONTROL_CLASSIFICATION ControlClassification {
		get { return controlClassification; }
		set { controlClassification = value; }
	}

	public LEVEL_OF_RISK[] LevelOfRiskValid {
		get { return levelOfRiskValid; }
		set { levelOfRiskValid = value; }
	}

	public CONTROL[] ControlsValid {
		get { return controlsValid; }
		set { controlsValid = value; }
	}

	public CONTROL_CLASSIFICATION[] ControlClassificationValids {
		get { return controlClassificationValids; }
		set { controlClassificationValids = value; }
	}

	public static string ListToString(List<string> list){
		string result = "";
		foreach(var text in list){
			result += text.Replace("_"," ")+",";
		}

		return result.Substring(0,result.Length-1);
	}

	public static string ListLevelToString(LEVEL_OF_RISK[] list){
		string result = "";
		foreach(var text in list){
			result += text.ToString().Replace("_"," ")+",";
		}

		return result.Substring(0,result.Length-1);
	}

	public static string ListControlToString(CONTROL[] list){
		string result = "";
		foreach(var text in list){
			result += text.ToString().Replace("_"," ")+",";
		}

		return result.Substring(0,result.Length-1);
	}

	public static string ListControlClassificationToString(CONTROL_CLASSIFICATION[] list){
		string result = "";
		foreach(var text in list){
			result += text.ToString().Replace("_"," ")+",";
		}

		return result.Substring(0,result.Length-1);
	}
}
