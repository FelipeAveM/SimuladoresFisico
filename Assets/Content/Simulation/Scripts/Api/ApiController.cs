using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;
using System.IO;
using UnityEngine.Networking;

public enum ENDPOINT
{
	ENDPOINT_PDF_RISKS_OFFICE,
	ENDPOINT_PDF_BIOLOGY,
	ENDPOINT_PDF_SECURITY,
	ENDPOINT_PDF_CHEMISTRY,
	ENDPOINT_PDF_PHYSICAL,
	ENDPOINT_PDF_BIOLOGY_LABORATORY_CHARACTER,
	ENDPOINT_PDF_OFFICE_MATRIX,
	ENDPOINT_SAVE_ADVANCE
}

public class ApiController : MonoBehaviour {

    [System.Serializable]
    public class Link : System.Object{
        public string title;
        public string url;

        public string Title
        {
            get { return title; }
        }

        public string URL
        {
            get { return url; }
        }
    }

    [System.Serializable]
    public class LinkRoot : System.Object{
        public List<Link> links;

        public List<Link> LINKS
        {
            get { return links; }
        }
    }   
    private LinkRoot urlList;

    [DllImport("__Internal")]
	private static extern void OpenUrl(string str);

    public bool islocal = false;
    public static ApiController instance = null;
	private List<string> URL_LIST = new List<string>();
    private string URL_BASE_LOC = "http://localhost:3000/";
    public static string URL_BASE_LOC3000 = "http://localhost:3000/";
    private string URL_BASE_LOCAL = "http://127.0.0.1:3000/picolabs/";
	private string URL_BASE_SERVER;
    private string URL_BASE_APIPOLI = "https://prepro-apisimuladores.poligran.edu.co/";
    private string URL_BASE_DOCS;
	public static string ENDPOINT_PDF_RISKS_OFFICE = "risks/office";
	public static string ENDPOINT_PDF_BIOLOGY = "biology/result";
	public static string ENDPOINT_PDF_SECURITY = "security/result";
	public static string ENDPOINT_PDF_CHEMISTRY = "chemistry/result";
	public static string ENDPOINT_PDF_PHYSICAL = "physical/result";
    public static string ENDPOINT_PDF_BIOLOGY_LABORATORY_CHARACTER = "biology/laboratory/user";
	public static string ENDPOINT_PDF_OFFICE_MATRIX = "office/matrix";
	public static string ENDPOINT_SAVE_ADVANCE = "risks/save";

    public string UrlBase{
        get{
            if (islocal){
                return this.URL_BASE_LOC;
                //return this.URL_BASE_LOCAL;
            }
			//URL_BASE_SERVER = URL_LIST[0];
            return URL_BASE_LOC;
        }
    }

	public string UrlBaseDocs{
		get
		{
			if (islocal)
			{
				return this.URL_BASE_LOCAL;
			}
			URL_BASE_DOCS = URL_LIST[1];
			return URL_BASE_DOCS;
		}
	}

    public string UrlBaseAPI{
        get
        {
            if (islocal)
            {
                return this.URL_BASE_APIPOLI;
            }
            URL_BASE_APIPOLI = URL_LIST[3];
            return URL_BASE_APIPOLI;
        }
    }
    void Awake(){
        if (instance == null)
        {
            instance = this;
			string path = System.IO.Path.Combine(Application.streamingAssetsPath, "baseUrl.json");
			StartCoroutine(GetText(path));
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    IEnumerator GetText(string p) {
        #if UNITY_EDITOR_OSX
            p = p.Insert(0, "file://");
        #endif
        WWW www = new WWW(p);
		yield return www;
		LinkRoot items = JsonUtility.FromJson<LinkRoot>(www.text);
        foreach (Link item in items.LINKS)
        {
            URL_LIST.Add(item.URL);
        }
    }

    public WWW GET(string endPoint, System.Action<WWW> onComplete, System.Action<string> onError)
    {
        string url = UrlBase + endPoint;

        WWW www = new WWW(url);
        StartCoroutine(WaitForRequest(www, onComplete, onError));
        return www;
    }

    public WWW GETJSON(string urlBegin, string endPoint, string jsonPost, System.Action<WWW> onComplete, System.Action<string> onError)
    {
        string url = urlBegin + endPoint;
        //Debug.Log(url);
        Dictionary<string, string> headers = new Dictionary<string, string>();
		headers.Add("Content-Type", "application/json");
        //byte[] postData = System.Text.Encoding.ASCII.GetBytes(jsonPost.ToCharArray());
		byte[] getData = System.Text.Encoding.UTF8.GetBytes(jsonPost.ToCharArray());
        WWW www = new WWW(url, getData, headers);
        StartCoroutine(WaitForRequest(www, onComplete, onError));
        return www;
    }
    /*
    public WWW POST(string endPoint, string jsonPost, System.Action<WWW> onComplete, System.Action<string> onError){
        string url = UrlBase + endPoint;
        Dictionary<string, string> headers = new Dictionary<string, string>();
		headers.Add("Content-Type", "application/json");
        //byte[] postData = System.Text.Encoding.ASCII.GetBytes(jsonPost.ToCharArray());
		byte[] postData = System.Text.Encoding.UTF8.GetBytes(jsonPost.ToCharArray());

        WWW www = new WWW(url, postData, headers);
        StartCoroutine(WaitForRequest(www, onComplete, onError));
        return www;
    }
    */

    public WWW POST(string urlBegin, string endPoint, string jsonPost, System.Action<WWW> onComplete, System.Action<string> onError){
        string url = urlBegin + endPoint;
        Debug.Log(url);
        Dictionary<string, string> headers = new Dictionary<string, string>();
		headers.Add("Content-Type", "application/json");
        //byte[] postData = System.Text.Encoding.ASCII.GetBytes(jsonPost.ToCharArray());
		byte[] postData = System.Text.Encoding.UTF8.GetBytes(jsonPost.ToCharArray());

        WWW www = new WWW(url, postData, headers);
        StartCoroutine(WaitForRequest(www, onComplete, onError));
        //StartCoroutine(DoLast());
        return www;
    }
    private IEnumerator DoLast() {
        bool inFirst = true;
        while(inFirst) yield return new WaitForSeconds(0.1f);
    }
    private IEnumerator WaitForRequest(WWW www, System.Action<WWW> onComplete, System.Action<string> onError)
    {
        yield return www;
        if (!string.IsNullOrEmpty(www.error))
        {
            string error = www.text;
            Debug.Log(www.error);
            onError(error);
        }
        else
        {
            onComplete(www);
        }
        
    }

    public void OpenUrlInWeb(string url)
    {
        #if UNITY_EDITOR
            Application.OpenURL(url);
        #elif UNITY_WEBGL
            OpenUrl(url);
        #endif
    }

	public void OpenDocInWeb(string suffix)
	{
		string url = UrlBaseDocs + suffix;
		#if UNITY_EDITOR
			Application.OpenURL(url);
		#elif UNITY_WEBGL
			OpenUrl(url);
		#endif
	}

    public void OpenNorma()
    {
        #if UNITY_EDITOR
                Application.OpenURL(Application.streamingAssetsPath + "/normatecnica.pdf");
        #elif UNITY_WEBGL
        OpenUrl(Application.streamingAssetsPath + "/normatecnica.pdf");
        #endif  
    }
}
