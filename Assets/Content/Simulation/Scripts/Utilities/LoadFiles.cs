using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class LoadFiles : MonoBehaviour {
	
	public UnityEvent postOk;

	public ApiController apiController;
	public DataBuilder dataBuilder;
			
	public void DownloadResult(int endPoint){
		gameObject.SetActive(true);
		string request = dataBuilder.GetDataRisk ();
		if(apiController.islocal)
			print(request);
		switch (endPoint)
		{
		case 0:
			apiController.POST(apiController.UrlBase, ApiController.ENDPOINT_PDF_RISKS_OFFICE, request, OnCompleted, OnError);
			break;
		case 1:
			apiController.POST(apiController.UrlBase, ApiController.ENDPOINT_PDF_BIOLOGY, request, OnCompleted, OnError);
			break;
		case 2:
			transform.GetChild(1).GetComponent<Text>().text = "Se está generando un documento con la información suministrada.";
			apiController.POST(apiController.UrlBase, ApiController.ENDPOINT_PDF_BIOLOGY_LABORATORY_CHARACTER, request, OnCompleted, OnError);
			break;
		case 3:
			apiController.POST(apiController.UrlBase, ApiController.ENDPOINT_PDF_OFFICE_MATRIX, request, OnCompleted, OnError);
			break;
		case 4:
			apiController.POST(apiController.UrlBase, ApiController.ENDPOINT_SAVE_ADVANCE, request, OnDBCompleted, OnError);
			break;
		case 5:
			apiController.POST(apiController.UrlBase, ApiController.ENDPOINT_PDF_SECURITY, request, OnCompleted, OnError);
			break;
		case 6:
			apiController.POST(apiController.UrlBase, ApiController.ENDPOINT_PDF_CHEMISTRY, request, OnCompleted, OnError);
			break;
		}	
	}

	public void OnCompleted(WWW response){
		PdfInfo pdfInfo = JsonUtility.FromJson<PdfInfo>(response.text);
		#if UNITY_WEBGL
		apiController.OpenUrlInWeb(pdfInfo.Data.LocationPdf);
		#else
		Application.OpenURL (pdfInfo.Data.LocationPdf);
		#endif
		postOk.Invoke();
		gameObject.SetActive(false);
	}

	public void OnDBCompleted(WWW response){
		print("POST OK");
		gameObject.SetActive(false);
	}

	public void OnError(string error){
		print (error);
		gameObject.GetComponent<Text>().text = error;
		gameObject.SetActive(false);
	}
}
