using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System;
using System.Linq;
//using System.Diagnostics;
using UnityEngine.SceneManagement;
public class Ficha1Fisicos : MonoBehaviour
{
    private AlertMessage alertMessage;

    public UnityEvent saveCard;
    public DataBuilderPhysical dataBuilder;
    public FlowControllerPhysical flowController;
    public Transform alert;
    [System.NonSerialized] public static MatrizMedidores [] matrizMedidores = new MatrizMedidores[8];
    [System.NonSerialized] public static MatrizMedidores [][] matrizFinalMedidores = new MatrizMedidores[3][];
    [System.NonSerialized] public static List<MatrizMedidores[]> listaFinalMedidores = FlowControllerPhysical.data.listaFinalMedidores;
    [System.NonSerialized] public static List<MatrizMedidores[]> listaFinalMedidoresPrueba = new List<MatrizMedidores[]>();
    [System.NonSerialized] public static int empresasCompletadas = 0;
    [System.NonSerialized] public string [] datosMatriz = new string[20];
    private Transform businessMapParent;
    public bool isMapVisible;
    [System.NonSerialized] public string [] mediciones;
    [System.NonSerialized] public int areaActual = 0;
    public FmodHandWriting baseHandWrite;
    [System.NonSerialized] public static int empresaSeleccionada = 0;
    [System.NonSerialized] public static int areasEmpCompletas = 0;
    [System.NonSerialized] public static bool guardoAreaF1 = false;
    public bool estadoCompletado = false;
    [System.NonSerialized] public static bool cargaEmp1 = false;
    public void Start(){
        listaFinalMedidoresPrueba = FlowControllerPhysical.data.getListaFinalMedidores();
        alertMessage = alert.GetComponent<AlertMessage>();
        businessMapParent = transform.GetChild(0);
        businessMapParent.gameObject.SetActive(true);
        businessMapParent.GetChild(0).GetChild(empresaSeleccionada-1).gameObject.SetActive(true);
        if(FlowControllerPhysical.data.getFichaActual() == 1) FlowControllerPhysical.data.setFichaActual(2);
        if(FlowControllerPhysical.data.getFichaActual() == 5) FlowControllerPhysical.data.setFichaActual(6);
        //if(FlowControllerPhysical.data.getFichaActual() > 2 && FlowControllerPhysical.data.getFichaActual() < 6) 
            //listaFinalMedidores.Add(FlowControllerPhysical.data.getListaFinalMedidores()[0]);
        //if(FlowControllerPhysical.data.getFichaActual() > 6) 
            //listaFinalMedidores.Add(FlowControllerPhysical.data.getListaFinalMedidores()[1]);
        areasEmpCompletas = 0;
        estadoCompletado = false;
        //Pruebas
        Debug.Log("Ficha 1 Ficha Actual: " + FlowControllerPhysical.data.getFichaActual());
        if(FlowControllerPhysical.data.getFichaActual() > 2 && FlowControllerPhysical.data.getFichaActual() != 6){
            Debug.Log("Entró a omitir ficha 1: " + FlowControllerPhysical.data.getFichaActual());
            if(FlowControllerPhysical.data.getFichaActual() > 6) {
                areasEmpCompletas = 8;
                estadoCompletado = true;
                Transform zonasHijas =  transform.Find("BusinessMap").GetChild(FlowControllerPhysical.data.getEmp1()).GetChild(1).GetChild(1);
                disableChilds(zonasHijas.gameObject);
                Transform guardarButon = transform.Find("BusinessMap").GetChild(FlowControllerPhysical.data.getEmp1()).GetChild(3);
                guardarButon.gameObject.SetActive(true);
            }
            else{
                areasEmpCompletas = 8;
                estadoCompletado = true;
                Transform zonasHijas =  transform.Find("BusinessMap").GetChild(FlowControllerPhysical.data.getEmp2()).GetChild(1).GetChild(1);
                disableChilds(zonasHijas.gameObject);
                Transform guardarButon = transform.Find("BusinessMap").GetChild(FlowControllerPhysical.data.getEmp2()).GetChild(3);
                guardarButon.gameObject.SetActive(true);
            }
            
        }
        for (int i = 0; i < businessMapParent.childCount; i++){
            businessMapParent.GetChild(i).gameObject.SetActive(false);
        }
        if (isMapVisible){
            businessMapParent.GetChild(empresaSeleccionada-1).gameObject.SetActive(true);
        }
        mediciones = new string [5];
        doMedicionesLab();
        if(empresaSeleccionada == 2 && (FlowControllerPhysical.data.getFichaActual() == 2 || FlowControllerPhysical.data.getFichaActual() ==6)){
            areasEmpCompletas = 1;
        }
        if(areasEmpCompletas != 8) changeTextSize();
    }

    public void StarForcedF1(){
       if(FlowControllerPhysical.data.getFichaActual() == 5 && !cargaEmp1){
           Debug.Log("Start F1 Forced");
           Start();
       } 
    }

    public void reStart(){
        if(empresasCompletadas > 0){
            Start();
        }
    }
    void Update(){        
    }
    public void ShowMap() {
        businessMapParent.GetChild(flowController.GetActiveBusiness()).gameObject.SetActive(true);
    }
    public void ShowMap(int n){
        businessMapParent.GetChild(n).gameObject.SetActive(true);
    }
    public void setEmpresaSeleccionada(int i){
        empresaSeleccionada = i;
        //Debug.Log(empresaSeleccionada);
    }
    public void datosIniciales(GameObject gameObject){
        MatrizMedidores matrizMedArea = new MatrizMedidores();
        Transform tabla = transform.Find("BusinessMap");
        if(empresaSeleccionada == 1){
            string empresa = "Laboratorio"; 
            matrizMedArea.setEmpresa(empresa);
            string area = gameObject.transform.GetChild(0).GetComponent<Text>().text;
            matrizMedArea.setArea(area);
        }
        else if(empresaSeleccionada == 2){
            string empresa = "Manufactura"; 
            matrizMedArea.setEmpresa(empresa);
            string area = gameObject.transform.GetChild(0).GetComponent<Text>().text;
            matrizMedArea.setArea(area);
        }
        else if(empresaSeleccionada == 3){
            string empresa = "Oficina"; 
            matrizMedArea.setEmpresa(empresa);
            string area = gameObject.transform.GetChild(0).GetComponent<Text>().text;
            matrizMedArea.setArea(area);
        }
        else if(empresaSeleccionada == 4){
            string empresa = "Pozo Petrolero"; 
            matrizMedArea.setEmpresa(empresa);
            string area = gameObject.transform.GetChild(0).GetComponent<Text>().text;
            matrizMedArea.setArea(area);
        }
        else if(empresaSeleccionada == 5){
            string empresa = "Servicios Generales"; 
            matrizMedArea.setEmpresa(empresa);
            string area = gameObject.transform.GetChild(0).GetComponent<Text>().text;
            matrizMedArea.setArea(area);
        }
        string a = gameObject.name;
        areaActual = Int32.Parse(a);
        matrizMedidores[areaActual-1] = matrizMedArea;


    }
    public void reinicarFE(GameObject gameObject){
        string errorMessage = "";
        bool isFull = true;
        
        Transform tabla = gameObject.transform;
        Transform personal = tabla.GetChild(0).GetChild(1);
        InputField textPersonal = personal.GetComponent<InputField>();
        Transform horas = tabla.GetChild(0).GetChild(3);
        InputField textHoras = horas.GetComponent<InputField>();

        if(textPersonal.text == "" || textHoras.text == ""){
            errorMessage = "Para continuar debes diligenciar toda la información solicitada.";
            isFull = false;
        }
        if(!errorMessage.Equals("") && !isFull){
            alert.gameObject.SetActive(true);
            alertMessage.CreateAlertMessage(errorMessage);
        }  
        else {
            Transform tablaRiesgo = transform.Find("BusinessMap");
            Transform tablaFML = null;
            Transform botonGuardarF1 = null; 
            Transform botonesRiesgos = null;
            //Lab
            if(empresaSeleccionada == 1){
                tablaFML = tablaRiesgo.Find("Empresa1").Find("DescZoneLab").Find("FichaMedidoresLab");
                botonesRiesgos = tablaRiesgo.Find("Empresa1").Find("DescZoneLab").Find("BotonesMedidores");
                botonGuardarF1 = tablaRiesgo.Find("Empresa1").Find("GuardarLab");
            }
            //Man
            else if(empresaSeleccionada == 2){
                tablaFML = tablaRiesgo.Find("Empresa2").Find("DescZoneMan").Find("FichaMedidoresMan");
                botonesRiesgos = tablaRiesgo.Find("Empresa2").Find("DescZoneMan").Find("BotonesMedidores");
                botonGuardarF1 = tablaRiesgo.Find("Empresa2").Find("GuardarMan");
            }
            //Ofi
            else if(empresaSeleccionada == 3){
                tablaFML = tablaRiesgo.Find("Empresa3").Find("DescZoneOfi").Find("FichaMedidoresOfi");
                botonesRiesgos = tablaRiesgo.Find("Empresa3").Find("DescZoneOfi").Find("BotonesMedidores");
                botonGuardarF1 = tablaRiesgo.Find("Empresa3").Find("GuardarOfi");
            }
            //Pozo
            else if(empresaSeleccionada == 4){
                tablaFML = tablaRiesgo.Find("Empresa4").Find("DescZonePozo").Find("FichaMedidoresPozo");
                botonesRiesgos = tablaRiesgo.Find("Empresa4").Find("DescZonePozo").Find("BotonesMedidores");
                botonGuardarF1 = tablaRiesgo.Find("Empresa4").Find("GuardarPozo");
            }
            //
            else if(empresaSeleccionada == 5){
                tablaFML = tablaRiesgo.Find("Empresa5").Find("DescZoneSG").Find("FichaMedidoresSG");
                botonesRiesgos = tablaRiesgo.Find("Empresa5").Find("DescZoneSG").Find("BotonesMedidores");
                botonGuardarF1 = tablaRiesgo.Find("Empresa5").Find("GuardarSG");
            }
            Transform row1 = tablaFML.GetChild(0);
            InputField textPersonas = row1.GetChild(1).GetChild(0).GetComponent<InputField>();
            textPersonas.text = textPersonal.text;
            Transform row2 = tablaFML.GetChild(1);
            InputField textHorasTra = row2.GetChild(1).GetChild(0).GetComponent<InputField>();
            textHorasTra.text = textHoras.text;
            textPersonal.text = "";
            textHoras.text = "";
            gameObject.SetActive(false);
            botonesRiesgos.gameObject.SetActive(true);
            botonGuardarF1.gameObject.SetActive(true);
        }
        changeTextSize();
    }
    public string randomMedi(int a){
        System.Random randomer = new System.Random();
        string random = "0";
        switch (a){
            case 1:
            //Luxómetro
                random = randomer.Next(1,2000).ToString();
                break;
            case 2:
            //Sonómetro
                random = randomer.Next(1,500).ToString() + "dB";
                break;
            case 3:
            //Dosímetro
                random = randomer.Next(1,200).ToString() + "msv";
                break;
            case 4:
            //Termómetro
                random = randomer.Next(1,50).ToString() + "°C";
                break;
            case 5:
            //Vibrómetro
                random = randomer.Next(1,20).ToString() + "m/s2";
                break;
            default: 
                random = "100";
                break;
        }
        return random;
    }
    public void changeValorMedidor(int i){
        switch (empresaSeleccionada){
            case 1:
                Transform tabla = transform.Find("BusinessMap").Find("Empresa1").Find("DescZoneLab").Find("Medidores");
                Text texto = tabla.GetChild(i-1).GetChild(0).GetComponent<Text>();
                texto.text = mediciones[i-1];
                /*
                if(i == 2){
                    Transform tabMediciones = transform.Find("BusinessMap").Find("Empresa1").Find("DescZoneLab").Find("FichaMedidoresLab");
                    InputField cantRuido = tabMediciones.GetChild(6).GetChild(1).GetChild(0).GetComponent<InputField>();
                    InputField cantPersonas = tabMediciones.GetChild(0).GetChild(1).GetChild(0).GetComponent<InputField>();
                    double perso = Convert.ToDouble(cantPersonas.text);
                    char [] ruido = mediciones[i-1].ToCharArray();
                    char [] sonido = new char [ruido.Length-2];
                    for (int index = 0; index < sonido.Length; index++){
                        sonido[index] = ruido[index];
                    }
                    string cantidadRuido = new string(sonido);
                    double cRuido = Convert.ToDouble(cantidadRuido);
                    cantRuido.text = (cRuido +(0.5*perso)).ToString();
                }
                if(i == 4){
                    Transform tabMediciones = transform.Find("BusinessMap").Find("Empresa1").Find("DescZoneLab").Find("FichaMedidoresLab");
                    InputField cantRuido = tabMediciones.GetChild(14).GetChild(1).GetChild(0).GetComponent<InputField>();
                    InputField cantPersonas = tabMediciones.GetChild(0).GetChild(1).GetChild(0).GetComponent<InputField>();
                    double perso = Convert.ToDouble(cantPersonas.text);
                    char [] ruido = mediciones[i-1].ToCharArray();
                    char [] sonido = new char [ruido.Length-2];
                    for (int index = 0; index < sonido.Length; index++){
                        sonido[index] = ruido[index];
                    }
                    string cantidadRuido = new string(sonido);
                    double cRuido = Convert.ToDouble(cantidadRuido);
                    cantRuido.text = (cRuido +perso).ToString();
                }
                */
                break;
            case 2:
                Transform tablaM = transform.Find("BusinessMap").Find("Empresa2").Find("DescZoneMan").Find("Medidores");
                Text textoM = tablaM.GetChild(i-1).GetChild(0).GetComponent<Text>();
                textoM.text = mediciones[i-1];
                /*
                if(i == 2){
                    Transform tabMediciones = transform.Find("BusinessMap").Find("Empresa2").Find("DescZoneMan").Find("FichaMedidoresMan");
                    InputField cantRuido = tabMediciones.GetChild(6).GetChild(1).GetChild(0).GetComponent<InputField>();
                    InputField cantPersonas = tabMediciones.GetChild(0).GetChild(1).GetChild(0).GetComponent<InputField>();
                    double perso = Convert.ToDouble(cantPersonas.text);
                    char [] ruido = mediciones[i-1].ToCharArray();
                    char [] sonido = new char [ruido.Length-2];
                    for (int index = 0; index < sonido.Length; index++){
                        sonido[index] = ruido[index];
                    }
                    string cantidadRuido = new string(sonido);
                    double cRuido = Convert.ToDouble(cantidadRuido);
                    cantRuido.text = (cRuido +(0.5*perso)).ToString();
                }
                if(i == 4){
                    Transform tabMediciones = transform.Find("BusinessMap").Find("Empresa2").Find("DescZoneMan").Find("FichaMedidoresMan");
                    InputField cantRuido = tabMediciones.GetChild(14).GetChild(1).GetChild(0).GetComponent<InputField>();
                    InputField cantPersonas = tabMediciones.GetChild(0).GetChild(1).GetChild(0).GetComponent<InputField>();
                    double perso = Convert.ToDouble(cantPersonas.text);
                    char [] ruido = mediciones[i-1].ToCharArray();
                    char [] sonido = new char [ruido.Length-2];
                    for (int index = 0; index < sonido.Length; index++){
                        sonido[index] = ruido[index];
                    }
                    string cantidadRuido = new string(sonido);
                    double cRuido = Convert.ToDouble(cantidadRuido);
                    cantRuido.text = (cRuido +perso).ToString();
                }
                */
                break;
            case 3:
                Transform tablaO = transform.Find("BusinessMap").Find("Empresa3").Find("DescZoneOfi").Find("Medidores");
                Text textoO = tablaO.GetChild(i-1).GetChild(0).GetComponent<Text>();
                textoO.text = mediciones[i-1];
                /*
                if(i == 2){
                    Transform tabMediciones = transform.Find("BusinessMap").Find("Empresa3").Find("DescZoneOfi").Find("FichaMedidoresOfi");
                    InputField cantRuido = tabMediciones.GetChild(6).GetChild(1).GetChild(0).GetComponent<InputField>();
                    InputField cantPersonas = tabMediciones.GetChild(0).GetChild(1).GetChild(0).GetComponent<InputField>();
                    double perso = Convert.ToDouble(cantPersonas.text);
                    char [] ruido = mediciones[i-1].ToCharArray();
                    char [] sonido = new char [ruido.Length-2];
                    for (int index = 0; index < sonido.Length; index++){
                        sonido[index] = ruido[index];
                    }
                    string cantidadRuido = new string(sonido);
                    double cRuido = Convert.ToDouble(cantidadRuido);
                    cantRuido.text = (cRuido +(0.5*perso)).ToString();
                }
                if(i == 4){
                    Transform tabMediciones = transform.Find("BusinessMap").Find("Empresa3").Find("DescZoneOfi").Find("FichaMedidoresOfi");
                    InputField cantRuido = tabMediciones.GetChild(14).GetChild(1).GetChild(0).GetComponent<InputField>();
                    InputField cantPersonas = tabMediciones.GetChild(0).GetChild(1).GetChild(0).GetComponent<InputField>();
                    double perso = Convert.ToDouble(cantPersonas.text);
                    char [] ruido = mediciones[i-1].ToCharArray();
                    char [] sonido = new char [ruido.Length-2];
                    for (int index = 0; index < sonido.Length; index++){
                        sonido[index] = ruido[index];
                    }
                    string cantidadRuido = new string(sonido);
                    double cRuido = Convert.ToDouble(cantidadRuido);
                    cantRuido.text = (cRuido +perso).ToString();
                }
                */
                break;
            case 4:
                Transform tablaP = transform.Find("BusinessMap").Find("Empresa4").Find("DescZonePozo").Find("Medidores");
                Text textoP = tablaP.GetChild(i-1).GetChild(0).GetComponent<Text>();
                textoP.text = mediciones[i-1];
                /*
                if(i == 2){
                    Transform tabMediciones = transform.Find("BusinessMap").Find("Empresa4").Find("DescZonePozo").Find("FichaMedidoresPozo");
                    InputField cantRuido = tabMediciones.GetChild(6).GetChild(1).GetChild(0).GetComponent<InputField>();
                    InputField cantPersonas = tabMediciones.GetChild(0).GetChild(1).GetChild(0).GetComponent<InputField>();
                    double perso = Convert.ToDouble(cantPersonas.text);
                    char [] ruido = mediciones[i-1].ToCharArray();
                    char [] sonido = new char [ruido.Length-2];
                    for (int index = 0; index < sonido.Length; index++){
                        sonido[index] = ruido[index];
                    }
                    string cantidadRuido = new string(sonido);
                    double cRuido = Convert.ToDouble(cantidadRuido);
                    cantRuido.text = (cRuido +(0.5*perso)).ToString();
                }
                if(i == 4){
                    Transform tabMediciones = transform.Find("BusinessMap").Find("Empresa4").Find("DescZonePozo").Find("FichaMedidoresPozo");
                    InputField cantRuido = tabMediciones.GetChild(14).GetChild(1).GetChild(0).GetComponent<InputField>();
                    InputField cantPersonas = tabMediciones.GetChild(0).GetChild(1).GetChild(0).GetComponent<InputField>();
                    double perso = Convert.ToDouble(cantPersonas.text);
                    char [] ruido = mediciones[i-1].ToCharArray();
                    char [] sonido = new char [ruido.Length-2];
                    for (int index = 0; index < sonido.Length; index++){
                        sonido[index] = ruido[index];
                    }
                    string cantidadRuido = new string(sonido);
                    double cRuido = Convert.ToDouble(cantidadRuido);
                    cantRuido.text = (cRuido +perso).ToString();
                }
                */
                break;
            case 5:
                Transform tablaS = transform.Find("BusinessMap").Find("Empresa5").Find("DescZoneSG").Find("Medidores");
                Text textoS = tablaS.GetChild(i-1).GetChild(0).GetComponent<Text>();
                textoS.text = mediciones[i-1];
                /*
                if(i == 2){
                    Transform tabMediciones = transform.Find("BusinessMap").Find("Empresa5").Find("DescZoneSG").Find("FichaMedidoresSG");
                    InputField cantRuido = tabMediciones.GetChild(6).GetChild(1).GetChild(0).GetComponent<InputField>();
                    InputField cantPersonas = tabMediciones.GetChild(0).GetChild(1).GetChild(0).GetComponent<InputField>();
                    double perso = Convert.ToDouble(cantPersonas.text);
                    char [] ruido = mediciones[i-1].ToCharArray();
                    char [] sonido = new char [ruido.Length-2];
                    for (int index = 0; index < sonido.Length; index++){
                        sonido[index] = ruido[index];
                    }
                    string cantidadRuido = new string(sonido);
                    double cRuido = Convert.ToDouble(cantidadRuido);
                    cantRuido.text = (cRuido +(0.5*perso)).ToString();
                }
                if(i == 4){
                    Transform tabMediciones = transform.Find("BusinessMap").Find("Empresa5").Find("DescZoneSG").Find("FichaMedidoresSG");
                    InputField cantRuido = tabMediciones.GetChild(14).GetChild(1).GetChild(0).GetComponent<InputField>();
                    InputField cantPersonas = tabMediciones.GetChild(0).GetChild(1).GetChild(0).GetComponent<InputField>();
                    double perso = Convert.ToDouble(cantPersonas.text);
                    char [] ruido = mediciones[i-1].ToCharArray();
                    char [] sonido = new char [ruido.Length-2];
                    for (int index = 0; index < sonido.Length; index++){
                        sonido[index] = ruido[index];
                    }
                    string cantidadRuido = new string(sonido);
                    double cRuido = Convert.ToDouble(cantidadRuido);
                    cantRuido.text = (cRuido +perso).ToString();
                }
                */
                break;
            default:
                break;
        }
    }
    public void resetCampos(){
        //Debug.Log("resetCampos método.");
    }
    public void reinicarFM(){
        Transform tabla;
        switch (empresaSeleccionada){
            case 1:
                tabla = transform.Find("BusinessMap").Find("Empresa1").Find("DescZoneLab").Find("FichaMedidoresLab");
                break;
            case 2:
                tabla = transform.Find("BusinessMap").Find("Empresa2").Find("DescZoneMan").Find("FichaMedidoresMan");
                break;
            case 3:
                tabla = transform.Find("BusinessMap").Find("Empresa3").Find("DescZoneOfi").Find("FichaMedidoresOfi");
                break;
            case 4:
                tabla = transform.Find("BusinessMap").Find("Empresa4").Find("DescZonePozo").Find("FichaMedidoresPozo");
                break;
            case 5:
                tabla = transform.Find("BusinessMap").Find("Empresa5").Find("DescZoneSG").Find("FichaMedidoresSG");
                break;
            default:
                tabla = transform.Find("BusinessMap").Find("Empresa1").Find("DescZoneLab").Find("FichaMedidoresLab");
                break;
        }


        for (int i = 0; i < tabla.childCount; i++){
            Transform row = tabla.GetChild(i);
            InputField text = row.GetChild(1).GetChild(0).GetComponent<InputField>();
            //Debug.Log(text.text);
        }
    }
    public void changeTextSize(){
        Transform tablaRiesgo = transform.Find("BusinessMap");
        Transform tablaFML = null;
        //Lab
        if(empresaSeleccionada == 1){
            tablaFML = tablaRiesgo.Find("Empresa1").Find("DescZoneLab").Find("FichaMedidoresLab");
        }
        //Man
        else if(empresaSeleccionada == 2){
            tablaFML = tablaRiesgo.Find("Empresa2").Find("DescZoneMan").Find("FichaMedidoresMan");
        }
        //Ofi
        else if(empresaSeleccionada == 3){
            tablaFML = tablaRiesgo.Find("Empresa3").Find("DescZoneOfi").Find("FichaMedidoresOfi");
        }
        //Pozo
        else if(empresaSeleccionada == 4){
            tablaFML = tablaRiesgo.Find("Empresa4").Find("DescZonePozo").Find("FichaMedidoresPozo");
        }
        //
        else if(empresaSeleccionada == 5){
            tablaFML = tablaRiesgo.Find("Empresa5").Find("DescZoneSG").Find("FichaMedidoresSG");
        }
        Transform celdaTexto = null;
        Transform celdaPlaceholder = null;
        Transform celdaVTexto = null;
        for (int i = 0; i < tablaFML.GetChildCount(); i++){
            for (int j = 0; j < tablaFML.GetChild(i).GetChildCount(); j++){
                celdaTexto = tablaFML.GetChild(i).GetChild(0).GetChild(0);
                Text textoCeldaTx = celdaTexto.GetComponent<Text>();
                textoCeldaTx.fontSize = 12;
                if(j == 1){
                    if(i == 0 || i == 1 || i == 2 || i == 5 || i == 8 || i == 12 || i == 15){
                        celdaPlaceholder = tablaFML.GetChild(i).GetChild(1).Find("Value").Find("Placeholder");
                        celdaVTexto = tablaFML.GetChild(i).GetChild(1).Find("Value").Find("Text");
                        Text textoCeldaPh = celdaPlaceholder.GetComponent<Text>();
                        Text textoCeldaVT = celdaVTexto.GetComponent<Text>();
                        textoCeldaPh.fontSize = 12;
                        textoCeldaVT.fontSize = 12;
                    }
                }
                
            }
        }

    }
    public void guardarTablaMedidores(GameObject tablaMedidores){
        Debug.Log("-----------------------------------------------------------");
        Debug.Log("Empresas completadas: " + empresasCompletadas);
        Debug.Log("Areas completadas: " + areasEmpCompletas);
        Debug.Log("Estado completado: " + estadoCompletado);
        if(FlowControllerPhysical.data.getFichaActual() == 2 || FlowControllerPhysical.data.getFichaActual() == 6){
            if(!estadoCompletado){
                string errorMessage = "";
                bool isFull = true;
                Transform tablaRiesgo = transform.Find("BusinessMap");
                Transform tablaFE = null;
                Transform botonFE = null; 
                Transform botonesMedidores = null;
                Transform botonesRiesgos = null;
                Transform zona = null;
                Transform botonGuardarF1 = null;
                //Lab
                if(empresaSeleccionada == 1){
                    tablaFE = tablaRiesgo.Find("Empresa1").Find("DescZoneLab").Find("FichaEmpleados");
                    botonesMedidores = tablaRiesgo.Find("Empresa1").Find("DescZoneLab").Find("Medidores");
                    botonesRiesgos = tablaRiesgo.Find("Empresa1").Find("DescZoneLab").Find("BotonesMedidores");
                    botonFE = tablaRiesgo.Find("Empresa1").Find("DescZoneLab").Find("FichaEmpleados").Find("Buttons").Find("Save").Find("btn");
                    zona = tablaRiesgo.Find("Empresa1").Find("DescZoneLab");
                    botonGuardarF1 = tablaRiesgo.Find("Empresa1").Find("GuardarLab");

                }
                //Man
                else if(empresaSeleccionada == 2){
                    tablaFE = tablaRiesgo.Find("Empresa2").Find("DescZoneMan").Find("FichaEmpleados");
                    botonesMedidores = tablaRiesgo.Find("Empresa2").Find("DescZoneMan").Find("Medidores");
                    botonesRiesgos = tablaRiesgo.Find("Empresa2").Find("DescZoneMan").Find("BotonesMedidores");
                    botonFE = tablaRiesgo.Find("Empresa2").Find("DescZoneMan").Find("FichaEmpleados").Find("Buttons").Find("Save").Find("btn");
                    zona = tablaRiesgo.Find("Empresa2").Find("DescZoneMan");
                    botonGuardarF1 = tablaRiesgo.Find("Empresa2").Find("GuardarMan");
                }
                //Ofi
                else if(empresaSeleccionada == 3){
                    tablaFE = tablaRiesgo.Find("Empresa3").Find("DescZoneOfi").Find("FichaEmpleados");
                    botonesMedidores = tablaRiesgo.Find("Empresa3").Find("DescZoneOfi").Find("Medidores");
                    botonesRiesgos = tablaRiesgo.Find("Empresa3").Find("DescZoneOfi").Find("BotonesMedidores");
                    botonFE = tablaRiesgo.Find("Empresa3").Find("DescZoneOfi").Find("FichaEmpleados").Find("Buttons").Find("Save").Find("btn");
                    zona = tablaRiesgo.Find("Empresa3").Find("DescZoneOfi");
                    botonGuardarF1 = tablaRiesgo.Find("Empresa3").Find("GuardarOfi");
                }
                //Pozo
                else if(empresaSeleccionada == 4){
                    tablaFE = tablaRiesgo.Find("Empresa4").Find("DescZonePozo").Find("FichaEmpleados");
                    botonesMedidores = tablaRiesgo.Find("Empresa4").Find("DescZonePozo").Find("Medidores");
                    botonesRiesgos = tablaRiesgo.Find("Empresa4").Find("DescZonePozo").Find("BotonesMedidores");
                    botonFE = tablaRiesgo.Find("Empresa4").Find("DescZonePozo").Find("FichaEmpleados").Find("Buttons").Find("Save").Find("btn");
                    zona = tablaRiesgo.Find("Empresa4").Find("DescZonePozo");
                    botonGuardarF1 = tablaRiesgo.Find("Empresa4").Find("GuardarPozo");
                }
                //SG
                else if(empresaSeleccionada == 5){
                    tablaFE = tablaRiesgo.Find("Empresa5").Find("DescZoneSG").Find("FichaEmpleados");
                    botonesMedidores = tablaRiesgo.Find("Empresa5").Find("DescZoneSG").Find("Medidores");
                    botonesRiesgos = tablaRiesgo.Find("Empresa5").Find("DescZoneSG").Find("BotonesMedidores");
                    botonFE = tablaRiesgo.Find("Empresa5").Find("DescZoneSG").Find("FichaEmpleados").Find("Buttons").Find("Save").Find("btn");
                    zona = tablaRiesgo.Find("Empresa5").Find("DescZoneSG");
                    botonGuardarF1 = tablaRiesgo.Find("Empresa5").Find("GuardarSG");
                }
                Transform tablaMed = tablaMedidores.transform;
                for (int i = 0; i < tablaMedidores.transform.GetChildCount(); i++){
                    //0,1,2,5,8,12,15
                    if(i == 0 || i == 1 || i == 2 || i == 5 || i == 8 || i == 12 || i == 15 ){
                        /*if(tablaMed.GetChild(i).GetChild(1).GetChild(0).GetChildCount() == 1){
                            Text textoCelda = tablaMed.GetChild(i).GetChild(1).GetChild(0).GetChild(0).GetComponent<Text>();
                            datosMatriz[i] = textoCelda.text.ToString();
                        }
                        else{*/
                        InputField textoCelda = tablaMed.GetChild(i).GetChild(1).GetChild(0).GetComponent<InputField>();
                        if(textoCelda.text == ""){
                            errorMessage = "Para continuar debes diligenciar toda la información solicitada.";
                            isFull = false;
                        }
                        else{
                            datosMatriz[i] = textoCelda.text.ToString();
                        }
                        //}
                    }
                    else{
                        Text textoCeldaDrop = tablaMed.GetChild(i).GetChild(1).GetChild(0).GetChild(3).GetComponent<Text>();
                        if(textoCeldaDrop.text == "- -"){
                            errorMessage = "Para continuar debes diligenciar toda la información solicitada.";
                            isFull = false;
                        }
                        else{
                            datosMatriz[i] = textoCeldaDrop.text.ToString();
                        }
                    }
                }
                if (!errorMessage.Equals("") && !isFull){
                            alert.gameObject.SetActive(true);
                            alertMessage.CreateAlertMessage(errorMessage);                    
                } 
                else{
                    llenarDatosObj(areaActual-1);
                    for (int i = 2; i < tablaMedidores.transform.GetChildCount(); i++){
                        if(i == 0 || i == 1 || i == 2 || i == 5 || i == 8 || i == 12 || i == 15){
                            InputField textoCelda = tablaMed.GetChild(i).GetChild(1).GetChild(0).GetComponent<InputField>();
                            textoCelda.text = "";
                        }
                        else{
                            Text textoCeldaDrop = tablaMed.GetChild(i).GetChild(1).GetChild(0).GetChild(3).GetComponent<Text>();
                            textoCeldaDrop.text = "- -";
                            Dropdown dropdown = tablaMed.GetChild(i).GetChild(1).GetChild(0).GetComponent<Dropdown>();
                            dropdown.value = 0;

                        }
                        /*
                        if(tablaMed.GetChild(i).GetChild(1).GetChild(0).GetChildCount() == 1){
                            Text textoCelda = tablaMed.GetChild(i).GetChild(1).GetChild(0).GetChild(0).GetComponent<Text>();
                            textoCelda.text = "NO";
                        }
                        else{
                            InputField textoCelda = tablaMed.GetChild(i).GetChild(1).GetChild(0).GetComponent<InputField>();
                            textoCelda.text = "";
                        }
                        */
                    }
                    tablaFE.gameObject.SetActive(true);
                    disableChilds(botonesMedidores.gameObject);
                    //Button bFE = botonFE.GetComponent<Button>();
                    //bFE.interactable = false;
                    botonesRiesgos.gameObject.SetActive(false);
                    tablaMedidores.SetActive(false);
                    zona.gameObject.SetActive(false);
                    botonGuardarF1.gameObject.SetActive(false);
                    //toJSONP();
                } 
            }
            else{
                Debug.Log(" El método 'guardarTablaMedidores' está completo");
            }
        }
        else{
            Debug.Log("No es necesario guardar datos.");
        }
    }
    public void disableChilds(GameObject papa){
        int sons = papa.transform.childCount;
        for (int i = 0; i < sons; i++){
            papa.transform.GetChild(i).gameObject.SetActive(false);
        }
    }
    public void disableAreasCompleted(GameObject gameObjectMask){
        if(guardoAreaF1){
            gameObjectMask.transform.GetChild(areaActual-1).gameObject.SetActive(false);
        }
        guardoAreaF1 = false;
    }
    public void llenarDatosObj(int i){
        convertToBoolean();
        //Método
        string [] medToAdd = new string[5];
        for (int j = 0; j < medToAdd.Length; j++){
            medToAdd[j] = mediciones[j];
        }
        matrizMedidores[i].setMediciones(medToAdd);
        matrizMedidores[i].setNumPersonas(Int32.Parse(datosMatriz[0]));
        matrizMedidores[i].setHorasTrabajadas(Int32.Parse(datosMatriz[1]));
        matrizMedidores[i].setLuxometro(datosMatriz[2]);
        matrizMedidores[i].isRiesgo(bool.Parse(datosMatriz[3]));
        matrizMedidores[i].isRutinaria(bool.Parse(datosMatriz[4]));
        matrizMedidores[i].setSonometro(datosMatriz[5]);
        //matrizMedidores[i].setCantidadRuido(datosMatriz[6]);
        matrizMedidores[i].isRiesgoSono(bool.Parse(datosMatriz[6]));
        matrizMedidores[i].isRutinariaSono(bool.Parse(datosMatriz[7]));
        matrizMedidores[i].setDosimetro(datosMatriz[8]);
        matrizMedidores[i].isEsIonizante(bool.Parse(datosMatriz[9]));
        matrizMedidores[i].isRiesgoDosi(bool.Parse(datosMatriz[10]));
        matrizMedidores[i].isRutinariaDosi(bool.Parse(datosMatriz[11]));
        matrizMedidores[i].setTermometro(datosMatriz[12]);
        //matrizMedidores[i].setTemperaturaArea(datosMatriz[14]);
        matrizMedidores[i].isRiesgoTermo(bool.Parse(datosMatriz[13]));
        matrizMedidores[i].isRutinariaTermo(bool.Parse(datosMatriz[14]));
        matrizMedidores[i].setVibrometro(datosMatriz[15]);
        matrizMedidores[i].isVibraManoBrazo(bool.Parse(datosMatriz[16]));
        matrizMedidores[i].isVibraCuerpo(bool.Parse(datosMatriz[17]));
        matrizMedidores[i].isRiesgoVibro(bool.Parse(datosMatriz[18]));
        matrizMedidores[i].isRutinariaVibro(bool.Parse(datosMatriz[19]));
        areasEmpCompletas++;
        guardoAreaF1 = true;
        toJSONP();
        doMedicionesLab();
        medToAdd = null;
    }
    public void convertToBoolean(){
        for (int i = 0; i < datosMatriz.Length; i++){
            if(datosMatriz[i] == "SÍ") datosMatriz[i] = "TRUE";
            if(datosMatriz[i] == "NO") datosMatriz[i] = "FALSE";
        }
    }
    public void doMedicionesLab(){
        
        mediciones[0] = randomMedi(1);
        mediciones[1] = randomMedi(2);
        mediciones[2] = randomMedi(3);
        mediciones[3] = randomMedi(4);
        mediciones[4] = randomMedi(5);

        Transform tabla;
        switch (empresaSeleccionada){
            case 1:
                tabla = transform.Find("BusinessMap").Find("Empresa1").Find("DescZoneLab").Find("Medidores");
                break;
            case 2:
                tabla = transform.Find("BusinessMap").Find("Empresa2").Find("DescZoneMan").Find("Medidores");
                break;
            case 3:
                tabla = transform.Find("BusinessMap").Find("Empresa3").Find("DescZoneOfi").Find("Medidores");
                break;
            case 4:
                tabla = transform.Find("BusinessMap").Find("Empresa4").Find("DescZonePozo").Find("Medidores");
                break;
            case 5:
                tabla = transform.Find("BusinessMap").Find("Empresa5").Find("DescZoneSG").Find("Medidores");
                break;
            default:
                tabla = transform.Find("BusinessMap").Find("Empresa1").Find("DescZoneLab").Find("Medidores");
                break;
        }

        for (int i = 0; i < 5; i++){
            Text texto = tabla.GetChild(i).GetChild(0).GetComponent<Text>();
            texto.text = "";
        }
        /*
        for (int i = 0; i < mediciones.Length; i++){
            Debug.Log(mediciones[i]);
        }
        */
        //Debug.Log("Terminó de crear mediciones.");
    }
    public void catchBoolsTable(GameObject gameObject){
        Transform celda = gameObject.transform;
        Text textoToChange = celda.GetChild(0).GetChild(0).GetComponent<Text>();
        if(textoToChange.text == "SÍ") textoToChange.text = "NO";
        else textoToChange.text = "SÍ";
    }
    public static void addToBigM(){
        Debug.Log("Empresa 1 Tabla 1 Lista!");
        if(empresaSeleccionada == 2){
            string [] med = { "", "", "", "", ""};
            MatrizMedidores m7 = new MatrizMedidores("", "", med, 0, 0, "", false, false, 
            "", false, false, "", false, false, false,
            "", false, false, "", false, false, false, false);
            matrizMedidores[7] = m7;
        }
        if(FlowControllerPhysical.data.getFichaActual() == 2){
            FlowControllerPhysical.data.listaFinalMedidores.Add(matrizMedidores);
            empresasCompletadas++;
            areasEmpCompletas = 0;
            //FlowControllerPhysical.data.setListaFinalMedidores(FlowControllerPhysical.data.listaFinalMedidores);
            Debug.Log("Set 1 Matriz Medidores data");
            FlowControllerPhysical.data.setFichaActual(3);
            FlowControllerPhysical.data.setMatrizMedidores1emp();
        }
        else if(FlowControllerPhysical.data.getFichaActual() == 6){
            FlowControllerPhysical.data.listaFinalMedidores.Add(matrizMedidores);
            empresasCompletadas++;
            areasEmpCompletas = 0;
            //FlowControllerPhysical.data.setListaFinalMedidores(FlowControllerPhysical.data.listaFinalMedidores);
            Debug.Log("Set 2 Matriz Medidores data");
            FlowControllerPhysical.data.setFichaActual(7);
            FlowControllerPhysical.data.setMatrizMedidores2emp();
        }
        Debug.Log("La matriz de la ficha 1 tiene  : " + FlowControllerPhysical.data.getListaFinalMedidores().Count);
        //Debug.Log("Datos Almacenados a la Lista!");
    }
    public void toJSONP(){
        //Debug.Log(JsonUtility.ToJson(matrizMedidores[areaActual-1]));
        //FlowControllerPhysical.printDataPlayer();
    }
}
