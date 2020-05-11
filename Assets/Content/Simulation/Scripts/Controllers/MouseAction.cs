using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent (typeof(MaterialHighlighter))]
public class MouseAction : MonoBehaviour {

	public bool active = false;
	public Texture2D cursorTexture;
    public Transform materialParent;
    public UnityEvent OnMouseClick;
    public UnityEvent OnMouseOverEvent;

    private Vector2 hotSpot = Vector2.zero;
	private MaterialHighlighter materialHighlighter;
	private CursorMode cursorMode = CursorMode.Auto;

	void Start(){
		materialHighlighter = transform.GetComponent<MaterialHighlighter>();
	}

	void OnMouseDown(){
		if (active) {
			OnMouseClick.Invoke ();
            SimpleCursor();
            if (materialParent == null)
            {
                materialHighlighter.Highlighter(transform, Color.black);
            }
            else {
                materialHighlighter.Highlighter(materialParent, Color.black);
            }
            
        }
    }

	void OnMouseOver(){
		if(active){
			Cursor.SetCursor(cursorTexture, hotSpot, cursorMode);
            OnMouseOverEvent.Invoke();
            if (!materialHighlighter.isHight)
            {
                if (materialParent == null)
                {
                    materialHighlighter.Highlighter(transform, materialHighlighter.hightColor);
                }
                else
                {
                    materialHighlighter.Highlighter(materialParent, materialHighlighter.hightColor);
                }
            }
        }
	}

	void OnMouseExit(){  
		if (active) {
			SimpleCursor ();
            if (materialParent == null)
            {
                materialHighlighter.Highlighter(transform, Color.black);
            }
            else
            {
                materialHighlighter.Highlighter(materialParent, Color.black);
            }
        }
	}

    public void SimpleCursor(){
		Cursor.SetCursor (null, hotSpot, cursorMode);
	}
}
