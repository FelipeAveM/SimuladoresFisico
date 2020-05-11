                                                                                                                                                                                                                                                                                                                                    using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class Ficha3GrupalSeguridad : MonoBehaviour {
	private AlertMessage alertMessage;

	public Transform alert;
	public DataBuilder dataBuilder;
	public UnityEvent saveCard;


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

		alertMessage = alert.GetComponent<AlertMessage>();

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
    }   

    bool CheckNextStep()
    {

        print("Count neList : " + neList.Count);
        print("Count ncList : " + ncList.Count);
        print("Count npList : " + npList.Count);
        print("Count ndList : " + ndList.Count);
        print("Count ineList : " + ineList.Count);
        print("Count incList : " + incList.Count);
        print("Count inpList : " + inpList.Count);
        print("Count indList : " + indList.Count);

        if (neList.Count == 7 &&
            ncList.Count == 7 &&
            npList.Count == 7 &&
            ndList.Count == 7 &&    
            ineList.Count == 7 &&
            incList.Count == 7 &&
            inpList.Count == 7 &&
            indList.Count == 7 )
        {
			return true;
        }
        else
        {
			string errorMessage = "Para continuar debes diligenciar toda la información solicitada.";
			alert.gameObject.SetActive(true);
			alertMessage.CreateAlertMessage(errorMessage);
			return false;
        }
    }

    public void Save()
    {
		if(CheckNextStep()){
			saveCard.Invoke();
	        List<FileEvaluationSecurity> fileList = new List<FileEvaluationSecurity>();

	        Transform table = transform.Find("TableEvaluation");

	        for (int i = 1; i < table.childCount; i++)
	        {
				FileEvaluationSecurity file = new FileEvaluationSecurity();

	            Transform child = table.GetChild(i);

	            InputField ND = child.GetChild(1).GetChild(0).GetComponent<InputField>();
				InputField IND = child.GetChild(2).GetChild(0).GetComponent<InputField>();
				InputField NE = child.GetChild(3).GetChild(0).GetComponent<InputField>();
				InputField INE = child.GetChild(4).GetChild(0).GetComponent<InputField>();
				InputField NP = child.GetChild(5).GetChild(0).GetComponent<InputField>();
				InputField INP = child.GetChild(6).GetChild(0).GetComponent<InputField>();
				InputField NC = child.GetChild(7).GetChild(0).GetComponent<InputField>();
				InputField INC = child.GetChild(8).GetChild(0).GetComponent<InputField>();
				InputField NR = child.GetChild(9).GetChild(0).GetComponent<InputField>();
				InputField INR = child.GetChild(10).GetChild(0).GetComponent<InputField>();

	            file.ND = int.Parse(ND.text);
	            file.IND = IND.text;
				file.NE = int.Parse(NE.text);
				file.INE = INE.text;
				file.NP = int.Parse(NP.text);
				file.INP = INP.text;
				file.NC = int.Parse(NC.text);
				file.INC = INC.text;
				file.NR = int.Parse(NR.text);
				file.INR = INR.text;

	            fileList.Add(file);

	        }
			dataBuilder.AddEvaluationSecurityGrupal(fileList);
		}
    }
}
