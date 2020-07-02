﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class URLParameterReader : MonoBehaviour {
    
    public static UserData usuario;
    public static DataRisk dataRisk;

        // Use this for initialization
        void Start () {
            //usuario = new userData();
            Dictionary <string, string> dict = URLParameters.GetSearchParameters();
            string str;
            if (dict.TryGetValue("guid", out str)){
                usuario.Guid = str;
                Debug.Log("------------El guid del usuario es: " + usuario.Guid);
                FlowControllerPhysical.getUserByGuid();
            }
            if (dict.TryGetValue("id", out str))
                usuario.Id = str;
            if (dict.TryGetValue("risk", out str))
            {
                int intData;
                int.TryParse(str, out intData);
                usuario.Risk = intData;
            }
        string urlAPI = gameObject.GetComponent<ApiController>().UrlBaseAPI + "api/Generic/InformationUser?guid=" + usuario.Guid;
        StartCoroutine(GetRequest(urlAPI,this.UserCallback));
        print(urlAPI);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    IEnumerator GetRequest(string uri, Action<string> callback = null)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();

            if (webRequest.isNetworkError)
            {
                Debug.Log(": Error: " + webRequest.error);
            }
            else
            {
                var data = webRequest.downloadHandler.text;
                if (callback != null)
                    callback(data);
            }
        }
    }

    private void UserCallback(string data)
    {
        usuario = JsonUtility.FromJson<UserData>(data);
        dataRisk.Usuario = usuario;
        //Destroy(GetComponent<ApiController>());
    }

    private void DBCallback(string data)
    {
        dataRisk = JsonUtility.FromJson<DataRisk>(data);
        dataRisk.Usuario = usuario;
        Destroy(GetComponent<ApiController>());
    }
}
