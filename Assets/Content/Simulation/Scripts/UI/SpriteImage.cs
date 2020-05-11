using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.Serialization;

public enum ANIMATION_LIST
{
    Lipsycn,
    Grettings,
    Whenever,
    Advice,
    Bored,
    Clap,
    Count,
    Point,
    PointAreaRight,
    PointRight,
    PointUP,
    UpHands,
    Share,
    Default
}

[System.Serializable]
public class Transition : System.Object
{
    public Sprite image;
}

public class SpriteImage : MonoBehaviour {

	[System.Serializable]
	public class AnimationCycle : System.Object
	{
		public string animationName;
		public bool pinPong;
		public Transition[] transitions;
		public 	UnityEvent action;
	}

	public List<AnimationCycle> animationList; 
	public float timeBetweenTranstions;
	public bool start;
	public bool loop = true;
	public bool isClose = true;
	public bool destroyOnEnd = false;
	public Sprite baseImage;
	public ANIMATION_LIST defaultAnimation = ANIMATION_LIST.Lipsycn;

	private Image image;

	void Awake()
	{
		image = GetComponent<Image>();
		if (start){
			StartAnimation (defaultAnimation);
		}
	}

	public void StartAnimation(ANIMATION_LIST animationName){
        StopAnimation();
        foreach (AnimationCycle element in animationList){
			if (animationName.ToString().Equals (element.animationName)) {
				StartCoroutine (StarAnimationProcess (element.transitions, element.pinPong).GetEnumerator ());
				break;
			}
		}
	}

	IEnumerable StarAnimationProcess(Transition[] transitions, bool isPinPong)
    { 
        do {
			if(!isClose){
                foreach (Transition transition in transitions) {
                    yield return StartCoroutine (ChangeSprite(transition).GetEnumerator());
				}
              
                if (isPinPong){
					for (int i = transitions.Length - 1; i >= 0; i--) {
						yield return StartCoroutine (ChangeSprite(transitions[i]).GetEnumerator());
					}
				}
               

			}else{
				image.sprite = baseImage;
			}
			yield return new WaitForSeconds(timeBetweenTranstions);
		} while(loop);
		StopAnimation ();
	}

	IEnumerable ChangeSprite(Transition transition){
		image.sprite = transition.image;
		yield return new WaitForSeconds(timeBetweenTranstions); 
	}

	public void StopAnimation(){
		StopAllCoroutines();
		image.sprite = baseImage;
	}
}
	
