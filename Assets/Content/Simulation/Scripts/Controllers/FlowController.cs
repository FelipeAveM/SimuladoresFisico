using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using System.Diagnostics;

public class FlowController : MonoBehaviour {

	[System.Serializable]
	public class Slides : System.Object{
		public string title;
		public string buttonName;
		public Transform[] panels;
		public int advance;
        public UnityEvent createSlide;
        public UnityEvent nextAction;
		public int story = 0;
		public bool startStory;
		public bool activeCharacterMouseAction;
		public bool activeRiskMouseAction;
        public bool isActiveEnviroment = false;
        public bool isActiveNextAction = false;
        public bool IsFreeHead = false;
        public bool IsHorizontalMove = false;
    }

	public Slides[] slides;
	public int index = 0;
	public Transform topBar;
	public Transform buttonBar;
	public FmodHandWriting baseHandWrite;
	public Transform charactersParent;
	public Transform riskParent;
    public UnityEvent startIntro;
	public UnityEvent postOk;
    public List<Transform> enviroment;

	private delegate void EventDelegate();
	private EventDelegate action;
	private Text title;
	private Text advanceLabel;
	private Text buttonLabel;
	private Slider sliderAdvance;
	private Button nextBtn;
	private DataBuilder dataBuilder;
    private Tour tour;
    private ApiController apiController;
    private int[] characters;
    private int[] risk;

    public int randomRisks = 7;//4
    public int randomCharacters = 3;//3
	public int simulator = 0; //0: Biologicos, 1: Seguridad, 2: Quimicos

    void Awake()
    {
        dataBuilder = GetComponent<DataBuilder>();
        apiController = GetComponent<ApiController>();
        tour = GetComponent<Tour>();
    }

    void Start(){
		if(characters == null)
        	characters = new int[randomCharacters];
		if(risk == null)
        	risk = new int[randomRisks];

        GetReference ();
		ClosePanels ();
        ChoiseRiskAndCharacters();
        OpenPanel (index);
	}

    void GetReference(){
		nextBtn = buttonBar.Find ("Button").Find("btn").GetComponent<Button>();
		buttonLabel = buttonBar.Find ("Button").Find("btn").Find ("Text").GetComponent<Text> ();
		sliderAdvance = buttonBar.Find ("Slider").GetComponent<Slider> ();
		advanceLabel = sliderAdvance.transform.Find ("Handle Slide Area").Find ("Handle").Find ("Text").GetComponent<Text> ();
		title = topBar.Find ("Title").GetComponent<Text> ();
	}

    public void OpenPanel(int panelIndex) {
        index = index + 1;
        dataBuilder.SetIndex(index);
        ClosePanels();

        foreach (Transform item in slides[panelIndex].panels) {
            item.gameObject.SetActive(true);
        }

        if (slides[panelIndex].title.Equals("INTRODUCCION"))
        {
            startIntro.Invoke();
        }

        slides[panelIndex].createSlide.Invoke();

        title.text = slides[panelIndex].title;
        SetAdvance(slides[panelIndex].advance);
        nextBtn.interactable = false;
        if (slides[panelIndex].startStory) {
            baseHandWrite.gameObject.SetActive(true);
            baseHandWrite.StartStory(slides[panelIndex].story);
        }
        buttonLabel.text = slides[panelIndex].buttonName;

        nextBtn.onClick.RemoveAllListeners();
        nextBtn.onClick.AddListener(() => NextSlider());

        ActiveMouseAction(riskParent, slides[panelIndex].activeRiskMouseAction, true);
        ActiveMouseAction(charactersParent, slides[panelIndex].activeCharacterMouseAction, false);

        nextBtn.interactable = slides[panelIndex].isActiveNextAction;
        if (tour != null) { 
            tour.moveFreehead = slides[panelIndex].IsFreeHead;
            tour.horizontalNavigation = slides[panelIndex].IsHorizontalMove;
        }

        foreach (var element in enviroment)
        {
            element.gameObject.SetActive(slides[panelIndex].isActiveEnviroment);
        }
	}

	public void NextSlider(){
        slides[index - 1 ].nextAction.Invoke();
        if(index < slides.Length)
		    OpenPanel(index);	
	}
		
	private void ClosePanels(){
		foreach(Slides element in slides){
			foreach(Transform item in element.panels){
				item.gameObject.SetActive (false);
			}
		}
	}

	IEnumerator Waiting(int waitingTime, EventDelegate action)
	{
		yield return new WaitForSeconds(waitingTime);
		action ();
	}

    MouseAction GetMouseAction(Transform item)
    {
        MouseAction mouseAction;
        if (item.GetComponent<MouseAction>() != null)
        {
            mouseAction = item.GetComponent<MouseAction>();
        }
        else
        {
            mouseAction = item.Find("master").Find("Reference").Find("Hips").GetComponent<MouseAction>();
        }
        return mouseAction;
    }

    public void ActiveMouseAction(Transform parent, bool enable, bool isRisk)
    {
        int[] array = (isRisk) ? risk : characters;
        foreach (var index in array)
        {
            MouseAction mouseAction = parent.GetChild(index - 1).GetComponent<MouseAction>();
            if(mouseAction == null)
            {
                mouseAction = parent.GetChild(index - 1).Find("master").Find("Reference").Find("Hips").GetComponent<MouseAction>();
            }
            mouseAction.active = enable;
        }
    }

	public void ActiveRisksMouseAction(bool enable){
        foreach (Transform item in riskParent){
			if (item.gameObject.activeSelf) {
                MouseAction mouseAction = GetMouseAction(item);
                mouseAction.active = enable;
				if(!enable){
					mouseAction.SimpleCursor ();
				}
			}
		}
	}
	public void ActiveCharactersMouseAction(bool enable){
		foreach (Transform item in charactersParent){
			if (item.gameObject.activeSelf) {
                MouseAction mouseAction = GetMouseAction(item);
                mouseAction.active = enable;
				if(!enable){
					mouseAction.SimpleCursor ();
				}
			}
		}
	}

	public void removeElementFromEnvironment(int element){
		enviroment.RemoveAt(element);
	}
		
	public void SetAdvance (int advance){
		sliderAdvance.value = advance;
		advanceLabel.text = advance.ToString() + "%";
	}

    private static int[] UniqueRandomNumbers(int min, int max, int howMany)
    {

        int[] myNumbers = new int[howMany];
        if (howMany == max - min)
        {
            for (int i = 0; i < howMany; i++)
                myNumbers[i] = i;
            return myNumbers;
        }

        System.Random randNum = new System.Random();
        for (int currIndex = 0; currIndex < howMany; currIndex++)
        {
            int randCandidate = randNum.Next(min, max);
            while (myNumbers.Contains(randCandidate))
            {
                randCandidate = randNum.Next(min, max);
            }

            myNumbers[currIndex] = randCandidate;
        }

        return myNumbers;
    }

    public void ChoiseRiskAndCharacters(){

        risk = UniqueRandomNumbers(0, riskParent.childCount + 1, randomRisks);
		if(simulator!=2)
       		characters = UniqueRandomNumbers(0, charactersParent.childCount + 1 , randomCharacters);
		else
			characters = risk;

        ActiveRandomRisk(riskParent);
        ActiveRandomElemnt(charactersParent);
    }

    private void ActiveRandomRisk( Transform parent)
    {
        foreach( int index in risk)
        {
            parent.GetChild(index - 1).gameObject.SetActive(true);
        } 
    }

	private void ActiveRandomElemnt(Transform parent){
        foreach (int index in characters)
        {
            parent.GetChild(index - 1).gameObject.SetActive(true);
        }
	}

	public void DowloadResult(int endPoint){
        string request = dataBuilder.GetDataRisk ();
		if(apiController.islocal)
        	print(request);
        switch (endPoint)
        {
            case 0:
                apiController.POST(apiController.UrlBase, ApiController.ENDPOINT_PDF_RISKS_OFFICE, request, OnCompleted, OnError);
                break;
            case 1:
                apiController.POST(apiController.UrlBase, ApiController.ENDPOINT_PDF_BIOLOGY, request, OnCompleted, OnError);
                break;
            case 2:
                apiController.POST(apiController.UrlBase, ApiController.ENDPOINT_PDF_BIOLOGY_LABORATORY_CHARACTER, request, OnCompleted, OnError);
                break;
			case 3:
				apiController.POST(apiController.UrlBase, ApiController.ENDPOINT_PDF_OFFICE_MATRIX, request, OnCompleted, OnError);
				break;
			case 4:
				apiController.POST(apiController.UrlBase, ApiController.ENDPOINT_SAVE_ADVANCE, request, OnDBCompleted, OnError);
				break;
			case 5:
				apiController.POST(apiController.UrlBase, ApiController.ENDPOINT_PDF_SECURITY, request, OnCompleted, OnError);
				break;
			case 6:
				apiController.POST(apiController.UrlBase, ApiController.ENDPOINT_PDF_CHEMISTRY, request, OnCompleted, OnError);
				break;
        }	
	}

	public void OnCompleted(WWW response){
		PdfInfo pdfInfo = JsonUtility.FromJson<PdfInfo>(response.text);
		#if UNITY_WEBGL
		    apiController.OpenUrlInWeb(pdfInfo.Data.LocationPdf);
		#else
		    Application.OpenURL (pdfInfo.Data.LocationPdf);
		#endif
        if(index == slides.Length)
        {
            postOk.Invoke();
        }
	}

    public void OnDBCompleted(WWW response){
		print("POST OK");
	}

	public void OnError(string error){
		print (error);
	}
}
