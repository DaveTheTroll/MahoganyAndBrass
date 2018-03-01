using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationTorque : MonoBehaviour
{
	public float moment = 0.05f;

	void FixedUpdate()
	{
		Rigidbody rigidBody = GetComponent<Rigidbody>();
		rigidBody.AddTorque(moment * Vector3.up, ForceMode.Force);
	}
}
