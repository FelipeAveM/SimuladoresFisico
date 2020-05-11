using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class ControlsCard_old : MonoBehaviour {
    [System.Serializable]
    public class ValidControls : System.Object
    {
        public Risk_Name risk;
        public List<CONTROL> controls;
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

		if( dataBuilder.dataRisk.File4.Count > 0){
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

            if (mainFile.Clasification.Equals("No es un riesgo")) { 
                dropClasification.interactable = false;
                dropControl.interactable = false;
                dropControl.value = dropControl.options.Count - 1;
                dropClasification.value = dropControl.options.Count - 1;
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

			file.Control =  controlDrop.options[controlDrop.value].text;
			file.Classification =  classificationDrop.options[classificationDrop.value].text;
			file.Risk = child.GetChild(0).GetChild(0).GetComponent<Text>().text;
			file.RiskFactor = child.GetChild(1).GetChild(0).GetComponent<Text>().text;

            float score = 0.0f;

			foreach(var document in file1){
				if(document.Risk.Equals(file.Risk)){
                    // Compare control
					if(document.Control.ToString().Replace("_"," ").Equals(file.Control.ToString().Replace("_"," "))){
						score = score + 2.5f;
					}else{
						if(document.ControlsValid.Length > 0){
							if(File1.ListControlToString(document.ControlsValid).Contains(file.Control.ToString())){
								score = score + 1.8f;
							}
						}
					}
                    // Compare classification
					if(document.ControlClassification.ToString().Replace("_"," ").Equals(file.Classification.ToString().Replace("_"," "))){
						score = score + 2.5f;
					}else{
						if(document.ControlClassificationValids.Length > 0){
							if(File1.ListControlClassificationToString(document.ControlClassificationValids).Contains(file.Classification.ToString())){
								score = score + 1.8f;
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
}
