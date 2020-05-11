using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Runtime.InteropServices;
using System.IO;

public class LibraryList : MonoBehaviour {

	[System.Serializable]
	public class Link : System.Object{
		public string title;
		public string url;

		public string Title{
			get { return title;}
		}

		public string URL{
			get { return url;}
		}
	}

	[System.Serializable]
	public class Library : System.Object{
		public int simulador;
		public List<Link> links;

		public int Simulador{
			get { return simulador;}
		}

		public List<Link> Links{
			get { return links;}
		}
	}

	[System.Serializable]
	public class LibraryRoot : System.Object{
		public List<Library> library;

		public List<Library> Library{
			get { return library;}
		}
	}

	public List<Link> library;
	public int rowHeight = 70;
	public Transform listItemPrefab;
    public ApiController apiController;
	public FlowController flowController;
    public Color colorTitle;
	public Color colorBack;
	private LibraryRoot items;

	void Start(){
		library = new List<Link>();
		string path = System.IO.Path.Combine(Application.streamingAssetsPath, "library.json");
		#if UNITY_EDITOR
		using (StreamReader r = new StreamReader(path))
		{
			string json = r.ReadToEnd();
			items = JsonUtility.FromJson<LibraryRoot>(json);
		}
		libraryCreation();
		#elif UNITY_WEBGL
		StartCoroutine(GetText(path));
		#endif
	}

	IEnumerator GetText(string p) {
		WWW www = new WWW(p);
		yield return www;
		items = JsonUtility.FromJson<LibraryRoot>(www.text);
		//print(items);
		libraryCreation();
	}

	void libraryCreation(){
		foreach(var item in items.library){
			if(item.simulador == flowController.simulator){
				foreach(var link in item.links){
					library.Add(link);
				}
			}
		}

		int count = 0;
		foreach(var link in library){
			CreateLibraryList (count, link);
			count++;
		}
	}

		
	void CreateLibraryList(int index, Link link){

		RectTransform parent = (RectTransform)transform;

		var position=index * (-rowHeight);
		RectTransform item = Instantiate(listItemPrefab) as RectTransform;
		item.SetParent(parent);
		item.name = "item List";
		item.localScale = Vector3.one;
		item.anchoredPosition3D = new Vector3(0,position);
		item.sizeDelta = new Vector2 (0, rowHeight);
		parent.sizeDelta = parent.sizeDelta + new Vector2 (0, rowHeight) ;

		item.Find("Title").GetComponent<Text>().text = link.title;
		item.Find("Title").GetComponent<Text>().color = colorTitle;
		item.GetComponent<Image>().color = colorBack;

		if (link.url != null) {
			item.GetComponent<Button> ().onClick.AddListener (delegate {
                OpenUrlInWeb(link.url);
			});
		}
	}

    public void OpenUrlInWeb(string url)
    {
		#if UNITY_EDITOR
		    Application.OpenURL (url);
		#elif UNITY_WEBGL
			apiController.OpenUrlInWeb(url);
        #endif
    }


}
