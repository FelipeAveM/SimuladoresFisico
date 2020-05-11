using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using FMODUnity;
using System;
using System.Runtime.InteropServices;


public class LoadScene : MonoBehaviour {

    [System.Serializable]
    public class SceneInfo : System.Object
    {
        public string nameSimulator;
        public string namesScene;
        public Color backgroundColor;
        /*
        [BankRef]
        public List<string> Banks;
        */
    }

    public bool PreloadSamples;
    public List<SceneInfo> info;
    public float waitingTime = 0 ;
    public int nextScene;

    public Text title;
    public Image backGround;
    public Transform panelHelp;

	public Transform button;
	public Transform loadingCircle;

    public LoaderGameEvent LoadEvent;
    public LoaderGameEvent UnloadEvent;
    public bool isTesting;
    private bool isQuitting;


	private string sceneToLoad;

    void HandleGameEvent(LoaderGameEvent gameEvent)
    {
        if (LoadEvent == gameEvent)
        {
            Load();
        }
        if (UnloadEvent == gameEvent)
        {
            Unload();
        }
    }

    void Start() {
        DontDestroyOnLoad(this.gameObject);

        if (!isTesting) { 
            // Load info
            title.text = info[nextScene].nameSimulator;
            backGround.color = info[nextScene].backgroundColor;

            // Load escene
            StartCoroutine(LoadNextScene(waitingTime, info[nextScene].namesScene));
        }
        else
        {
            panelHelp.gameObject.SetActive(true);
        }

        // Keep changes
        RuntimeUtils.EnforceLibraryOrder();
        HandleGameEvent(LoaderGameEvent.ObjectStart);
        //DontDestroyOnLoad(gameObject);
    }

    public void StartNewScene(int nextScene)
    {
        // Load Info
        title.text = info[nextScene].nameSimulator;
        backGround.color = info[nextScene].backgroundColor;

        // Load Scene
        StartCoroutine(LoadNextScene(waitingTime, info[nextScene].namesScene));
    }


    void OnApplicationQuit()
    {
        isQuitting = true;
    }

    void OnDestroy()
    {
        if (!isQuitting)
        {
            HandleGameEvent(LoaderGameEvent.ObjectDestroy);
        }
    }

    public void Load()
    {/*
        foreach (var bankRef in info[nextScene].Banks)
        {
            try
            {
                RuntimeManager.LoadBank(bankRef, PreloadSamples);
                print("Load Banks: " + info[nextScene].namesScene);
            }
            catch (BankLoadException e)
            {
                UnityEngine.Debug.LogException(e);
            }
        }
        RuntimeManager.WaitForAllLoads();*/
    }

    public void Unload()
    {
        /*
         * foreach (var bankRef in info[nextScene].Banks)
        {
            RuntimeManager.UnloadBank(bankRef);
        }
        */
    }

	IEnumerator LoadNextScene(float waitingTime, string nameScene)
	{
		yield return new WaitForSeconds(waitingTime);

		while (FMODUnity.RuntimeManager.AnyBankLoading())
		{
			yield return new WaitForSeconds(0.5f);
		}

		Debug.Log("Master Bank Loaded");
		sceneToLoad = nameScene;
		button.gameObject.SetActive(true);
		loadingCircle.gameObject.SetActive(false);
	}

	public void LoadFinal(){
		SceneManager.LoadScene(sceneToLoad, LoadSceneMode.Single);
	}

}
