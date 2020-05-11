using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CameraController : MonoBehaviour
{

	[Range(0, 10)]
	public float speedMovement;
	[Range(100, 300)]
	public float speedRotation;
	public float xMax = 30;
	public float xMin = -20;

	public Vector3 targetRot = Vector3.zero;

	public Tour tour;

	float posY;
	public void Start()
	{
		posY = transform.localPosition.y;
		targetRot = transform.localEulerAngles;
	}

	// Update is called once per frame
	void Update()
	{
		if (tour.moveFreehead && !tour.lookReference)
		{
			if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
			{
				transform.localPosition += transform.forward * speedMovement * Time.deltaTime;
			}
			if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
			{
				transform.localPosition -= transform.forward * speedMovement * Time.deltaTime;
			}
			if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
			{
				transform.localPosition += transform.right * speedMovement * Time.deltaTime;
			}
			if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
			{
				transform.localPosition -= transform.right * speedMovement * Time.deltaTime;
			}
			transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(targetRot), Time.deltaTime * 10);
			if (Input.GetKey(KeyCode.Mouse0))
			{
				targetRot.x -= Input.GetAxis("Mouse Y") * speedRotation * Time.deltaTime;
				targetRot.y += Input.GetAxis("Mouse X") * speedRotation * Time.deltaTime;
				targetRot.x = Mathf.Clamp(targetRot.x, xMin, xMax);
			}
		}
		transform.localPosition = new Vector3(transform.localPosition.x, posY, transform.localPosition.z);

	}
}