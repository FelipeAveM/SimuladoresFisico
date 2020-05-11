using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boxExplosion : MonoBehaviour {
	public float speed = 1.0f; //how fast it shakes
	public float amount = 1.0f; //how much it shakes
	private GameObject objeto;
	private float x;
	private float z;

	// Use this for initialization
	void Start () {
		objeto = this.gameObject;
		x = objeto.transform.position.x;
		z = objeto.transform.position.z;
	}

	// Update is called once per frame
	void Update()
	{
		Vector3 position = new Vector3();
		position = objeto.transform.position;
		position.x = x + Mathf.Sin(Time.time * speed) * amount;
		position.z = z + Mathf.Sin(Time.time * speed) * amount;
		objeto.transform.position = position;
	}
}
