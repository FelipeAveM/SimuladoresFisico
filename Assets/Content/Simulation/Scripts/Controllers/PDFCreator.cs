using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Security.AccessControl;
using System.Runtime.InteropServices;
using System.Net;
using System.IO;
using System;


public class PdfCreator : MonoBehaviour
{
    public Camera cam;
    public RenderTexture renderTexture;
	public Toggle withUI;
	public Transform panelPdf;

	private ProcessImage processImage;
	private ApiController apicontroller;

	delegate void MyDelegate(int num);
	MyDelegate myDelegate;

	void Start(){
		processImage = ProcessImage.instance;
		apicontroller = ApiController.instance;
	}

	private void openUrl(string url){
		Application.OpenURL (url);
	}
		
	public void captureImage(){
		if(withUI.isOn){
			StartCoroutine (processImage.CaptureScreenShot(BuildDocument));
		}else{
			byte[] image;
			image = processImage.captureFrame (Camera.main);
			BuildDocument (image);
		}
	}

	public void BuildDocument(byte[] image){
		Document document = null;
		document = buildBody (image);

		string requestJson = ExtensionMethods.DataSerialization (document);
		print ("Request Service " + requestJson);
		//
		//
		//apicontroller.POST(apiController.UrlBase, "End point", requestJson, OnCompleted, OnError);
		//
		//
	}

	public void OnCompleted(WWW response){
		PdfInfo pdfInfo = JsonUtility.FromJson<PdfInfo>(response.text);
		openUrl (pdfInfo.Data.LocationPdf);
		print ("Response Service " + response.text);
	}
	public void OnError(string error){
		print (error);
	}

	Document buildBody(byte[] image){
		Document document = new Document();

		//get order
		List<Transform> listElemts = new List<Transform>();
		foreach (Transform item in panelPdf) {
			listElemts.Add (item);
		}

		listElemts.Sort(delegate(Transform a, Transform b) {
			return (b.position.y)
				.CompareTo(a.position.y);
		});

		//Example to create pdf
		foreach(Transform item in listElemts){
			string option = item.name;

			switch (option){
			case "Title":
				Title title = addTitle ("Testing use pdf generator", 14);
				document.addElementsData(title);
				break;
			case "Table":
				ImagePdf imagePdf = addImage(image, "Testing ImagePdf in document", "https://www.w3schools.com/w3css/img_fjords.jpg");
				document.addElementsData(imagePdf);
				break;
			case "Image":
				Table table = addTable ();
				document.addElementsData(table);
				break;
			case "Paragraph":
				string lorem = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Donec id urna ut erat varius pulvinar vitae non erat. Praesent convallis eleifend libero. Praesent tempus velit diam, ut condimentum neque euismod eget. Phasellus non eros sit amet turpis convallis vehicula. Ut at pulvinar sapien. Aliquam ut magna vel nisl elementum sagittis. Vestibulum maximus risus nec blandit blandit. Quisque ultrices vestibulum sodales. Aliquam ornare purus nec vehicula mattis. Morbi pretium a sapien sed laoreet. Donec maximus volutpat eros et lacinia. Nunc non nunc lectus. Fusce diam velit, imperdiet ut erat in, vehicula gravida arcu.";
				Paragraph paragraph = addParragraph(lorem,11);
				document.addElementsData(paragraph);
				break;
			}
		}

		//Add document 
		document.Username = "zubcarz";

		return document;
	}

	private Title addTitle(string label, int fontSize){
		Title title = new Title ();
		title.Label = "Testing use pdf generator";
		title.FontSize = 14;
		return title;
	}

	private Paragraph addParragraph(string content, int fontSize){
		Paragraph paragraph = new Paragraph ();
		paragraph.FontSize = fontSize;
		paragraph.Content = content;
		return paragraph;
	}

	private ImagePdf addImage(byte[] image, string label, string url){
		ImagePdf imagePdf = new ImagePdf ();
		imagePdf.Url = url;
		imagePdf.Label = label;
		imagePdf.ByteImage = image;  
		return imagePdf;
	}

	private Table addTable(){
		Table table = new Table();
		table.addElementHeaders("Sector");
		table.addElementHeaders("Value");
		table.addElementHeaders("Rate");

		List<String> row1 = new List<String> ();
		row1.Add ("enginner");
		row1.Add ("10");
		row1.Add ("0.1%");

		List<String> row2 = new List<String> ();
		row2.Add ("economics");
		row2.Add ("100");
		row2.Add ("10.1%");

		List<String> row3 = new List<String> ();
		row3.Add ("Industry");
		row3.Add ("15");
		row3.Add ("2.1%");

		table.addElementRows(row1);
		table.addElementRows(row2);
		table.addElementRows(row3);
		return table;
	}
}
