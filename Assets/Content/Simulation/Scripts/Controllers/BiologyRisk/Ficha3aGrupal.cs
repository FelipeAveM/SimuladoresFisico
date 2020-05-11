using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class Ficha3aGrupal : MonoBehaviour {

    public UnityEvent saveCard;
    public DataBuilder dataBuilder;

    public class Hygiene : BaseFieldInput { }
    public class DC : BaseFieldInput { }
    public class TC : BaseFieldInput { }
    public class Calculation : BaseFieldInput { }
    public class Assessment : BaseFieldInput { }
    public class Calculation3 : BaseFieldInput { }

    List<Hygiene> hygieneList;
    List<DC> dCList;
    List<TC> tCList;
    List<Calculation> calculationList;
    List<Assessment> assessmentList;
    List<Calculation3> calculation3List;

    void Start()
    {
        hygieneList = new List<Hygiene>();
        dCList = new List<DC>();
        tCList = new List<TC>();
        calculationList = new List<Calculation>();
        assessmentList = new List<Assessment>();
        calculation3List = new List<Calculation3>();

        LoadInfo();
    }

    void LoadInfo()
    {
        List<File1> file1 = dataBuilder.dataRisk.File1;
		List<File2> file2 = dataBuilder.dataRisk.File2;
        List<FileEvaluation> fileEvaluation = dataBuilder.dataRisk.FileEvaluation;
		Transform answer = transform.Find("Question");
        Transform tableA = transform.Find("Preinfo");
        Transform tableB = transform.Find("TableEvaluation");

		float percentage = 0;
		for(int i = 0; i<file2.Count; i++){
			percentage += file2[i].Score;
		}
		percentage/=file2.Count;
		answer.GetChild(0).GetChild(0).GetComponent<Text>().text = percentage.ToString("F2") + "%";

        for (int i = 1; i < tableA.childCount; i++)
        {
            Transform childA = tableA.GetChild(i);
            Transform childB = tableB.GetChild(i);

            if (file1 != null && file1.Count > 0)
            {
                childA.GetChild(0).GetChild(0).GetComponent<Text>().text = file1[i - 1].Risk;
                childB.GetChild(0).GetChild(0).GetComponent<Text>().text = file1[i - 1].Risk;
            }
            FileEvaluation currentFile = fileEvaluation[i - 1];
            CalculateRiskCorrection(currentFile, childA, childB);
            Dropdown messuresHygienic = childA.GetChild(1).GetChild(0).GetComponent<Dropdown>();
         
            messuresHygienic.onValueChanged.AddListener(
                delegate {
                    CalculateRiskCorrection(currentFile, childA, childB);
                 });
        }
    }

    void CalculateRiskCorrection(FileEvaluation fileEvaluation, Transform itemA, Transform itemB)
    {
        // Table A
        Dropdown messuresHygienic = itemA.GetChild(1).GetChild(0).GetComponent<Dropdown>();
        Text dcLabel = itemA.GetChild(2).GetChild(0).GetComponent<Text>();
        Text tcLabel = itemA.GetChild(3).GetChild(0).GetComponent<Text>();
        Text levelRiskCorrectionLabel = itemA.GetChild(4).GetChild(0).GetComponent<Text>();

        // Table B        
        Text levelRiskLabel = itemB.GetChild(1).GetChild(0).GetComponent<Text>();
        Text levelRiskCorrectionLabelB = itemB.GetChild(2).GetChild(0).GetComponent<Text>();

        int dc = fileEvaluation.D - messuresHygienic.value;
        if (dc <= 0)
            dc = 1;

        int tc = fileEvaluation.T - messuresHygienic.value;

        dcLabel.text = dc.ToString();
        tcLabel.text = tc.ToString();   

        //Rc = (Dc * V) + (Tc + I + F)
        int levelRisk = (dc * fileEvaluation.V) + tc + fileEvaluation.I + fileEvaluation.F;
        levelRiskCorrectionLabel.text = levelRisk.ToString();

        levelRiskLabel.text = fileEvaluation.LevelOfRisk.ToString();
        levelRiskCorrectionLabelB.text = levelRisk.ToString();
    }


    public void Save()
    {
        saveCard.Invoke();
        List<FileEvaluatiomAnnexed> fileList = new List<FileEvaluatiomAnnexed>();

        Transform tableA = transform.Find("Preinfo");
        Transform tableB = transform.Find("TableEvaluation");

        for (int i = 1; i < tableB.childCount; i++)
        {
            FileEvaluatiomAnnexed file = new FileEvaluatiomAnnexed();

            Transform childA = tableA.GetChild(i);
            Transform childB = tableB.GetChild(i);

            // Table A

            Text riskLabel = childA.GetChild(0).GetChild(0).GetComponent<Text>();
            Dropdown messuresLabel = childA.GetChild(1).GetChild(0).GetComponent<Dropdown>();
            Text dcLabel = childA.GetChild(2).GetChild(0).GetComponent<Text>();
            Text tcLabel = childA.GetChild(3).GetChild(0).GetComponent<Text>();
            Text riskCorrectionLabel = childA.GetChild(4).GetChild(0).GetComponent<Text>();

            file.Risk = riskLabel.text;
            file.Messureshygienics = messuresLabel.options[messuresLabel.value].text;
            file.Dc = int.Parse(dcLabel.text);
            file.Tc = int.Parse(tcLabel.text);
            file.LevelOfRiskCorrection = int.Parse(riskCorrectionLabel.text);

            // Table B

            Dropdown interpretationDropdown = childB.GetChild(3).GetChild(0).GetComponent<Dropdown>();
            Text riskLevelLabel = childB.GetChild(1).GetChild(0).GetComponent<Text>();

            file.LevelOfRisk = int.Parse(riskLevelLabel.text);
            file.Interpretation = interpretationDropdown.options[interpretationDropdown.value].text;

            fileList.Add(file);

        }
        dataBuilder.AddEvaluationGrupalAnnexed(fileList);
    }
}
