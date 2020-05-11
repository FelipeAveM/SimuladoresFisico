using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class RiskCardGrupal : MonoBehaviour {

    private RectTransform item;
    private AlertMessage alertMessage;
    private DataBuilder dataBuilder;
    private FlowController flowController;

    public Transform prefabRiskCard;
	public Transform prefabMedidor;
    public FmodHandWriting handWriting;
    public Transform GameControl;
	public Transform alert;
    public Text riskCountLabel;
    public bool playStory = true;
    public int story;
	public int story2;
    public UnityEvent createCard;
    public UnityEvent closeCard;
    public UnityEvent saveCard;
    public UnityEvent saveAllCards;

	void Start(){
        flowController = GameControl.GetComponent<FlowController>();
        dataBuilder = GameControl.GetComponent<DataBuilder>();
        alertMessage = alert.GetComponent<AlertMessage>();
    }

    public void CreateCard(RiskCardGrupalObject obj)
    {
        createCard.Invoke();
        gameObject.SetActive(true);

        if (item == null)
        {
            flowController.ActiveRisksMouseAction(false);

			//CODIGO PARA RIESGOS QUIMICOS - PANTALLA DE MEDICIONES
			if(flowController.simulator==2 && flowController.index==7){
				item = Instantiate(prefabMedidor) as RectTransform;
				item.SetParent(transform);
				item.name = "Ficha3C";
				item.localScale = Vector3.one;
				item.anchoredPosition3D = new Vector3(0, 0, 0);

				Text txtTitle = item.Find("Ficha").Find("Name").GetComponent<Text>();
				txtTitle.text = obj.Title.ToString().Replace("_", " ");

				Button actionButton = item.Find("Ficha").Find("Save").Find("btn").GetComponent<Button>();
				actionButton.onClick.AddListener(delegate {
					//Save card info
					Save(item);
					if (dataBuilder.dataRisk.CheckLimitRisk3C())
					{
						saveAllCards.Invoke();
						handWriting.gameObject.SetActive(true);
						if(playStory)
							handWriting.StartStory(story2);
					}
				});

				Button closeButton = item.Find("Ficha").Find("Close").GetComponent<Button>();
				closeButton.onClick.AddListener(delegate {
					//Close card
					CloseCard();
				});
				item.GetChild(1).GetChild(0).GetChild(0).GetChild(0).GetComponent<Medidor>().obj = obj;
			}
			//CODIGO PARA RIESGOS BIOLOGICOS, SEGURIDAD Y QUIMICOS
			else{
	            item = Instantiate(prefabRiskCard) as RectTransform;
	            item.SetParent(transform);
	            item.name = "CardRisk";
	            item.localScale = Vector3.one;
	            item.anchoredPosition3D = new Vector3(0, 0, 0);
				if(!item.gameObject.activeSelf)
					item.gameObject.SetActive(true);

	            Text txtTitle = item.Find("Title").GetComponent<Text>();
	            txtTitle.text = obj.Title.ToString().Replace("_", " ");

	            Image imgRisk = item.Find("Image").GetComponent<Image>();
	            imgRisk.sprite = obj.ImageResource;

	            if (obj.transitions.Length > 0)
	            {
	                item.Find("Image").GetComponent<SpriteImage>().animationList[0].transitions = obj.transitions;
	                item.Find("Image").GetComponent<SpriteImage>().baseImage = obj.ImageResource;
	            }
	            item.Find("Image").gameObject.SetActive(true);

	            Button actionButton = item.Find("Save").Find("btn").GetComponent<Button>();
	            actionButton.onClick.AddListener(delegate {
	                //Save card info
	                Save(item);

	                if (dataBuilder.dataRisk.CheckLimitRisk())
	                {
	                    saveAllCards.Invoke();
	                    handWriting.gameObject.SetActive(true);
	                    if(playStory)
	                        handWriting.StartStory(story);
	                }
	            });

	            Button closeButton = item.Find("Close").GetComponent<Button>();
	            closeButton.onClick.AddListener(delegate
	            {
	                CloseCard();
	            });
			}
        }
		LoadInfoCard(item);
    }

    void LoadInfoCard(Transform card)
    {
		//CODIGO PARA RIESGOS QUIMICOS - PANTALLA DE MEDICIONES
		if(flowController.simulator==2 && flowController.index==7){
			File3CChemistry file = dataBuilder.dataRisk.GetRisk3C(card.Find("Ficha").Find("Name").GetComponent<Text>().text.Replace("_", " "));
			if (file != null)
			{
				card.Find("Ficha").Find("Content").Find("Presentacion").GetComponent<Dropdown>().value = file.Presentacion;
				card.Find("Ficha").Find("Content").Find("Medicion").GetChild(0).GetComponent<Text>().text = file.Medicion;
				card.Find("Ficha").Find("Content").Find("Permitido").GetComponent<InputField>().text = file.Permitido;
				card.Find("Ficha").Find("Content").Find("Limite").GetComponent<Dropdown>().value = file.Limite;
			}
		}
		//CODIGO PARA RIESGOS BIOLOGICOS, SEGURIDAD Y QUIMICOS
		else{
	        File1 file = dataBuilder.dataRisk.GetRisk(card.Find("Title").GetComponent<Text>().text.Replace("_", " "));
            if (file != null)
            {
                card.Find("Content").Find("Description").GetComponent<InputField>().text = file.DecriptionGTC;
                card.Find("Content").Find("Characterizacion").GetComponent<InputField>().text = file.Characterization;
	        }
		}
    }

    public void Save(Transform card)
    {
		string risk;
		//CODIGO PARA RIESGOS QUIMICOS - PANTALLA DE MEDICIONES
		if(flowController.simulator==2 && flowController.index==7){
			risk = card.Find("Ficha").Find("Name").GetComponent<Text>().text;
			int presentacion = card.Find("Ficha").Find("Content").Find("Presentacion").GetComponent<Dropdown>().value;
			string medicion = card.Find("Ficha").Find("Content").Find("Medicion").GetChild(0).GetComponent<Text>().text;
			string permitido = card.Find("Ficha").Find("Content").Find("Permitido").GetComponent<InputField>().text;
			int limite  = card.Find("Ficha").Find("Content").Find("Limite").GetComponent<Dropdown>().value;

			if(presentacion != 0 && !medicion.Contains("--") && permitido.Length > 0 && limite != 0){
				CloseCard();
				dataBuilder.AddRisk3C(risk, presentacion, medicion, permitido, limite);
				riskCountLabel.text = dataBuilder.dataRisk.FileEvaluation3CChemistry.Count + "/4";
				saveCard.Invoke();
			}
			else{
				alert.gameObject.SetActive(true);
				alertMessage.CreateAlertMessage("Debes diligenciar toda la información de la tabla para continuar.");
			}
		}
		//CODIGO PARA RIESGOS BIOLOGICOS, SEGURIDAD Y QUIMICOS
		else{
			risk = card.Find("Title").GetComponent<Text>().text;
	        string characterization = card.Find("Content").Find("Characterizacion").GetComponent<InputField>().text;
	        string decriptionGTC = card.Find("Content").Find("Description").GetComponent<InputField>().text;

	        if (characterization.Length > 0 && decriptionGTC.Length > 0)
	        {
	            CloseCard();
                dataBuilder.AddRisk(risk, characterization, decriptionGTC);

                if (flowController.simulator!=2)
	            	riskCountLabel.text = dataBuilder.dataRisk.File1.Count + "/7";
				else
					riskCountLabel.text = dataBuilder.dataRisk.File1.Count + "/4";
	            saveCard.Invoke();
	        }
	        else
	        {
	            alert.gameObject.SetActive(true);
	            alertMessage.CreateAlertMessage("No has completado la Ficha, por favor complétala para avanzar.");
	        }
		}
    }

    public void CloseCard()
    {
        closeCard.Invoke();
        Destroy(item.gameObject);

        // Activar Choise Characters
        flowController.ActiveRisksMouseAction(true);
    }

}
