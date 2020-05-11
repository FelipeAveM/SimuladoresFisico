using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.EventSystems;

public class Tooltip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Vector2 size = new Vector2(200,75);
    public Vector2 offset = new Vector2(0,50);
    [Tooltip("Time in seconds to show tooltip")]
    public float showTooltipAfter = 1;

    [Header("Content")]
    public string title;
    [TextArea()]
    public string body;
    
    CanvasGroup group;

    float groupTargetAlpha = 1;
    float tooltipTimer;
    bool isHover = false;

    // Use this for initialization
    void Start () {
        //load and spawn tooltip inside this component owner
        GameObject goInstance = Instantiate(Resources.Load<GameObject>("UI/UI_Tooltip"), transform);
        goInstance.transform.localScale = Vector3.one;

        //move tooltip to the desired position (owner position + offset)
        Vector3 targetPos = transform.position + new Vector3(offset.x, offset.y);
        targetPos.y = Mathf.Clamp(targetPos.y, size.y / 2, GetComponentInParent<Canvas>().pixelRect.height - (size.y / 2));
        targetPos.x = Mathf.Clamp(targetPos.x, size.x / 2, GetComponentInParent<Canvas>().pixelRect.width - (size.x / 2));
        goInstance.transform.position = targetPos;

        //get canvas group
        group = goInstance.GetComponent<CanvasGroup>();

        //get rect transform and apply desired size
        RectTransform rect = goInstance.GetComponent<RectTransform>();
        rect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, size.x);
        rect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, size.y);

        //set the default value for tooltip timer
        tooltipTimer = showTooltipAfter;

        //fill texts
        Text[] texts = goInstance.GetComponentsInChildren<Text>();
        texts[0].text = title;
        texts[1].text = body;
    }
	
	// Update is called once per frame
	void Update () {

        if (isHover)
        {
            if (tooltipTimer <= 0) groupTargetAlpha = 1;
            else tooltipTimer -= Time.deltaTime;
        }
        else
        {
            groupTargetAlpha = 0;
        }

        //update tooltip alpha accord to isHover and timer values 
        group.alpha = Mathf.Lerp(group.alpha, groupTargetAlpha, Time.deltaTime * 5);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        isHover = true;
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        isHover = false;
        tooltipTimer = showTooltipAfter;
    }
}
