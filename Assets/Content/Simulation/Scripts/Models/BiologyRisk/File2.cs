using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[System.Serializable]
public class ValidObject : System.Object
{

	[SerializeField]
	public string value;

	[SerializeField]
	public bool isCorrect;
}

[System.Serializable]
public class ValidList : System.Object
{

	[SerializeField]
	public List<string> value;

	[SerializeField]
	public bool isCorrect;
}

[System.Serializable]
public class GrupalInfo : System.Object
{

    [SerializeField]
    public string name;
	[SerializeField]
	public string profile;
    [SerializeField]
    public string age;
    [SerializeField]
    public string ocupation;
    [SerializeField]
    public string seniority;
    [SerializeField]
    public string conditionHealth;
    [SerializeField]
    public string report;
    [SerializeField]
    public string nameProcess;
    [SerializeField]
    public string nameAgent;
    [SerializeField]
    public string zone;
    [SerializeField]
    public string ingressOrganism;
    [SerializeField]
    public string schedule;
    [SerializeField]
    public string rest;
    [SerializeField]
    public string tools;
    [SerializeField]
    public string workTime;
    [SerializeField]
    public string countPerson;
    [SerializeField]
    public string characteristicsZone;
    [SerializeField]
    public string enviroment;
    [SerializeField]
    public string source;
    [SerializeField]
    public string admin;
    [SerializeField]
    public string epp;
    [SerializeField]
    public string infoAgent;

	//INICIO QUIMICOS
	[SerializeField]
	public string process;
	[SerializeField]
	public string amountS;
	[SerializeField]
	public string amountM;
	[SerializeField]
	public string amountL;
	//FIN QUIMICOS

    [SerializeField]
    public string gender;

	[SerializeField]
	public List<Answers> answersList; 
}

[System.Serializable]
public class Answers : System.Object
{
	[SerializeField]
	public bool isApply;
}

[System.Serializable]
public class File2 : System.Object {

    [SerializeField]
    public string referenceName;

    [SerializeField]
    public GrupalInfo grupalInfo;

    [SerializeField]
	public ValidObject name;

	[SerializeField]
	private ValidObject age;

	[SerializeField]
	private ValidObject sex;

	[SerializeField]
	private ValidObject ocupation;

	[SerializeField]
	private ValidObject seniority;

	[SerializeField]
	private ValidList condition;

	[SerializeField]
	private ValidObject schedule;

	[SerializeField]
	private ValidObject kindOfRest;

	[SerializeField]
	private ValidList tools;

	[SerializeField]
	private ValidList epp;

	[SerializeField]
	private ValidObject temperature;

	[SerializeField]
	private ValidObject cleanliness;

	[SerializeField]
	private ValidList noise;

	[SerializeField]
	float score = 0.0f;

    public int questionAmount;

	public ValidObject Name {
		get{ return name;}
		set{ name = value;}
	}

	public ValidObject Age {
		get{ return age;}
		set{ age = value;}
	}

	public ValidObject Sex {
		get{ return sex;}
		set{ sex = value;}
	}

	public ValidObject Ocupation {
		get{ return ocupation;}
		set{ ocupation = value;}
	}

	public ValidObject Seniority {
		get{ return seniority;}
		set{ seniority = value;}
	}

	public ValidList Condition {
		get{ return condition;}
		set{ condition = value;}
	}

	public ValidObject Schedule {
		get{ return schedule;}
		set{ schedule = value;}
	}

	public ValidObject KindOfRest {
		get{ return kindOfRest;}
		set{ kindOfRest = value;}
	}

	public ValidList Tools {
		get{ return tools;}
		set{ tools = value;}
	}

	public ValidList Epp {
		get{ return epp;}
		set{ epp = value;}
	}

	public ValidObject Temperature {
		get{ return temperature;}
		set{ temperature = value;}
	}

	public ValidObject Cleanliness {
		get{ return cleanliness;}
		set{ cleanliness = value;}
	}

	public ValidList Noise {
		get{ return noise;}
		set{ noise = value;}
	}

	public float Score {
		get{ return score;}
		set{ score = value;}
	}

    public int QuestionAmount{
        get { return questionAmount; }
        set { questionAmount = value; }
    }

    public static string listToString(List<string> list){
		string result = "";
		foreach(var text in list){
			result += text+",";
		}

		return result.Substring(0,result.Length-1);
	}
}
