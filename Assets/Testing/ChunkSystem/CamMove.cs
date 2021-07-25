using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamMove : MonoBehaviour
{
	[SerializeField] private float moveSpeed;

	// Start is called before the first frame update
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
		if (Input.GetKey(KeyCode.A))
		{
			transform.position -= transform.right * moveSpeed * Time.deltaTime;
		}
		if (Input.GetKey(KeyCode.D))
		{
			transform.position += transform.right * moveSpeed * Time.deltaTime;
		}
		if (Input.GetKey(KeyCode.W))
		{
			transform.position += transform.forward * moveSpeed * Time.deltaTime;
		}
		if (Input.GetKey(KeyCode.S))
		{
			transform.position -= transform.forward * moveSpeed * Time.deltaTime;
		}
	}
}
