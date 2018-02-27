using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ray : MonoBehaviour
{
	public float maxDistance = 1000;

	float baseScale;

	void Start()
	{
		baseScale = transform.localScale.y;
	}

	void Update()
	{
		float distance;
		RaycastHit hitInfo;
		if (Physics.Raycast(transform.position, transform.rotation * Vector3.up, out hitInfo))
		{
			distance = hitInfo.distance;
		}
		else
		{
			distance = maxDistance;
		}
		transform.localScale = new Vector3(transform.localScale.x, baseScale * distance, transform.localScale.z);
	}
}
