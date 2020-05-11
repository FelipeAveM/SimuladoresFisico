using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Row
{
    public Color backgroundColor = Color.white;
    public List<string> cells = new List<string>();
    public bool cellsBorder = true;
}

public class Datasheet : MonoBehaviour {

    public GameObject rowPrefab;
    public List<Row> rows = new List<Row>();

    public float cellsWidth = 200;
    public float cellsHeigth = 200;

    void Start()
    {
        Build();
    }

    public void Build()
    {
        for (int i = 0; i < rows.Count; i++)
        {
            GameObject ins = Instantiate(rowPrefab, rowPrefab.transform.parent);
            ins.transform.localScale = Vector3.one;
            ins.GetComponentInChildren<Datasheet_Row>().Build(cellsWidth, cellsHeigth, rows[i]);
            ins.SetActive(true);
        }
    }

}
