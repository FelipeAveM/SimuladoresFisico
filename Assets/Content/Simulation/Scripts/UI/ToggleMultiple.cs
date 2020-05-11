using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleMultiple : MonoBehaviour {

	public List<Toggle> toggleGroup;

	public List<Transform> GetActive(){
		List<Transform> activeElements = new List<Transform>();
		foreach(var element in toggleGroup){
			if(element.isOn)
				activeElements.Add (element.transform);
		}
		return activeElements;
	}

	public List<string> GetActiveNames(){
		List<string> activeElementsName = new List<string>();
		foreach(Toggle element in toggleGroup){
			if(element.isOn)
				activeElementsName.Add (element.transform.Find("Label").GetComponent<Text>().text);
		}
		return activeElementsName;
	}

    public void SetValues(List<string> values)
    {
        foreach (var element in values)
        {
            foreach (var item in toggleGroup)
            {
                string itemLabel = item.transform.Find("Label").GetComponent<Text>().text;
                if (itemLabel.Equals(element))
                {
                    item.isOn = true;
                }
            }
        }
    }

	public void SetAllTogglesOff(){
		foreach(var element in toggleGroup){
			element.isOn = false;
		}
	}

	public bool AnyIsActive(){
		foreach(var element in toggleGroup){
			if (!element.isOn)
				return true;
		}

		return false;
	}

}
