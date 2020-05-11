using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBastago : MonoBehaviour {

    public float smooth = 0.5f;
    public float limitMin = -40f;
    public float limitMax = 25f;

    private Quaternion target;
    private float angleTarget;

    void Start()
    {
        angleTarget = limitMax;
        StartCoroutine(ToggleDirection());
    }

    void Update()
    {
        target = Quaternion.Euler(0, 25, angleTarget);
        transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * smooth);
    }

    private IEnumerator ToggleDirection()
    {
        yield return new WaitForSeconds(2.5f);
        if (angleTarget == limitMax)
        {
            angleTarget = limitMin;
        }
        else if (angleTarget == limitMin)
        {
            angleTarget = limitMax;
        }
        StartCoroutine(ToggleDirection());
    }
}
