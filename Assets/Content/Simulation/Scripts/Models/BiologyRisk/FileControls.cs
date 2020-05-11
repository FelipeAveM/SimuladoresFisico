using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[System.Serializable]
public class Controls
{
    public List<string> justifications;
    public List<string> control;
}

[System.Serializable]
public class FileControls : System.Object
{
    [SerializeField]
    string risk;

    [SerializeField]
    Controls control;

    public string Risk
    {
        get { return risk; }
        set { risk = value; }
    }

    public Controls Control
    {
        get { return control; }
        set { control = value; }
    }

}
