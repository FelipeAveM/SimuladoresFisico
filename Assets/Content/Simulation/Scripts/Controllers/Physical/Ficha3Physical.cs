using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System;

public class Ficha3Physical : MonoBehaviour {
	private AlertMessage alertMessage;
	public Transform alert;
    public static List<int []> controlesIndex = new List<int[]>();
    public static bool canPassF4 = false;
    public static MatrizMedidores [] matVar;
    public static List<MatrizMedidores[]> listaFinalMedidores3 = new List<MatrizMedidores[]>();
    public static List<MatrizMedidores[]> listaFinalMedidores3b = new List<MatrizMedidores[]>();
    public void Start(){
		alertMessage = alert.GetComponent<AlertMessage>();
        if(FlowControllerPhysical.dataPlayer.getFichaActual() == 2) FlowControllerPhysical.dataPlayer.setFichaActual(3);
        if(FlowControllerPhysical.dataPlayer.getFichaActual() == 6) FlowControllerPhysical.dataPlayer.setFichaActual(7);
        if(FlowControllerPhysical.dataPlayer.getFichaActual() == 3) matVar = FlowControllerPhysical.data.getListaFinalMedidores()[0];
        else matVar = FlowControllerPhysical.data.getListaFinalMedidores()[1];
        LoadInfo();
    }
    public void StarForcedF3(){
       if(FlowControllerPhysical.dataPlayer.getFichaActual() > 3){
           Debug.Log("Start F3 Forced");
           Start();
       } 
    }
    void LoadInfo(){
        for (int j = 0; j < 8; j++){
            //Debug.Log(JsonUtility.ToJson(matVar[j]));
        }
        if(Ficha1Fisicos.empresaSeleccionada == 2){
            Transform tablaFicha3 = transform.Find("Scroll").Find("TableEvaluation");         
            for (int i = 0; i < tablaFicha3.GetChildCount(); i++){
                tablaFicha3.GetChild(i).GetChild(8).gameObject.SetActive(false); 
            }
            tablaFicha3.GetChild(0).GetChild(7).Find("Controles (1)").gameObject.SetActive(true); 
        }
        Transform header = transform.Find("Scroll").Find("TableEvaluation").Find("RowHeaders");
        for (int i = 1; i < header.GetChildCount(); i++){
            Text area = header.GetChild(i).GetChild(0).GetComponent<Text>();
            if(matVar.Length > 7){
                area.text = matVar[i-1].getArea().ToUpper();
            }
        }
        Transform data = transform.Find("Scroll").Find("TableEvaluation");
        for (int i = 1; i < data.GetChildCount(); i++){
            int dataSon = data.GetChild(i).GetChildCount();
            for (int j = 1; j < dataSon; j++){
                InputField texto = data.GetChild(i).GetChild(j).GetChild(0).GetComponent<InputField>();
                switch (i){
                    case 1:
                        texto.text = matVar[j-1].getNumPersonas().ToString();
                        texto.interactable = false;
                        break;
                    case 2:
                        texto.text = matVar[j-1].getHorasTrabajadas().ToString();
                        texto.interactable = false;
                        break;
                    case 3:
                        texto.text = matVar[j-1].getLuxometro();
                        texto.interactable = false;
                        break;
                    case 4:
                        if(matVar[j-1].getRiesgo()){
                            texto.text = "";
                            int [] indices = new int[2];
                            indices[0] = i;
                            indices[1] = j;
                            controlesIndex.Add(indices);
                            texto.characterLimit = 1000;
                        } 
                        else{
                            texto.text = "NO";
                            texto.interactable = false;
                        } 
                        break;
                    case 5:
                        if(matVar[j-1].getRutinaria()){
                            texto.text = "SÍ";
                            texto.interactable = false;
                        }
                        else{
                            texto.text = "NO";
                            texto.interactable = false;
                        }
                        break;
                    case 6:
                        texto.text = matVar[j-1].getSonometro();
                        texto.interactable = false;
                        break;
                        
                    case 7:
                        if(Ficha1Fisicos.empresaSeleccionada == 2 && j == 8){
                            break;
                        }
                        else{
                            double perso = Convert.ToDouble(matVar[j-1].getNumPersonas());
                            char [] ruido = matVar[j-1].getSonometro().ToCharArray();
                            char [] sonido = new char [ruido.Length];
                            for (int index = 0; index < sonido.Length; index++){
                                sonido[index] = ruido[index];
                            }
                            string cantidadRuido = new string(sonido);
                            double cRuido = Convert.ToDouble(cantidadRuido);
                            texto.text = (cRuido +(0.5*perso)).ToString();
                            //
                            string cantRuido = texto.text;
                            matVar[j-1].setCantidadRuido(cantRuido);
                        }
                        texto.interactable = false;
                        break;
                        
                    case 8:
                        if(matVar[j-1].getRiesgoSono()){
                            texto.text = "";
                            int [] indices = new int[2];
                            indices[0] = i;
                            indices[1] = j;
                            controlesIndex.Add(indices);
                            texto.characterLimit = 1000;
                        } 
                        else{
                            texto.text = "NO";
                            texto.interactable = false;
                        } 
                        break;
                    case 9:
                        if(matVar[j-1].getRutinariaSono()) texto.text = "SÍ";
                        else texto.text = "NO";
                        texto.interactable = false;
                        break;
                    case 10:
                        texto.text = matVar[j-1].getDosimetro();
                        texto.interactable = false;
                        break;
                    case 11:
                        if(matVar[j-1].getEsIonizante()) texto.text = "SÍ";
                        else{
                            texto.text = "NO";
                        }
                        texto.interactable = false;
                        break;
                    case 12:
                        if(matVar[j-1].getRiesgoDosi()) {
                            texto.text = "";
                            int [] indices = new int[2];
                            indices[0] = i;
                            indices[1] = j;
                            controlesIndex.Add(indices);
                            texto.characterLimit = 1000;
                        }
                        else{
                            texto.text = "NO";
                            texto.interactable = false;
                        }
                        break;
                    case 13:
                        if(matVar[j-1].getRutinariaDosi()) texto.text = "SÍ";
                        else{
                            texto.text = "NO";
                        }
                        texto.interactable = false;
                        break;
                    case 14:
                        texto.text = matVar[j-1].getTermometro();
                        texto.interactable = false;
                        break;
                    case 15:
                        if(Ficha1Fisicos.empresaSeleccionada == 2 && j == 8){
                            break;
                        }
                        else{
                            int personas = matVar[j-1].getNumPersonas();
                            int tempera = Int32.Parse(matVar[j-1].getTermometro());
                            texto.text = (tempera +personas).ToString();
                            //
                            string tempArea = texto.text;
                            matVar[j-1].setTemperaturaArea(tempArea);
                        }
                        texto.interactable = false; 
                        break;
                    case 16:
                        if(matVar[j-1].getRiesgoTermo()){
                            texto.text = "";
                            int [] indices = new int[2];
                            indices[0] = i;
                            indices[1] = j;
                            controlesIndex.Add(indices);
                            texto.characterLimit = 1000;
                        } 
                        else{
                            texto.text = "NO";
                            texto.interactable = false;
                        }
                        break;
                    case 17:
                        if(matVar[j-1].getRutinariaTermo()) texto.text = "SÍ";
                        else{
                            texto.text = "NO";
                        }
                        texto.interactable = false;
                        break;
                    case 18:
                        texto.text = matVar[j-1].getVibrometro();
                        texto.interactable = false;
                        break;
                    case 19:
                        if(matVar[j-1].getVibraManoBrazo()) texto.text = "SÍ";
                        else{
                            texto.text = "NO";
                        }
                        texto.interactable = false;
                        break;
                    case 20:
                        if(matVar[j-1].getVibraCuerpo()) texto.text = "SÍ";
                        else{
                            texto.text = "NO";
                        }
                        texto.interactable = false;
                        break;
                    case 21:
                        if(matVar[j-1].getRiesgoVibro()){
                            texto.text = "";
                            int [] indices = new int[2];
                            indices[0] = i;
                            indices[1] = j;
                            controlesIndex.Add(indices);
                            texto.characterLimit = 1000;
                        } 
                        else{
                            texto.text = "NO";
                            texto.interactable = false;
                        }
                        break;
                    case 22:
                        if(matVar[j-1].getRutinariaVibro()) texto.text = "SÍ";
                        else{
                            texto.text = "NO";
                        }
                        texto.interactable = false;
                        break;
                    default:
                        texto.text = "";
                        texto.interactable = false;
                        break;
                }
            } 
        }
    }
    public void StartForced(){
        if(Ficha1Fisicos.empresasCompletadas > 1){
            Start();
        }
    }
    public void Save(GameObject gat){
        string errorMessage = "";
        bool isFull = true;
        Transform data = transform.Find("Scroll").Find("TableEvaluation");
        if(Ficha1Fisicos.empresaSeleccionada == 2){
            for (int i = 1; i < data.GetChildCount(); i++){
                int dataSon = data.GetChild(i).GetChildCount()-1;
                for (int j = 1; j < dataSon; j++){
                    InputField texto = data.GetChild(i).GetChild(j).GetChild(0).GetComponent<InputField>();
                    if(texto.text == ""){
                        errorMessage = "Para continuar debes diligenciar toda la información solicitada.";
                        isFull = false;
                    }
                    else continue;
                }
            }
        }
        else{
            for (int i = 1; i < data.GetChildCount(); i++){
                int dataSon = data.GetChild(i).GetChildCount();
                for (int j = 1; j < dataSon; j++){
                    InputField texto = data.GetChild(i).GetChild(j).GetChild(0).GetComponent<InputField>();
                    if(texto.text == ""){
                        errorMessage = "Para continuar debes diligenciar toda la información solicitada.";
                        isFull = false;
                    }
                    else continue;
                }
            }

        }
        if (!errorMessage.Equals("") && !isFull){
            //Debug.Log("Espacios vacios encontrados");
            alert.gameObject.SetActive(true);
            alertMessage.CreateAlertMessage(errorMessage);                    
        }
        else{
            for (int i = 0; i < controlesIndex.Count; i++){
                int [] indices = controlesIndex[i];
                InputField texto = data.GetChild(indices[0]).GetChild(indices[1]).GetChild(0).GetComponent<InputField>();
                //Debug.Log("218) "+ indices[0] + " - " + indices[1]);
                switch (indices[0]){
                    //luxo
                    case 4:
                        matVar[indices[1]-1].setControlRiesgoLux(texto.text);
                        break;
                    //Sono
                    case 8:
                        matVar[indices[1]-1].setControlRiesgoSono(texto.text);
                        break;
                    //Dosi
                    case 12:
                        matVar[indices[1]-1].setControlRiesgoDosi(texto.text);
                        break;
                    //Termo
                    case 16:
                        matVar[indices[1]-1].setControlRiesgoTermo(texto.text);
                        break;
                    //Vibro
                    case 21:
                        matVar[indices[1]-1].setControlRiesgoVibro(texto.text);
                        break;
                    default:
                        break;
                }   
            }
            //Debug.Log("Todos los campos llenos.");
            for (int i = 1; i < data.GetChildCount(); i++){
            int dataSon = data.GetChild(i).GetChildCount();
            for (int j = 1; j < dataSon; j++){
                InputField texto = data.GetChild(i).GetChild(j).GetChild(0).GetComponent<InputField>();
                texto.text = "";
                texto.interactable = true;
                }
            }
            canPassF4 = true;
            if(Ficha1Fisicos.empresasCompletadas == 1){
                Debug.Log("matVar2");
                MatrizMedidores [] matVar2 = new MatrizMedidores[8];
                for (int i = 0; i < matVar.Length; i++){
                    matVar2[i] = matVar[i];
                }
                listaFinalMedidores3.Add(matVar2);
                FlowControllerPhysical.data.setListaFinalMedidores(listaFinalMedidores3);
            }
            else{
                listaFinalMedidores3.Add(matVar);
                FlowControllerPhysical.data.setListaFinalMedidores(listaFinalMedidores3);
            }
            
            Debug.Log("Matriz datos tamaño: " + listaFinalMedidores3.Count);
            for (int i = 0; i < listaFinalMedidores3.Count; i++){
                for (int j = 0; j < listaFinalMedidores3[i].Length; j++){
                    Debug.Log(JsonUtility.ToJson(listaFinalMedidores3[i][j]));
                }
            }
            if(Ficha1Fisicos.empresaSeleccionada == 2) Reboot();
            gat.transform.Find("Ficha3").gameObject.SetActive(false);
            gat.transform.Find("Ficha4").gameObject.SetActive(true);
        }
    }
    public void Reboot(){
        Transform tablaFicha3 = transform.Find("Scroll").Find("TableEvaluation");         
            for (int i = 0; i < tablaFicha3.GetChildCount(); i++){
                tablaFicha3.GetChild(i).GetChild(8).gameObject.SetActive(true); 
            }
            tablaFicha3.GetChild(0).GetChild(7).Find("Controles (1)").gameObject.SetActive(false);
    }
    public void DestroyF4(GameObject gameObject){
        if(FlowControllerPhysical.dataPlayer.getFichaActual() == 3){
            //UnityEditor.PrefabUtility.ResetToPrefabState(this.gameObject);
        }
    }
}
