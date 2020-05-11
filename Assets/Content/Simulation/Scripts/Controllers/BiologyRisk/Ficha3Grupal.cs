using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public enum RowLabel
{
    PersonAfefcted,
    Sickness,
    Hygiene,
    DC,
    TC,
    Calculation,
    Assessment,
    Calculation3b,
    ND,
    IND,
    NE,
    INE,
    NP,
    INP,
    NC,
    INC
}

public class BaseFieldInput
{
    public string risk;
    public string value;
}

public class Ficha3Grupal : MonoBehaviour {
    public UnityEvent activeNext;
    public UnityEvent deactiveNext;
    public UnityEvent saveCard;

    public DataBuilder dataBuilder;

    public class NE : BaseFieldInput { }
    public class NC : BaseFieldInput { }
    public class NP : BaseFieldInput { }
    public class ND : BaseFieldInput { }
    public class INE : BaseFieldInput { }
    public class INC : BaseFieldInput { }
    public class INP : BaseFieldInput { }
    public class IND : BaseFieldInput { }

    private List<NE> neList;
    private List<NC> ncList;
    private List<NP> npList;
    private List<ND> ndList;
    private List<INE> ineList;
    private List<INC> incList;
    private List<INP> inpList;
    private List<IND> indList;

    void Start()
    {
        neList = new List<NE>();
        ncList = new List<NC>();
        npList = new List<NP>();
        ndList = new List<ND>();
        ineList = new List<INE>();
        incList = new List<INC>();
        inpList = new List<INP>();
        indList = new List<IND>();

        LoadInfo();
    }

    void LoadInfo()
    {
        List<File1> file1 = dataBuilder.dataRisk.File1;
    
        Transform table = transform.Find("TableEvaluation");

        for (int i = 1; i < table.childCount; i++)
        {
            Transform child = table.GetChild(i);
            if (file1 != null && file1.Count > 0)
            {
                child.GetChild(0).GetChild(0).GetComponent<Text>().text = file1[i - 1].Risk;
            }
            InputField Input1 = child.GetChild(1).GetChild(0).GetComponent<InputField>();
            InputField Input2 = child.GetChild(2).GetChild(0).GetComponent<InputField>();
            InputField Input3 = child.GetChild(3).GetChild(0).GetComponent<InputField>();
            InputField Input4 = child.GetChild(4).GetChild(0).GetComponent<InputField>();
            InputField Input5 = child.GetChild(5).GetChild(0).GetComponent<InputField>();
            InputField Input6 = child.GetChild(6).GetChild(0).GetComponent<InputField>();
            InputField Input7 = child.GetChild(7).GetChild(0).GetComponent<InputField>();
            InputField Input8 = child.GetChild(8).GetChild(0).GetComponent<InputField>();

            Input1.onValueChanged.AddListener(delegate { EndEdit(Input1, RowLabel.ND); });
            Input2.onValueChanged.AddListener(delegate { EndEdit(Input2, RowLabel.IND); });
            Input3.onValueChanged.AddListener(delegate { EndEdit(Input3, RowLabel.NE); });
            Input4.onValueChanged.AddListener(delegate { EndEdit(Input4, RowLabel.INE); });
            Input5.onValueChanged.AddListener(delegate { EndEdit(Input5, RowLabel.NP); });
            Input6.onValueChanged.AddListener(delegate { EndEdit(Input6, RowLabel.INP); });
            Input7.onValueChanged.AddListener(delegate { EndEdit(Input7, RowLabel.NC); });
            Input8.onValueChanged.AddListener(delegate { EndEdit(Input8, RowLabel.INC); });
        } 
    }

    public void EndEdit(InputField inText, RowLabel rowLabel)
    {
        string value = inText.text;
        string risk = inText.transform.parent.parent.GetChild(0).GetChild(0).GetComponent<Text>().text;
            
        int index = 0;

        if (value != null)
        {
            switch (rowLabel) {
                case RowLabel.ND:
                    index = ndList.FindIndex(obj => obj.risk == risk);
                    ND nd = new ND();
                    nd.risk = risk;
                    nd.value = value;
                    if (index < 0)
                    {
                        ndList.Add(nd);
                    }
                    else
                    {
                        ndList[index] = nd;
                    }
                    if (value.Trim() == "")
                    {
                        ndList.Remove(nd);
                    }

                    break;
                case RowLabel.NE:
                    index = neList.FindIndex(obj => obj.risk == risk);
                    NE ne = new NE();
                    ne.risk = risk;
                    ne.value = value;
                    if (index < 0)
                    {
                        neList.Add(ne);
                    }
                    else
                    {
                        neList[index] = ne;
                    }
                    if (value.Trim() == "")
                    {
                        neList.Remove(ne);
                    }

                    break;
                case RowLabel.NP:
                    index = npList.FindIndex(obj => obj.risk == risk);
                    NP np = new NP();
                    np.risk = risk;
                    np.value = value;
                    if (index < 0)
                    {
                        npList.Add(np);
                    }
                    else
                    {
                        npList[index] = np;
                    }
                    if (value.Trim() == "")
                    {
                        npList.Remove(np);
                    }

                    break;
                case RowLabel.NC:
                    index = ncList.FindIndex(obj => obj.risk == risk);
                    NC nc = new NC();
                    nc.risk = risk;
                    nc.value = value;
                    if (index < 0)
                    {
                        ncList.Add(nc);
                    }
                    else
                    {
                        ncList[index] = nc;
                    }
                    if (value.Trim() == "")
                    {
                        ncList.Remove(nc);
                    }

                    break;
                case RowLabel.IND:
                    index = indList.FindIndex(obj => obj.risk == risk);
                    IND ind = new IND();
                    ind.risk = risk;
                    ind.value = value;
                    if (index < 0)
                    {
                        indList.Add(ind);
                    }
                    else
                    {
                        indList[index] = ind;
                    }
                    if (value.Trim() == "")
                    {
                        indList.Remove(ind);
                    }

                    break;
                case RowLabel.INE:
                    index = ineList.FindIndex(obj => obj.risk == risk);
                    INE ine = new INE();
                    ine.risk = risk;
                    ine.value = value;
                    if (index < 0)
                    {
                        ineList.Add(ine);
                    }
                    else
                    {
                        ineList[index] = ine;
                    }
                    if (value.Trim() == "")
                    {
                        ineList.Remove(ine);
                    }

                    break;
                case RowLabel.INP:
                    index = inpList.FindIndex(obj => obj.risk == risk);
                    INP inp = new INP();
                    inp.risk = risk;
                    inp.value = value;
                    if (index < 0)
                    {
                        inpList.Add(inp);
                    }
                    else
                    {
                        inpList[index] = inp;
                    }
                    if (value.Trim() == "")
                    {
                        inpList.Remove(inp);
                    }

                    break;
                case RowLabel.INC:
                    index = incList.FindIndex(obj => obj.risk == risk);
                    INC inc = new INC();
                    inc.risk = risk;
                    inc.value = value;
                    if (index < 0)
                    {
                        incList.Add(inc);
                    }
                    else
                    {
                        incList[index] = inc;
                    }
                    if (value.Trim() == "")
                    {
                        incList.Remove(inc);
                    }

                    break;
            }
        }

        CheckNextStep();
    }   

    void CheckNextStep()
    {

        print("Count neList : " + neList.Count);
        print("Count ncList : " + ncList.Count);
        print("Count npList : " + npList.Count);
        print("Count ndList : " + ndList.Count);
        print("Count ineList : " + ineList.Count);
        print("Count incList : " + incList.Count);
        print("Count inpList : " + inpList.Count);
        print("Count indList : " + indList.Count);

        if (neList.Count == 4 &&
            ncList.Count == 4 &&
            npList.Count == 4 &&
            ndList.Count == 4 &&    
            ineList.Count == 4 &&
            incList.Count == 4 &&
            inpList.Count == 4 &&
            indList.Count == 4 )
        {
            activeNext.Invoke();

        }
        else
        {
            deactiveNext.Invoke();
        }
    }

    public void Save()
    {
        saveCard.Invoke();
        List<FileEvaluation> fileList = new List<FileEvaluation>();

        Transform tableA = transform.Find("Preinfo");
        Transform tableB = transform.Find("TableEvaluation");

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
            InputField sickLabel = childB.GetChild(1).GetChild(0).GetComponent<InputField>();
            Dropdown dropdownD = childB.GetChild(2).GetChild(0).GetComponent<Dropdown>();
            Dropdown dropdownT = childB.GetChild(3).GetChild(0).GetComponent<Dropdown>();
            Dropdown dropdownI = childB.GetChild(4).GetChild(0).GetComponent<Dropdown>();
            Dropdown dropdownV = childB.GetChild(5).GetChild(0).GetComponent<Dropdown>();
            Dropdown dropdownF = childB.GetChild(6).GetChild(0).GetComponent<Dropdown>();
            Text levelRiskLabel = childB.GetChild(7).GetChild(0).GetComponent<Text>();

            file.LevelOfRisk = int.Parse(levelRiskLabel.text);
            file.Sickness = sickLabel.text;
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
