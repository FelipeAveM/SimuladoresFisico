using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class Ficha3BGrupalChemistry : MonoBehaviour
{
	public Transform alert;
	public DataBuilder dataBuilder;
	public UnityEvent saveCard;

	private Transform tableParent;
	private AlertMessage alertMessage;

	void Start ()
	{
		Button saveButton = transform.Find("Button").Find("btn").GetComponent<Button>();
		saveButton.onClick.AddListener( delegate
			{
				Save();
			});

		if( dataBuilder.dataRisk.FileEvaluation3AChemistry.Count > 0){
			LoadInfo ();
		}
		alertMessage = alert.GetComponent<AlertMessage>();
	}

	void LoadInfo()
	{
		List<File1> file1 = dataBuilder.dataRisk.File1;

		Transform table = transform.Find("TableEvaluation");

		for (int i = 1; i < table.childCount; i++)
		{
			Transform child = table.GetChild(i);
			if (file1 != null && file1.Count > 0)
			{
				child.GetChild(0).GetChild(0).GetComponent<Text>().text = file1[i-1].Risk;
			}
		} 
	}

	public void Save()
	{
		Transform table = transform.Find("TableEvaluation");

		List<File3BChemistry> fileList = new List<File3BChemistry>();

		bool filled = true;
		for(int i = 1; i < table.childCount; i++){
			Transform row = table.GetChild(i);
			if( row.GetChild(1).GetChild(0).GetComponent<Dropdown>().value == 0 ||
				row.GetChild(2).GetChild(0).GetComponent<Dropdown>().value == 0 ||
				row.GetChild(3).GetChild(0).GetComponent<Dropdown>().value == 0 ){
				filled = false;
				i = table.childCount;
			}
		}

		if(filled){
			saveCard.Invoke();
			for(int i = 1; i < table.childCount; i++){
				File3BChemistry file = new File3BChemistry();
				Transform row = table.GetChild(i);
				file.Risk = row.GetChild(0).GetComponentInChildren<Text>().text;
				file.Peligrosidad = row.GetChild(1).GetChild(0).GetComponent<Dropdown>().value;
				file.ViaDeIngreso = row.GetChild(2).GetChild(0).GetComponent<Dropdown>().value;
				file.Valoracion = row.GetChild(3).GetChild(0).GetComponent<Dropdown>().value;

				fileList.Add(file);
			}
			dataBuilder.AddEvaluationChemistry3B(fileList);
		}
		else
		{
			alert.gameObject.SetActive(true);
			alertMessage.CreateAlertMessage("No has completado la Ficha, por favor complétala para avanzar.");
		}
	}
}

