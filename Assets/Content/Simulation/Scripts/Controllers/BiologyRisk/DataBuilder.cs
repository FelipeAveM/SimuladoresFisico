using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataBuilder : MonoBehaviour {

	public DataRisk dataRisk;
    private FlowController flowController;

	void Start(){
        flowController = transform.GetComponent<FlowController>();
        //LECTURA DE ARCHIVOS DESDE LA BASE DE DATOS, ANTES DE INICIAR SIMULACION
        if(GameObject.Find("SceneController")!=null)
            dataRisk = GameObject.Find("SceneController").GetComponent<URLParameterReader>().dataRisk;
        else
            dataRisk.Usuario = GameObject.Find("SceneController").GetComponent<URLParameterReader>().usuario;
        flowController.index = dataRisk.index;
    }

    public void AddRisk(string risk,RiskCardGrupalObject riskCardObject)
    {
        File1 file = new File1();
        file.Risk = risk;
        dataRisk.AddFileRisk(file);
        //print (JsonUtility.ToJson(dataRisk));
    }

    public void AddRisk(string risk, string characterization, string decriptionGTC)
    {
        File1 file = new File1();
        file.Risk = risk;
        file.Characterization = characterization;
        file.DecriptionGTC = decriptionGTC;

        dataRisk.AddFileRisk(file);
        //print (JsonUtility.ToJson(dataRisk));
    }

    public void AddRisk(string risk, string classification, RiskCardObject riskCardObject){
		string response = riskCardObject.AnswerClassification.ToString ().Replace("_"," ");
		File1 file = new File1 ();
		file.Risk = risk;
		file.Clasification = classification;
		file.LevelOfRisk = riskCardObject.LevelOfRisk;
		file.LevelOfRiskValid = riskCardObject.LevelOfRiskValid;
		file.Control = riskCardObject.Control;
		file.ControlClassification = riskCardObject.ControlClassification;
		file.ControlsValid = riskCardObject.ControlsValid;
		file.ControlClassificationValids = riskCardObject.ControlClassificationValids;

		//OJO, SE MODIFICÓ PARA EVITAR MULTIPLICAR CADA VEZ POR 10 EL RESULTADO EN RESULT Y EN EVALUATION
		float score = 0.0f;
		if(classification.Equals (response)){
			switch(flowController.simulator){
			case 0: score = 1.429f; break;
			case 1: score = 1.429f; break; 
			case 2: score = 1.429f; break;
			}
		}
		file.Score = score; 
		dataRisk.AddFileRisk(file);
		//print (JsonUtility.ToJson(dataRisk));
	}

	public void AddRisk3C(string risk, int presentacion, string medicion, string permitido, int limite)
	{
		File3CChemistry file = new File3CChemistry();
		file.Risk = risk;
		file.Presentacion = presentacion;
		file.Medicion = medicion;
		file.Permitido = permitido;
		file.Limite = limite;

		dataRisk.AddFileRisk3C(file);
		//print (JsonUtility.ToJson(dataRisk));
	}

	public void AddWorker(File2 file){
		dataRisk.AddFileWorkers(file);
		//print (JsonUtility.ToJson(dataRisk));
	}

	public void AddEvaluation(List<File3> fileList){
		dataRisk.File3 = fileList;
		//print (JsonUtility.ToJson(dataRisk));
	}

	public void AddTable(List<File4> fileList){		
		dataRisk.File4  = fileList;
		//print (JsonUtility.ToJson(dataRisk));
	}

	public void AddControls(List<File5> fileList){
		dataRisk.File5  = fileList;		
		//print (JsonUtility.ToJson(dataRisk));
	}

    public void AddEvaluationGrupal(List<FileEvaluation> fileList)
    {
        dataRisk.FileEvaluation = fileList;
        //print (JsonUtility.ToJson(dataRisk));
    }

    public void AddEvaluationGrupalAnnexed(List<FileEvaluatiomAnnexed> fileList)
    {
        dataRisk.FileEvaluatiomAnnexed = fileList;
        //print(JsonUtility.ToJson(dataRisk));
    }

	public void AddEvaluationSecurityGrupal(List<FileEvaluationSecurity> fileList)
	{
		dataRisk.FileEvaluationSecurity = fileList;
		//print (JsonUtility.ToJson(dataRisk));
	}

	public void AddEvaluationChemistry3A(List<File3AChemistry> fileList)
	{
		dataRisk.FileEvaluation3AChemistry = fileList;
		//print (JsonUtility.ToJson(dataRisk));
	}

	public void AddEvaluationChemistry3B(List<File3BChemistry> fileList)
	{
		dataRisk.FileEvaluation3BChemistry = fileList;
		//print (JsonUtility.ToJson(dataRisk));
	}

	public void AddEvaluationChemistry3C(List<File3CChemistry> fileList)
	{
		dataRisk.FileEvaluation3CChemistry = fileList;
		//print (JsonUtility.ToJson(dataRisk));
	}

    public void AddControlsGrupal (List<FileControls> fileList)
    {
        dataRisk.FileControls = fileList;
    }

    public string GetDataRisk(){
		return JsonUtility.ToJson (dataRisk);
	}

    public void SetIndex(int extIndex)
    {
        dataRisk.index = extIndex;
    }
}
