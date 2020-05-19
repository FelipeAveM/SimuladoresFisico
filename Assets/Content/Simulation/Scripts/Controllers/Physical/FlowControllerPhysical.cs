﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
//using System.Diagnostics;
using UnityEngine.SceneManagement;
using System;

public class FlowControllerPhysical : MonoBehaviour {

	[System.Serializable]
	public class Slides : System.Object{
		public string title;
		public string buttonName;
		public Transform[] panels;
		public int advance;
        public UnityEvent createSlide;
        public UnityEvent nextAction;
		public int story = 0;
		public bool startStory;
		public bool activeBusinessMouseAction;
        public bool isActiveNextAction = false;

    }

    [System.NonSerialized] public static MatrizMedidores [] matVar = MatrizMedidoresPrueba.matrizDePrueba; 
    [System.NonSerialized] public static MatrizMedidores [] matVar2 = MatrizMedidoresPrueba2.matrizDePrueba; 
    [System.NonSerialized] public static MatrizRiesgosFisico [] matrizRiesgosFisicoVar = MatrizRiesgosFisicosPrueba.mrfs;
    [System.NonSerialized] public static List<MatrizMedidores[]> listaFinalMedidoresPrueba = new List<MatrizMedidores[]>(); 
    [System.NonSerialized] public static List<MatrizRiesgosFisico []> matrizFinalRiesgosFisicos = new List<MatrizRiesgosFisico[]>();
    [System.NonSerialized] public static DataGame data;
	public Slides[] slides;
	public int index = 0;
	public Transform topBar;
	public Transform buttonBar;
	public FmodHandWriting baseHandWrite;
    public Transform baseHandWriteIntro;
	public Transform businessParent;
    public UnityEvent startIntro;
	public UnityEvent postOk;

	private delegate void EventDelegate();
	private EventDelegate action;
	private Text title;
    private Text buttonLabel;
	private Button nextBtn;
    private DataBuilderPhysical dataBuilder;
    private Tour tour;
    private ApiController apiController;
    private int[] business;
	private int activeBusiness;

    public int empresaSeleccionada;
    public int riesgoActivo = 0;
    public int randomBusiness = 2;
    public int zonasTerminadas = 0;
    public bool tutoMedidores;
    public static DataPlayer dataPlayer;

    void Awake(){
        dataBuilder = GetComponent<DataBuilderPhysical>();
        apiController = GetComponent<ApiController>();
        tour = GetComponent<Tour>();
        // Pueba
        //listaFinalMedidoresPrueba.Add(matVar);
        //listaFinalMedidoresPrueba.Add(matVar2);
        //matrizFinalRiesgosFisicos.Add(matrizRiesgosFisicoVar);
    }

    void Start(){
        tutoMedidores = false;
        checkJSONDataPlayer();
        //Pruebas
        //dataPlayer.data.setMatrizMedidores1emp();
        //dataPlayer.data.setMatrizMedidores2emp();
        //Debug.Log(JsonUtility.ToJson(dataPlayer));
        switch (dataPlayer.getFichaActual()){
            case 0:
                //Nuevo
                dataPlayer.setFichaActual(1);
                business = new int[randomBusiness];
                GetReference ();
                ClosePanels ();
                ChooseBusiness();
                OpenPanel (index);
                break;
            case 1:
                GetReference ();
                ClosePanels ();
                ChooseBusines2(dataPlayer.getEmp1(), dataPlayer.getEmp2());
                OpenPanel (index);
                break;
            case 2:
                GetReference ();
                ClosePanels ();
                ChooseBusines2(dataPlayer.getEmp1(), dataPlayer.getEmp2());
                OpenPanel (index);
                break;
            case 3:
                GetReference ();
                ClosePanels ();
                ChooseBusines2(dataPlayer.getEmp1(), dataPlayer.getEmp2());
                OpenPanel (index);
                break;
            case 5:
                GetReference ();
                ClosePanels ();
                ChooseBusines3(dataPlayer.getEmp1(), dataPlayer.getEmp2());
                OpenPanel (index);
                baseHandWriteIntro.transform.GetComponent<FmodHandWriting>().StartStory(1);
                break;
            case 7:
                //
                GetReference ();
                ClosePanels ();
                ChooseBusines2(dataPlayer.getEmp1(), dataPlayer.getEmp2());
                OpenPanel (index);
                break;
            default:
                dataPlayer.setFichaActual(1);
                business = new int[randomBusiness];
                GetReference ();
                ClosePanels ();
                ChooseBusiness();
                OpenPanel (index);
                break;
        }
	}

    void checkJSONDataPlayer(){
        //Real
        //getSimulatorData();
        //getSimulatorDataPlayer();
        data = new DataGame();
        dataPlayer = new DataPlayer("usuarioPoli","nombre1","apellido","usuario@poligran.edu.co","Fisico","Laboratorio", false, 0, 0, 0, data);
        
        //Pruebas
        //data = new DataGame();
        //data.setListaFinalMedidores(listaFinalMedidoresPrueba);
        //Debug.Log("Length Lista Final Medidores Prueba: " + data.getListaFinalMedidores().Count);
        /*for (int i = 0; i < FlowControllerPhysical.data.getListaFinalMedidores().Count; i++){
            Debug.Log("Matriz: " + i);
            for (int j = 0; j < FlowControllerPhysical.data.getListaFinalMedidores()[i].Length; j++){
                Debug.Log(JsonUtility.ToJson(FlowControllerPhysical.data.getListaFinalMedidores()[i][j]));
            }
        }*/
        //dataPlayer = new DataPlayer("mfavendano","Felipe","Avendaño","mfavendano@poligran.edu.co","Fisico","Laboratorio", false, 3, 0, 1, data);
        
    }

    public static void setM2(){
        listaFinalMedidoresPrueba.Add(matVar2);
        Debug.Log("Length Lista Final Medidores Prueba: " + data.getListaFinalMedidores().Count);
        dataPlayer.data.setMatrizMedidores2emp();
    }
    void GetReference(){
		nextBtn = buttonBar.Find ("Button").Find("btn").GetComponent<Button>();
		buttonLabel = buttonBar.Find ("Button").Find("btn").Find ("Text").GetComponent<Text> ();
        title = topBar.Find ("Title").GetComponent<Text> ();
	}
    public void OpenPanel(int panelIndex) {
        index = index + 1;
        //dataBuilder.SetIndex(index);
        ClosePanels();
        foreach (Transform item in slides[panelIndex].panels) {
            item.gameObject.SetActive(true);
        }
        if (slides[panelIndex].title.Equals("INTRODUCCION")){
            startIntro.Invoke();
        }
        slides[panelIndex].createSlide.Invoke();
        title.text = slides[panelIndex].title;
        nextBtn.interactable = false;
        if (slides[panelIndex].startStory) {
            baseHandWrite.gameObject.SetActive(true);
            baseHandWrite.StartStory(slides[panelIndex].story);
        }
        buttonLabel.text = slides[panelIndex].buttonName;
        nextBtn.onClick.RemoveAllListeners();
        nextBtn.onClick.AddListener(() => NextSlider());

        nextBtn.interactable = slides[panelIndex].isActiveNextAction;
	}
	public void NextSlider(){
        slides[index - 1 ].nextAction.Invoke();
        if(index < slides.Length)
		    OpenPanel(index);	
	}		
	private void ClosePanels(){
		foreach(Slides element in slides){
			foreach(Transform item in element.panels){
				item.gameObject.SetActive (false);
			}
		}
	}
	IEnumerator Waiting(int waitingTime, EventDelegate action)
	{
		yield return new WaitForSeconds(waitingTime);
		action ();
	}

    MouseAction GetMouseAction(Transform item)
    {
        MouseAction mouseAction;
        if (item.GetComponent<MouseAction>() != null)
        {
            mouseAction = item.GetComponent<MouseAction>();
        }
        else
        {
            mouseAction = item.Find("master").Find("Reference").Find("Hips").GetComponent<MouseAction>();
        }
        return mouseAction;
    }

	public void ActiveBusinessMouseAction(bool enable){
        foreach (Transform item in businessParent){
			if (item.gameObject.activeSelf) {
                MouseAction mouseAction = GetMouseAction(item);
                mouseAction.active = enable;
				if(!enable){
					mouseAction.SimpleCursor ();
				}
			}
		}
	}

    public int GetActiveBusiness() {
        return activeBusiness;
    }

    private static int[] UniqueRandomNumbers(int min, int max, int howMany){

        int[] myNumbers = new int[howMany];
        if (howMany == max - min)
        {
            for (int i = 0; i < howMany; i++)
                myNumbers[i] = i;
            return myNumbers;
        }

        System.Random randNum = new System.Random();
        for (int currIndex = 0; currIndex < howMany; currIndex++)
        {
            int randCandidate = randNum.Next(min, max);
            while (myNumbers.Contains(randCandidate))
            {
                randCandidate = randNum.Next(min, max);
            }

            myNumbers[currIndex] = randCandidate;
        }

        return myNumbers;
    }

    public void ChooseBusines3(int index, int j){
		//ASEGURAR QUE ESTÁN ESCONDIDOS
		for(int i=0; i<businessParent.childCount; i++){
			businessParent.GetChild(i).gameObject.SetActive(false);
		}
        businessParent.GetChild(index).gameObject.SetActive(true);
        businessParent.GetChild(index).GetComponent<Button>().interactable = false;  
        businessParent.GetChild(j).gameObject.SetActive(true);
        businessParent.GetChild(j).GetComponent<Button>().interactable = true;
    }

    public void ChooseBusines2(int index, int j){
		//ASEGURAR QUE ESTÁN ESCONDIDOS
		for(int i=0; i<businessParent.childCount; i++){
			businessParent.GetChild(i).gameObject.SetActive(false);
		}
        businessParent.GetChild(index).gameObject.SetActive(true);
        businessParent.GetChild(index).GetComponent<Button>().interactable = false;  
        businessParent.GetChild(j).gameObject.SetActive(true);
        businessParent.GetChild(j).GetComponent<Button>().interactable = false;  
    }
    public void ChooseBusiness(){
		//ASEGURAR QUE ESTÁN ESCONDIDOS
		for(int i=0; i<businessParent.childCount; i++){
			businessParent.GetChild(i).gameObject.SetActive(false);
		}
		//MOSTRAR ÚNICAMENTE LOS ÍCONOS ESCOGIDOS EN LA SIMULACIÓN
		business = UniqueRandomNumbers(0, businessParent.childCount + 1, randomBusiness);
		foreach( int index in business)
		{
			businessParent.GetChild(index - 1).gameObject.SetActive(true);
			businessParent.GetChild(index - 1).GetComponent<Button>().interactable = false;   
		} 
        dataPlayer.setEmp1(business[0]-1);
        dataPlayer.setEmp2(business[1]-1);
        /*
		for(int i=0; i<businessParent.childCount; i++){
			if(businessParent.GetChild(i).gameObject.activeSelf == true){
				businessParent.GetChild(i).GetComponent<Button>().interactable = true;
				i=businessParent.childCount;
			}
		}
        */
    }

    public void ActivateBusiness() {
        //ChooseBusiness();
        int contN = 0;
        if (riesgoActivo >= 0 && riesgoActivo < businessParent.childCount)
        {
            for (int i = 0; i < businessParent.childCount; i++)
            {
                if (businessParent.GetChild(i).gameObject.activeSelf == true)
                {
                    if (contN == riesgoActivo)
                    {
                        businessParent.GetChild(i).GetComponent<Button>().interactable = true;
                        activeBusiness = i;
                        i = businessParent.childCount;
                    }
                    contN++;
                }
            }
        }
        if(riesgoActivo < randomBusiness){
            riesgoActivo++;
        }
    }

    public void DowloadResult(int endPoint){
        switch(endPoint){
            case 1:
                break;
            case 2:
                string request = JsonUtility.ToJson(Ficha4PhysicalRisk.matrizFinalRiesgosFisicos[0][Ficha1Fisicos.empresasCompletadas]);
                #if UNITY_EDITOR
                print(request);
                #endif
                apiController.POST(apiController.UrlBase, ApiController.ENDPOINT_PDF_PHYSICAL, request, OnCompleted, OnError);
                break;
            default:
                break;
           
        }
	}

    public void updateSimulador(){
        /*
        Debug.Log("Update Simulador");
        //data = new DataGame(listaFinalMedidoresPrueba, matrizFinalRiesgosFisicos);
        string request = JsonUtility.ToJson(dataPlayer);
        #if UNITY_EDITOR
        print(request);
        #endif
        //apiController.POST(apiController.UrlBase, "update/simulator", request, OnCompleted, OnError);   
        */
    }

    public void getSimulatorData(){
        DatosPruebaUsuario datosPruebaUsuario = new DatosPruebaUsuario();
        string request = JsonUtility.ToJson(datosPruebaUsuario);       
        #if UNITY_EDITOR
        print(request);
        #endif
        apiController.GETJSON(apiController.UrlBase, "get/simulador", request, OnCompleted, OnError);   
    } 

    public void getSimulatorDataPlayer(){
        DatosPruebaUsuario datosPruebaUsuario = new DatosPruebaUsuario();
        string request = JsonUtility.ToJson(datosPruebaUsuario);       
        #if UNITY_EDITOR
        print(request);
        #endif
        apiController.GETJSON(apiController.UrlBase, "get/simulador/phys", request, OnCompleted, OnError);   
    }    

    public void catchPlayer(string player){
        Debug.Log(player);
        string data = JsonUtility.ToJson(player);
    }

	public void OnCompleted(WWW response){
        //print("INICIO DE INFORME");
        String dataPlayerJsonGet = JsonUtility.ToJson(response);
        Debug.Log(dataPlayerJsonGet);
        print(JsonUtility.ToJson(response));
        print(response.text);
        string playerResponse = response.text;
        catchPlayer(playerResponse);
        //PdfInfo pdfInfo = JsonUtility.FromJson<PdfInfo>(response.text);
        //Información del informe para guardado en la API
        if (index >= slides.Length - 1)
        {
            //informe.SimulacionTerminada1 = true;
        }
        else {
            //informe.SimulacionTerminada1 = false;
        }
        //informe.InformeFinal1 = pdfInfo.Data.LocationPdf;
        //informe.NombreArchivo1 = pdfInfo.Filename;
                
        //DowloadResult(8);           //Envío de informe a la API
        /*
		#if UNITY_WEBGL
		    apiController.OpenUrlInWeb(pdfInfo.Data.LocationPdf);
		#else
		    Application.OpenURL (pdfInfo.Data.LocationPdf);
		#endif*/
        if(index == slides.Length)
        {
            postOk.Invoke();
        }
	}

	public void OnDBCompleted(WWW response){
		print("POST OK");
	}

	public void OnError(string error){
		print (error);
	}

    public void disableKids(GameObject papa){
        int sons = papa.transform.childCount;
        for (int i = 0; i < sons; i++){
            papa.transform.GetChild(i).gameObject.SetActive(false);
        }
    }
    public void checkZonas(){
        if(Ficha1Fisicos.areasEmpCompletas > 7){
            Ficha1Fisicos.addToBigM();
            baseHandWrite.StartStory(9);
        }
    }
    public void pasarFicha4(){
        if(Ficha3Physical.canPassF4){
            baseHandWrite.StartStory(19);
        }
    }
    public void finishTuto(GameObject tuto){
        /*if(!tutoMedidores){
            tuto.gameObject.SetActive(true);
        }
        tutoMedidores = true;*/
        tuto.gameObject.SetActive(true);
    }
    public void pasarIntro(GameObject canvasIn){
        if(Ficha4PhysicalRisk.toIntro && Ficha1Fisicos.empresasCompletadas < 2){
            canvasIn.transform.Find("Commons").Find("Content").Find("Ficha4").gameObject.SetActive(false);
            canvasIn.transform.Find("Commons").gameObject.SetActive(false);
            canvasIn.transform.Find("Introduction").gameObject.SetActive(true);
            canvasIn.transform.Find("Introduction").Find("Content").Find("BernardFisicosIntro").Find("Tooltip").gameObject.SetActive(true);
            canvasIn.transform.Find("Business").gameObject.SetActive(true);
            ActivateBusiness();
        }
    }
    public void finalBernardIntro(GameObject bernard){
        if(Ficha4PhysicalRisk.toIntro && Ficha1Fisicos.empresasCompletadas < 2){
            bernard.transform.GetComponent<FmodHandWriting>().StartStory(1);
        }
    }
    public void changeColor(Text text, Color c){
        /*float r = 255;
        float g = 255;
        float b = 255;
        c = new Color(r,g,b);*/
        text.color = c;
    }
    public static void printDataPlayer(){
        //Debug.Log(JsonUtility.ToJson(dataPlayer));
    }
}
