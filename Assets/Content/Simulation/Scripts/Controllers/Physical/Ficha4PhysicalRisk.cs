using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System;

public class Ficha4PhysicalRisk : MonoBehaviour {
	private AlertMessage alertMessage;
	public Transform alert;
    [System.NonSerialized] MatrizMedidores [] matRiesgo;  
    [System.NonSerialized] public int areaInt = 1;
    [System.NonSerialized] public int riesgoInt = 0;
    [System.NonSerialized] public Transform tablaPrincipal;
    [System.NonSerialized] public Transform area;
    [System.NonSerialized] public Transform cellE;
    [System.NonSerialized] public Transform cellG;
    [System.NonSerialized] public Transform cellJ;
    [System.NonSerialized] public Transform cellI;
    [System.NonSerialized] public Transform cellK;
    [System.NonSerialized] public Transform cellN;
    [System.NonSerialized] public Transform cellM;
    [System.NonSerialized] public Transform cellP;
    [System.NonSerialized] public static MatrizRiesgosFisico [] matrizRiesgosFisico;
    [System.NonSerialized] public static List<MatrizRiesgosFisico []> matrizFinalRiesgosFisicos = new List<MatrizRiesgosFisico[]>();
    [System.NonSerialized] public static bool toIntro;
    [System.NonSerialized] public static bool cleanTable = false;
    [System.NonSerialized] public static int width = 2045;
    [System.NonSerialized] public static int height = 6073;
    public void Start(){
        //Pruebas
        //matRiesgo = Ficha3Physical.listaFinalMedidores3[0];
        if(FlowControllerPhysical.dataPlayer.getFichaActual() == 3 
            //Prueba
            || FlowControllerPhysical.dataPlayer.getFichaActual() == 4){
            matRiesgo = Ficha3Physical.listaFinalMedidores3[0];
        }
        else{
            matRiesgo = Ficha3Physical.listaFinalMedidores3[1];
        }
        //matRiesgo = Ficha3Physical.listaFinalMedidores3[Ficha1Fisicos.empresasCompletadas-1];
		alertMessage = alert.GetComponent<AlertMessage>();
        tablaPrincipal = transform.Find("Scroll").Find("TableEvaluation");
        area = tablaPrincipal.GetChild(areaInt);
        cellE = area.Find("CellDescripciones").GetChild(riesgoInt).Find("celle").Find("Edropdown");
        cellG = area.Find("CellDescripciones").GetChild(riesgoInt).Find("cellg").Find("Gdropdown");
        cellJ = area.Find("CellDescripciones").GetChild(riesgoInt).Find("cellj").Find("Jdropdown");
        cellI = area.Find("CellDescripciones").GetChild(riesgoInt).Find("celli").Find("Value").Find("Placeholder");
        cellK = area.Find("CellDescripciones").GetChild(riesgoInt).Find("cellk").Find("Kdropdown");
        cellN = area.Find("CellDescripciones").GetChild(riesgoInt).Find("celln").Find("Ndropdown");
        cellM = area.Find("CellDescripciones").GetChild(riesgoInt).Find("cellm").Find("Value").Find("Placeholder");
        cellP = area.Find("CellDescripciones").GetChild(riesgoInt).Find("cellp").Find("Value").Find("Placeholder");
        toIntro = false;
        if(FlowControllerPhysical.dataPlayer.getFichaActual() == 3) FlowControllerPhysical.dataPlayer.setFichaActual(4);
        if(FlowControllerPhysical.dataPlayer.getFichaActual() == 7) FlowControllerPhysical.dataPlayer.setFichaActual(8);
        Debug.Log("Ficha 4 Ficha Actual: "+FlowControllerPhysical.dataPlayer.getFichaActual());
        LoadInfo();
    }
    public void StarForcedF4(){
       if(FlowControllerPhysical.dataPlayer.getFichaActual() == 7 && !Ficha1Fisicos.cargaEmp1){
           Debug.Log("Start F4 Forced");
           resetFillSpaces();
           resetFicha4();
           Start();
       } 
    }

    void changeCellDrop(){
        area = tablaPrincipal.GetChild(areaInt);
        cellE = area.Find("CellDescripciones").GetChild(riesgoInt).Find("celle").Find("Edropdown");
        cellG = area.Find("CellDescripciones").GetChild(riesgoInt).Find("cellg").Find("Gdropdown");
        cellJ = area.Find("CellDescripciones").GetChild(riesgoInt).Find("cellj").Find("Jdropdown");
        cellI = area.Find("CellDescripciones").GetChild(riesgoInt).Find("celli").Find("Value").Find("Placeholder");
        cellK = area.Find("CellDescripciones").GetChild(riesgoInt).Find("cellk").Find("Kdropdown");
        cellN = area.Find("CellDescripciones").GetChild(riesgoInt).Find("celln").Find("Ndropdown");
        cellM = area.Find("CellDescripciones").GetChild(riesgoInt).Find("cellm").Find("Value").Find("Placeholder");
        cellP = area.Find("CellDescripciones").GetChild(riesgoInt).Find("cellp").Find("Value").Find("Placeholder");
    }
    void LoadInfo(){
        enableRisk();
        /*
        if(Ficha1Fisicos.empresaSeleccionada == 2){
            Transform tablaFicha4 = transform.Find("Scroll").Find("TableEvaluation");
            tablaFicha4.GetChild(7).gameObject.SetActive(false);
            RectTransform dimTabla = tablaFicha4.GetComponent<RectTransform>();
            height = height-758;
            dimTabla.sizeDelta = new Vector2(width, height);
        }
        */
        for (int i = 0; i < tablaPrincipal.GetChildCount(); i++){
            Text texto = tablaPrincipal.GetChild(i).GetChild(0).GetChild(0).GetComponent<Text>();
            texto.text = matRiesgo[i].getArea();
            for (int j = 0; j <area.Find("CellDescripciones").GetChildCount(); j++){
                InputField textoArea = tablaPrincipal.GetChild(i).GetChild(2).GetChild(j).GetChild(0).GetChild(0).GetComponent<InputField>();
                Text textoNumEmp = tablaPrincipal.GetChild(i).GetChild(2).GetChild(j).Find("cellp").Find("Value").Find("Placeholder").GetComponent<Text>();
                switch (j){
                    case 0:
                        textoArea.text = matRiesgo[i].getControlRiesgoLux();
                        textoNumEmp.text = matRiesgo[i].getNumPersonas().ToString();
                        break;
                    case 1:
                        textoArea.text = matRiesgo[i].getControlRiesgoSono();
                        textoNumEmp.text = matRiesgo[i].getNumPersonas().ToString();
                        break;
                    case 2:
                        textoArea.text = matRiesgo[i].getControlRiesgoDosi();
                        textoNumEmp.text = matRiesgo[i].getNumPersonas().ToString();
                        break;
                    case 3:
                        textoArea.text = matRiesgo[i].getControlRiesgoTermo();  
                        textoNumEmp.text = matRiesgo[i].getNumPersonas().ToString();
                        break;
                    case 4:
                        textoArea.text = matRiesgo[i].getControlRiesgoVibro();
                        textoNumEmp.text = matRiesgo[i].getNumPersonas().ToString();
                        break;
                    default:
                        break;
                }
            }
        }
        disableRisk();
        disableRisk1();

        if(FlowControllerPhysical.dataPlayer.getFichaActual() > 4){
            llenarMatriz();
        }
    }
    public void enableRisk(){
        Transform tablaFicha4 = transform.Find("Scroll").Find("TableEvaluation");
        RectTransform dimTabla = tablaFicha4.GetComponent<RectTransform>();
        for (int i = 0; i < tablaFicha4.GetChildCount(); i++){
            for (int j = 0; j < tablaFicha4.GetChild(i).GetChild(2).GetChildCount(); j++){
                tablaFicha4.GetChild(i).gameObject.SetActive(true);                
                tablaFicha4.GetChild(i).GetChild(1).GetChild(j).gameObject.SetActive(true);
                tablaFicha4.GetChild(i).GetChild(2).GetChild(j).gameObject.SetActive(true);
                Text textoA = tablaFicha4.GetChild(i).GetChild(0).GetChild(0).GetComponent<Text>();
                Text textoDesc = tablaFicha4.GetChild(i).GetChild(2).GetChild(j).GetChild(0).Find("Value").Find("Text").GetComponent<Text>();
                Text textoD = tablaFicha4.GetChild(i).Find("CellDescripciones").GetChild(j).Find("celld").Find("Value").Find("Text").GetComponent<Text>();
                Text textoQ = tablaFicha4.GetChild(i).Find("CellDescripciones").GetChild(j).Find("cellq").Find("Value").Find("Text").GetComponent<Text>();
                Text textoR = tablaFicha4.GetChild(i).Find("CellDescripciones").GetChild(j).Find("cellr").Find("Value").Find("Text").GetComponent<Text>();
                Text textoS = tablaFicha4.GetChild(i).Find("CellDescripciones").GetChild(j).Find("cells").Find("Value").Find("Text").GetComponent<Text>();
                Text textoT = tablaFicha4.GetChild(i).Find("CellDescripciones").GetChild(j).Find("cellt").Find("Value").Find("Text").GetComponent<Text>();
                Text textoU = tablaFicha4.GetChild(i).Find("CellDescripciones").GetChild(j).Find("cellu").Find("Value").Find("Text").GetComponent<Text>();
                Text textoV = tablaFicha4.GetChild(i).Find("CellDescripciones").GetChild(j).Find("cellv").Find("Value").Find("Text").GetComponent<Text>();
                Text textoW = tablaFicha4.GetChild(i).Find("CellDescripciones").GetChild(j).Find("cellw").Find("Value").Find("Text").GetComponent<Text>();               
                textoA.text = "Area n";
                textoDesc.text = "";
                textoD.text = "";
                textoQ.text = "";
                textoR.text = "";
                textoS.text = ""; 
                textoT.text = "";
                textoU.text = ""; 
                textoV.text = "";
                textoW.text = ""; 
                Text textoDescr = tablaFicha4.GetChild(i).GetChild(2).GetChild(j).GetChild(0).Find("Value").Find("Placeholder").GetComponent<Text>();
                Text textoDc = tablaFicha4.GetChild(i).Find("CellDescripciones").GetChild(j).Find("celld").Find("Value").Find("Text").GetComponent<Text>();
                Text textoQc = tablaFicha4.GetChild(i).Find("CellDescripciones").GetChild(j).Find("celld").Find("Value").Find("Text").GetComponent<Text>();
                Text textoRc = tablaFicha4.GetChild(i).Find("CellDescripciones").GetChild(j).Find("cellr").Find("Value").Find("Text").GetComponent<Text>();
                Text textoSc = tablaFicha4.GetChild(i).Find("CellDescripciones").GetChild(j).Find("cells").Find("Value").Find("Text").GetComponent<Text>();
                Text textoTc = tablaFicha4.GetChild(i).Find("CellDescripciones").GetChild(j).Find("cellt").Find("Value").Find("Text").GetComponent<Text>();
                Text textoUc = tablaFicha4.GetChild(i).Find("CellDescripciones").GetChild(j).Find("cellu").Find("Value").Find("Text").GetComponent<Text>();
                Text textoVc = tablaFicha4.GetChild(i).Find("CellDescripciones").GetChild(j).Find("cellv").Find("Value").Find("Text").GetComponent<Text>();
                Text textoWc = tablaFicha4.GetChild(i).Find("CellDescripciones").GetChild(j).Find("cellw").Find("Value").Find("Text").GetComponent<Text>();               
                textoDescr.text = "";
                textoDc.text = "";
                textoQc.text = "";
                textoRc.text = "";
                textoSc.text = ""; 
                textoTc.text = "";
                textoUc.text = ""; 
                textoVc.text = "";
                textoWc.text = "";                    
                Text textoI = tablaFicha4.GetChild(i).Find("CellDescripciones").GetChild(j).Find("celli").Find("Value").Find("Placeholder").GetComponent<Text>();
                Text textoM = tablaFicha4.GetChild(i).Find("CellDescripciones").GetChild(j).Find("cellm").Find("Value").Find("Placeholder").GetComponent<Text>();
                Text textoO = tablaFicha4.GetChild(i).Find("CellDescripciones").GetChild(j).Find("cello").Find("Value").Find("Placeholder").GetComponent<Text>();
                textoI.text = "- -"; 
                textoM.text = "- -";
                textoO.text = "- -";
            }
        }
        height = 6073;
        dimTabla.sizeDelta = new Vector2(width, height);
        for (int i = 0; i < tablaFicha4.GetChildCount(); i++){
            for (int j = 0; j < tablaFicha4.GetChild(i).Find("CellDescripciones").GetChildCount(); j++){
                //Text textoDesc = tablaFicha4.GetChild(i).GetChild(2).GetChild(j).GetChild(0).Find("Value").Find("Text").GetComponent<Text>();
                //if(textoDesc.text != "No es necesario un control" || textoDesc.text != "NO"){
                    //Debug.Log("i: " + i + " j: " + j);
                //}
            }
        }
    }
    public void disableRisk(){
        int contadorTotal = 0;
        int areasDesable = 0;
        Transform tablaFicha4 = transform.Find("Scroll").Find("TableEvaluation");
        RectTransform dimTabla = tablaFicha4.GetComponent<RectTransform>();
         for (int i = 0; i < tablaFicha4.GetChildCount(); i++){
            int contador = 0;
            for (int j = 0; j < tablaFicha4.GetChild(i).GetChild(2).GetChildCount(); j++){
                Text textoDesc = tablaFicha4.GetChild(i).GetChild(2).GetChild(j).GetChild(0).Find("Value").Find("Text").GetComponent<Text>();
                if(textoDesc.text == "No es necesario un control" || textoDesc.text == "NO"){
                    tablaFicha4.GetChild(i).GetChild(1).GetChild(j).gameObject.SetActive(false);
                    tablaFicha4.GetChild(i).GetChild(2).GetChild(j).gameObject.SetActive(false);
                    contador++;
                    contadorTotal++;
                }
                if(contador == 5){
                    tablaFicha4.GetChild(i).gameObject.SetActive(false);
                    areasDesable++;
                }
            }
            //Debug.Log(height);
            height = height - ((contador * 150) - contador-1);
        }
        //height = height - contadorTotal - 1; 
        Debug.Log(height);
        if(height < 0) height = 0;
        dimTabla.sizeDelta = new Vector2(width, height);
        matrizRiesgosFisico = new MatrizRiesgosFisico[40 - contadorTotal];
    }
    public void disableRisk1(){
        int contadorTotal = 0;
        int areasDesable = 0;
        Transform tablaFicha4 = transform.Find("Scroll").Find("TableEvaluation");
        RectTransform dimTabla = tablaFicha4.GetComponent<RectTransform>();
        Transform tablaFicha41 = transform.Find("Scroll (1)").Find("TableEvaluation");
        RectTransform dimTabla1 = tablaFicha41.GetComponent<RectTransform>();
        for(int i = 0; i < tablaFicha4.GetChildCount(); i++){
            int riesgoDis = 0;
            for(int j = 0; j < tablaFicha4.GetChild(i).GetChild(1).GetChildCount(); j++){
                if(!tablaFicha4.GetChild(i).GetChild(1).GetChild(j).gameObject.active){
                    tablaFicha41.GetChild(i).GetChild(1).GetChild(j).gameObject.SetActive(false);
                    riesgoDis++;
                    contadorTotal++;
                }
            }
            if(riesgoDis == 5){
                tablaFicha41.GetChild(i).gameObject.SetActive(false);
                areasDesable++;
            } 
            //height = height - ((riesgoDis * 150) - riesgoDis-1);
            
        }
        //if(height < 0) height = 0;
        dimTabla1.sizeDelta = new Vector2(141, height);
        for(int i = 0; i < tablaFicha4.GetChildCount(); i++){
            if(tablaFicha4.GetChild(i).gameObject.active){
                Text textoAhi = tablaFicha41.GetChild(i).GetChild(0).GetChild(0).GetComponent<Text>();
                Text textoAbajo = tablaFicha4.GetChild(i).GetChild(0).GetChild(0).GetComponent<Text>();
                textoAhi.text = textoAbajo.text;
            }
        }

    }
    void changeCellsByTal(){
        Transform tablaPrincipal = transform.Find("TableEvaluation");
        int contador = 0;
        for (int i = 0; i < tablaPrincipal.GetChildCount(); i++){
            for (int j = 0; j < 5; j++){
                for (int k = 0; k < 21; k++){
                    Transform celda = tablaPrincipal.GetChild(i).GetChild(2).GetChild(j).GetChild(k).GetChild(0);
                    Transform texto = tablaPrincipal.GetChild(i).GetChild(2).GetChild(j).GetChild(k).GetChild(0).GetChild(1);
                    celda.GetComponent<InputField>().characterLimit = 1000;
                    Color color;
                    ColorUtility.TryParseHtmlString("#ACD6E5", out color);
                    if(contador % 2 == 1){
                        texto.GetComponent<Text>().color = color;
                    }
                }
                contador++;
            }
        }

    }
    public void StartForced(){
        if(Ficha1Fisicos.empresasCompletadas > 1){
            resetFicha4();
            Start();
            Debug.Log("Llamó el método Start() ficha 4");
        }
    }
    //Selecciones
    public void changeCellF(GameObject dest4){
        Transform cellEx = dest4.transform.Find("celle").Find("Edropdown");
        Dropdown textoE = cellEx.GetComponent<Dropdown>();
        Transform celdaF = dest4.transform.Find("cellf").Find("Value").Find("Placeholder");
        Text textoF = celdaF.GetComponent<Text>();
        switch(textoE.value){
            case 1:
                textoF.text = "BAJO";
                break;
            case 2:
                textoF.text = "MEDIO";
                break;
            case 3:
                textoF.text = "ALTO";
                break;
            case 4:
                textoF.text = "MUY ALTO";
                break;
            default:
                textoF.text = "BAJO";
                break;
        }
        changeCellI(dest4);
	}
    public void changeCelH(GameObject dest4){
        Transform cellEx = dest4.transform.Find("cellg").Find("Gdropdown");
        Dropdown textoE = cellEx.GetComponent<Dropdown>();
        Transform celdaF = dest4.transform.Find("cellh").Find("Value").Find("Placeholder");
        Text textoF = celdaF.GetComponent<Text>();
        switch(textoE.value){
            case 1:
                textoF.text = "ESPORÁDICA";
                break;
            case 2:
                textoF.text = "OCASIONAL";
                break;
            case 3:
                textoF.text = "FRECUENTE";
                break;
            case 4:
                textoF.text = "CONTINUA";
                break;
            default:
                textoF.text = "ESPORÁDICA";
                break;
        }
        changeCellI(dest4);
	}
    public void changeCelL( GameObject dest4){
        Transform cellEx = dest4.transform.Find("cellk").Find("Kdropdown");
        Dropdown textoE = cellEx.GetComponent<Dropdown>();
        Transform celdaF = dest4.transform.Find("celll").Find("Value").Find("Placeholder");
        Text textoF = celdaF.GetComponent<Text>();
        switch(textoE.value){
            case 1:
                textoF.text = "LEVE";
                break;
            case 2:
                textoF.text = "GRAVE";
                break;
            case 3:
                textoF.text = "MUY MAL";
                break;
            case 4:
                textoF.text = "MORTAL";
                break;
            default:
                textoF.text = "LEVE";
                break;
        }
        changeCellM(dest4);
	}
    public void changeCelO( GameObject dest4){
        Transform cellEx = dest4.transform.Find("celln").Find("Ndropdown");
        Dropdown textoE = cellEx.GetComponent<Dropdown>();
        Transform celdaF = dest4.transform.Find("cello").Find("Value").Find("Placeholder");
        Text textoO = celdaF.GetComponent<Text>();
        Image colorCelda = dest4.transform.Find("cello").GetComponent<Image>();
        switch(textoE.value){
            case 4:
                colorCelda.color = Color.red;
                textoO.text = "NO ACEPTABLE";
                break;
            case 3:
                colorCelda.color = Color.yellow;
                textoO.text = "ACEPTABLE CON CONTROL ESPECÍFICO";
                break;
            case 2:
                colorCelda.color = Color.green;
                textoO.text = "MEJORABLE";
                break;
            case 1:
                colorCelda.color = Color.white;
                textoO.text = "ACEPTABLE";
                break;
            case 0:
                Color color;
                ColorUtility.TryParseHtmlString("#ACD6E5", out color);
                colorCelda.color = color;
                textoO.text = "- -";
                break;
            default:
                break;
        }
        changeCellM(dest4);
	}
    //Multiplicaciones
    public void changeCellI(GameObject dest4){
        Text textoI = dest4.transform.Find("celli").Find("Value").Find("Placeholder").GetComponent<Text>();
        int a = 0;
        int b = 1;
        Transform cellEx = dest4.transform.Find("celle").Find("Edropdown");
        Dropdown textoE = cellEx.GetComponent<Dropdown>();
        switch(textoE.value){
            case 1:
                a = 0;
                break;
            case 2:
                a = 2;
                break;
            case 3:
                a = 6;
                break;
            case 4:
                a = 10;
                break;
            default:
                a = 0;
                break;    
        }
        Transform cellgx = dest4.transform.Find("cellg").Find("Gdropdown");
        Dropdown textog = cellgx.GetComponent<Dropdown>();
        switch(textog.value){
            case 1:
                b = 1;
                break;
            case 2:
                b = 2;
                break;
            case 3:
                b = 3;
                break;
            case 4:
                b = 4;
                break;
            default:
                b = 1;
                break;    
        }
        int mult = a * b;
        textoI.text = mult.ToString();
    }
    public void changeCellM(GameObject dest4){
        Text textoI = dest4.transform.Find("cellm").Find("Value").Find("Placeholder").GetComponent<Text>();
        int c = 0;
        int a = 0;
        int b = 1;
        Transform cellEx = dest4.transform.Find("celle").Find("Edropdown");
        Dropdown textoE = cellEx.GetComponent<Dropdown>();
        switch(textoE.value){
            case 1:
                a = 0;
                break;
            case 2:
                a = 2;
                break;
            case 3:
                a = 6;
                break;
            case 4:
                a = 10;
                break;
            default:
                a = 0;
                break;    
        }
        Transform cellgx = dest4.transform.Find("cellg").Find("Gdropdown");
        Dropdown textog = cellgx.GetComponent<Dropdown>();
        switch(textog.value){
            case 1:
                b = 1;
                break;
            case 2:
                b = 2;
                break;
            case 3:
                b = 3;
                break;
            case 4:
                b = 4;
                break;
            default:
                b = 1;
                break;    
        }
        int mult = a * b;
        Transform cellkx = dest4.transform.Find("cellk").Find("Kdropdown");
        Dropdown textom = cellkx.GetComponent<Dropdown>();
        switch(textom.value){
            case 1:
                c = 10;
                break;
            case 2:
                c = 25;
                break;
            case 3:
                c = 60;
                break;
            case 4:
                c = 100;
                break;
            default:
                c = 0;
                break;    
        }
        int multi = mult * c;
        textoI.text = multi.ToString();
    }
    public void llenarMatriz(){
        for (int i = 0; i < tablaPrincipal.GetChildCount(); i++){
            for (int j = 0; j < tablaPrincipal.GetChild(i).Find("CellDescripciones").GetChildCount(); j++){
                if(tablaPrincipal.GetChild(i).Find("CellDescripciones").GetChild(j).gameObject.active){
                    Text textoD = tablaPrincipal.GetChild(i).Find("CellDescripciones").GetChild(j).Find("celld").Find("Value").Find("Text").GetComponent<Text>();
                    Text textoQ = tablaPrincipal.GetChild(i).Find("CellDescripciones").GetChild(j).Find("celld").Find("Value").Find("Text").GetComponent<Text>();
                    Text textoR = tablaPrincipal.GetChild(i).Find("CellDescripciones").GetChild(j).Find("cellr").Find("Value").Find("Text").GetComponent<Text>();
                    Text textoS = tablaPrincipal.GetChild(i).Find("CellDescripciones").GetChild(j).Find("cells").Find("Value").Find("Text").GetComponent<Text>();
                    Text textoT = tablaPrincipal.GetChild(i).Find("CellDescripciones").GetChild(j).Find("cellt").Find("Value").Find("Text").GetComponent<Text>();
                    Text textoU = tablaPrincipal.GetChild(i).Find("CellDescripciones").GetChild(j).Find("cellu").Find("Value").Find("Text").GetComponent<Text>();
                    Text textoV = tablaPrincipal.GetChild(i).Find("CellDescripciones").GetChild(j).Find("cellv").Find("Value").Find("Text").GetComponent<Text>();
                    Text textoW = tablaPrincipal.GetChild(i).Find("CellDescripciones").GetChild(j).Find("cellw").Find("Value").Find("Text").GetComponent<Text>();
                    Text textoI = tablaPrincipal.GetChild(i).Find("CellDescripciones").GetChild(j).Find("celli").Find("Value").Find("Placeholder").GetComponent<Text>();
                    Text textoM = tablaPrincipal.GetChild(i).Find("CellDescripciones").GetChild(j).Find("cellm").Find("Value").Find("Placeholder").GetComponent<Text>();
                    Text textoO = tablaPrincipal.GetChild(i).Find("CellDescripciones").GetChild(j).Find("cello").Find("Value").Find("Placeholder").GetComponent<Text>();
                    textoD.text = FlowControllerPhysical.data.getMatrizFinalRiesgosFisicos()[0][0].getEfectosPosibles(); 
                    textoQ.text = FlowControllerPhysical.data.getMatrizFinalRiesgosFisicos()[0][0].getPorConsecuencia();
                    textoR.text = FlowControllerPhysical.data.getMatrizFinalRiesgosFisicos()[0][0].getExistenciaRequisito();
                    textoS.text = FlowControllerPhysical.data.getMatrizFinalRiesgosFisicos()[0][0].getSustitucion();
                    textoT.text = FlowControllerPhysical.data.getMatrizFinalRiesgosFisicos()[0][0].getEliminacion();
                    textoU.text = FlowControllerPhysical.data.getMatrizFinalRiesgosFisicos()[0][0].getControles();
                    textoV.text = FlowControllerPhysical.data.getMatrizFinalRiesgosFisicos()[0][0].getSenalizacion();
                    textoW.text = FlowControllerPhysical.data.getMatrizFinalRiesgosFisicos()[0][0].getEpp();
                    textoI.text = FlowControllerPhysical.data.getMatrizFinalRiesgosFisicos()[0][0].getProba().ToString();
                    textoM.text = FlowControllerPhysical.data.getMatrizFinalRiesgosFisicos()[0][0].getNr().ToString();
                    textoO.text = FlowControllerPhysical.data.getMatrizFinalRiesgosFisicos()[0][0].getNrInter().ToString();
                }
            }
        }
    }
    public bool matrizLlena(){
        if(FlowControllerPhysical.dataPlayer.getFichaActual() >= 4 ){
            for (int i = 0; i < tablaPrincipal.GetChildCount(); i++){
                for (int j = 0; j < tablaPrincipal.GetChild(i).Find("CellDescripciones").GetChildCount(); j++){
                    if(tablaPrincipal.GetChild(i).gameObject.active){
                        if(tablaPrincipal.GetChild(i).Find("CellDescripciones").GetChild(j).gameObject.active){
                            Text textoD = tablaPrincipal.GetChild(i).Find("CellDescripciones").GetChild(j).Find("celld").Find("Value").Find("Text").GetComponent<Text>();
                            Text textoQ = tablaPrincipal.GetChild(i).Find("CellDescripciones").GetChild(j).Find("cellq").Find("Value").Find("Text").GetComponent<Text>();
                            Text textoR = tablaPrincipal.GetChild(i).Find("CellDescripciones").GetChild(j).Find("cellr").Find("Value").Find("Text").GetComponent<Text>();
                            Text textoS = tablaPrincipal.GetChild(i).Find("CellDescripciones").GetChild(j).Find("cells").Find("Value").Find("Text").GetComponent<Text>();
                            Text textoT = tablaPrincipal.GetChild(i).Find("CellDescripciones").GetChild(j).Find("cellt").Find("Value").Find("Text").GetComponent<Text>();
                            Text textoU = tablaPrincipal.GetChild(i).Find("CellDescripciones").GetChild(j).Find("cellu").Find("Value").Find("Text").GetComponent<Text>();
                            Text textoV = tablaPrincipal.GetChild(i).Find("CellDescripciones").GetChild(j).Find("cellv").Find("Value").Find("Text").GetComponent<Text>();
                            Text textoW = tablaPrincipal.GetChild(i).Find("CellDescripciones").GetChild(j).Find("cellw").Find("Value").Find("Text").GetComponent<Text>();
                            if(textoD.text == "" || textoQ.text == "" || textoR.text == "" || textoS.text == "" || textoT.text == "" || textoU.text == "" || textoV.text == "" || textoW.text == ""){
                                Debug.Log("i: " + i + " j: " + j);
                                return true;
                            }
                            Text textoI = tablaPrincipal.GetChild(i).Find("CellDescripciones").GetChild(j).Find("celli").Find("Value").Find("Placeholder").GetComponent<Text>();
                            Text textoM = tablaPrincipal.GetChild(i).Find("CellDescripciones").GetChild(j).Find("cellm").Find("Value").Find("Placeholder").GetComponent<Text>();
                            Text textoO = tablaPrincipal.GetChild(i).Find("CellDescripciones").GetChild(j).Find("cello").Find("Value").Find("Placeholder").GetComponent<Text>();
                            if(textoI.text == "- -" || textoM.text == "- -" || textoO.text == "- -"){
                                Debug.Log("i: " + i + " j: " + j);
                                return true;
                            }
                        }
                    }
                }
            }
            return false;
        }
        else return false;
    }
    public void guardarMatriz(){
        int contador = 0;
        for (int i = 0; i < tablaPrincipal.GetChildCount(); i++){
            for (int j = 0; j < tablaPrincipal.GetChild(i).Find("CellDescripciones").GetChildCount(); j++){
                if(tablaPrincipal.GetChild(i).Find("CellDescripciones").GetChild(j).gameObject.active){
                    string empresa = matRiesgo[i].getEmpresa();
                    string area = matRiesgo[i].getArea();
                    string riesgo =  "";
                    string descRiesgo = "";
                    string efectosPosibles = "";
                    int nd = 0;
                    string ndCuali = "";
                    int ne = 0;
                    string neCuali = "";
                    int proba = nd*ne;
                    string impactProba = "";
                    int nc = 0;
                    string ncCuali = "";
                    int nr = 0;
                    string nrInter = "";
                    string aceptabilidadRiesgo = "";
                    int numEmpleados = matRiesgo[i].getNumPersonas();
                    string porConsecuencia = "";
                    string existenciaRequisito = "";
                    string sustitucion = "";
                    string eliminacion = "";
                    string controles = "";
                    string senalizacion = "";
                    string epp = "";
                    switch (j){
                        case 0:
                            riesgo = "Iluminación";
                            descRiesgo = matRiesgo[i].getControlRiesgoLux();
                            break;
                        case 1:
                            riesgo = "Sonido";
                            descRiesgo = matRiesgo[i].getControlRiesgoSono();
                            break;
                        case 2:
                            riesgo = "Radiación";
                            descRiesgo = matRiesgo[i].getControlRiesgoDosi();
                            break;
                        case 3:
                            riesgo = "Temperatura";
                            descRiesgo = matRiesgo[i].getControlRiesgoTermo();  
                            break;
                        case 4:
                            riesgo = "Oscilación";
                            descRiesgo = matRiesgo[i].getControlRiesgoVibro();
                            break;
                        default:
                            break;
                    }
                    efectosPosibles = tablaPrincipal.GetChild(i).Find("CellDescripciones").GetChild(j).Find("celld").Find("Value").Find("Text").GetComponent<Text>().text;
                    Transform cellEx = tablaPrincipal.GetChild(i).Find("CellDescripciones").GetChild(j).Find("celle").Find("Edropdown");
                    Dropdown textoE = cellEx.GetComponent<Dropdown>();
                    switch(textoE.value){
                        case 0:
                            nd = 0;
                            ndCuali = "BAJO";
                            break;
                        case 1:
                            nd = 2;
                            ndCuali = "MEDIO";
                            break;
                        case 2:
                            nd = 6;
                            ndCuali = "ALTO";
                            break;
                        case 3:
                            nd = 10;
                            ndCuali = "MUY ALTO";
                            break;
                        default:
                            break;
                    }
                    Transform cellGx = tablaPrincipal.GetChild(i).Find("CellDescripciones").GetChild(j).Find("cellg").Find("Gdropdown");
                    Dropdown textoH = cellGx.GetComponent<Dropdown>();
                    switch(textoH.value){
                        case 0:
                            ne = 1;
                            neCuali = "ESPORÁDICA";
                            break;
                        case 1:
                            ne = 2;
                            neCuali = "OCASIONAL";
                            break;
                        case 2:
                            ne = 3;
                            neCuali = "FRECUENTE";
                            break;
                        case 3:
                            ne = 4;
                            neCuali = "CONTINUA";
                            break;
                        default:
                            break;
                    }
                    Transform celljx = tablaPrincipal.GetChild(i).Find("CellDescripciones").GetChild(j).Find("cellj").Find("Jdropdown");
                    Dropdown textoJ = celljx.GetComponent<Dropdown>();
                    switch(textoJ.value){
                        case 0:
                            impactProba = "BAJO";
                            break;
                        case 1:
                            impactProba = "MEDIO";
                            break;
                        case 2:
                            impactProba = "ALTO";
                            break;
                        case 3:
                            impactProba = "MUY ALTO";
                            break;
                        default:
                            break;
                    }
                    Transform cellKx = tablaPrincipal.GetChild(i).Find("CellDescripciones").GetChild(j).Find("cellk").Find("Kdropdown");
                    Dropdown textoK = cellKx.GetComponent<Dropdown>();
                    switch(textoK.value){
                        case 0:
                            nc = 10;
                            ncCuali = "LEVE";
                            break;
                        case 1:
                            nc = 25;
                            ncCuali = "GRAVE";
                            break;
                        case 2:
                            nc = 60;
                            ncCuali = "MUY MAL";
                            break;
                        case 3:
                            nc = 100;
                            ncCuali = "MORTAL";
                            break;
                        default:
                            break;
                    }
                    Transform cellNx = tablaPrincipal.GetChild(i).Find("CellDescripciones").GetChild(j).Find("celln").Find("Ndropdown");
                    Dropdown textoN = cellNx.GetComponent<Dropdown>();
                    switch(textoN.value){
                        case 4:
                            nrInter = "I";
                            aceptabilidadRiesgo = "NO ACEPTABLE";
                            break;
                        case 3:
                            nrInter = "II";
                            aceptabilidadRiesgo = "ACEPTABLE CON CONTROL ESPECÍFICO";
                            break;
                        case 2:
                            nrInter = "III";
                            aceptabilidadRiesgo = "MEJORABLE";
                            break;
                        case 1:
                            nrInter = "IV";
                            aceptabilidadRiesgo = "ACEPTABLE";
                            break;
                        default:
                            break;
                    }
                    porConsecuencia = tablaPrincipal.GetChild(i).Find("CellDescripciones").GetChild(j).Find("cellq").Find("Value").Find("Text").GetComponent<Text>().text;
                    existenciaRequisito = tablaPrincipal.GetChild(i).Find("CellDescripciones").GetChild(j).Find("cellr").Find("Value").Find("Text").GetComponent<Text>().text;
                    sustitucion = tablaPrincipal.GetChild(i).Find("CellDescripciones").GetChild(j).Find("cells").Find("Value").Find("Text").GetComponent<Text>().text;
                    eliminacion = tablaPrincipal.GetChild(i).Find("CellDescripciones").GetChild(j).Find("cellt").Find("Value").Find("Text").GetComponent<Text>().text;
                    controles = tablaPrincipal.GetChild(i).Find("CellDescripciones").GetChild(j).Find("cellu").Find("Value").Find("Text").GetComponent<Text>().text;
                    senalizacion = tablaPrincipal.GetChild(i).Find("CellDescripciones").GetChild(j).Find("cellv").Find("Value").Find("Text").GetComponent<Text>().text;
                    epp = tablaPrincipal.GetChild(i).Find("CellDescripciones").GetChild(j).Find("cellw").Find("Value").Find("Text").GetComponent<Text>().text;
                    proba = nd*ne;
                    nr = proba * nc;
                    MatrizRiesgosFisico mrf = new MatrizRiesgosFisico(empresa, area, riesgo, descRiesgo, efectosPosibles, nd, ndCuali,
                    ne, neCuali, proba, impactProba, nc, ncCuali, nr, nrInter, aceptabilidadRiesgo, numEmpleados, porConsecuencia,
                    existenciaRequisito, sustitucion, eliminacion, controles, senalizacion, epp);
                    matrizRiesgosFisico[contador] = mrf;
                    contador++;
                    tablaPrincipal.GetChild(i).Find("CellDescripciones").GetChild(j).Find("cellq").Find("Value").Find("Text").GetComponent<Text>().text = "";
                    tablaPrincipal.GetChild(i).Find("CellDescripciones").GetChild(j).Find("cellr").Find("Value").Find("Text").GetComponent<Text>().text = "";
                    tablaPrincipal.GetChild(i).Find("CellDescripciones").GetChild(j).Find("cells").Find("Value").Find("Text").GetComponent<Text>().text = "";
                    tablaPrincipal.GetChild(i).Find("CellDescripciones").GetChild(j).Find("cellt").Find("Value").Find("Text").GetComponent<Text>().text = "";
                    tablaPrincipal.GetChild(i).Find("CellDescripciones").GetChild(j).Find("cellu").Find("Value").Find("Text").GetComponent<Text>().text = "";
                    tablaPrincipal.GetChild(i).Find("CellDescripciones").GetChild(j).Find("cellv").Find("Value").Find("Text").GetComponent<Text>().text = "";
                    tablaPrincipal.GetChild(i).Find("CellDescripciones").GetChild(j).Find("cellw").Find("Value").Find("Text").GetComponent<Text>().text = "";
                }
            }
        }
        if(Ficha1Fisicos.empresasCompletadas == 1){
            int tamano = matrizRiesgosFisico.Length;
            MatrizRiesgosFisico[] matrizRiesgosFisico2 = new MatrizRiesgosFisico[tamano];
            for (int i = 0; i < matrizRiesgosFisico.Length; i++){
                matrizRiesgosFisico2[i] = matrizRiesgosFisico[i];
            }
            matrizFinalRiesgosFisicos.Add(matrizRiesgosFisico2);
            FlowControllerPhysical.data.setMatrizFinalRiesgosFisicos(matrizFinalRiesgosFisicos);
            FlowControllerPhysical.data.setMatrizRiesgos2emp();
        }
        else{
            matrizFinalRiesgosFisicos.Add(matrizRiesgosFisico);
            FlowControllerPhysical.data.setMatrizFinalRiesgosFisicos(matrizFinalRiesgosFisicos);
            FlowControllerPhysical.data.setMatrizRiesgos1emp();
        }


    }
    public void Save(){
        string errorMessage = "";
        bool isFull = true;
        if(matrizLlena()){
            errorMessage = "Para continuar debes diligenciar toda la información solicitada.";
            isFull = false;
        }
        if (!errorMessage.Equals("") && !isFull){
            alert.gameObject.SetActive(true);
            alertMessage.CreateAlertMessage(errorMessage); 
        }
        else{
            Debug.Log("Tabla llena");
            guardarMatriz();
            if(FlowControllerPhysical.dataPlayer.getFichaActual() == 4) resetFillSpaces();
            toIntro = true;
            Debug.Log("Va a imprimir matriz final de riesgos físicos");
            for (int i = 0; i < matrizFinalRiesgosFisicos[0].Length; i++){
                Debug.Log(JsonUtility.ToJson(matrizFinalRiesgosFisicos[0][i]));
            }
            //enableRisk();
            //matrizFinalRiesgosFisicos.Add(matrizRiesgosFisico);
            FlowControllerPhysical.data.setMatrizFinalRiesgosFisicos(matrizFinalRiesgosFisicos);
        }
        
    }

    public void resetFillSpaces(){
        int contadorLlenos = 0;
        if(!matrizLlena()){
            for (int i = 0; i < tablaPrincipal.GetChildCount(); i++){
                for (int j = 0; j < tablaPrincipal.GetChild(i).Find("CellDescripciones").GetChildCount(); j++){
                    if(tablaPrincipal.GetChild(i).gameObject.active){
                        contadorLlenos++;
                        InputField textoD = tablaPrincipal.GetChild(i).Find("CellDescripciones").GetChild(j).Find("celld").Find("Value").GetComponent<InputField>();
                        InputField textoQ = tablaPrincipal.GetChild(i).Find("CellDescripciones").GetChild(j).Find("cellq").Find("Value").GetComponent<InputField>();
                        InputField textoR = tablaPrincipal.GetChild(i).Find("CellDescripciones").GetChild(j).Find("cellr").Find("Value").GetComponent<InputField>();
                        InputField textoS = tablaPrincipal.GetChild(i).Find("CellDescripciones").GetChild(j).Find("cells").Find("Value").GetComponent<InputField>();
                        InputField textoT = tablaPrincipal.GetChild(i).Find("CellDescripciones").GetChild(j).Find("cellt").Find("Value").GetComponent<InputField>();
                        InputField textoU = tablaPrincipal.GetChild(i).Find("CellDescripciones").GetChild(j).Find("cellu").Find("Value").GetComponent<InputField>();
                        InputField textoV = tablaPrincipal.GetChild(i).Find("CellDescripciones").GetChild(j).Find("cellv").Find("Value").GetComponent<InputField>();
                        InputField textoW = tablaPrincipal.GetChild(i).Find("CellDescripciones").GetChild(j).Find("cellw").Find("Value").GetComponent<InputField>();
                        textoD.text = ""; 
                        textoQ.text = ""; 
                        textoR.text = ""; 
                        textoS.text = ""; 
                        textoT.text = ""; 
                        textoU.text = ""; 
                        textoV.text = ""; 
                        textoW.text = ""; 
                        Text textoD1 = tablaPrincipal.GetChild(i).Find("CellDescripciones").GetChild(j).Find("celld").Find("Value").Find("Text").GetComponent<Text>();
                        Text textoQ1 = tablaPrincipal.GetChild(i).Find("CellDescripciones").GetChild(j).Find("cellq").Find("Value").Find("Text").GetComponent<Text>();
                        Text textoR1 = tablaPrincipal.GetChild(i).Find("CellDescripciones").GetChild(j).Find("cellr").Find("Value").Find("Text").GetComponent<Text>();
                        Text textoS1 = tablaPrincipal.GetChild(i).Find("CellDescripciones").GetChild(j).Find("cells").Find("Value").Find("Text").GetComponent<Text>();
                        Text textoT1 = tablaPrincipal.GetChild(i).Find("CellDescripciones").GetChild(j).Find("cellt").Find("Value").Find("Text").GetComponent<Text>();
                        Text textoU1 = tablaPrincipal.GetChild(i).Find("CellDescripciones").GetChild(j).Find("cellu").Find("Value").Find("Text").GetComponent<Text>();
                        Text textoV1 = tablaPrincipal.GetChild(i).Find("CellDescripciones").GetChild(j).Find("cellv").Find("Value").Find("Text").GetComponent<Text>();
                        Text textoW1 = tablaPrincipal.GetChild(i).Find("CellDescripciones").GetChild(j).Find("cellw").Find("Value").Find("Text").GetComponent<Text>();
                        textoD1.text = ""; 
                        textoQ1.text = ""; 
                        textoR1.text = ""; 
                        textoS1.text = ""; 
                        textoT1.text = ""; 
                        textoU1.text = ""; 
                        textoV1.text = ""; 
                        textoW1.text = ""; 
                        Dropdown textoE = tablaPrincipal.GetChild(i).Find("CellDescripciones").GetChild(j).Find("celle").GetChild(0).GetComponent<Dropdown>();
                        Dropdown textoG = tablaPrincipal.GetChild(i).Find("CellDescripciones").GetChild(j).Find("cellg").GetChild(0).GetComponent<Dropdown>();
                        Dropdown textoJ = tablaPrincipal.GetChild(i).Find("CellDescripciones").GetChild(j).Find("cellj").GetChild(0).GetComponent<Dropdown>();
                        Dropdown textoK = tablaPrincipal.GetChild(i).Find("CellDescripciones").GetChild(j).Find("cellk").GetChild(0).GetComponent<Dropdown>();
                        Dropdown textoN = tablaPrincipal.GetChild(i).Find("CellDescripciones").GetChild(j).Find("celln").GetChild(0).GetComponent<Dropdown>();
                        Text textoI = tablaPrincipal.GetChild(i).Find("CellDescripciones").GetChild(j).Find("celli").Find("Value").Find("Placeholder").GetComponent<Text>();
                        Text textoM = tablaPrincipal.GetChild(i).Find("CellDescripciones").GetChild(j).Find("cellm").Find("Value").Find("Placeholder").GetComponent<Text>();
                        textoE.value = 0; 
                        textoG.value = 0; 
                        textoJ.value = 0; 
                        textoK.value = 0; 
                        textoN.value = 0; 
                        textoI.text = "- -"; 
                        textoM.text = "- -";
                        Text textoF = tablaPrincipal.GetChild(i).Find("CellDescripciones").GetChild(j).Find("cellf").Find("Value").Find("Placeholder").GetComponent<Text>();
                        Text textoH = tablaPrincipal.GetChild(i).Find("CellDescripciones").GetChild(j).Find("cellh").Find("Value").Find("Placeholder").GetComponent<Text>();
                        Text textoL = tablaPrincipal.GetChild(i).Find("CellDescripciones").GetChild(j).Find("celll").Find("Value").Find("Placeholder").GetComponent<Text>();
                        Text textoO = tablaPrincipal.GetChild(i).Find("CellDescripciones").GetChild(j).Find("cello").Find("Value").Find("Placeholder").GetComponent<Text>();
                        textoF.text = "";
                        textoH.text = "";
                        textoL.text = "";
                        textoO.text = "";
                    }
                }
            }
        }
        Debug.Log("Filas llenas: " + contadorLlenos);
        if(matrizLlena()){
            Debug.Log("Puede continuar 0 filas llenas.");
        }
        else Debug.Log("Siguen filas llenas.");
    }
    public void resetFicha4(){

        Transform tablaFicha4Scroll = transform.Find("Scroll (1)").GetChild(0);

        for (int i = 0; i < tablaFicha4Scroll.GetChildCount(); i++){
            tablaFicha4Scroll.GetChild(i).gameObject.SetActive(true);
            for (int j = 0; j < tablaFicha4Scroll.GetChild(i).GetChild(1).GetChildCount(); j++){
                tablaFicha4Scroll.GetChild(i).GetChild(1).GetChild(j).gameObject.SetActive(true);
            }

        }
        for (int i = 0; i < tablaPrincipal.GetChildCount(); i++){
            tablaPrincipal.GetChild(i).gameObject.SetActive(true);
            for (int j = 0; j < tablaPrincipal.GetChild(i).Find("CellDescripciones").GetChildCount(); j++){
                tablaPrincipal.GetChild(i).GetChild(1).GetChild(j).gameObject.SetActive(true);
                tablaPrincipal.GetChild(i).Find("CellDescripciones").GetChild(j).gameObject.SetActive(true);
            }

        }
        Transform tablaFicha4 = transform.Find("Scroll").Find("TableEvaluation");
        RectTransform dimTabla = tablaFicha4.GetComponent<RectTransform>();
        height = 6073;
        dimTabla.sizeDelta = new Vector2(width, height);
        for (int i = 0; i < tablaFicha4.GetChildCount(); i++){
            for (int j = 0; j < tablaFicha4.GetChild(i).GetChild(2).GetChildCount(); j++){
                Text textoA = tablaFicha4.GetChild(i).GetChild(0).GetChild(0).GetComponent<Text>();
                Text textoDesc = tablaFicha4.GetChild(i).GetChild(2).GetChild(j).GetChild(0).Find("Value").Find("Text").GetComponent<Text>();
                Text textoD = tablaFicha4.GetChild(i).Find("CellDescripciones").GetChild(j).Find("celld").Find("Value").Find("Text").GetComponent<Text>();
                Text textoQ = tablaFicha4.GetChild(i).Find("CellDescripciones").GetChild(j).Find("cellq").Find("Value").Find("Text").GetComponent<Text>();
                Text textoR = tablaFicha4.GetChild(i).Find("CellDescripciones").GetChild(j).Find("cellr").Find("Value").Find("Text").GetComponent<Text>();
                Text textoS = tablaFicha4.GetChild(i).Find("CellDescripciones").GetChild(j).Find("cells").Find("Value").Find("Text").GetComponent<Text>();
                Text textoT = tablaFicha4.GetChild(i).Find("CellDescripciones").GetChild(j).Find("cellt").Find("Value").Find("Text").GetComponent<Text>();
                Text textoU = tablaFicha4.GetChild(i).Find("CellDescripciones").GetChild(j).Find("cellu").Find("Value").Find("Text").GetComponent<Text>();
                Text textoV = tablaFicha4.GetChild(i).Find("CellDescripciones").GetChild(j).Find("cellv").Find("Value").Find("Text").GetComponent<Text>();
                Text textoW = tablaFicha4.GetChild(i).Find("CellDescripciones").GetChild(j).Find("cellw").Find("Value").Find("Text").GetComponent<Text>();               
                textoA.text = "Area n";
                textoDesc.text = "";
                textoD.text = "";
                textoQ.text = "";
                textoR.text = "";
                textoS.text = ""; 
                textoT.text = "";
                textoU.text = ""; 
                textoV.text = "";
                textoW.text = ""; 
                Text textoDescr = tablaFicha4.GetChild(i).GetChild(2).GetChild(j).GetChild(0).Find("Value").Find("Placeholder").GetComponent<Text>();
                Text textoDc = tablaFicha4.GetChild(i).Find("CellDescripciones").GetChild(j).Find("celld").Find("Value").Find("Text").GetComponent<Text>();
                Text textoQc = tablaFicha4.GetChild(i).Find("CellDescripciones").GetChild(j).Find("celld").Find("Value").Find("Text").GetComponent<Text>();
                Text textoRc = tablaFicha4.GetChild(i).Find("CellDescripciones").GetChild(j).Find("cellr").Find("Value").Find("Text").GetComponent<Text>();
                Text textoSc = tablaFicha4.GetChild(i).Find("CellDescripciones").GetChild(j).Find("cells").Find("Value").Find("Text").GetComponent<Text>();
                Text textoTc = tablaFicha4.GetChild(i).Find("CellDescripciones").GetChild(j).Find("cellt").Find("Value").Find("Text").GetComponent<Text>();
                Text textoUc = tablaFicha4.GetChild(i).Find("CellDescripciones").GetChild(j).Find("cellu").Find("Value").Find("Text").GetComponent<Text>();
                Text textoVc = tablaFicha4.GetChild(i).Find("CellDescripciones").GetChild(j).Find("cellv").Find("Value").Find("Text").GetComponent<Text>();
                Text textoWc = tablaFicha4.GetChild(i).Find("CellDescripciones").GetChild(j).Find("cellw").Find("Value").Find("Text").GetComponent<Text>();               
                textoDescr.text = "";
                textoDc.text = "";
                textoQc.text = "";
                textoRc.text = "";
                textoSc.text = ""; 
                textoTc.text = "";
                textoUc.text = ""; 
                textoVc.text = "";
                textoWc.text = "";                    
                Text textoI = tablaFicha4.GetChild(i).Find("CellDescripciones").GetChild(j).Find("celli").Find("Value").Find("Placeholder").GetComponent<Text>();
                Text textoM = tablaFicha4.GetChild(i).Find("CellDescripciones").GetChild(j).Find("cellm").Find("Value").Find("Placeholder").GetComponent<Text>();
                Text textoO = tablaFicha4.GetChild(i).Find("CellDescripciones").GetChild(j).Find("cello").Find("Value").Find("Placeholder").GetComponent<Text>();
                textoI.text = "- -"; 
                textoM.text = "- -";
                textoO.text = "- -";
            }
        }
        Debug.Log("Terminó de reiniciar la tabla");
    }

    public void finishHim(GameObject gameObjectC){
        Debug.Log("Faltan datos en la matriz? ->" + matrizLlena());
        Debug.Log("Ficha Actual: " + FlowControllerPhysical.dataPlayer.getFichaActual());
        if(!matrizLlena()){
            if(FlowControllerPhysical.dataPlayer.getFichaActual() == 8){
                Debug.Log("Créditooooosss!!!");
                gameObjectC.SetActive(true);
                FlowControllerPhysical.printDataPlayer();
            }
            else{
                FlowControllerPhysical.dataPlayer.setFichaActual(5);
                Debug.Log(FlowControllerPhysical.dataPlayer.getFichaActual());
                Debug.Log("Matriz Llena: " + matrizLlena());
                Debug.Log("Ficha Actual: " + FlowControllerPhysical.dataPlayer.getFichaActual());
            }
        }
    }
}
