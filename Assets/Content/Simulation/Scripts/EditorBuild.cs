using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR 
using UnityEditor;
#endif
using FMODUnity;


public class EditorBuild : MonoBehaviour {

#if UNITY_EDITOR
    public class MessageHelp {
        public string helpMessage = "Welcome Editor mode";
        public MessageType messageType = MessageType.Info;
        public MessageHelp() { }

        public MessageHelp(string helpMessage, MessageType messageType)
        {
            this.helpMessage = helpMessage;
            this.messageType = messageType;
        }
    }

    // Audios List
    public FmodHandWriting fmodTarget;
    [EventRef]
    public string defaultAudio;
    public CHARACTER characterDefault = CHARACTER.Bernard;
    [TextArea]
    // Input text
    public string story;
    // List phrases
    public string[] phrases;
    // Moment Object
    public FmodHandWriting.Phrase[] phrasesList;
    // Audios List
    [EventRef]
    public string[] audiosList;

    public MessageHelp SizeHistory()
    {
        phrases = story.Split('\n');

        if (phrases.Length > 1)
        {
            return (new MessageHelp("This history have :" + phrases.Length +" Phrases", MessageType.Info));
        }
        else
        {
            return (new MessageHelp("Don't have prhases", MessageType.Error));
        }
    }

    public MessageHelp CreateHistory()
    {
        phrasesList = new FmodHandWriting.Phrase[phrases.Length];
        audiosList = new string[phrases.Length];
        int index = 0;
        foreach (var phrase in phrases)
        {
            FmodHandWriting.Phrase phraseObject = new FmodHandWriting.Phrase();
            phraseObject.phrase = phrase;
            phraseObject.fontSize = 14;
            phraseObject.selectCharacter = characterDefault;
            phraseObject.justyfy = TextAnchor.UpperLeft;
            phraseObject.waitEndAction = 0.1f;
            phraseObject.bodyAnimation = ANIMATION_LIST.Grettings; ;
            phraseObject.audioPath = defaultAudio;
            audiosList[index] = defaultAudio;
            phrasesList[index] = phraseObject;
            index++;
        }
        return (new MessageHelp("Create phrase list", MessageType.Info));
    }

    public MessageHelp AddAudio()
    {
        int index = 0;
        foreach (var phrase in phrasesList)
        {
    
            phrase.audioPath = audiosList[index];
            phrasesList[index] = phrase;
            index++;
        }
        return (new MessageHelp("Create Audio list", MessageType.Info));
    }

    public MessageHelp PutHistory(int index)
    {
        fmodTarget.ChangePhrase(phrasesList, index);
        return (new MessageHelp("History was Updated", MessageType.Info));
    }

    public MessageHelp Reset()
    {
        story = "";
        fmodTarget = null;
        defaultAudio = "";
        phrases = null;
        phrasesList = null;
        audiosList = null;

        return (new MessageHelp("Welcome Editor mode", MessageType.Info));
    }
#endif
}
