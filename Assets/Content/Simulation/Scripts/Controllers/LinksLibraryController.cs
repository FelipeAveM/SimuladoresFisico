using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinksLibraryController : MonoBehaviour {

    public GameObject libraryItemPrefab;

	// Use this for initialization
	void Start () {
        TextAsset data = Resources.Load<TextAsset>("data");
        SimpleJSON.JSONNode jNode = SimpleJSON.JSONNode.Parse(data.text);
        foreach (var i in jNode["library"].Children)
        {
            Debug.Log(i);
            GameObject go = Instantiate(libraryItemPrefab, libraryItemPrefab.transform.parent);
            go.transform.localScale = Vector3.one;
            go.GetComponentInChildren<UnityEngine.UI.Button>().onClick.AddListener(() => { Application.OpenURL(i["link"].Value); });
            UnityEngine.UI.Text[] texts = go.GetComponentsInChildren<UnityEngine.UI.Text>();
            texts[0].text = i["title"].Value;
            texts[1].text = i["link"].Value;
            go.SetActive(true);
        }
    }
}
