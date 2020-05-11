using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AlertMessage : MonoBehaviour {

	[System.Serializable]
	public class DetailObject : System.Object{
		public Details[] detailList;
	}

	[System.Serializable]
	public class Details : System.Object{
		public string title;
		public string content;
	}

	public Transform alertPrefab;
	public Transform detailsPrefab;
    public Transform showMessage;
    public Transform sectionFull;
	public Transform sectionSumary;
    public DetailObject[] detailsObject;

    public void CreateMessage(string content)
    {
        RectTransform item = InstanceElement(showMessage, "Showmessage");
        string title = content.Split(',')[0];
        string message = content.Split(',')[1];

        item.Find("Elements").Find("Title").GetComponent<Text>().text = title;
        item.Find("Elements").Find("Messages").GetComponent<Text>().text = message.Replace("\\n", "\n");

        //Close 
        item.Find("Close").GetComponent<Button>().onClick.AddListener(() =>
        {
            Destroy(item.gameObject);
            gameObject.SetActive(false);
        });

    }

	public void CreateAlertMessage (string message){
		RectTransform item = InstanceElement (alertPrefab, "AlertMessage");
		item.Find ("Messages").GetComponent<Text>().text = message;
        Button actioAlert = item.Find("Button").Find("btn").GetComponent<Button>();

        actioAlert.onClick.AddListener( () => 
			{ 
				Destroy(item.gameObject);
				gameObject.SetActive(false);
			});
	}

	public void CreateDetailsView(int indexDetail){
		RectTransform item = InstanceElement (detailsPrefab, "DetailsMesagge");
        //Close View
        item.Find("Close").GetComponent<Button>().onClick.AddListener(() =>
        {
            Destroy(item.gameObject);
            gameObject.SetActive(false);
        });

        int countIndex = 0;
		foreach(Details element in detailsObject[indexDetail].detailList){
			CreateSumarySection (null, item.Find("List"), detailsObject[indexDetail].detailList, countIndex);
			countIndex++;
		}
	}

	private void CreateFullSection(RectTransform currentSection, Transform parent ,Details[] detailList, int childIndex){
		Destroy(currentSection.gameObject);

		// Close all open section
		CloseCreateOne(detailList, childIndex);
	}

	private void CreateSumarySection(RectTransform currentSection, Transform parent ,Details[] detailList, int childIndex){

		if(currentSection!=null)
			Destroy(currentSection.gameObject);
		
		RectTransform sectionItem = InstanceElement (sectionSumary, "SectionSumary");
		sectionItem.SetParent (parent);
		sectionItem.SetSiblingIndex (childIndex);
		sectionItem.Find ("Title").GetComponent<Text> ().text = detailList[childIndex].title;
		sectionItem.Find ("Title").Find ("Display").GetComponent<Button> ().onClick.AddListener(() => CreateFullSection(sectionItem, parent, detailList, childIndex));
	}

	private void CloseCreateOne(Details[] detailList, int childIndex){
		Transform parent = transform.Find("DetailsMesagge").Find("List");
		foreach (Transform child in parent) {
				Destroy(child.gameObject);
				
		}

		int index = 0;
		foreach (Details detail in detailList) {
			if (index == childIndex) {
				RectTransform sectionItem = InstanceElement (sectionFull, "SectionFull");
				sectionItem.SetParent (parent);
				sectionItem.SetSiblingIndex (childIndex);
				sectionItem.Find ("Title").GetComponent<Text> ().text = detailList[index].title;
				sectionItem.Find ("Info").GetComponent<Text> ().text =  detailList[index].content;
				sectionItem.Find ("Title").Find ("Display").GetComponent<Button> ().onClick.AddListener (() => CreateSumarySection (sectionItem, parent, detailList, childIndex));
			} else {
				CreateSumarySection (null, parent, detailList, index);//
			}
			index++;
		}
	}

	private RectTransform InstanceElement(Transform element, string nameElement){
		RectTransform item;
		item = Instantiate (element) as RectTransform;
		item.SetParent(transform);
		item.name = nameElement;
		item.localScale = Vector3.one;
		item.anchoredPosition3D = new Vector3 (0, 0, 0);

		return item;
	}
}
