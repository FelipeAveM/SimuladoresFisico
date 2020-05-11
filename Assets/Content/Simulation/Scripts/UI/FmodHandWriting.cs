using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using FMODUnity;
using System;
using System.Runtime.InteropServices;


public enum CHARACTER
{
    Bernard,
    Supervisor
}

public class FmodHandWriting : MonoBehaviour {

	[System.Serializable]
	public class Phrase : System.Object{
        public CHARACTER selectCharacter = CHARACTER.Bernard;
        public string phrase;
		public int fontSize = 0;
		[Tooltip("Synchronize audio")]
		public TextAnchor justyfy = TextAnchor.UpperLeft;
		public float waitEndAction = 0.0f;
        public ANIMATION_LIST bodyAnimation = ANIMATION_LIST.Grettings;

        [EventRef]
        public string audioPath;
        public UnityEvent StartPhrase;
		public UnityEvent EndPhrase;    
    }
        
	[System.Serializable]
	public class Moment : System.Object{
		public Phrase[] phrases;
	}

    [System.Serializable]
    public class DisplayCharacter : System.Object{
        public CHARACTER typeCharacter;
        public List<Transform> elements;
    }

    public List<DisplayCharacter> displayCharacters;
	public bool playinStart = false;
    public bool syncrony;
    public Moment[] moments;
	public float waitBetweenWords;
	public float waitBetweenPhrases;
	public float playInSeconds = 0.2f;
    public float moduleAmplitude = 20f;
    public FMOD.Studio.EventInstance fmodEmitter;
    public UnityEvent closeTooltip;	
	public Sprite soundImg;
	public Sprite muteImg;
    [EventRef]
    public string defaultAudioPath;

    private Text textMessage;
	private SpriteImage headAnimation;
    private SpriteImage bodyAnimation;
	private Button closeBtn, nextBtn, muteBtn, replayBtn;
	private Transform tooltip,character;
    private Transform[] highLightCharacter;
	private int currentPhraseIndex = 0;
	private int currentStoryIndex = 0;
    private float clipLenght = 0;
    private delegate void SoundDelegate();
    private SoundDelegate soundAction;
    private FMOD.DSP fft;
   // private LineRenderer lineRenderer;
    private bool isMute = false;
    private bool isHide = false;

    const int WindowSize = 256;

    void Awake()
    {
        GetReferences();
        FMODUnity.RuntimeManager.LowlevelSystem.setDSPBufferSize(2048, 2);  //Originalmente 512, 8
    }

    void Start()
    {
        if (replayBtn != null)
        {
            //transform.Find("Character").Find("Replay").gameObject.SetActive(false);
            replayBtn.onClick.AddListener(delegate () {
                StartStory(currentStoryIndex);
            });
        }

        if (playinStart)
        {
			StartStory(currentStoryIndex);
        }

        closeBtn.onClick.AddListener(delegate () {
            closeTooltip.Invoke();
        });

        nextBtn.onClick.AddListener(delegate () {
            StartStory(currentStoryIndex, currentPhraseIndex);
        });

        muteBtn.onClick.AddListener(delegate () {
            soundAction();
        });
    }

	void Update ()
	{	
	}

	float MaxValue (float[] array ){
		float max = array[0];
		for (int i = 1; i < array.Length; i++) {
			if (array[i] > max) {
				max = array[i];
			}
		}
		return max;
	}

	void GetReferences(){
		character = transform.Find ("Character");
        muteBtn = character.Find ("Mute").GetComponent<Button> ();
        if(transform.Find("Character").Find("Replay")!=null)
            replayBtn = character.Find("Replay").GetComponent<Button>();

        tooltip = transform.Find ("Tooltip");

        textMessage = tooltip.Find("Text").GetComponent<Text>();	
		closeBtn = tooltip.Find ("Close").GetComponent<Button> ();
		nextBtn = tooltip.Find ("Next").GetComponent<Button> ();
		soundAction = MuteSound;
	}

    void GetAnimationReference(CHARACTER type)
    {
        if (type == CHARACTER.Bernard)
        {
            displayCharacters[0].elements[0].gameObject.SetActive(true);
            displayCharacters[0].elements[1].gameObject.SetActive(true);
            headAnimation = displayCharacters[0].elements[0].GetComponent<SpriteImage>();
            bodyAnimation = displayCharacters[0].elements[1].GetComponent<SpriteImage>();
            if(displayCharacters.Count > 1)
                displayCharacters[1].elements[0].gameObject.SetActive(false);
        }

        if (type == CHARACTER.Supervisor)
        {
            displayCharacters[1].elements[0].gameObject.SetActive(true);
			headAnimation = null;
            bodyAnimation = null;
            displayCharacters[0].elements[0].gameObject.SetActive(false);
            displayCharacters[0].elements[1].gameObject.SetActive(false);
        }
    }
		
	IEnumerator PutWord(string word, int fontSize, TextAnchor justyfy)
	{
        yield return new WaitForSeconds(waitBetweenWords);   
        textMessage.alignment = justyfy;
		textMessage.fontSize = fontSize;
		textMessage.text = textMessage.text + word + " ";
	}

	IEnumerable PutPhrase(Phrase story)
	{
        // Put audio
        PutDialog(story.audioPath, story.phrase);

        // Start Animation
		if(headAnimation!=null){
			headAnimation.StartAnimation (ANIMATION_LIST.Lipsycn);
			headAnimation.loop = true;
		}
        if(bodyAnimation != null)
            bodyAnimation.StartAnimation(story.bodyAnimation);

		if(story.StartPhrase.GetPersistentEventCount() > 0){
			story.StartPhrase.Invoke ();
		}

		textMessage.text = story.phrase;
		/*
		string[] words = story.phrase.Split (' ');

		foreach(string word in words){
			yield return StartCoroutine (PutWord(word, story.fontSize, story.justyfy));
		}*/

        // wating to stop audioclip
        yield return new WaitWhile (() =>  IsPlaying(fmodEmitter));

		if(headAnimation!=null){
			headAnimation.loop = false;
		}

		yield return new WaitForSeconds(waitBetweenPhrases);

		if(story.EndPhrase.GetPersistentEventCount() > 0){
			yield return new WaitForSeconds(story.waitEndAction);
			story.EndPhrase.Invoke ();
		}
	}

	IEnumerable PutStory(Moment moment)
	{
        Phrase[] phrases = moment.phrases;
        GetAnimationReference(phrases[0].selectCharacter);
		currentPhraseIndex = 0;
		foreach(Phrase phraseObj in phrases){
			if(phrases.Length-1 == currentPhraseIndex){
				nextBtn.gameObject.SetActive (false);
			}
			textMessage.text = "";

			yield return StartCoroutine (PutPhrase(phraseObj).GetEnumerator ());
			currentPhraseIndex++;
		}
		//headAnimation.loop = false;
	}

	IEnumerable PutStory(Moment moment, int indexPhrase)
	{
		currentPhraseIndex = indexPhrase + 1;
		Phrase[] phrases = moment.phrases;
        GetAnimationReference(phrases[0].selectCharacter);
        for (int i = currentPhraseIndex; i < phrases.Length; i++){
			if(phrases.Length-1 == currentPhraseIndex){
				nextBtn.gameObject.SetActive (false);
			}
			textMessage.text = "";

			yield return StartCoroutine (PutPhrase(phrases[i]).GetEnumerator ());
			currentPhraseIndex = i;
		}
		//headAnimation.loop = false;
	}

	public void StartStory(int index, int indexPhrase){
        currentStoryIndex = index;
        StopAllCoroutines();
		nextBtn.gameObject.SetActive (true);
		fmodEmitter.stop (FMOD.Studio.STOP_MODE.IMMEDIATE);
		StartCoroutine(PutStory(moments[index], indexPhrase).GetEnumerator());
	}

	public void StartStory(int index){
        currentStoryIndex = index;
        if (isHide)
            ReturnFmodHandWritting();

        StopAllCoroutines();
		nextBtn.gameObject.SetActive (true);
        fmodEmitter.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        gameObject.SetActive (true);
		StartCoroutine(PutStory(moments[index]).GetEnumerator());
	}

	public void StartLastHistory(){
        if (isHide)
            ReturnFmodHandWritting();

        StopAllCoroutines();
		nextBtn.gameObject.SetActive (true);
        fmodEmitter.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        StartCoroutine(PutStory(moments[currentStoryIndex]).GetEnumerator());
	}

    public void StopHistory()   
    {
        closeTooltip.Invoke();
        if (IsPlaying(fmodEmitter))
        {
            fmodEmitter.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
            headAnimation.StopAllCoroutines();
            if (bodyAnimation != null)
                bodyAnimation.StopAllCoroutines();
            StopAllCoroutines();
        }
    }

    public void HideFmodHandWritting()
    {
        StopHistory();
        closeTooltip.Invoke();
        tooltip.gameObject.SetActive(false);
        character.gameObject.SetActive(false);
        isHide = true;
    }

    public void ReturnFmodHandWritting()
    {
        tooltip.gameObject.SetActive(true);
        character.gameObject.SetActive(true);
        isHide = false;
    }


    void MuteSound(){
        muteBtn.onClick.RemoveAllListeners();
        muteBtn.transform.Find ("Image").GetComponent<Image> ().sprite = muteImg;
        fmodEmitter.setVolume(0);

        soundAction = ActiveSound;
        muteBtn.onClick.AddListener(() => soundAction());
        isMute = true;
    }

	void ActiveSound(){
        muteBtn.onClick.RemoveAllListeners();
        muteBtn.transform.Find ("Image").GetComponent<Image> ().sprite = soundImg;
        fmodEmitter.setVolume(1);

        soundAction = MuteSound;
        muteBtn.onClick.AddListener(() => soundAction());
        isMute = false;
    }

	private void CalculateWaitingTime(string phrase){

	 	int wordsCount = 0;
		int letterCount = 0;
		string contentBuffer = phrase;

		letterCount = contentBuffer.Trim ().Length;
		wordsCount = contentBuffer.Split (' ').Length;

        waitBetweenWords =  clipLenght/wordsCount;
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

    void PutDialog(string audioPath, string phrase)
    {
        try {
            FMODUnity.RuntimeManager.GetEventDescription(audioPath);
        }
        catch (EventNotFoundException)
        {
            audioPath = defaultAudioPath;
            //throw new EventNotFoundException(audioPath);
        }

        if (IsPlaying(fmodEmitter))
            fmodEmitter.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);

        fmodEmitter = FMODUnity.RuntimeManager.CreateInstance(audioPath);
     
        clipLenght = GetClipDuration();
        if (syncrony)
        {
            CalculateWaitingTime(phrase);
        }

        fmodEmitter.start();

        if (isMute)
        {
            MuteSound();
        }else
        {
            ActiveSound();
        }
       
    }

    private float GetClipDuration()
    {
        FMOD.Studio.EventDescription description;
        fmodEmitter.getDescription(out description);
        int lenght = 0;
        description.getLength(out lenght);
        return (float)lenght/1000;//s
    }

    public void ChangePhrase(Phrase[] phrases, int indexMoment)
    {
        moments[indexMoment].phrases = phrases;
    }

}

