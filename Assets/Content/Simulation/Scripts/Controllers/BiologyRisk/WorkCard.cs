using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using FMODUnity;
using System;
using System.Runtime.InteropServices;
using UnityEngine.EventSystems;

public class WorkCard : MonoBehaviour {

	private RectTransform item;
	private AlertMessage alertMessage;
	private File2 response;
    private FlowController flowController;
    private DataBuilder dataBuilder;
    private int countAudioPlay = 0;
    private float audioLength = 0;
    private bool isPause = false;

    public FMOD.Studio.EventInstance fmodEmitter;
    public FmodHandWriting handWriting;
    public Transform prefabRiskCard;
	public Transform alert;
    public Transform GameControl;
    public Text workersCountLabel;
    public Sprite playIcon, stopIcon, replayIcon;
	public int story;

    public UnityEvent createCard;
    public UnityEvent closeCard;
    public UnityEvent saveCard;
	public UnityEvent saveAllCards;


    void Start(){
        flowController = GameControl.GetComponent<FlowController>();
        dataBuilder = GameControl.GetComponent<DataBuilder>();
        alertMessage = alert.GetComponent<AlertMessage> ();
	}

    private void Update()
    {
        if (IsPlaying(fmodEmitter))
        //if (IsPlaying(fmodEmitter) && countAudioPlay == 0)
        {
            int currenAudioTime = 0;
            fmodEmitter.getTimelinePosition(out currenAudioTime);
            if (currenAudioTime >= audioLength)
            {
                fmodEmitter.setTimelinePosition(0);
                fmodEmitter.start();
                fmodEmitter.setPaused(true);
                Transform button = item.Find("SoundButton");
                Image image = button.Find("Image").GetComponent<Image>();
                image.sprite = replayIcon;

                //countAudioPlay++;
            }
        }
    }

    private float GetClipDuration()
    {
        FMOD.Studio.EventDescription description;
        fmodEmitter.getDescription(out description);
        int lenght = 0;
        description.getLength(out lenght);
        return (float)lenght - 500;
    }

    public void CreateCard(WorkerCardObject workerCard){
		if (item == null) {
            createCard.Invoke();
            response = workerCard.response;

			flowController.ActiveCharactersMouseAction (false);

			item = Instantiate (prefabRiskCard) as RectTransform;
			item.SetParent(transform);
			item.name = "CardWork-"+workerCard.Name;
			item.localScale = Vector3.one;
			item.anchoredPosition3D = new Vector3 (0, 0, 0);
			item.Find ("CharacterPanel").Find ("ImageProfile").GetComponent<Image> ().sprite = workerCard.ImageCharacter;
			item.Find ("CharacterPanel").Find ("WorkerName").GetComponent<Text> ().text = workerCard.Name.ToString();		//Se agregó esta linea para incluir el nombre del trabajador en la tarjeta

            StopAudio();

            fmodEmitter = FMODUnity.RuntimeManager.CreateInstance(workerCard.AudioPath);
            fmodEmitter.start();
            audioLength = GetClipDuration();
            print(audioLength);
            handWriting.HideFmodHandWritting();

            Transform button = item.Find("SoundButton");
            Image image = button.Find("Image").GetComponent<Image>();
            button.GetComponent<Button>().onClick.AddListener(delegate { EventClickPause(image); });

            Transform imageProfile = item.Find("CharacterPanel").Find("ImageProfile");
            EventTrigger trigger = imageProfile.GetComponent<EventTrigger>();
            EventTrigger.Entry entry = new EventTrigger.Entry();
            entry.eventID = EventTriggerType.PointerClick;
            entry.callback.AddListener((data) => { OnPointerClickDelegate((PointerEventData)data, image); });
            trigger.triggers.Add(entry);

            Button actionButton = item.Find ("Save").Find ("btn").GetComponent<Button> ();
			actionButton.onClick.AddListener (delegate {
				
				handWriting.gameObject.SetActive(true);
				Save (item.gameObject);

				if (dataBuilder.dataRisk.CheckLimitWorkers ()) {
					handWriting.StartStory(story);	
					saveAllCards.Invoke();
				}
			});

            Button closeButton = item.Find("Close").GetComponent<Button>();
            closeButton.onClick.AddListener(delegate
            {
                CloseCard();
            });

            LoadCard(item, workerCard);

		}
	}

    public void OnPointerClickDelegate(PointerEventData data, Image image)
    {
        EventClickPause(image);
    }

    private void EventClickPause(Image image)
    {
        fmodEmitter.getPaused(out isPause);

        if (!isPause)
        {
            image.sprite = playIcon;
            fmodEmitter.setPaused(true);
        }
        else
        {
            image.sprite = stopIcon;
            fmodEmitter.setPaused(false);
        }
    }

    void LoadCard(Transform card, WorkerCardObject workcard)
    {
        File2 file = dataBuilder.dataRisk.GetWorkers(workcard.response.Name.value);

        if (file != null)
        {

            Transform dataWorker = card.transform.Find("DataWorker");
            Transform dataTask = card.transform.Find("DataTask");
            Transform dataEnviroment = card.transform.Find("DataEnviroment");

            Dropdown nameDrop = dataWorker.Find("FirstSection").Find("Name").Find("Dropdown").GetComponent<Dropdown>();
            Dropdown ageDrop = dataWorker.Find("FirstSection").Find("Age").Find("Dropdown").GetComponent<Dropdown>();
            Dropdown ocupationDrop = dataWorker.Find("SecondSection").Find("Ocupation").Find("Dropdown").GetComponent<Dropdown>();
            Dropdown seniorityDrop = dataWorker.Find("SecondSection").Find("Seniority").Find("Dropdown").GetComponent<Dropdown>();
            Dropdown scheduleDrop = dataTask.Find("FirstSection").Find("Schedule").Find("Dropdown").GetComponent<Dropdown>();
            Dropdown kindOfRestDrop = dataTask.Find("FirstSection").Find("KindofRest").Find("Dropdown").GetComponent<Dropdown>();
            Dropdown weatherDrop = dataEnviroment.Find("FirstSection").Find("Weather").Find("Dropdown").GetComponent<Dropdown>();
            Dropdown orderDeop = dataEnviroment.Find("FirstSection").Find("Order").Find("Dropdown").GetComponent<Dropdown>();

            ToggleGroup sexGroup = dataWorker.Find("FirstSection").Find("Sex").Find("Group").GetComponent<ToggleGroup>();
            ToggleMultiple conditionGroup = dataWorker.Find("ThirdSection").GetComponent<ToggleMultiple>();
            ToggleMultiple toolsGroup = dataTask.Find("SecondSection").GetComponent<ToggleMultiple>();
            ToggleMultiple eppGroup = dataTask.Find("ThirdSection").GetComponent<ToggleMultiple>();
            ToggleMultiple noiseGroup = dataEnviroment.Find("SecondSection").GetComponent<ToggleMultiple>();

            nameDrop.value = nameDrop.options.FindIndex(obj => obj.text == file.Name.value);
            ageDrop.value = ageDrop.options.FindIndex(obj => obj.text == file.Age.value);
            ocupationDrop.value = ocupationDrop.options.FindIndex(obj => obj.text == file.Ocupation.value);
            seniorityDrop.value = seniorityDrop.options.FindIndex(obj => obj.text == file.Seniority.value);
            scheduleDrop.value = scheduleDrop.options.FindIndex(obj => obj.text == file.Schedule.value);
            kindOfRestDrop.value = kindOfRestDrop.options.FindIndex(obj => obj.text == file.KindOfRest.value);
            weatherDrop.value = weatherDrop.options.FindIndex(obj => obj.text == file.Temperature.value);
            orderDeop.value = orderDeop.options.FindIndex(obj => obj.text == file.Cleanliness.value);

            Toggle[] options = sexGroup.GetComponentsInChildren<Toggle>();
            if (file.Sex.value.Equals("Masculino")) {
                options[0].isOn = true;
            }
            else
            {
                options[1].isOn = true;
            }

            conditionGroup.SetValues(file.Condition.value);
            toolsGroup.SetValues(file.Tools.value);
            eppGroup.SetValues(file.Epp.value);     
            noiseGroup.SetValues(file.Noise.value);
        }

    }

	public void  Save(GameObject card){

        // Reference
        Transform dataWorker =  card.transform.Find("DataWorker");
		Transform dataTask = card.transform.Find("DataTask");
		Transform dataEnviroment = card.transform.Find("DataEnviroment");

		Dropdown nameDrop = dataWorker.Find ("FirstSection").Find ("Name").Find ("Dropdown").GetComponent<Dropdown> ();
		Dropdown ageDrop = dataWorker.Find ("FirstSection").Find ("Age").Find ("Dropdown").GetComponent<Dropdown> ();
		Dropdown ocupationDrop = dataWorker.Find ("SecondSection").Find ("Ocupation").Find ("Dropdown").GetComponent<Dropdown> ();
		Dropdown seniorityDrop = dataWorker.Find ("SecondSection").Find ("Seniority").Find ("Dropdown").GetComponent<Dropdown> ();
		Dropdown scheduleDrop = dataTask.Find ("FirstSection").Find ("Schedule").Find ("Dropdown").GetComponent<Dropdown> ();
		Dropdown kindOfRestDrop = dataTask.Find ("FirstSection").Find ("KindofRest").Find ("Dropdown").GetComponent<Dropdown> ();
		Dropdown weatherDrop = dataEnviroment.Find ("FirstSection").Find ("Weather").Find ("Dropdown").GetComponent<Dropdown> ();
		Dropdown orderDeop = dataEnviroment.Find ("FirstSection").Find ("Order").Find ("Dropdown").GetComponent<Dropdown> ();

		ToggleGroup sexGroup = dataWorker.Find ("FirstSection").Find ("Sex").Find ("Group").GetComponent<ToggleGroup> ();
		ToggleMultiple conditionGroup = dataWorker.Find ("ThirdSection").GetComponent<ToggleMultiple>();
		ToggleMultiple toolsGroup = dataTask.Find ("SecondSection").GetComponent<ToggleMultiple>();
		ToggleMultiple eppGroup = dataTask.Find ("ThirdSection").GetComponent<ToggleMultiple>();
		ToggleMultiple noiseGroup = dataEnviroment.Find ("SecondSection").GetComponent<ToggleMultiple>();

		// get all data 
		File2 file = new File2 ();

		ValidObject name = new ValidObject ();
		ValidObject age = new ValidObject ();
		ValidObject ocupation = new ValidObject ();
		ValidObject seniority = new ValidObject ();
		ValidObject schedule = new ValidObject ();
		ValidObject kindOfRest = new ValidObject ();
		ValidObject temperature = new ValidObject ();
		ValidObject cleanliness = new ValidObject ();

		name.value = nameDrop.options [nameDrop.value].text;
		age.value = ageDrop.options[ageDrop.value].text;	
		ocupation.value = ocupationDrop.options[ocupationDrop.value].text;
		seniority.value = seniorityDrop.options[seniorityDrop.value].text;
		schedule.value =  scheduleDrop.options[scheduleDrop.value].text;
		kindOfRest.value =  kindOfRestDrop.options[kindOfRestDrop.value].text;
		temperature.value =  weatherDrop.options[weatherDrop.value].text;
		cleanliness.value = orderDeop.options[orderDeop.value].text;

		file.Name = name;
        file.referenceName = response.name.value;
		file.Age = age;
		file.Ocupation = ocupation;
		file.Seniority = seniority; 
		file.Schedule = schedule;
		file.KindOfRest = kindOfRest;
		file.Temperature = temperature;
		file.Cleanliness = cleanliness;
        file.questionAmount = response.questionAmount;

		ValidObject sex = new ValidObject ();
		ValidList condition = new ValidList ();
		ValidList tools = new ValidList ();
		ValidList epp = new ValidList ();
		ValidList noise = new ValidList ();

		IEnumerator<Toggle> activeGroup = sexGroup.ActiveToggles ().GetEnumerator();
		activeGroup.MoveNext();

		string errorMessage = ""; 
		if(activeGroup.Current!=null){
			sex.value = activeGroup.Current.transform.Find ("Label").GetComponent<Text>().text;
			file.Sex = sex; 
			if(conditionGroup.GetActive().Count > 0){
				condition.value = conditionGroup.GetActiveNames ();
				file.Condition = condition;
				if(toolsGroup.GetActiveNames ().Count > 0){
					tools.value = toolsGroup.GetActiveNames ();
					file.Tools = tools; 
					if(eppGroup.GetActiveNames ().Count > 0){
						epp.value = eppGroup.GetActiveNames ();
						file.Epp = epp;
						if(noiseGroup.GetActiveNames ().Count > 0){
							noise.value = noiseGroup.GetActiveNames ();
							file.Noise = noise;
						}else{
							errorMessage = "No ha identificado el ruido, por favor identifíquelo";
						}
					}else{
						errorMessage = "No ha identificado las Epp, por favor identifíquelo";
					}
				}else{
					errorMessage = "No ha identificado las herramientas, por favor identifíquelo";
				}
			}else{
				errorMessage = "No ha identificado las afecciones a la salud, por favor identifíquelas";
			}
		}else{
			errorMessage = "No ha identificado el género, por favor identifíquelo";
		}

		if(!errorMessage.Equals("")){
			alert.gameObject.SetActive (true);
			alertMessage.CreateAlertMessage (errorMessage);
		}else{
            saveCard.Invoke();
            // Stop Audio
            StopAudio();
            
            // Calculate Score
            float score = Qualify (ref file);
			file.Score = score;

            // Save Document
			dataBuilder.AddWorker (file);
            workersCountLabel.text = dataBuilder.dataRisk.File2.Count + "/3";
            Destroy (card);

            // Activar Choise Characters
			flowController.ActiveCharactersMouseAction (true);
		}
	}

	float Qualify(ref File2 file){
        float success = 10.0f / file.QuestionAmount;
        print(success);
        float score = 0.0f;
		int multiplicador = 0;

		if (file.Name.value.Equals (response.Name.value)) {
			score = score + success;
			file.Name.isCorrect = true;
		} else {
			file.Name.isCorrect = false;
		}
        print(score);

        if (file.Age.value.Equals(response.Age.value)){
			score += success;
			file.Age.isCorrect = true;
		} else {
			file.Age.isCorrect = false;
		}
        print(score);
        if (file.Sex.value.Equals(response.Sex.value)){
			score += success;
			file.Sex.isCorrect = true;
		} else {
			file.Sex.isCorrect = false;
		}
        print(score);
        if (file.Ocupation.value.Equals(response.Ocupation.value)){
			score += success;
			file.Ocupation.isCorrect = true;
		} else {
			file.Ocupation.isCorrect = false;
		}
        print(score);
        if (file.Seniority.value.Equals(response.Seniority.value)){
			score += success;
			file.Seniority.isCorrect = true;
		} else {
			file.Seniority.isCorrect = false;
		}
        print(score);
        if (file.Schedule.value.Equals(response.Schedule.value)){
			score += success;
			file.Schedule.isCorrect = true;
		} else {
			file.Schedule.isCorrect = false;
		}
        print(score);
        if (file.KindOfRest.value.Equals(response.KindOfRest.value)){
			score += success;
			file.KindOfRest.isCorrect = true;
		} else {
			file.KindOfRest.isCorrect = false;
		}
        print(score);
        if (file.Temperature.value.Equals(response.Temperature.value)){
			score += success;
			file.Temperature.isCorrect = true;
		} else {
			file.Temperature.isCorrect = false;
		}
        print(score);
        if (file.Cleanliness.value.Equals(response.Cleanliness.value)){
			score += success;
			file.Cleanliness.isCorrect = true;
		} else {
			file.Cleanliness.isCorrect = false;
		}
        print(score);
        multiplicador = EqualList(file.Condition.value, response.Condition.value);
		if (multiplicador>0){
			file.Condition.isCorrect = true;
		} else {
			file.Condition.isCorrect = false;
		}
		score += success*multiplicador;
        print(score);
        multiplicador = EqualList(file.Tools.value, response.Tools.value);
		if (multiplicador>0){
			file.Tools.isCorrect = true;
		} else {
			file.Tools.isCorrect = false;
		}
		score += success*multiplicador;
        print(score);
        multiplicador = EqualList(file.Epp.value, response.Epp.value);
		if (multiplicador>0){
			file.Epp.isCorrect = true;
		} else {
			file.Epp.isCorrect = false;
		}
		score += success*multiplicador;
        print(score);
        multiplicador = EqualList(file.Noise.value, response.Noise.value);
		if (multiplicador>0){
			file.Noise.isCorrect = true;
		} else {
			file.Noise.isCorrect = false;
		}
		score += success*multiplicador;
        print(score);
        if (score>10)
			return 10f;
		return score;
	}

	int EqualList (List<string> a, List<string> b){		//a es la respuesta de usuario, b es la respuesta correcta.
		string compareString = "";
		int totalUsuario = a.Count;
		int totalReal = b.Count;
		int validas = 0;
		foreach(var textb in b){
			compareString += textb;
		}

		foreach(var textA in a){
			if (compareString.Contains (textA))
				validas++;
		}
		if(totalUsuario <= totalReal)
			return validas;
		else{
			int calculo = totalUsuario - validas;			//Se penaliza al usuario si pone más respuestas de las necesarias y comete errores
			if(calculo < validas*1.5)
				return validas;
			else
				return 0;
		}
	}
	/*
	bool EqualList (List<string> a, List<string> b){		//VERSION ANTERIOR - NO TENIA EN CUENTA LA CANTIDAD DE PALABRAS CORRECTAS O INCORRECTAS
		string compareString = "";
		foreach(var textb in b){
			compareString += textb;
		}

		foreach(var textA in a){
			if (!compareString.Contains (textA))
				return false;
		}

		return true;
	}*/

    public bool IsPlaying(FMOD.Studio.EventInstance instance)
    {
        if (instance.isValid() && instance.isValid())
        {
            FMOD.Studio.PLAYBACK_STATE playbackState;
            instance.getPlaybackState(out playbackState);
            return (playbackState != FMOD.Studio.PLAYBACK_STATE.STOPPED);
        }
        return false;
    }

    public void StopAudio()
    {
        if (IsPlaying(fmodEmitter))
            fmodEmitter.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
    }

    public void CloseCard()
    {
        closeCard.Invoke();
        StopAudio();
        Destroy(item.gameObject);

        // Activar Choise Characters
        flowController.ActiveCharactersMouseAction(true);
    }
}
