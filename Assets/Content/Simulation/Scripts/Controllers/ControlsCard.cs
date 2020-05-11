using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class ControlsCard : MonoBehaviour {
    [System.Serializable]
    public class ValidControls : System.Object
    {
        public Risk_Name risk;
        public List<CONTROL> controls;
		public List<string> feedback;
    }

	private AlertMessage alertMessage;
	private Transform tableParent;

    public DataBuilder dataBuilder;
    public List<ValidControls> validControls;
    public UnityEvent saveCard;
	public Transform alert;

   

    void Start(){
        Button saveButton = transform.Find("Button").Find("btn").GetComponent<Button>();
        saveButton.onClick.AddListener( delegate
        {
            Save();
        });

		if( dataBuilder.dataRisk.File4.Count > 0){
			LoadInfo ();
		}
		alertMessage = alert.GetComponent<AlertMessage>();
	}

    List<Dropdown.OptionData> GetControlsValid(string riskName)
    {
        foreach (var validControl in validControls)
        {
            if (validControl.risk.ToString().Replace("_"," ") == riskName)
            {
                List<Dropdown.OptionData> optionList = new List<Dropdown.OptionData>();
                foreach (var control in validControl.controls)
                {
                    Dropdown.OptionData option = new Dropdown.OptionData();
                    option.text = control.ToString().Replace("_", " ");
                    optionList.Add(option);
                }
                return optionList;
            }
        }
        return null;
    }

	public void LoadInfo(){
		List<File4> file4 = dataBuilder.dataRisk.File4;
        List<File1> file1 = dataBuilder.dataRisk.File1;
        tableParent = transform.Find ("TableControls");

        for (int i = 1; i < tableParent.childCount; i++) {
            File4 file = file4[i - 1];
            File1 mainFile = file1[i - 1];

            Transform child = tableParent.GetChild(i);
			child.GetChild (0).GetChild (0).GetComponent<Text> ().text = file.Risk;	
			child.GetChild (1).GetChild (0).GetComponent<Text> ().text = file.LevelOfRisk;

            var dropControl = child.GetChild(2).GetChild(0).GetComponent<Dropdown>();
            var dropClasification = child.GetChild(3).GetChild(0).GetComponent<Dropdown>();

            foreach (var option in GetControlsValid(mainFile.Risk))
            {
				dropControl.options.Add(option);
            }

            child.GetChild(2).GetChild(0).Find("Label").GetComponent<Text>().text = dropControl.options[0].text;

            if (mainFile.Clasification.Contains("No es un riesgo")) { 
                dropClasification.interactable = false;
                dropControl.interactable = false;
                dropControl.value = dropControl.options.Count - 1;
				child.GetChild(2).GetChild(0).GetComponent<Dropdown>().value = 0;
                child.GetChild(2).GetChild(0).GetComponent<Dropdown>().options[0].text = "No es necesario";
                child.GetChild(3).GetChild(0).GetComponent<Dropdown>().value = 0;
                child.GetChild(3).GetChild(0).GetComponent<Dropdown>().options[0].text = "No es necesario";
                child.GetChild(2).GetChild(0).GetComponent<Image>().color = new Color(64, 79, 76, 24);
                child.GetChild(3).GetChild(0).GetComponent<Image>().color = new Color(64, 79, 76, 24);
            }
        }
    }

	public void Save(){
		bool tableFull = true;
        List<File1> file1 = dataBuilder.dataRisk.File1;
        for (int i = 1; i < tableParent.childCount; i++){
            File1 mainFile = file1[i - 1];
            Transform child = tableParent.GetChild(i);
			Dropdown control = child.GetChild (2).GetChild(0).GetComponent<Dropdown>();
			Transform controlLabel = child.GetChild (2).GetChild(0).Find("Label");
			Dropdown tipoControl = child.GetChild (3).GetChild(0).GetComponent<Dropdown>();
			Transform tipoControlLabel = child.GetChild (3).GetChild(0).Find("Label");
            if (!mainFile.Clasification.Contains("No es un riesgo"))
            {
                if (control.value == 0)
                {
                    tableFull = false;
                    i = tableParent.childCount;
                }
                if (tipoControl.value == 0)
                {
                    tableFull = false;
                    i = tableParent.childCount;
                }
            }
		}

		if(tableFull){
			saveCard.Invoke ();
			List<File5> fileList= new List<File5> ();
	        for (int i = 1; i < tableParent.childCount; i++){
				Transform child = tableParent.GetChild(i);
				File5 file = new File5 ();

				//Dropdown controlDrop = child.GetChild (2).GetChild(0).GetComponent<Dropdown> ();
				//Dropdown classificationDrop = child.GetChild (3).GetChild(0).GetComponent<Dropdown> ();

				//Las opciones seleccionadas por el usuario
				file.Risk = child.GetChild(0).GetChild(0).GetComponent<Text>().text;
				file.RiskFactor = child.GetChild(1).GetChild(0).GetComponent<Text>().text;
				file.Control =  child.GetChild (2).GetChild(0).Find("Label").GetComponent<Text>().text;								
				file.Classification =  child.GetChild (3).GetChild(0).Find("Label").GetComponent<Text>().text;

	            float score = 0.0f;

				foreach(var document in file1){
					if(document.Risk.Equals(file.Risk)){
	                    // Compare control
                        // Si el control del archivo es el mismo del usuario
						if(document.Control.ToString().Replace("_"," ").Equals(file.Control.ToString())){
							score += 10/7.0f;
						}else{
                            //Si el control del archivo dice "No es necesario"
							if(document.Control.ToString().Replace("_"," ").Equals("No es necesario")){
								score += 0.0f;
							}
							else if(document.ControlsValid.Length > 0){
								if(document.ControlsValid[0].Equals(file.Control.ToString())){
									score += 10*3/7.0f;
								}
								else if(document.ControlsValid[1].Equals(file.Control.ToString())){
									score += 10*2/7.5f;
								}
							}
						}
	                    // Compare classification
						if(document.Control.ToString().Replace("_"," ").Equals("No es necesario") && document.Control.ToString().Replace("_"," ").Equals(file.Control.ToString())){
							score += 10/7.0f;
						}
						else if(document.ControlClassification.ToString().Replace("_"," ").Equals(file.Classification.ToString()) && document.Control.ToString().Replace("_"," ").Equals(file.Control.ToString())){
							score += 10/7.0f;
						}else{
							if(document.Control.ToString().Replace("_"," ").Equals("No es necesario")){
								score += 0.0f;
							}
							else if(document.ControlClassificationValids.Length > 0 && document.ControlsValid.Length > 0){
								if(document.ControlsValid[0].ToString().Replace("_"," ").Equals(file.Control.ToString()) && document.ControlClassificationValids[0].Equals(file.Classification.ToString()) ||
									document.ControlsValid[1].ToString().Replace("_"," ").Equals(file.Control.ToString()) && document.ControlClassificationValids[1].Equals(file.Classification.ToString()))
									score += 10/7.0f;
							}
						}
						foreach(var element in validControls){
							if(element.risk.ToString().Replace("_"," ").Equals(file.Risk)){
								//Si el usuario puso que no era riesgo de seguridad, y efectivamente no lo era.
								//file.Control = lo que acaba de seleccionar el usuario
								//document.Control = La información traida de la ficha de riesgo
								if(file.Control.ToString().Equals("No es necesario")){
									if(document.Control.ToString().Replace("_"," ").Equals("No es necesario"))
										file.Feedback = "Muy bien, seleccionaste que no era riesgo, por lo tanto no era necesario plantear ningún control.";
									else
									{
										//El usuario puso que no era riesgo, pero sí lo era.
										switch(dataBuilder.dataRisk.simulator){
										case 0: file.Feedback = "Indicaste que no se trataba de un riesgo biológico, un buen evaluador debe detectar correctamente los riesgos."; break;
										case 1: file.Feedback = "Indicaste que no se trataba de un riesgo de seguridad, un buen evaluador debe detectar correctamente los riesgos."; break;
										case 2: file.Feedback = "Indicaste que no se trataba de un riesgo químico, un buen evaluador debe detectar correctamente los riesgos."; break;
										}
									}
								} else {
									if(element.feedback.Count>0){
										//Si el usuario puso un control y era un riesgo efectivamente
										for(int j = 0; j<3; j++){
											if(element.controls[j].ToString().Replace("_"," ").Equals(file.Control.ToString()))
												file.Feedback = element.feedback[j];
										}
									}
									else{
										//Si el usuario puso un control para una opción que no era un riesgo de seguridad
										switch(dataBuilder.dataRisk.simulator){
										case 0: file.Feedback = "Indicaste que se trataba de un riesgo biológico, sin serlo. No es necesario plantear controles para este caso."; break;
										case 1: file.Feedback = "Indicaste que se trataba de un riesgo de seguridad, sin serlo. No es necesario plantear controles para este caso."; break;
										case 2: file.Feedback = "Indicaste que se trataba de un riesgo químico, sin serlo. No es necesario plantear controles para este caso."; break;
										}
									}
								}
							}
						}
					}
				}
				file.Score = score;
				fileList.Add (file);
			}

			dataBuilder.AddControls (fileList);
		}
		else{
			alert.gameObject.SetActive (true);
			alertMessage.CreateAlertMessage ("No has seleccionado todas las opciones de la tabla.");
		}
	}
}
