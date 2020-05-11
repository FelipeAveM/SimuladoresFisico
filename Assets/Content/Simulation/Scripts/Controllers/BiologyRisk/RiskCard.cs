using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class RiskCard : MonoBehaviour {
	
	private RectTransform item;
	private AlertMessage alertMessage;
	private RiskCardObject riskCardObject;
    private DataBuilder dataBuilder;
    private FlowController flowController;

    public Transform prefabRiskCard;
    public FmodHandWriting handWriting;
    public Transform GameControl;
    public Transform alert;
    public Text riskCountLabel;
   	
	public int story;
    public UnityEvent createCard;
    public UnityEvent closeCard;
    public UnityEvent saveCard;
	public UnityEvent saveAllCards;

	void Start(){
        flowController = GameControl.GetComponent<FlowController>();
        dataBuilder = GameControl.GetComponent<DataBuilder>();
        alertMessage = alert.GetComponent<AlertMessage>();
	}
		
	public void CreateCard(RiskCardObject obj){

        createCard.Invoke();
        gameObject.SetActive(true);
		riskCardObject = obj;

		if (item == null) {

			flowController.ActiveRisksMouseAction(false);

			item = Instantiate(prefabRiskCard) as RectTransform;
			item.SetParent(transform);
			item.name = "CardRisk";
			item.localScale = Vector3.one;
			item.anchoredPosition3D = new Vector3 (0, 0, 0);

			Text txtTitle = item.Find ("Title").GetComponent<Text> ();	
			txtTitle.text = obj.Title.ToString ().Replace ("_", " ");

			Image imgRisk = item.Find ("Image").GetComponent<Image> ();
			imgRisk.sprite = obj.ImageResource;

            if(obj.transitions.Length > 0)
            {
                item.Find("Image").GetComponent<SpriteImage>().animationList[0].transitions = obj.transitions;
                item.Find("Image").GetComponent<SpriteImage>().baseImage = obj.ImageResource;
            }
            item.Find("Image").gameObject.SetActive(true);  

            Button actionButton = item.Find ("Save").Find ("btn").GetComponent<Button> ();
			actionButton.onClick.AddListener (delegate {

				//Save card info
				Save (item);

				if(dataBuilder.dataRisk.CheckLimitRisk()){
					saveAllCards.Invoke();
					handWriting.gameObject.SetActive(true);
					handWriting.StartStory(story);
				}
			});

            Button closeButton = item.Find("Close").GetComponent<Button>();
            closeButton.onClick.AddListener(delegate
            {
                CloseCard();
            });
            LoadInfoCard(item);
        }
    }

    void LoadInfoCard(Transform card)
    {
        File1 file = dataBuilder.dataRisk.GetRisk(riskCardObject.Title.ToString()
            .Replace("_"," ")
            .Replace(",", "")
            .Replace("(","")
            .Replace(")", " "));

        if (file != null)
        {
            Transform toogleGroup = card.Find("GTC045");
            foreach (Transform toogle in toogleGroup)
            {
                if (toogle.name.Equals(file.Clasification))
                {
                    toogle.GetComponent<Toggle>().isOn = true;
                }
            }
        }
 
    }

    public void  Save(Transform card){
		string risk =  card.Find ("Title").GetComponent<Text> ().text;
		string classification = "";
		Transform toogleGroup = card.Find ("GTC045");
		foreach(Transform toogle in toogleGroup){
			if(toogle.GetComponent<Toggle>().isOn){
				classification = toogle.Find ("Label").GetComponent<Text> ().text;
			}
		}

		if (classification.Length > 0) {
            dataBuilder.AddRisk (risk, classification, riskCardObject);
			flowController.ActiveRisksMouseAction (true);
			if(flowController.simulator!=2)
				riskCountLabel.text = dataBuilder.dataRisk.File1.Count + "/7";
			else
				riskCountLabel.text = dataBuilder.dataRisk.File1.Count + "/4";
            saveCard.Invoke();
            Destroy(card.gameObject);
        } else {
			alert.gameObject.SetActive (true);
			alertMessage.CreateAlertMessage ("No ha identificado el peligro, por favor identifíquelo para avanzar.");
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
