using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[System.Serializable]
public class FileEvaluation : System.Object
{
    [SerializeField]
    string risk;

    [SerializeField]
    int numberOfAfected;

    [SerializeField]
    string rageIncidence;
            
    [SerializeField]
    string sickness;

    [SerializeField]
    int d;

    [SerializeField]
    int t;

    [SerializeField]
    int i;

    [SerializeField]
    int v;

    [SerializeField]
    int f;

    [SerializeField]
    int levelOfRisk;

    public int LevelOfRisk
    {
        get { return levelOfRisk; }
        set { levelOfRisk = value; }
    }

    public int NumberOfAfected
    {
        get { return numberOfAfected; }
        set { numberOfAfected = value; }
    }

    public string Risk
    {
        get { return risk; }
        set { risk = value; }
    }

    public string RageIncidence
    {
        get { return rageIncidence; }
        set { rageIncidence = value; }
    }

    public string Sickness
    {
        get { return sickness; }
        set { sickness = value; }
    }

    public int D
    {
        get { return d; }
        set { d = value; }
    }

    public int T
    {
        get { return t; }
        set { t = value; }
    }

    public int I
    {
        get { return i; }
        set { i = value; }
    }

    public int V
    {
        get { return v; }
        set { v = value; }
    }

    public int F
    {
        get { return f; }
        set { f = value; }
    }
}
