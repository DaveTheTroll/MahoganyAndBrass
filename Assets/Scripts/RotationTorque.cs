using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationTorque : MonoBehaviour
{
	public float maxTorque = 0.05f;
	public float gain = 0.1f;
	public float desiredSpeed = Mathf.PI/2; // rad/s
	public Vector3 axis = Vector3.up;

	void FixedUpdate()
	{
		Rigidbody rigidBody = GetComponent<Rigidbody>();
		float speed = Vector3.Dot(rigidBody.angularVelocity, axis); // rad/s
		float dSpeed = desiredSpeed - speed;
		float torque = Mathf.Min(Mathf.Abs(dSpeed * gain), maxTorque) * Mathf.Sign(dSpeed);

		rigidBody.AddTorque(torque * axis, ForceMode.Force);
	}
}
