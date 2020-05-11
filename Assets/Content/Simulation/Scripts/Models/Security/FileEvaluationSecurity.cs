using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[System.Serializable]
public class FileEvaluationSecurity : System.Object
{
    [SerializeField]
    int nd;

    [SerializeField]
    string ind;

    [SerializeField]
    int ne;

    [SerializeField]
	string ine;

	[SerializeField]
	int np;

	[SerializeField]
	string inp;

	[SerializeField]
	int nc;

	[SerializeField]
	string inc;

	[SerializeField]
	int nr;

	[SerializeField]
	string inr;

    public int ND
    {
        get { return nd; }
        set { nd = value; }
    }

	public string IND
	{
		get { return ind; }
		set {ind = value; }
	}

	public int NE
	{
		get { return ne; }
		set { ne = value; }
	}

	public string INE
	{
		get { return ine; }
		set {ine = value; }
	}


	public int NP
	{
		get { return np; }
		set { np = value; }
	}

	public string INP
	{
		get { return inp; }
		set {inp = value; }
	}


	public int NC
	{
		get { return nc; }
		set { nc = value; }
	}

	public string INC
	{
		get { return inc; }
		set {inc = value; }
	}


	public int NR
	{
		get { return nr; }
		set { nr = value; }
	}

	public string INR
	{
		get { return inr; }
		set {inr = value; }
	}
}
