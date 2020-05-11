using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Medidor : MonoBehaviour {
	public Transform quimico;
	public Transform unidades;
	public Transform label;
	public RiskCardGrupalObject obj;

	private string quimicoTxt;
	private string unidadesTxt;

	// Use this for initialization
	void Start () {
	}

	public void LoadInformation () {
		quimicoTxt = "--";
		unidadesTxt = "--";

		foreach(Transform toggle in quimico.Find("List")){
			if(toggle.GetComponent<Toggle>().isOn){
				quimicoTxt = toggle.name;
			}
		}

		foreach(Transform toggle in unidades.Find("List")){
			if(toggle.GetComponent<Toggle>().isOn){
				unidadesTxt = toggle.name;
			}
		}

		if(quimicoTxt!="--" && unidadesTxt!="--"){
			if((obj.MG.Count == 0 && int.Parse(unidadesTxt) == 0) || (obj.PPM.Count == 0 && int.Parse(unidadesTxt) == 1)){
				quimicoTxt = "No registra";
				unidadesTxt = "";
			}
			else if(obj.MG.Count != 0 && int.Parse(unidadesTxt) == 0){
				quimicoTxt = obj.MG[int.Parse(quimicoTxt)];
				unidadesTxt = "mg/m3";
			}
			else if(obj.PPM.Count != 0 && int.Parse(unidadesTxt) == 1){
				quimicoTxt = obj.PPM[int.Parse(quimicoTxt)];
				unidadesTxt = "ppm";
			}

			string pantalla = string.Concat(quimicoTxt, " ", unidadesTxt);
			gameObject.GetComponent<Text>().text = pantalla;
			label.GetComponent<Text>().text = pantalla;
		}
	}
}
