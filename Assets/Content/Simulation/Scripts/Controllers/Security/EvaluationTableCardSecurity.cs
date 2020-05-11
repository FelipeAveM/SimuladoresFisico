using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class EvaluationTableCardSecurity : MonoBehaviour {

	private AlertMessage alertMessage;

	public UnityEvent saveCard;
	public UnityEvent downloadPdf;
	public DataBuilder dataBuilder;
	public Transform tableParent;
	public Transform alert;

	public string classificationMatriz;

	void Start(){
        if ( dataBuilder.dataRisk.File3.Count > 0){
			LoadInfo ();
		}
		alertMessage = alert.GetComponent<AlertMessage>();
	}

	private  void LoadInfo(){
		List<File3> file3 = dataBuilder.dataRisk.File3;
        List<File1> file1 = dataBuilder.dataRisk.File1;

		for (int i = 0; i < tableParent.childCount; i++) {
			File3 file = file3[i];
            File1 mainFile = file1[i];

            Transform child = tableParent.GetChild(i);
			child.GetChild (0).GetChild (0).GetComponent<Text> ().text = file.Risk;
			child.GetChild (1).GetChild (0).GetComponent<Text> ().text = file.Nd;
			child.GetChild (2).GetChild (0).GetComponent<Text> ().text = file.Ne;
			child.GetChild (5).GetChild (0).GetComponent<Text> ().text = file.Nc;

			int ndValue = 0;
			int.TryParse(file.Nd.Trim().Split('-')[0], out ndValue);

			int neValue = 0;
			int.TryParse(file.Ne.Trim().Split('-')[0], out neValue);

			// Ne * Nd
			int Np = ndValue * neValue;
			child.GetChild (3).GetChild (0).GetComponent<Text> ().text = Np.ToString();

			string interNp = "";
			if (Np <= 4) {
				interNp = "Bajo";
			} else if (Np <= 8) {
				interNp = "Medio";
			} else if (Np <= 20) {
				interNp = "Alto";
			} else {
				interNp = "Muy Alto";
			}
			child.GetChild (4).GetChild (0).GetComponent<Text> ().text = interNp;

			int ncValue = 0;
			int.TryParse(file.Nc.Trim().Split('-')[0], out ncValue);

			// Np * Nc
			int Nr = Np * ncValue;
			child.GetChild (6).GetChild (0).GetComponent<Text> ().text = Nr.ToString ();

			if (mainFile.Clasification.Contains("No es un riesgo"))
            {
                var dropControl = child.GetChild(7).GetChild(0).GetComponent<Dropdown>();
                dropControl.interactable = false;
                dropControl.value = 4;
                child.GetChild(7).GetChild(0).GetComponent<Image>().color = new Color(64, 79, 76, 24);
            }
        }
	}

	public void Save(){
		bool tableFull = true;
		for (int i = 0; i < tableParent.childCount; i++){
			Transform child = tableParent.GetChild(i);
			if(child.GetChild(7).GetChild (0).GetComponent<Dropdown>().value == 5){
				tableFull = false;
				i=tableParent.childCount;
			}
		}

		if(tableFull){
			saveCard.Invoke ();
			List<File4> fileList= new List<File4> ();
			List<File1> file1 = dataBuilder.dataRisk.File1;

			for (int i = 0; i < tableParent.childCount; i++){
				Transform child = tableParent.GetChild(i);
				File4 file = new File4 ();

				Dropdown levelDropdown = child.GetChild (7).GetChild(0).GetComponent<Dropdown> ();
				file.LevelOfRisk =  levelDropdown.options[levelDropdown.value].text;

				int nrValue = 0;
				bool parceNr = int.TryParse(child.GetChild (6).GetChild (0).GetComponent<Text> ().text, out nrValue);
				if (parceNr)
					file.NR = 	nrValue;

				file.Risk = 	child.GetChild(0).GetChild(0).GetComponent<Text>().text;
				file.InterNd = child.GetChild (1).GetChild (0).GetComponent<Text> ().text;
				file.InterNe = child.GetChild (2).GetChild (0).GetComponent<Text> ().text;
				file.NP = child.GetChild (3).GetChild (0).GetComponent<Text> ().text;
				file.InterNp = child.GetChild (4).GetChild (0).GetComponent<Text> ().text;
				file.InterNc = child.GetChild (5).GetChild (0).GetComponent<Text> ().text;
				file.Meaning = child.GetChild (8).GetChild (0).GetComponent<Text> ().text;

				string feeback = "";
				float score = 0.0f;
				foreach(var filetoCompare in file1){
					if(filetoCompare.Risk.Equals(file.Risk)){
						if(filetoCompare.LevelOfRisk.ToString().Replace("_"," ").Equals(file.LevelOfRisk.ToString())){
							score = 40/7.0f;
							feeback = "Muy bien, elegiste el nivel de riesgo adecuado";
						}else{
							if(filetoCompare.LevelOfRisk.ToString().Equals("No_es_un_riesgo")){
								if(filetoCompare.Clasification.Contains("No es un riesgo")){
									score = 40/7.0f;
									feeback = "Muy bien, elegiste que no es un riesgo de seguridad. Un buen evaluador de riesgos debe saber el tipo de riesgo que está evaluando.";
								}
								else{
									feeback = "Si estás evaluando un riesgo de seguridad, ¿por qué eliges otro tipo de riesgo que no es de seguridad?";
								}
							}else{
								if(File1.ListLevelToString(filetoCompare.LevelOfRiskValid).Contains(file.LevelOfRisk.ToString())){
									score = 28/7.0f;
									feeback = "Seleccionaste un nivel de riesgo aproximado, sin embargo no es el ideal, revisa la forma como evaluaste el riesgo.";
								}else{
									score = 16/7.0f;
									feeback = "Revisa muy bien cómo evaluaste el nivel de riesgo porque no corresponde con las condiciones del caso y está mal tu evaluación.";
								}
							}
						}
					}
				}
				file.Feeback = feeback;
				file.Score = score;

				fileList.Add (file);
			}

			dataBuilder.AddTable (fileList);
		}
		else{
			alert.gameObject.SetActive (true);
			alertMessage.CreateAlertMessage ("No has seleccionado todos los niveles de riesgo.");
		}
	}

	public void changeLevelofRisk(Transform parent){
		Dropdown dropRisk = parent.GetChild (7).GetChild(0).GetComponent<Dropdown> ();
		 
		string means = "";
		switch (dropRisk.value) {
		case 0:
			means = "IV- Mantener las medidas de control existentes, pero se deberían considerar soluciones o mejorar y se deben hacer comprobaciones periódicas para asegurar que el riesgo aún es tolerable.";
			break;
		case 1:
			means = "III- Mejorar si es posible. Sería conveniente justificar la intervención y su rentabilidad.";
			break;
		case 2:
			means = "II- Corregir y adoptar medidas de control de inmediato. Sin embargo, suspenda actividades si el nivel de consecuencia está por encima de 60.";
			break;
		case 3:
			means = "I- Situación crítica. Suspender actividades hasta que el riesgo esté bajo control. Intervención urgente.";
			break;
		case 4:
			means = "No es un riesgo.";
			break;
		case 5:
			means = "No has seleccionado una opción válida.";
			break;
		}

		parent.GetChild (8).GetChild (0).GetComponent<Text> ().text = means;

	}

	public void download(){
		#if UNITY_WEBGL
		downloadPdf.Invoke ();
		#else
		Application.OpenURL (classificationMatriz);
		#endif
	}
}
