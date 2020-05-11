using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterButton : MonoBehaviour {
	public WorkerCardObject[] cards;
	public Transform characterObject;
	public GameObject sender;
	public string PDFBaseURL;
	private string fullPDFURL;

	// Use this for initialization
	void Start () {
		fullPDFURL = sender.GetComponent<ApiController>().UrlBaseDocs + PDFBaseURL;
		foreach(var worker in cards){
			Transform actualWorker = transform.Find(worker.name);
			actualWorker.GetChild(0).GetComponent<Image>().sprite = worker.ImageCharacter;
			actualWorker.GetChild(1).GetComponent<Text>().text = worker.name;
		
			Button btn = actualWorker.GetComponent<Button>();
			btn.onClick.AddListener(delegate {DownloadWorkerFile( fullPDFURL + worker.name + ".pdf"); });
		}
	}

	public void LoadButtons () {
		transform.parent.Find("Workers").gameObject.SetActive(false);
		gameObject.SetActive(true);
		foreach(var worker in cards){
			Transform actualWorker = transform.Find(worker.name);
			if(characterObject.Find(worker.name).gameObject.activeInHierarchy == true){
				actualWorker.gameObject.SetActive(true);
			}
			else{
				actualWorker.gameObject.SetActive(false);
			}
		}
	}

	void DownloadWorkerFile(string message)
	{
		sender.transform.GetComponent<ApiController>().OpenUrlInWeb(message);
	}
}
