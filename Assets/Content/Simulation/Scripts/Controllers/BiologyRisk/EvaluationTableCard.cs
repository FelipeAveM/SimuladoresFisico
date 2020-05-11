using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class EvaluationTableCard : MonoBehaviour {

	public UnityEvent saveCard;
	public UnityEvent downloadPdf;
	public DataBuilder dataBuilder;

	public string classificationMatriz;

	void Start(){
        if ( dataBuilder.dataRisk.File3.Count > 0){
			LoadInfo ();
		}
	}

	private  void LoadInfo(){
		List<File3> file3 = dataBuilder.dataRisk.File3;
        List<File1> file1 = dataBuilder.dataRisk.File1;
        Transform tableParent = transform.Find ("TableEvaluation");

     
		for (int i = 1; i < tableParent.childCount; i++) {
			File3 file = file3[i-1];
            File1 mainFile = file1[i - 1];

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

            if (mainFile.Clasification.Equals("No es un riesgo"))
            {
                var dropControl = child.GetChild(7).GetChild(0).GetComponent<Dropdown>();
                dropControl.interactable = false;
                dropControl.value = 0;
                child.GetChild(7).GetChild(0).GetComponent<Image>().color = new Color(64, 79, 76, 24);
            }
        }
	}

	public void Save(){
		saveCard.Invoke ();
		List<File4> fileList= new List<File4> ();
		List<File1> file1 = dataBuilder.dataRisk.File1;
		Transform tableParent = transform.Find ("TableEvaluation");

		for (int i = 1; i < tableParent.childCount; i++){
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
			foreach(var filetoComparete in file1){
				if(filetoComparete.Risk.Equals(file.Risk)){
					if(filetoComparete.LevelOfRisk.ToString().Replace("_"," ").Equals(file.LevelOfRisk.ToString())){
						score = 10;
						feeback = "Muy bien, elegiste el nivel de riesgo adecuado";
					}else{
						if(filetoComparete.LevelOfRisk.ToString().Equals("No_es_un_riesgo")){
							feeback = "Si estás evaluando un riesgo biológico, ¿por qué eliges otro tipo de riesgo que no es biológico?";
						}else{
							if(File1.ListLevelToString(filetoComparete.LevelOfRiskValid).Contains(file.LevelOfRisk.ToString())){
								score = 7;
								feeback = "Seleccionaste un nivel de riesgo aproximado, sin embargo no es el ideal, revisa la forma como evaluaste el riesgo.";
							}else{
								score = 4;
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

	public void changeLevelofRisk(Transform parent){
		Dropdown dropRisk = parent.GetChild (7).GetChild(0).GetComponent<Dropdown> ();
		 
		string means = "";
		switch (dropRisk.value) {
		case 0:
			means = "No es un riesgo biologico";
			break;
		case 1:
			means = "Mantener las medidas de control existentes, pero se deberían considerar\nsoluciones o mejoras y se deben hacer comprobaciones periódicas para\nasegurar que el riesgo aún es aceptable. ";
			break;
		case 2:
			means = "Mejorar si es posible. Sería conveniente justificar la intervención y su\nrentabilidad. ";
			break;
		case 3:
			means = "Corregir y adoptar medidas de control de inmediato. Sin embargo,\nsuspenda actividades si el nivel de riesgo está por encima o igual de 360. ";
			break;
		case 4:
			means = "Situación crítica. Suspender actividades hasta que el riesgo esté bajo\ncontrol. Intervención urgente. ";
		
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
