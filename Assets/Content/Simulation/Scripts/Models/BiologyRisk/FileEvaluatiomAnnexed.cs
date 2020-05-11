using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[System.Serializable]
public class FileEvaluatiomAnnexed : System.Object
{
    [SerializeField]
    string risk;

    [SerializeField]
    string messureshygienics;

    [SerializeField]
    string interpretation;

    [SerializeField]
    int levelOfRisk;

    [SerializeField]
    int levelOfRiskCorrection;

    [SerializeField]
    int dc;

    [SerializeField]
    int tc;

    public int LevelOfRisk
    {
        get { return levelOfRisk; }
        set { levelOfRisk = value; }
    }

    public int LevelOfRiskCorrection
    {
        get { return levelOfRiskCorrection; }
        set { levelOfRiskCorrection = value; }
    }

    public string Risk
    {
        get { return risk; }
        set { risk = value; }
    }

    public string Messureshygienics
    {
        get { return messureshygienics; }
        set { messureshygienics = value; }
    }

    public string Interpretation
    {
        get { return interpretation; }
        set { interpretation = value; }
    }

    public int Dc
    {
        get { return dc; }
        set { dc = value; }
    }

    public int Tc
    {
        get { return tc; }
        set { tc = value; }
    }

}
