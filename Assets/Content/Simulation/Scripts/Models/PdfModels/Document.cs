using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Document :  MonoBehaviour, ISerializationCallbackReceiver {

	[SerializeField]
	private string username;

	//private List<System.Object> data;
	private List<System.Object> dataList;

	[SerializeField, HideInInspector]
	private string data;

	public string Username{
		get { return username; }
		set { username = value; }
	}

	public List<System.Object> Data{
		get { return dataList; }
		set { dataList = value; }
	}
		
	public Document (){
		dataList = new List<System.Object> ();
	}

	public void addElementsData(System.Object element)
	{
		if (this.dataList == null) {
			this.dataList = new List<System.Object> ();
		}

		this.dataList.Add(element);
	}

	public void OnBeforeSerialize()
	{
		int listCount = 0;
		data = "[";
		foreach(object element in dataList){
			listCount++;
			var type = element.GetType().ToString();	

			switch (type){
			case "Title":
				data += JsonUtility.ToJson((Title)element);
				break;
			case "ImagePdf":
				data += JsonUtility.ToJson((ImagePdf)element);
				break;
			case "Table":
				data += JsonUtility.ToJson((Table)element);
				break;
			case "Paragraph":
				data += JsonUtility.ToJson((Paragraph)element);
				break;
			}
				
			if(listCount != dataList.Count){
				data += ",";
			}
		}
		data += "],end";
	}

	public void OnAfterDeserialize()
	{
		dataList.Add("json to object");
	}

}