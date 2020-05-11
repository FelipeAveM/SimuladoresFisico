using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Place : MonoBehaviour
{

    public Transform reference;
    public UnityEvent MovePlace;

    void Update()
    {
        transform.LookAt(reference);
    }

    void OnMouseOver()
    {
        transform.GetChild(0).gameObject.SetActive(true);
    }

    void OnMouseExit()
    {
        transform.GetChild(0).gameObject.SetActive(false);
    }

    void OnMouseDown()
    {
        MovePlace.Invoke();
    }
}
