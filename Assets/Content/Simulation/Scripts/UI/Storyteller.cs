using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

[System.Serializable]
public struct Story
{
    public string title;
    [TextArea()]
    public string body;
    public Sprite bodySprite;
    public Sprite characterSprite;
    public AudioClip clip;
    public string actionButtonLabel;

    public UnityEvent onActionClick;
    public UnityEvent onAudioPlaybackStart;
}

public class Storyteller : MonoBehaviour {

    [SerializeField]
    public Story[] stories;

    public Text title;
    public Text body;
    public Image bodyImg;
    public GameObject videoContainer;
    public Image characterImg;
    public Button actionButton;
    public Button playMediaButton;
    public Button next;
    public Button back;

    public int actualStory = 0;

    // Use this for initialization
    void Start () {
        next.onClick.AddListener(Next);
        back.onClick.AddListener(Back);
        GenerateStory();
    }

    void GenerateStory()
    {
        title.text = stories[actualStory].title;
        body.text = stories[actualStory].body;
        actionButton.GetComponentInChildren<Text>().text = stories[actualStory].actionButtonLabel;

        if (stories[actualStory].bodySprite != null)
        {
            bodyImg.sprite = stories[actualStory].bodySprite;
            bodyImg.gameObject.SetActive(true);
        }
        else
        { bodyImg.gameObject.SetActive(false); }

        if (stories[actualStory].characterSprite != null)
        {
            characterImg.sprite = stories[actualStory].characterSprite;
            characterImg.gameObject.SetActive(true);
        }
        else
        { characterImg.gameObject.SetActive(false); }
			
        if (stories[actualStory].clip != null)
        {
            playMediaButton.onClick.RemoveAllListeners();
            playMediaButton.onClick.AddListener(() => { stories[actualStory].onAudioPlaybackStart.Invoke(); });
            playMediaButton.onClick.AddListener(() => { AudioSource.PlayClipAtPoint(stories[actualStory].clip, Vector3.zero); });
        }

        actionButton.gameObject.SetActive(!string.IsNullOrEmpty(stories[actualStory].actionButtonLabel));
        actionButton.onClick.RemoveAllListeners();
        actionButton.onClick.AddListener(() => { stories[actualStory].onActionClick.Invoke(); });
    }

	// Update is called once per frame
	void Update () {
		
	}

    public void Next()
    {
        if (actualStory < stories.Length - 1)
        {
            actualStory++;
            GenerateStory();
        }
    }

    public void Back()
    {
        if (actualStory >0)
        {
            actualStory--;
            GenerateStory();
        }
    }
}
