using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mosquito : MonoBehaviour
{
    public float speedX = 1.0f; //how fast it shakes
    public float speedY = 1.0f; //how fast it shakes
    public float speedZ = 1.0f; //how fast it shakes
    public float amountX = 1.0f; //how much it shakes
    public float amountY = 1.0f; //how much it shakes
    public float amountZ = 1.0f; //how much it shakes
    private Vector3 origin;

    // Use this for initialization
    void Start()
    {
        origin = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 position = new Vector3();
        position = origin;
        position.x = origin.x + Mathf.Sin(Time.time * speedX) * amountX;
        position.y = origin.y + Mathf.Cos(Time.time * speedY) * amountY;
        position.z = origin.z + Mathf.Sin(Time.time * speedZ) * amountZ;
        transform.position = position;
    }
}
