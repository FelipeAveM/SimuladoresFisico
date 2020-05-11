using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class EvaluationCard : MonoBehaviour {

	public UnityEvent saveCard;
	public DataBuilder dataBuilder;
    public Transform rowEvaluation;

	void Start(){
		if( dataBuilder.dataRisk.File1.Count > 0){
			LoadInfo ();
		}
	}

	public void LoadInfo(){
		List<File1> file1 = dataBuilder.dataRisk.File1;
		Transform tableParent = transform.Find ("TableEvaluation");
        int countRow = 1;

        foreach (var file in file1)
        {
            RectTransform item = Instantiate(rowEvaluation) as RectTransform;
            item.SetParent(tableParent);
            item.name = "RowRisk"+countRow;
            item.localScale = Vector3.one;
            item.anchoredPosition3D = new Vector3(0, 0, 0);
            item.GetChild(0).GetChild(0).GetComponent<Text>().text = file.Risk;
			if (file.Clasification.Contains("No es un riesgo"))
            {
                var nd = item.GetChild(1).GetChild(0).GetComponent<Dropdown>();
                var ne = item.GetChild(2).GetChild(0).GetComponent<Dropdown>();
                var nc = item.GetChild(3).GetChild(0).GetComponent<Dropdown>();

                nd.interactable = false;
                ne.interactable = false;
                nc.interactable = false;

                nd.value = nd.options.Count - 1;
                ne.value = ne.options.Count - 1;
                nc.value = nc.options.Count - 1;

				switch(dataBuilder.dataRisk.simulator){
				case 0: 
					item.GetChild(1).GetChild(0).GetComponent<Image>().color = new Color(64, 79, 76, 24);
					item.GetChild(2).GetChild(0).GetComponent<Image>().color = new Color(64, 79, 76, 24);
					item.GetChild(3).GetChild(0).GetComponent<Image>().color = new Color(64, 79, 76, 24);  
					break;
				case 1: 
					item.GetChild(1).GetChild(0).GetComponent<Image>().color = new Color(19, 79, 76, 24);
					item.GetChild(2).GetChild(0).GetComponent<Image>().color = new Color(19, 79, 76, 24);
					item.GetChild(3).GetChild(0).GetComponent<Image>().color = new Color(19, 79, 76, 24);;  
					break;
				case 2: 
					item.GetChild(1).GetChild(0).GetComponent<Image>().color = new Color(48, 79, 76, 24);
					item.GetChild(2).GetChild(0).GetComponent<Image>().color = new Color(48, 79, 76, 24);
					item.GetChild(3).GetChild(0).GetComponent<Image>().color = new Color(48, 79, 76, 24);  
					break;
				}
            }

            countRow++;
        }
	}

	public void Save(){
		saveCard.Invoke ();
		List<File3> fileList= new List<File3> ();
		Transform tableParent = transform.Find ("TableEvaluation");

		for (int i = 1; i < tableParent.childCount; i++){
			Transform child = tableParent.GetChild(i);
			File3 file = new File3 ();

			Dropdown ndDropdown = child.GetChild (1).GetChild(0).GetComponent<Dropdown> ();
			Dropdown neDropdown = child.GetChild (2).GetChild(0).GetComponent<Dropdown> ();
			Dropdown ncDropdown = child.GetChild (3).GetChild(0).GetComponent<Dropdown> ();

			file.Risk = child.GetChild(0).GetChild(0).GetComponent<Text>().text;
			file.Nd =  ndDropdown.options[ndDropdown.value].text;
			file.Ne =  neDropdown.options[neDropdown.value].text;
			file.Nc =  ncDropdown.options[ncDropdown.value].text;
			fileList.Add (file);
		}

        dataBuilder.AddEvaluation (fileList);
	}
}
