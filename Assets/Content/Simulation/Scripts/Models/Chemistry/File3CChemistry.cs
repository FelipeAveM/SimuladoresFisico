using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[System.Serializable]
public class File3CChemistry : System.Object
{
	[SerializeField]
	string risk;

	[SerializeField]
	int presentacion;

	[SerializeField]
	string medicion;

	[SerializeField]
	string permitido;

	[SerializeField]
	int limite;

	public string Risk
	{
		get { return risk; }
		set { risk = value; }
	}

	public int Presentacion	//0: "", 1: Sólido, 2: Líquido
	{
		get { return presentacion; }
		set { presentacion = value; }
	}

	public string Medicion
	{
		get { return medicion; }
		set { medicion = value; }
	}

	public string Permitido
	{
		get { return permitido; }
		set { permitido = value; }
	}

	public int Limite //0: "", 1: Si, 2: No
	{
		get { return limite; }
		set { limite = value; }
	}
}

