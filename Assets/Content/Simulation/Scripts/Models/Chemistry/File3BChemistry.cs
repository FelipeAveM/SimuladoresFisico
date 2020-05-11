using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[System.Serializable]
public class File3BChemistry : System.Object
{
	[SerializeField]
	string risk;

	[SerializeField]
	int peligrosidad;

	[SerializeField]
	int viaDeIngreso;

	[SerializeField]
	int valoracion;

	public string Risk
	{
		get { return risk; }
		set { risk = value; }
	}

	public int Peligrosidad
	{
		get { return peligrosidad; }
		set { peligrosidad = value; }
	}

	public int ViaDeIngreso
	{
		get { return viaDeIngreso; }
		set { viaDeIngreso = value; }
	}

	public int Valoracion
	{
		get { return valoracion; }
		set { valoracion = value; }
	}
}

