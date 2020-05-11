using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Datasheet_Row : MonoBehaviour {

    public GameObject cellPrefab;
    
    GridLayoutGroup layoutGroup;

    // Use this for initialization
    public void Build (float cellsWidth, float cellsHeigth, Row row) {

        layoutGroup = GetComponent<GridLayoutGroup>();
        layoutGroup.cellSize = new Vector2(cellsWidth, cellsHeigth);
        GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, cellsHeigth);
        GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, cellsWidth* row.cells.Count);
        GetComponent<Image>().color = row.backgroundColor;
        for (int i = 0; i < row.cells.Count; i++)
        {
            GameObject ins = Instantiate(cellPrefab, transform);
            ins.transform.localScale = Vector3.one;
            ins.GetComponentInChildren<Text>().text = row.cells[i];
            ins.GetComponent<Image>().color = row.backgroundColor;
            ins.GetComponent<Outline>().enabled= row.cellsBorder;
            ins.SetActive(true);
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
