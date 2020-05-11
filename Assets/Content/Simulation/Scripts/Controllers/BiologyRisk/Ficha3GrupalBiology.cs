using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class Ficha3GrupalBiology : MonoBehaviour
{
	private AlertMessage alertMessage;
	private List<SICKNESS>  sicknessList = new List<SICKNESS>();
	private int cantPersonas;

	public Transform alert;
    public DataBuilder dataBuilder;
    public UnityEvent saveCard;
    public class SICKNESS : BaseFieldInput { }
   

    void Start()
    {
        sicknessList = new List<SICKNESS>();
		alertMessage = alert.GetComponent<AlertMessage>();
		cantPersonas = 0;
        LoadInfo();
    }

    void LoadInfo()
    {
        List<File1> file1 = dataBuilder.dataRisk.File1;

		Transform question = transform.Find("Question");
        Transform tableA = transform.Find("Preinfo");
        Transform tableB = transform.Find("TableEvaluation");

        for (int i = 1; i < tableA.childCount; i++)
        {
            // Table A
            Transform childA = tableA.GetChild(i);
            if (file1 != null && file1.Count > 0)
            {
                childA.GetChild(0).GetChild(0).GetComponent<Text>().text = file1[i - 1].Risk;
            }
            InputField CountPersonInput = childA.GetChild(1).GetChild(0).GetComponent<InputField>();
			InputField localCantPersonas = question.GetChild(0).GetChild(0).GetComponent<InputField>();
            Text incidenceRate = childA.GetChild(2).GetChild(0).GetComponent<Text>();

			CountPersonInput.onValueChanged.AddListener(delegate { SetIncidenceRate(CountPersonInput, incidenceRate, localCantPersonas); });
			localCantPersonas.onValueChanged.AddListener(delegate { SetIncidenceRate(CountPersonInput, incidenceRate, localCantPersonas); });
            // Table B
            Transform childB = tableB.GetChild(i);
            if (file1 != null && file1.Count > 0)
            {
                childB.GetChild(0).GetChild(0).GetComponent<Text>().text = file1[i - 1].Risk;
            }

            InputField sickness = childB.GetChild(1).GetChild(0).GetComponent<InputField>();
            sickness.onValueChanged.AddListener(delegate { AddSickness(sickness); });

            Dropdown d = childB.GetChild(2).GetChild(0).GetComponent<Dropdown>();
            Dropdown t = childB.GetChild(3).GetChild(0).GetComponent<Dropdown>();
            Dropdown vI = childB.GetChild(4).GetChild(0).GetComponent<Dropdown>();
            Dropdown v = childB.GetChild(5).GetChild(0).GetComponent<Dropdown>();
            Dropdown f = childB.GetChild(6).GetChild(0).GetComponent<Dropdown>();
            Text riskLabel = childB.GetChild(7).GetChild(0).GetComponent<Text>();

            CalculateRisk(d, t, vI, v, f, riskLabel);
            d.onValueChanged.AddListener(delegate { CalculateRisk(d, t, vI, v, f, riskLabel); });
            t.onValueChanged.AddListener(delegate { CalculateRisk(d, t, vI, v, f, riskLabel); });
            vI.onValueChanged.AddListener(delegate { CalculateRisk(d, t, vI, v, f, riskLabel); });
            v.onValueChanged.AddListener(delegate { CalculateRisk(d, t, vI, v, f, riskLabel); });
            f.onValueChanged.AddListener(delegate { CalculateRisk(d, t, vI, v, f, riskLabel); });
        }
    }

	public void SetIncidenceRate(InputField input, Text label, InputField cant)
    {
        float incedenceRate = 0;
        float countPerson;
        if (!string.IsNullOrEmpty(input.text))
            countPerson = int.Parse(input.text);
        else
            countPerson = 0;

        if (!string.IsNullOrEmpty(cant.text))
            cantPersonas = int.Parse(cant.text);
        else
            cantPersonas = 0;

		/* //LIMITAR CON UN VALOR SUPERIOR EL MAXIMO DE INCIDENCIA EN PORCENTAJE
        if (countPerson > cantPersonas)
        {
            countPerson = cantPersonas;
            input.text = countPerson.ToString();
        }
        */

        if (countPerson < 0)
        {
            countPerson = 0;
            input.text = countPerson.ToString();
        }

		if(cantPersonas>0){
			incedenceRate = (countPerson / cantPersonas) * 100;
        	label.text = incedenceRate.ToString("F2") + "%";
		}else{
			incedenceRate = 0;
			label.text = "NA%";
		}
    }


    public void CalculateRisk(Dropdown d, Dropdown t, Dropdown i, Dropdown v, Dropdown f, Text riskLabel)
    {
        //R = (D x V) + T + I + F
        float riskLevel = ((d.value + 1) * (v.value + 1)) + (t.value + 1) + (i.value + 1) + (f.value + 1);
        riskLabel.text = riskLevel.ToString();
    }

    public void AddSickness(InputField sickness)
    {
        string value = sickness.text;
        string risk = sickness.transform.parent.parent.GetChild(0).GetChild(0).GetComponent<Text>().text;
        int index = sicknessList.FindIndex(obj => obj.risk == risk);
        // create object sickness
        SICKNESS sicknessObj = new SICKNESS();
        sicknessObj.risk = risk;
        sicknessObj.value = value;

        if (index < 0)
        {
            sicknessList.Add(sicknessObj);
        }
        else
        {
            sicknessList[index] = sicknessObj;
        }
        if (value.Trim() == "")
        {
            sicknessList.Remove(sicknessObj);
        }
    }

    public void Save()
    {
        List<FileEvaluation> fileList = new List<FileEvaluation>();

		string errorMessage = "";

		Transform question = transform.Find("Question");
		Transform tableA = transform.Find("Preinfo");
        Transform tableB = transform.Find("TableEvaluation");

		if(cantPersonas==0){
			errorMessage = "Para continuar debes diligenciar toda la información solicitada.";
		}
		else{
			for (int i = 1; i < tableB.childCount; i++)
			{
				Transform childB = tableB.GetChild(i);
				InputField sickness = childB.GetChild(1).GetChild(0).GetComponent<InputField>();
				if(sickness.text==""){
					errorMessage = "Para continuar debes diligenciar toda la información solicitada.";
				}
			}
		}
			
		if (!errorMessage.Equals(""))
		{
			alert.gameObject.SetActive(true);
			alertMessage.CreateAlertMessage(errorMessage);
		}
		else {
			saveCard.Invoke();
			// get all data 
	        for (int i = 1; i < tableB.childCount; i++)
	        {
	            FileEvaluation file = new FileEvaluation();

	            Transform childA = tableA.GetChild(i);
	            Transform childB = tableB.GetChild(i);

	            // Table A

	            Text riskLabel = childA.GetChild(0).GetChild(0).GetComponent<Text>();
	            InputField affectedPeople = childA.GetChild(1).GetChild(0).GetComponent<InputField>();
	            Text inidentLabel = childA.GetChild(2).GetChild(0).GetComponent<Text>();

	            file.Risk = riskLabel.text;
	            file.NumberOfAfected = int.Parse(affectedPeople.text);
	            file.RageIncidence = inidentLabel.text;

	            // Table B
	            Dropdown dropdownD = childB.GetChild(2).GetChild(0).GetComponent<Dropdown>();
	            Dropdown dropdownT = childB.GetChild(3).GetChild(0).GetComponent<Dropdown>();
	            Dropdown dropdownI = childB.GetChild(4).GetChild(0).GetComponent<Dropdown>();
	            Dropdown dropdownV = childB.GetChild(5).GetChild(0).GetComponent<Dropdown>();
	            Dropdown dropdownF = childB.GetChild(6).GetChild(0).GetComponent<Dropdown>();
	            InputField sickLabel = childB.GetChild(1).GetChild(0).GetComponent<InputField>();
	            Text levelRiskLabel = childB.GetChild(7).GetChild(0).GetComponent<Text>();

	            file.LevelOfRisk = int.Parse(levelRiskLabel.text);
	            file.Sickness = childB.GetChild(1).GetChild(0).GetComponent<InputField>().text;

	            file.D = dropdownD.value + 1;
	            file.T = dropdownT.value + 1;
	            file.I = dropdownI.value + 1;
	            file.V = dropdownV.value + 1;
	            file.F = dropdownF.value + 1;

	            fileList.Add(file);

	        }
	        dataBuilder.AddEvaluationGrupal(fileList);
		}
    }
}
