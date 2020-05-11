using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainSceneController : MonoBehaviour {

	public Transform itemScene;
	public RectTransform parentTable;
	public int rowHeight = 63;

	private  int countItem = 0;


	// Use this for initialization
	void Start () {
		
		int sceneCount = UnityEngine.SceneManagement.SceneManager.sceneCountInBuildSettings;     
		for( int i = 1; i < sceneCount; i++ )
		{
			CreateItem(i, System.IO.Path.GetFileNameWithoutExtension( UnityEngine.SceneManagement.SceneUtility.GetScenePathByBuildIndex( i ) ));
		}
			
	}
	
	void CreateItem(int position, string name){

		parentTable.sizeDelta=parentTable.sizeDelta+ new Vector2 (0, rowHeight) ;

		RectTransform item = Instantiate(itemScene) as RectTransform;
		item.parent=parentTable;
		item.name="item"+countItem.ToString();
		item.localScale=Vector3.one;
		item.anchoredPosition3D= new Vector3(0,position,0);
		countItem += 1;
		position=position-rowHeight;

		item.Find ("Text").GetComponent<Text> ().text = name;

		//add action
		item.Find("Button").GetComponent<Button>().onClick.AddListener(delegate {
			OpenScene(name);
		});
	}


	public void OpenScene(string name){
		SceneManager.LoadScene (name);
	}

}

