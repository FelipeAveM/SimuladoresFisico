using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AnimationIndice
{
    None,
    Bashful,
    Carrying,
    DiceIdle,
    DwarfIdle,
    Idle,
    Idle1,
    Idle2,
    IdleSad,
    JogCircle,
    PacingTalking,
    SeatedIdle,
    SittingDisbelief,
    SittingIdle,
    SittingMeetingMale,
    SittingTalking,
    SittingTalking1,
    SittingTyping,
    SittingWorking,
    Talking,
    Talking2,
    Talking3,
    Texting,
    Threatening,
    WalkCircle,
    WalkInCircle,
	HappyIdle
}

public class AnimatorGeneral : MonoBehaviour {

    private Animator animator;
    public AnimationIndice animationIndice = AnimationIndice.Bashful;

    // Use this for initialization
    void Start () {
        animator = GetComponent<Animator>();
        animator.SetInteger("AnimationIndice", (int)animationIndice);
        //StartCoroutine(WaitingBaseSate());
    }
    /*
    IEnumerator WaitingBaseSate()
    {

        yield return new WaitForEndOfFrame();
        animator.SetInteger("AnimationIndice", (int)AnimationIndice.None);
    }*/
	
}
