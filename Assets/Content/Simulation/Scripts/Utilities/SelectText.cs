using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectText : MonoBehaviour {

    public Color selectColor;
    public Color normalColor;

    private Text text;

    void Start()
    {
        text = GetComponent<Text>();
    }

    public void EnterElement()
    {
        text.color = selectColor;
    }

    public void ExitElement()
    {
        text.color = normalColor;
    }
}
