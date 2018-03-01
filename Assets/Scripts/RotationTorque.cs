using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationTorque : MonoBehaviour
{
	public float maxTorque = 0.05f;
	public float gain = 0.1f;
	public float desiredSpeed = Mathf.PI/2; // rad/s
	public Vector3 axis = Vector3.up;

	float speed = 0;

	void FixedUpdate()
	{
		Rigidbody rigidBody = GetComponent<Rigidbody>();
		float speed = Vector3.Dot(rigidBody.angularVelocity, axis); // rad/s
		float dSpeed = desiredSpeed - speed;
		float torque = Mathf.Min(Mathf.Abs(dSpeed * gain), maxTorque) * Mathf.Sign(dSpeed);

		Debug.LogFormat("Torque {0:0.0000} DSpeed {1:0.0000}", torque, dSpeed);

		rigidBody.AddTorque(torque * axis, ForceMode.Force);
	}
}
