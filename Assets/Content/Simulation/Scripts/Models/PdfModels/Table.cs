using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Table : MonoBehaviour, ISerializationCallbackReceiver{

	[SerializeField]
	List<string> headers;

	List<List<string>> rowsList;

	[SerializeField]
	private string typeDocument = "Table";

	public List<string> Headers{
		get { return headers; }
		set { headers = value; }
	}

	public string TypeDocument{
		get { return typeDocument; }
		set { typeDocument = value; }
	}

	[SerializeField, HideInInspector]
	private string rows;

	public List<List<string>>  Rows{
		get { return rowsList; }
		set { rowsList = value; }
	}
		
	public Table(){
		// Starting list
		headers = new List<string>();
		rowsList = new List<List<string>>();
	}

	public void addElementRows(List<string> row)
	{
		if (this.rowsList == null) {
			this.rowsList = new List<List<string>> ();
		}

		this.rowsList.Add (row);
	}

	public void addElementHeaders(string header)
	{
		if (this.headers == null) {
			this.headers = new List<string> ();
		}

		this.headers.Add (header);
	}

	public void OnBeforeSerialize()
	{
		int countRow = 0;
		rows = "[";

		foreach(List<string> row in rowsList){
			countRow++;
			int countCells = 0;
			rows += "[";
			foreach(string cell in row){
				countCells++;
				rows += "\"" + cell + "\"";
				if(countCells != row.Count){
					rows += ",";
				}
			}
			rows += "]";
			if(countRow != rowsList.Count){
				rows += ",";
			}
		}
		rows += "],end";
	}

	public void OnAfterDeserialize()
	{
		rowsList = new List<List<string>>();
	}
}
