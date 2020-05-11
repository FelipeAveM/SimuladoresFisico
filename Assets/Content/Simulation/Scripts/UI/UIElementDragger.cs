using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIElementDragger : MonoBehaviour {

    public const string DRAGGABLE_TAG = "Draggable";
	public GameObject ui;
	public Color highLightColor;

    private bool dragging = false;
    private Vector2 originalPosition;
    private Transform objectToDrag;
	private Image objectToDragImage;

    List<RaycastResult> hitObjects = new List<RaycastResult>();

    #region Monobehaviour API

    void Update ()
    {
		if (Input.GetMouseButtonDown(0))
        {
            objectToDrag = GetDraggableTransformUnderMouse();

            if (objectToDrag != null)
            {
				toggleTextEnable ( objectToDrag.transform, false);
                dragging = true;

                objectToDrag.SetAsLastSibling();

                originalPosition = objectToDrag.position;
                objectToDragImage = objectToDrag.GetComponent<Image>();
				objectToDragImage.color = highLightColor;
				objectToDragImage.raycastTarget = false;
            }
        }

        if (dragging)
        {
            objectToDrag.position = Input.mousePosition;
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (objectToDrag != null)
            {
                var objectToReplace = GetDraggableTransformUnderMouse();

                if (objectToReplace != null)
                {
                    objectToDrag.position = objectToReplace.position;
                    objectToReplace.position = originalPosition;
                }
                else
                {
                    objectToDrag.position = originalPosition;
                }

                objectToDragImage.raycastTarget = true;
				toggleTextEnable (objectToDrag, true);
				unSelectDraggables (objectToDrag.name);
                objectToDrag = null;

            }

            dragging = false;


        }
	}

    private GameObject GetObjectUnderMouse()
    {
        var pointer = new PointerEventData(EventSystem.current);

        pointer.position = Input.mousePosition;

        EventSystem.current.RaycastAll(pointer, hitObjects);

        if (hitObjects.Count <= 0) return null;

        return hitObjects.First().gameObject;        
    }

    private Transform GetDraggableTransformUnderMouse()
    {
        var clickedObject = GetObjectUnderMouse();

        // get top level object hit
        if (clickedObject != null && clickedObject.tag == DRAGGABLE_TAG)
        {
            return clickedObject.transform;
        }

        return null;
    }

	private void toggleTextEnable(Transform item, bool active){
		
		Text[] textElement = item.parent.GetComponentsInChildren<Text> ();

		foreach(Text element in textElement){
			element.enabled = active;
		}
	}

	private void unSelectDraggables(string exption){
		GameObject[] draggables = GameObject.FindGameObjectsWithTag (DRAGGABLE_TAG);
		foreach(GameObject item in draggables){
			if(!exption.Equals(item.name))
				item.transform.GetComponent<Image> ().color = Color.white;
		}
	}

    #endregion
}
