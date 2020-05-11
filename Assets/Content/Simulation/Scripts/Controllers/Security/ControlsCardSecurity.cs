using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class ControlsCardSecurity : MonoBehaviour {
    [System.Serializable]
    public class ValidControls : System.Object
    {
        public Risk_Name risk;
        public List<CONTROL> controls;
		public List<string> feedback;
    }

    public DataBuilder dataBuilder;
    public List<ValidControls> validControls;
    public UnityEvent saveCard;

    private Transform tableParent;

    void Start(){
        Button saveButton = transform.Find("Button").Find("btn").GetComponent<Button>();
        saveButton.onClick.AddListener( delegate
        {
            Save();
        });

		if( dataBuilder.dataRisk.FileEvaluationSecurity.Count > 0){	//File4 no existe
			LoadInfo ();
		}
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
				child.GetChild(2).GetChild(0).Find("Label").GetComponent<Text>().text = "No es necesario";
				child.GetChild(3).GetChild(0).Find("Label").GetComponent<Text>().text = "No es necesario";
                child.GetChild(2).GetChild(0).GetComponent<Image>().color = new Color(64, 79, 76, 24);
                child.GetChild(3).GetChild(0).GetComponent<Image>().color = new Color(64, 79, 76, 24);
            }

        }
    }

	public void Save(){
		saveCard.Invoke ();
		List<File5> fileList= new List<File5> ();
		List<File1> file1= dataBuilder.dataRisk.File1;
        for (int i = 1; i < tableParent.childCount; i++){
			Transform child = tableParent.GetChild(i);
			File5 file = new File5 ();

			Dropdown controlDrop = child.GetChild (2).GetChild(0).GetComponent<Dropdown> ();
			Dropdown classificationDrop = child.GetChild (3).GetChild(0).GetComponent<Dropdown> ();

			//Las opciones seleccionadas por el usuario
			file.Risk = child.GetChild(0).GetChild(0).GetComponent<Text>().text;
			file.RiskFactor = child.GetChild(1).GetChild(0).GetComponent<Text>().text;
			file.Control =  child.GetChild (2).GetChild(0).Find("Label").GetComponent<Text>().text;								
			file.Classification =  child.GetChild (3).GetChild(0).Find("Label").GetComponent<Text>().text;

            float score = 0.0f;

			foreach(var document in file1){
				if(document.Risk.Equals(file.Risk)){
                    // Compare control
					if(document.Control.ToString().Replace("_"," ").Equals(file.Control.ToString().Replace("_"," "))){
						score = score + 10/7.0f;
						file.Feedback = document.Control.ToString();
					}else{
						if(document.Control.ToString().Replace("_"," ").Equals("No es necesario")){
							score += 0.0f;
						}
						else if(document.ControlsValid.Length > 0){
							if(document.ControlsValid[0].Equals(file.Control.ToString().Replace("_"," "))){
								score = score + 10*3/7.0f;
								file.Feedback = document.ControlsValid[0].ToString();
							}
							else if(document.ControlsValid[1].Equals(file.Control.ToString().Replace("_"," "))){
								score = score + 10*2/7.5f;
								file.Feedback = document.ControlsValid[1].ToString();
							}
						}
					}
                    // Compare classification
					if(document.Control.ToString().Replace("_"," ").Equals("No es necesario") && document.Control.ToString().Replace("_"," ").Equals(file.Control.ToString().Replace("_"," "))){
						score = score + 10/7.0f;
					}
					else if(document.ControlClassification.ToString().Replace("_"," ").Equals(file.Classification.ToString().Replace("_"," ")) && document.Control.ToString().Replace("_"," ").Equals(file.Control.ToString().Replace("_"," "))){
						score = score + 10/7.0f;
					}else{
						if(document.Control.ToString().Replace("_"," ").Equals("No es necesario")){
							score += 0.0f;
						}
						else if(document.ControlClassificationValids.Length > 0 && document.ControlsValid.Length > 0){
							if(document.ControlsValid[0].Equals(file.Control.ToString().Replace("_"," ")) && document.ControlClassificationValids[0].Equals(file.Classification.ToString().Replace("_"," ")) ||
							   document.ControlsValid[1].Equals(file.Control.ToString().Replace("_"," ")) && document.ControlClassificationValids[1].Equals(file.Classification.ToString().Replace("_"," ")))
								score = score + 10/7.0f;
						}
					}
				}
			}
			file.Score = score;
			fileList.Add (file);
		}

		dataBuilder.AddControls (fileList);
	}
}
