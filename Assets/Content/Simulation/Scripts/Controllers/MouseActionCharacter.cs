using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent (typeof(MaterialHighlighter))]
public class MouseActionCharacter : MonoBehaviour {

	public Vector3 offsetFocus;
	public UnityEvent OnMouseClick;
	public Texture2D cursorTexture;

	private Vector2 hotSpot = Vector2.zero;
	private MaterialHighlighter materialHighlighter;
	private CursorMode cursorMode = CursorMode.Auto;

	void Start(){
		materialHighlighter = transform.GetComponent<MaterialHighlighter> ();
	}

	void OnMouseDown(){
		//CameraFocus (transform);
		OnMouseClick.Invoke ();
	}

	void OnMouseOver(){
		Cursor.SetCursor(cursorTexture, hotSpot, cursorMode);
		if(!materialHighlighter.isHight )
			materialHighlighter.Highlighter(transform, materialHighlighter.hightColor);
	}

	void OnMouseExit(){  
		Cursor.SetCursor(null, hotSpot, cursorMode);
		materialHighlighter.Highlighter(transform, Color.black);
	}
		

	/*void CameraFocus (Transform target) {

		Vector3 pointOnside = target.position + new Vector3 (target.localScale.x, 0.0f, target.localScale.z) + offsetFocus;
		float aspect = (float)Screen.width / (float)Screen.height;
		float maxDistance = (target.localScale.y * 0.5f) / Mathf.Tan (Mathf.Deg2Rad * (Camera.main.fieldOfView / aspect));

		Camera.main.transform.position = Vector3.MoveTowards (pointOnside, target.position, -maxDistance);	
		Camera.main.transform.LookAt (target.position + new Vector3(0,1f,0));
		//Move to left
		Camera.main.transform.position = Camera.main.transform.position  + new Vector3 (1f,0f,0f);;
	}*/
		
}
