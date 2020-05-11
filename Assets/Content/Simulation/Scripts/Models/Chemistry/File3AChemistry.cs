using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[System.Serializable]
public class File3AChemistry : System.Object
{
	[SerializeField]
	string risk;

	[SerializeField]
	string proceso;

	[SerializeField]
	public List<string> circunstancias;

	[SerializeField]
	string medidas;

	public string Risk
	{
		get { return risk; }
		set { risk = value; }
	}

	public string Proceso
	{
		get { return proceso; }
		set { proceso = value; }
	}

	public List<string> Circunstancias
	{
		get { return circunstancias; }
		set { circunstancias = value; }
	}

	public string Medidas
	{
		get { return medidas; }
		set { medidas = value; }
	}
}

