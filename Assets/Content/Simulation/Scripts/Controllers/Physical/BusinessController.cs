using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

public class BusinessController : MonoBehaviour {

    private RectTransform empresa;
    private AlertMessage alertMessage;
    private DataBuilderPhysical dataBuilder;
    private FlowControllerPhysical flowController;

    public Transform GameControl;
    public Transform alert;
    public UnityEvent createCard;
    public UnityEvent closeCard;
    public UnityEvent saveCard;
    public UnityEvent saveAllCards;
    
    void Start()
    {
        flowController = GameControl.GetComponent<FlowControllerPhysical>();
        dataBuilder = GameControl.GetComponent<DataBuilderPhysical>();
        alertMessage = alert.GetComponent<AlertMessage>();
    }

    // Update is called once per frame
    void Update () {
		
	}
}
