using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using FMODUnity;
using System;
using System.Runtime.InteropServices;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class WorkCardGrupal : MonoBehaviour {

    private RectTransform item;
    private AlertMessage alertMessage;
    private File2 response;
    private FlowController flowController;
    private DataBuilder dataBuilder;
    private Tour tour;
    private float audioLength = 0;
    private bool isPause = false;
    private Transform content;
    private Transform messuresData;
    private int sizeMessures;
    private Transform contentPercentage;
    private Slider barAudioTime;
    private bool updateTimeBar = true;

    public bool measureOnForTest = false;
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
    public FMOD.Studio.EventInstance fmodEmitter;
    
    void Start()
    {
        flowController = GameControl.GetComponent<FlowController>();
        dataBuilder = GameControl.GetComponent<DataBuilder>();
        tour = GameControl.GetComponent<Tour>();
        alertMessage = alert.GetComponent<AlertMessage>();
    }

    private void Update()
    {
        if (IsPlaying(fmodEmitter))
        {
            int currenAudioTime = 0;
            fmodEmitter.getTimelinePosition(out currenAudioTime);
            if (updateTimeBar)
                barAudioTime.value = currenAudioTime / audioLength;

            if (currenAudioTime > audioLength)
            {
                fmodEmitter.setTimelinePosition(0);
                fmodEmitter.start();
                fmodEmitter.setPaused(true);
                Transform button = item.Find("SoundButton");
                Image image = button.Find("Image").GetComponent<Image>();
                image.sprite = replayIcon;
            }
        }
    }

    private float GetClipDuration()
    {
        FMOD.Studio.EventDescription description;
        fmodEmitter.getDescription(out description);
        int lenght = 0;
        description.getLength(out lenght);
        return (float)lenght;
    }

    void MessueresOff(Transform  card)
    {
        Transform content = card.transform.Find("Content").Find("SectionRight").Find("Scroll").Find("Info");
        Transform messuresData = content.Find("Mesures");
        foreach (Transform element in messuresData.Find("TableEvaluation"))
        {
            if(element.GetChild(1).GetChild(0).GetComponent<Toggle>() != null)
                element.GetChild(1).GetChild(0).GetComponent<Toggle>().isOn = true;
        }
    }

    public void CreateCard(WorkerCardObject workerCard)
    {
        if (item == null)
        {
            createCard.Invoke();

            tour.moveFreehead = false;
            response = workerCard.response;
            flowController.ActiveCharactersMouseAction(false);

            item = Instantiate(prefabRiskCard) as RectTransform;
            item.SetParent(transform);
            item.name = "CardWork-" + workerCard.Name;
            item.localScale = Vector3.one;
            item.anchoredPosition3D = new Vector3(60, 0, 0);
            Transform leftContent = item.Find("Content").Find("SectionLeft").Find("CharacterPanel");
            Transform imageProfile = leftContent.Find("ImageProfile");
            imageProfile.GetComponent<Image>().sprite = workerCard.ImageCharacter;

            barAudioTime = leftContent.Find("Time").GetComponent<Slider>();
            EventTrigger triggerBar = barAudioTime.GetComponent<EventTrigger>();

            EventTrigger.Entry entrybar = new EventTrigger.Entry();
            entrybar.eventID = EventTriggerType.PointerEnter;
            entrybar.callback.AddListener((data) => { OnBarTimeEnter();});

            EventTrigger.Entry exitbar = new EventTrigger.Entry();
            exitbar.eventID = EventTriggerType.PointerExit;
            exitbar.callback.AddListener((data) => { OnBarTimeExit();});

            EventTrigger.Entry pointerUpBar = new EventTrigger.Entry();
            pointerUpBar.eventID = EventTriggerType.PointerUp;
            pointerUpBar.callback.AddListener((data) => { OnPointerTimeUp();});

            triggerBar.triggers.Add(entrybar);
            triggerBar.triggers.Add(exitbar);
            triggerBar.triggers.Add(pointerUpBar);

            StopAudio();
            fmodEmitter = FMODUnity.RuntimeManager.CreateInstance(workerCard.AudioPath);
            fmodEmitter.start();
            audioLength = GetClipDuration();
            handWriting.HideFmodHandWritting();

            Transform button = item.Find("SoundButton");
            Image image = button.Find("Image").GetComponent<Image>();

            button.GetComponent<Button>().onClick.AddListener(() =>
            {
                EventClickPause(image);
            });
 
            EventTrigger trigger = imageProfile.GetComponent<EventTrigger>();
            EventTrigger.Entry entry = new EventTrigger.Entry();
            entry.eventID = EventTriggerType.PointerClick;
            entry.callback.AddListener((data) => { OnPointerClickDelegate(image); });
            trigger.triggers.Add(entry);

            Button actionButton = item.Find("Save").Find("btn").GetComponent<Button>();
            actionButton.onClick.AddListener(delegate {

                handWriting.gameObject.SetActive(true);
                Save(item.gameObject);

                if (dataBuilder.dataRisk.CheckLimitWorkers())
                {
                    handWriting.StartStory(story);
                    saveAllCards.Invoke();
                }
            });

            Button closeButton = item.Find("Close").GetComponent<Button>();
            closeButton.onClick.AddListener(delegate
            {
                CloseCard();
            });

            if (measureOnForTest)
            {
                MessueresOff(item);
            }

            content = item.gameObject.transform.Find("Content").Find("SectionRight").Find("Scroll").Find("Info");

            contentPercentage = item.gameObject.transform.Find("Content").Find("SectionLeft").Find("PercentageContent");
            messuresData = content.Find("Mesures");
			if (messuresData != null && messuresData.gameObject.activeSelf) { 
                sizeMessures = messuresData.Find("TableEvaluation").childCount - 1;
           
                int count = 0;
                foreach (Transform child in messuresData.Find("TableEvaluation"))
                {
                    if (count > 0)
                    {
                        child.GetChild(1).GetComponentInChildren<Toggle>().onValueChanged.AddListener(
                            delegate
                            {
                                CheckMessures();
                            });

                        child.GetChild(2).GetComponentInChildren<Toggle>().onValueChanged.AddListener(
                          delegate
                          {
                              CheckMessures();
                          });
                    }
                    count++;
                }
            }
            LoadCard(item, workerCard);
        }
    }

    private void OnPointerTimeUp()
    {
        fmodEmitter.setPaused(true);
        fmodEmitter.setTimelinePosition((int)(barAudioTime.value * audioLength));
        fmodEmitter.setPaused(false);
    }

    private void OnBarTimeEnter()
    {
        updateTimeBar = false;
        print("Entry");
    }

    private void OnBarTimeExit()
    {
        updateTimeBar = true;
        print("Exit");
    }

    public void OnPointerClickDelegate(Image image)
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
            Transform dataWorker = content.Find("DataWorker");
            Transform dataTask = content.Find("DataTask");
            Transform dataEnviroment = content.Find("DataEnviroment");
            Transform dataControls = content.Find("DataControls");

            dataWorker.Find("FirstSection").Find("Name").Find("InputField").GetComponent<InputField>().text = file.grupalInfo.name;
            dataWorker.Find("FirstSection").Find("Age").Find("InputField").GetComponent<InputField>().text = file.grupalInfo.age;
            dataWorker.Find("SecondSection").Find("Ocupation").Find("InputField").GetComponent<InputField>().text = file.grupalInfo.ocupation;
            dataWorker.Find("SecondSection").Find("Seniority").Find("InputField").GetComponent<InputField>().text = file.grupalInfo.seniority;
            dataWorker.Find("ThirdSection").Find("Health").Find("InputField").GetComponent<InputField>().text = file.grupalInfo.conditionHealth;
            dataWorker.Find("FourthSection").Find("InputField").GetComponent<InputField>().text = file.grupalInfo.report;
            dataTask.Find("FirstSection").Find("Procces").Find("InputField").GetComponent<InputField>().text = file.grupalInfo.nameProcess;
			dataTask.Find("ThirdSection").Find("Zone").Find("InputField").GetComponent<InputField>().text = file.grupalInfo.zone;
            dataTask.Find("FifthSection").Find("WorkTime").Find("InputField").GetComponent<InputField>().text = file.grupalInfo.schedule;
            dataTask.Find("FifthSection").Find("Rest").Find("InputField").GetComponent<InputField>().text = file.grupalInfo.rest;
            dataTask.Find("SixthSection").Find("TaskTime").Find("InputField").GetComponent<InputField>().text = file.grupalInfo.workTime;
            dataTask.Find("SeventhdSection").Find("Tools").Find("InputField").GetComponent<InputField>().text = file.grupalInfo.tools;
            dataTask.Find("EighthSection").Find("CountPersons").Find("InputField").GetComponent<InputField>().text = file.grupalInfo.countPerson;
            dataEnviroment.Find("FirstSection").Find("Features").Find("InputField").GetComponent<InputField>().text = file.grupalInfo.characteristicsZone;
            dataControls.Find("FirstSection").Find("Enviroment").Find("InputField").GetComponent<InputField>().text = file.grupalInfo.enviroment;
            dataControls.Find("FirstSection").Find("Source").Find("InputField").GetComponent<InputField>().text = file.grupalInfo.source;
            dataControls.Find("SecondSection").Find("Admin").Find("InputField").GetComponent<InputField>().text = file.grupalInfo.admin;
            dataControls.Find("SecondSection").Find("Epp").Find("InputField").GetComponent<InputField>().text = file.grupalInfo.epp;
            if (flowController.simulator != 0)
            {
                dataControls.Find("ThirdSection").Find("Information").Find("InputField").GetComponent<InputField>().text = file.grupalInfo.infoAgent;
            }      
            else{
                dataControls.Find("FourthSection").Find("Information").Find("InputField").GetComponent<InputField>().text = file.grupalInfo.infoAgent;
            }
            //NO VA EN SEGURIDAD
            if (flowController.simulator!=1){
				dataTask.Find("SecondSection").Find("Agent").Find("InputField").GetComponent<InputField>().text = file.grupalInfo.nameAgent;
				dataTask.Find("FourthSection").Find("In").Find("InputField").GetComponent<InputField>().text = file.grupalInfo.ingressOrganism;
			}

			//SOLO VA EN QUIMICOS
			if(flowController.simulator==2){
				dataTask.Find("ThirdSectionB").Find("InputField").GetComponent<InputField>().text = file.grupalInfo.process;
				dataTask.Find("EighthSectionB").Find("AmountS").Find("InputField").GetComponent<InputField>().text = file.grupalInfo.amountS;
				dataTask.Find("EighthSectionB").Find("AmountM").Find("InputField").GetComponent<InputField>().text = file.grupalInfo.amountM;
				dataTask.Find("EighthSectionB").Find("AmountL").Find("InputField").GetComponent<InputField>().text = file.grupalInfo.amountL;
			}

            Transform sexGroup = dataWorker.Find("FirstSection").Find("Sex").Find("Group");
            if (file.grupalInfo.gender == "Masculino")
            {
                sexGroup.GetChild(0).GetComponent<Toggle>().isOn = true;
            }
            else
            {
                sexGroup.GetChild(1).GetComponent<Toggle>().isOn = true;
            }
			if (messuresData != null && messuresData.gameObject.activeSelf) { 
                int countMessureList = 0;
                foreach (Transform element in messuresData.Find("TableEvaluation"))
                {
                    if (element.GetChild(1).GetChild(0).GetComponent<Toggle>() != null)
                    {
                        if (file.grupalInfo.answersList[countMessureList].isApply)
                        {
                            element.GetChild(1).GetChild(0).GetComponent<Toggle>().isOn = true;
                        }
                        else
                        {
                            element.GetChild(2).GetChild(0).GetComponent<Toggle>().isOn = true;
                        }
                        countMessureList++;
                    }
                }
            }
        }
    }

	public List<Answers> GetAnswers(Transform messuresData)
	{
		List<Answers> answersList = new List<Answers>();

		foreach (Transform element in messuresData.Find("TableEvaluation"))
		{
			ToggleGroup group = element.GetComponent<ToggleGroup>();
			if (group != null)
			{
				IEnumerator<Toggle> active = group.ActiveToggles().GetEnumerator();
				active.MoveNext();

				if (active.Current != null)
				{
					Answers item = new Answers();

					item.isApply = false;
					if (active.Current.name.Equals("yes"))
					{
						item.isApply = true;
					}

					answersList.Add(item);
				}
			}
		}

		return answersList;
	}

    public float CheckMessures()
    {
        int sizeMessures = messuresData.Find("TableEvaluation").childCount - 1;
        List<Answers> answersList = GetAnswers(messuresData);

        if (answersList.Count == sizeMessures)
        {
            List<Answers> affirmativeAnsware = answersList.FindAll(x => x.isApply == true);
            float percentage = ((float)affirmativeAnsware.Count / (float)sizeMessures) * 100f;
            contentPercentage.Find("Percentage").GetComponent<Text>().text = percentage.ToString("F2") + " %";
            contentPercentage.gameObject.SetActive(true);
			return percentage;
        }
        else
        {
            contentPercentage.gameObject.SetActive(false);
			return 0f;
        }
    }

    public void Save(GameObject card)
    {
        // Elements
        Transform content = card.transform.Find("Content").Find("SectionRight").Find("Scroll").Find("Info");
        Transform dataWorker = content.Find("DataWorker");
        Transform dataTask = content.Find("DataTask");
        Transform dataEnviroment = content.Find("DataEnviroment");
        Transform dataControls = content.Find("DataControls");
        Transform messuresData = content.Find("Mesures");

		Image profile = card.transform.Find("Content").Find("SectionLeft").Find("CharacterPanel").GetChild(0).GetComponent<Image>();
		//DATA WORKER
        InputField name = dataWorker.Find("FirstSection").Find("Name").Find("InputField").GetComponent<InputField>();
        InputField age = dataWorker.Find("FirstSection").Find("Age").Find("InputField").GetComponent<InputField>();
        InputField ocupation = dataWorker.Find("SecondSection").Find("Ocupation").Find("InputField").GetComponent<InputField>();
        InputField seniority = dataWorker.Find("SecondSection").Find("Seniority").Find("InputField").GetComponent<InputField>();
        InputField conditionHealth = dataWorker.Find("ThirdSection").Find("Health").Find("InputField").GetComponent<InputField>();
        InputField report = dataWorker.Find("FourthSection").Find("InputField").GetComponent<InputField>();
		//DATA TASK
        InputField nameProcess = dataTask.Find("FirstSection").Find("Procces").Find("InputField").GetComponent<InputField>();
		InputField zone = dataTask.Find("ThirdSection").Find("Zone").Find("InputField").GetComponent<InputField>();
		InputField schedule = dataTask.Find("FifthSection").Find("WorkTime").Find("InputField").GetComponent<InputField>();
		InputField rest = dataTask.Find("FifthSection").Find("Rest").Find("InputField").GetComponent<InputField>();
		InputField workTime = dataTask.Find("SixthSection").Find("TaskTime").Find("InputField").GetComponent<InputField>();
		InputField tools = dataTask.Find("SeventhdSection").Find("Tools").Find("InputField").GetComponent<InputField>();
		InputField countPerson = dataTask.Find("EighthSection").Find("CountPersons").Find("InputField").GetComponent<InputField>();

		//Asignación de una variable temporal en caso de ser simulador de riesgos de seguridad
		InputField nameAgent = countPerson;
		InputField ingressOrganism = countPerson;

		//Asignación de una variable temporal en caso de no ser simulador de riesgos químicos
		InputField process = countPerson;
		InputField amountS = countPerson;
		InputField amountM = countPerson;
		InputField amountL = countPerson;

		if(flowController.simulator!=1){
			nameAgent = dataTask.Find("SecondSection").Find("Agent").Find("InputField").GetComponent<InputField>();
			ingressOrganism = dataTask.Find("FourthSection").Find("In").Find("InputField").GetComponent<InputField>();
		}

		//SOLO QUIMICOS
		if(flowController.simulator==2){
			process = dataTask.Find("ThirdSectionB").Find("InputField").GetComponent<InputField>();
			amountS = dataTask.Find("EighthSectionB").Find("AmountS").Find("InputField").GetComponent<InputField>();
			amountM = dataTask.Find("EighthSectionB").Find("AmountM").Find("InputField").GetComponent<InputField>();
			amountL = dataTask.Find("EighthSectionB").Find("AmountL").Find("InputField").GetComponent<InputField>();
		}

		//DATA CHARACTERISTICS
        InputField characteristicsZone = dataEnviroment.Find("FirstSection").Find("Features").Find("InputField").GetComponent<InputField>();
		//DATA CONTROLS
        InputField enviroment = dataControls.Find("FirstSection").Find("Enviroment").Find("InputField").GetComponent<InputField>();
        InputField source = dataControls.Find("FirstSection").Find("Source").Find("InputField").GetComponent<InputField>();
        InputField admin = dataControls.Find("SecondSection").Find("Admin").Find("InputField").GetComponent<InputField>();
        InputField epp = dataControls.Find("SecondSection").Find("Epp").Find("InputField").GetComponent<InputField>();
        InputField infoAgent;
        if (flowController.simulator != 0) {
            infoAgent = dataControls.Find("ThirdSection").Find("Information").Find("InputField").GetComponent<InputField>();
        }
        else {
            infoAgent = dataControls.Find("FourthSection").Find("Information").Find("InputField").GetComponent<InputField>();
        }
        

        ToggleGroup sexGroup = dataWorker.Find("FirstSection").Find("Sex").Find("Group").GetComponent<ToggleGroup>();
        IEnumerator<Toggle> activeGroup = sexGroup.ActiveToggles().GetEnumerator();
        activeGroup.MoveNext();


		List<Answers> answersList = new List<Answers>();
		if (messuresData != null){
        	//messuresList = GetMessures(messuresData);
			answersList = GetAnswers(messuresData);
		}

        string errorMessage = "";
        float scrollPosition = 0;
        GrupalInfo groupInfo = new GrupalInfo();


        if (name.text.Trim() != "" && age.text.Trim() != "" && ocupation.text.Trim() != "" && seniority.text.Trim() != "" && conditionHealth.text.Trim() != ""
            && report.text.Trim() != "" && activeGroup.Current != null)
        {
            if (nameProcess.text.Trim() != "" && nameAgent.text.Trim() != "" && zone.text.Trim() != "" && ingressOrganism.text.Trim() != "" && 
                schedule.text.Trim() != "" && rest.text.Trim() != "" && workTime.text.Trim() != "" && tools.text.Trim() != "" && countPerson.text.Trim() != "" &&
				(flowController.simulator != 2 || (process.text.Trim() != "" && amountS.text.Trim() != "" && amountM.text.Trim() != "" && amountL.text.Trim() != "" ) ) )
            {
                if (characteristicsZone.text.Trim() != "")
                {
                    if (enviroment.text.Trim() != "" && source.text.Trim() != "" && admin.text.Trim() != "" && epp.text.Trim() != "" && infoAgent.text.Trim() != "")
                    {
                        if (answersList.Count == sizeMessures)
                        {
                            string gender = activeGroup.Current.transform.Find("Label").GetComponent<Text>().text;
                            groupInfo.name = name.text;
							groupInfo.profile = profile.sprite.name.ToString();
                            groupInfo.age = age.text;
                            groupInfo.ocupation = ocupation.text;
                            groupInfo.seniority = seniority.text;
                            groupInfo.conditionHealth = conditionHealth.text;
                            groupInfo.report = report.text;
                            groupInfo.gender = gender;
                            groupInfo.nameProcess = nameProcess.text;
                            groupInfo.nameAgent = nameAgent.text;
                            groupInfo.zone = zone.text;
                            groupInfo.ingressOrganism = ingressOrganism.text;
                            groupInfo.schedule = schedule.text;
                            groupInfo.rest = rest.text;
                            groupInfo.workTime = workTime.text;
                            groupInfo.tools = tools.text;
                            groupInfo.countPerson = countPerson.text;
                            groupInfo.enviroment = enviroment.text;
                            groupInfo.source = source.text;
                            groupInfo.admin = admin.text;
                            groupInfo.epp = epp.text;
                            groupInfo.infoAgent = infoAgent.text;
                            groupInfo.characteristicsZone = characteristicsZone.text;
							groupInfo.answersList = answersList;
							if(flowController.simulator == 2){
								groupInfo.process = process.text;
								groupInfo.amountS = amountS.text;
								groupInfo.amountM = amountM.text;
								groupInfo.amountL = amountL.text;
							}
                        }
                        else
                        {
                            errorMessage = "Debes completar la lista de medidas.";
                            scrollPosition = (messuresData as RectTransform).anchoredPosition3D.y;
                        }
                    }
                    else
                    {
                        errorMessage = "Debes completar la información de los controles.";
                        scrollPosition = (dataControls as RectTransform).anchoredPosition3D.y;
                    }
                }
                else
                {
                    errorMessage = "Debes completar los datos del ambiente de trabajo.";
                    scrollPosition = (dataEnviroment as RectTransform).anchoredPosition3D.y;
                }
            }
            else
            {
                errorMessage = "Debes completar los datos de la actividad.";
                scrollPosition = (dataTask as RectTransform).anchoredPosition3D.y;
            }      
        }
        else
        {
            errorMessage = "Debes completar los datos del trabajador.";
            scrollPosition =0;
        }


        if (!errorMessage.Equals(""))
        {
            alert.gameObject.SetActive(true);
            alertMessage.CreateAlertMessage(errorMessage);
            (content as RectTransform).anchoredPosition = new Vector3(0,scrollPosition,0);
        }
        else
        {
            saveCard.Invoke();
            // get all data 
            File2 file = new File2();
            file.referenceName = response.name.value;
            file.grupalInfo = groupInfo;
			file.Score = CheckMessures();

			// Save information on server
			flowController.DowloadResult(4);

            // Stop Audio
            StopAudio();

            // Save Document
            dataBuilder.AddWorker(file);
			if(flowController.simulator!=2)
            	workersCountLabel.text = dataBuilder.dataRisk.File2.Count + "/3";
			else
				workersCountLabel.text = dataBuilder.dataRisk.File2.Count + "/4";
            closeCard.Invoke();
            Destroy(card);

            // Activar Choise Characters
			if(dataBuilder.dataRisk.File2.Count < flowController.randomCharacters)
            	flowController.ActiveCharactersMouseAction(true);
        }    
    }

    float Qualify(ref File2 file)
    {
        float success = 30f / 39f;
        float score = 0.0f;

        if (file.Name.value.Equals(response.Name.value))
        {
            score = score + success;
            file.Name.isCorrect = true;
        }
        else
        {
            file.Name.isCorrect = false;
        }

        if (file.Age.value.Equals(response.Age.value))
        {
            score += success;
            file.Age.isCorrect = true;
        }
        else
        {
            file.Age.isCorrect = false;
        }

        if (file.Sex.value.Equals(response.Sex.value))
        {
            score += success;
            file.Sex.isCorrect = true;
        }
        else
        {
            file.Sex.isCorrect = false;
        }

        if (file.Ocupation.value.Equals(response.Ocupation.value))
        {
            score += success;
            file.Ocupation.isCorrect = true;
        }
        else
        {
            file.Ocupation.isCorrect = false;
        }

        if (file.Seniority.value.Equals(response.Seniority.value))
        {
            score += success;
            file.Seniority.isCorrect = true;
        }
        else
        {
            file.Seniority.isCorrect = false;
        }

        if (file.Schedule.value.Equals(response.Schedule.value))
        {
            score += success;
            file.Schedule.isCorrect = true;
        }
        else
        {
            file.Schedule.isCorrect = false;
        }

        if (file.KindOfRest.value.Equals(response.KindOfRest.value))
        {
            score += success;
            file.KindOfRest.isCorrect = true;
        }
        else
        {
            file.KindOfRest.isCorrect = false;
        }

        if (file.Temperature.value.Equals(response.Temperature.value))
        {
            score += success;
            file.Temperature.isCorrect = true;
        }
        else
        {
            file.Temperature.isCorrect = false;
        }

        if (file.Cleanliness.value.Equals(response.Cleanliness.value))
        {
            score += success;
            file.Cleanliness.isCorrect = true;
        }
        else
        {
            file.Cleanliness.isCorrect = false;
        }

        if (EqualList(file.Condition.value, response.Condition.value))
        {
            score += success;
            file.Condition.isCorrect = true;
        }
        else
        {
            file.Condition.isCorrect = false;
        }

        if (EqualList(file.Tools.value, response.Tools.value))
        {
            score += success;
            file.Tools.isCorrect = true;
        }
        else
        {
            file.Tools.isCorrect = false;
        }

        if (EqualList(file.Epp.value, response.Epp.value))
        {
            score += success;
            file.Epp.isCorrect = true;
        }
        else
        {
            file.Epp.isCorrect = false;
        }

        if (EqualList(file.Noise.value, response.Noise.value))
        {
            score += success;
            file.Noise.isCorrect = true;
        }
        else
        {
            file.Noise.isCorrect = false;
        }

        return score;
    }

    bool EqualList(List<string> a, List<string> b)
    {
        string compareString = "";
        foreach (var textb in b)
        {
            compareString += textb;
        }

        foreach (var textA in a)
        {
            if (!compareString.Contains(textA))
                return false;
        }

        return true;
    }

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
        // Allow navigaition
        tour.moveFreehead = true;
        closeCard.Invoke();
        StopAudio();
        Destroy(item.gameObject);

        // Activar Choise Characters
        flowController.ActiveCharactersMouseAction(true);
    }
}
