using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class Ficha3AGrupalChemistry: MonoBehaviour {
	
	private AlertMessage alertMessage;

	public Transform alert;
	public DataBuilder dataBuilder;
	public UnityEvent saveCard;

	private Transform tableParent;

    void Start()
    {
        Button saveButton = transform.Find("Button").Find("btn").GetComponent<Button>();
		alertMessage = alert.GetComponent<AlertMessage>();
        saveButton.onClick.AddListener( delegate
        {
            Save();
        });

		if( dataBuilder.dataRisk.File2.Count > 0){
			LoadInfo ();
		}
    }

    void LoadInfo()
	{
        List<File1> file1 = dataBuilder.dataRisk.File1;
    
		Transform table = transform.Find("Scroll").Find("TableEvaluation");

        for (int i = 0; i < table.childCount; i++)
        {
            Transform child = table.GetChild(i);
            if (file1 != null && file1.Count > 0)
            {
                child.GetChild(0).GetChild(0).GetComponent<Text>().text = file1[i].Risk;
			}
        } 
    }

    public void Save()
    {
		string errorMessage = "";
		bool isFull = true;

		Transform table = transform.Find("Scroll").Find("TableEvaluation");

		for(int i = 0; i < table.childCount && isFull; i++){
			Transform row = table.GetChild(i);
			InputField text1 = row.GetChild(1).GetChild(0).GetComponent<InputField>();
			InputField text2 = row.GetChild(3).GetChild(0).GetComponent<InputField>();
			if(text1.text == "" || text2.text == ""){
				errorMessage = "Para continuar debes diligenciar toda la información solicitada.";
				isFull = false;
			}	
			Transform item = row.GetChild(2);
			for(int j = 0; j < item.childCount && isFull; j++){
				InputField text3 = item.GetChild(j).GetChild(1).GetComponent<InputField>();
				if(text3.text == ""){
					errorMessage = "Para continuar debes diligenciar toda la información solicitada.";
					isFull = false;
				}
			}
		}

		if (!errorMessage.Equals(""))
		{
			alert.gameObject.SetActive(true);
			alertMessage.CreateAlertMessage(errorMessage);
		}
		else {
			saveCard.Invoke();
			List<File3AChemistry> fileList = new List<File3AChemistry>();
			for(int i = 0; i < table.childCount && isFull; i++){
				File3AChemistry file = new File3AChemistry();

				Transform row = table.GetChild(i);
				file.Risk = row.GetChild(0).GetComponentInChildren<Text>().text;
				file.Proceso = row.GetChild(1).GetChild(0).GetComponent<InputField>().text;
				file.Medidas = row.GetChild(3).GetChild(0).GetComponent<InputField>().text;

				Transform item = row.GetChild(2);
				file.circunstancias = new List<string>();
				for(int j = 0; j < item.childCount && isFull; j++){
					file.circunstancias.Add( item.GetChild(j).GetChild(1).GetComponent<InputField>().text );
				}

				fileList.Add(file);
			}
			dataBuilder.AddEvaluationChemistry3A(fileList);
		}
    }
}
