using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class Ficha4GrupalChemistry : MonoBehaviour
{
	private AlertMessage alertMessage;

	public Transform alert;
    public UnityEvent saveCard;
    public DataBuilder dataBuilder;

    List<Control> controlRisk1;
    List<Control> controlRisk2;
    List<Control> controlRisk3;
    List<Control> controlRisk4;

    public class Control
    {
        public int index;
        public string value;
        public bool isFill = false;

        public Control() { }
    }

    void Start()
    {
		alertMessage = alert.GetComponent<AlertMessage>();

        Control baseControl = new Control();

        controlRisk1 = new List<Control>(new Control[] { baseControl, baseControl, baseControl, baseControl, baseControl, baseControl });
        controlRisk2 = new List<Control>(new Control[] { baseControl, baseControl, baseControl, baseControl, baseControl, baseControl });
        controlRisk3 = new List<Control>(new Control[] { baseControl, baseControl, baseControl, baseControl, baseControl, baseControl });
        controlRisk4 = new List<Control>(new Control[] { baseControl, baseControl, baseControl, baseControl, baseControl, baseControl });

        LoadInfo();
    }

    void LoadInfo()
    {
        List<File1> file1 = dataBuilder.dataRisk.File1;
        Transform table = transform.Find("Scroll").Find("TableEvaluation");
        int fileIndex = 0;
        for (int i = 1; i < table.childCount; i++)
        {
            int index= i - 1;
            Transform item = table.GetChild(i);
            
            if (file1 != null && file1.Count > 0 && (index) % 6 == 0)
            {
                item.GetChild(0).GetChild(0).GetComponent<Text>().text = file1[fileIndex].Risk;
                fileIndex++;
            }

            InputField inputJustification = item.GetChild(1).GetChild(0).GetComponent<InputField>();
            inputJustification.onValueChanged.AddListener(
                delegate
                {
                    EndEdit(inputJustification.transform, index);
                });

        }
    }

    public void EndEdit(Transform textObject, int indexTable)
    {
        string value = textObject.GetComponent<InputField>().text;
        int pos = textObject.parent.parent.GetSiblingIndex() - 1;
        int riskPosition = pos % 6;
  
        Control control = new Control();
		control.index = riskPosition;
		control.value = value;
		control.isFill = true;

        switch ((int)pos/6)
        {
		case 0:
			controlRisk1[indexTable%6] = control;
			break;
		case 1:
			controlRisk2[indexTable%6] = control;
			break;
		case 2:
			controlRisk3[indexTable%6] = control;
			break;
		case 3:
			controlRisk4[indexTable%6] = control;
			break;
          }
		if (value.Trim() == "")
		{
			control.isFill = false;
		}
    }

    bool CheckNextStep()
    {
        int countRisk1 = controlRisk1.FindAll(obj => obj.isFill == true).Count;
        int countRisk2 = controlRisk2.FindAll(obj => obj.isFill == true).Count;
        int countRisk3 = controlRisk3.FindAll(obj => obj.isFill == true).Count;
        int countRisk4 = controlRisk4.FindAll(obj => obj.isFill == true).Count;

        if (countRisk1 >= 3 && 
            countRisk2 >= 3 &&
            countRisk3 >= 3 &&
			countRisk4 >= 3 )
			return true;
		else
			return false;
    }

    public void Save()
    {
		string errorMessage = "";

		if(!CheckNextStep()){
			errorMessage = "Para continuar debes diligenciar entre 3 y 6 controles para cada factor de riesgo.";
		}

		Transform table = transform.Find("Scroll").Find("TableEvaluation");

		for (int i = 1; i < table.childCount; i++)
		{
			if(table.GetChild(i).GetChild(2).GetChild(0).GetComponent<Dropdown>().value == 0){
				errorMessage = "Para continuar debes seleccionar todas las clasificaciones de los controles.";
				i = table.childCount;
			}
		}
			
		if (!errorMessage.Equals(""))
		{
			alert.gameObject.SetActive(true);
			alertMessage.CreateAlertMessage(errorMessage);
		}
			else {
			saveCard.Invoke();

	        List<FileControls> fileList = new List<FileControls>();
	        Transform tableA = transform.Find("Scroll").Find("TableEvaluation");

	        for (int i = 1; i < tableA.childCount; i = i + 6)
	        {
	            FileControls file = new FileControls();
	            Transform childA = tableA.GetChild(i);

	            // Table A
	            Text riskLabel = childA.GetChild(0).GetChild(0).GetComponent<Text>();
	            string risk = "";

	            risk = riskLabel.text;
	            Controls controls = new Controls();
	            List<string> justifications = new List<string>();
	            List<string> control = new List<string>();
	            for (int j = i; j < (i + 6); j++)
	            { 
	                Transform childB = tableA.GetChild(j);
	                InputField typeControlLabel = childB.GetChild(1).GetChild(0).GetComponent<InputField>();
	                Dropdown classification = childB.GetChild(2).GetChild(0).GetComponent<Dropdown>();

	                if (typeControlLabel.text.Trim() != "")
	                {
	                    justifications.Add(typeControlLabel.text);
	                    control.Add(classification.options[classification.value].text);
	                }

	            }
	            controls.justifications = justifications;
	            controls.control = control;

	            file.Control = controls;
	            file.Risk = risk;

	            fileList.Add(file);

	        }
	        dataBuilder.AddControlsGrupal(fileList);
			}
	    }
	}
