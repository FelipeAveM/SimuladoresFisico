using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class ResultCardSecurity : MonoBehaviour {

	public DataBuilder dataBuilder;
	public Transform rowFile1;
	public Transform rowFile4;
	public Transform rowFile5;
	public UnityEvent downloadPdf;

	public Color wrongColor;

	delegate string TypeDelegate();
	TypeDelegate typeDelegate;

	void Start(){
			LoadInfo ();
	}

	void LoadInfo(){
		
		List<File1> file1 = dataBuilder.dataRisk.File1;
		List<File2> file2 = dataBuilder.dataRisk.File2;
		List<File3> file3 = dataBuilder.dataRisk.File3;
		List<File4> file4 = dataBuilder.dataRisk.File4;
		List<File5> file5 = dataBuilder.dataRisk.File5;

        Transform tableResult = transform.Find ("Content").Find("Feeback").Find("ScrollArea");

		Transform section1 = tableResult.Find ("Ficha1");
		Transform section2 = tableResult.Find ("Ficha2");
		Transform section3 = tableResult.Find ("Ficha3");
		Transform section4 = tableResult.Find ("Ficha4");
		Transform totalSection = tableResult.Find ("Total");

		#region File 1
		float totalScoreSection1 = 0.0f;
		float totalScoreSection2 = 0.0f;
		float totalScoreSection3 = 0.0f;
		float totalScoreSection4 = 0.0f;
		float resultEvaluation = 0.0f;

		float tempScore; 

		foreach(var file in file1){
			RectTransform item;
			item = Instantiate (rowFile1) as RectTransform;
			item.SetParent(section1);
			item.SetSiblingIndex(1);
			((RectTransform)section1).sizeDelta = new Vector2 (((RectTransform)section1).sizeDelta.x, ((RectTransform)section1).sizeDelta.y + 50);
			((RectTransform)tableResult).sizeDelta = new Vector2 (((RectTransform)tableResult).sizeDelta.x, ((RectTransform)tableResult).sizeDelta.y + 50);
			item.name = "Row";
			item.localScale = Vector3.one;
			item.anchoredPosition3D = new Vector3 (0, 0, 0);
			
			item.Find ("NoteRisk").GetComponent<Text> ().text = file.Risk;
			item.Find ("ClassificationNote").GetComponent<Text> ().text = file.Clasification;
			item.Find ("NoteValue").GetComponent<Text> ().text = file.Score.ToString("0.00");
			if (file.Score == 0) {
				item.Find ("ClassificationNote").GetComponent<Text> ().color = wrongColor;
				item.Find ("NoteValue").GetComponent<Text> ().color = wrongColor;
			}
			totalScoreSection1 += file.Score;
		}

		section1.Find("Total").Find("Note").GetComponent<Text>().text = totalScoreSection1.ToString("0.00");
		if(totalScoreSection1 < 7){
			section1.Find("Total").Find("Note").GetComponent<Text>().color = wrongColor;	
		}

		#endregion

		#region File2
		Transform table  = section2.Find("TableControls");
		for (int i = 1; i< table.childCount; i++){
			Transform item = table.GetChild(i);
			int countColumn = 1;
			foreach(File2 file in file2){
				switch(item.name){
					case "RowName":
					SetChildSection2(countColumn,file.Name.value,file.Name.isCorrect,item);
						break;
					case "RowYears":
					SetChildSection2(countColumn,file.Age.value,file.Age.isCorrect,item);
						break;
					case "RowGender":
					SetChildSection2(countColumn,file.Sex.value,file.Sex.isCorrect,item);
						break;
					case "RowProfession":
					SetChildSection2(countColumn,file.Ocupation.value,file.Ocupation.isCorrect,item);
						break;
					case "RowAntiquity":
					SetChildSection2(countColumn,file.Seniority.value,file.Seniority.isCorrect,item);
						break;
					case "RowCondition":
					SetChildSection2(countColumn,File2.listToString(file.Condition.value),file.Condition.isCorrect,item);
						break;
					case "RowSchedule":
					SetChildSection2(countColumn,file.Schedule.value,file.Schedule.isCorrect,item);
						break;
					case "RowRest":
					SetChildSection2(countColumn,file.KindOfRest.value,file.KindOfRest.isCorrect,item);
						break;
					case "RowTool":
					SetChildSection2(countColumn,File2.listToString(file.Tools.value),file.Tools.isCorrect,item);
						break;
					case "RowEpp":
					SetChildSection2(countColumn,File2.listToString(file.Epp.value),file.Epp.isCorrect,item);
						break;
					case "RowTemperature":
					SetChildSection2(countColumn,file.Temperature.value,file.Temperature.isCorrect,item);
						break;
					case "RowSortandCleanliness":
					SetChildSection2(countColumn,file.Cleanliness.value,file.Cleanliness.isCorrect,item);
						break;
					case "RowNoise":
					SetChildSection2(countColumn,File2.listToString(file.Noise.value),file.Noise.isCorrect,item);
						break;
				}
				countColumn++;
			}
		}

	
		foreach(var file in file2){
			totalScoreSection2 += file.Score;
		}

		section2.Find("Total").Find("Note").GetComponent<Text>().text = totalScoreSection2.ToString("0.00");

		#endregion;

		#region file 3 y 4


		foreach(var file in file4){

			RectTransform item;
			item = Instantiate (rowFile4) as RectTransform;
			item.SetParent(section3);
			item.SetSiblingIndex(2);
			((RectTransform)section3).sizeDelta = new Vector2 (((RectTransform)section1).sizeDelta.x, ((RectTransform)section1).sizeDelta.y + 50);
			((RectTransform)tableResult).sizeDelta = new Vector2 (((RectTransform)tableResult).sizeDelta.x, ((RectTransform)tableResult).sizeDelta.y + 50);
			item.name = "Row";
			item.localScale = Vector3.one;
			item.anchoredPosition3D = new Vector3 (0, 0, 0);

			item.Find ("Risk").GetComponent<Text> ().text = file.Risk;
			item.Find ("Clasification").GetComponent<Text> ().text = file.LevelOfRisk;
			item.Find ("Feeback").GetComponent<Text> ().text = file.Feeback;
			item.Find ("Total").GetComponent<Text> ().text = file.Score.ToString("0.00");

			if (file.Score < 4) {
				item.Find ("Risk").GetComponent<Text> ().color = wrongColor;
				item.Find ("Clasification").GetComponent<Text> ().color = wrongColor;
				item.Find ("Feeback").GetComponent<Text> ().color = wrongColor;
				item.Find ("Total").GetComponent<Text> ().color = wrongColor;
			}

			totalScoreSection3 += file.Score;
		}

		section3.Find("Total").Find("Note").GetComponent<Text>().text = totalScoreSection3.ToString("0.00");
		if(totalScoreSection3 < 28){
			section3.Find("Total").Find("Note").GetComponent<Text>().color = wrongColor;	
		}
		#endregion

		#region File 5 
		foreach(var file in file5){

			RectTransform item;
			item = Instantiate (rowFile5) as RectTransform;
			item.SetParent(section4);
			item.SetSiblingIndex(2);
			((RectTransform)section4).sizeDelta = new Vector2 (((RectTransform)section1).sizeDelta.x, ((RectTransform)section1).sizeDelta.y + 50);
			((RectTransform)tableResult).sizeDelta = new Vector2 (((RectTransform)tableResult).sizeDelta.x, ((RectTransform)tableResult).sizeDelta.y + 50);
			item.name = "Row";
			item.localScale = Vector3.one;
			item.anchoredPosition3D = new Vector3 (0, 0, 0);

			item.Find ("Danger").GetComponent<Text> ().text = file.Risk;
			item.Find ("Control").GetComponent<Text> ().text = file.Control;
			item.Find ("Clasification").GetComponent<Text> ().text = file.Classification;
			item.Find ("Total").GetComponent<Text> ().text = file.Score.ToString("0.00");
			if(file.Score < 2.0f){
				item.Find ("Total").GetComponent<Text> ().color = wrongColor;
			}

			totalScoreSection4 += file.Score;
		}

		section4.Find("Total").Find("Note").GetComponent<Text>().text = totalScoreSection4.ToString("0.00");
		if(totalScoreSection4 < 14){
			section4.Find("Total").Find("Note").GetComponent<Text>().color = wrongColor;	
		}

		#endregion

		#region total
			
		totalSection.Find("Result1").Find("Note").GetComponent<Text>().text = totalScoreSection1.ToString("0.00");
		totalSection.Find("Result1").Find("Note").GetComponent<Text>().color = section1.Find("Total").Find("Note").GetComponent<Text>().color;

		totalSection.Find("Result2").Find("Note").GetComponent<Text>().text = totalScoreSection2.ToString("0.00");
		totalSection.Find("Result2").Find("Note").GetComponent<Text>().color = section2.Find("Total").Find("Note").GetComponent<Text>().color;

		totalSection.Find("Result3").Find("Note").GetComponent<Text>().text = totalScoreSection3.ToString("0.00");
		totalSection.Find("Result3").Find("Note").GetComponent<Text>().color = section3.Find("Total").Find("Note").GetComponent<Text>().color;
	
		totalSection.Find("Result4").Find("Note").GetComponent<Text>().text = totalScoreSection4.ToString("0.00");
		totalSection.Find("Result4").Find("Note").GetComponent<Text>().color = section4.Find("Total").Find("Note").GetComponent<Text>().color;

		resultEvaluation = totalScoreSection1 + totalScoreSection2 + totalScoreSection3 + totalScoreSection4;
		totalSection.Find("Total").Find("Note").GetComponent<Text>().text = resultEvaluation.ToString("0.00");
		if(resultEvaluation < 70){
			totalSection.Find("Total").Find("Note").GetComponent<Text>().color = wrongColor;
		}

		#endregion

	}

	void SetChildSection2(int count, string response, bool isCorrect,Transform item){

		item.GetChild(count).GetChild(0).GetComponent<Text>().text = response;
		if (!isCorrect)
			item.GetChild (count).GetChild (0).GetComponent<Text> ().color = wrongColor;
	}

}

