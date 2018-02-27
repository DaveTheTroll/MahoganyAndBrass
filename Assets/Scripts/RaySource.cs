using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaySource : MonoBehaviour
{
	public Ray ray;
	public float range = 10;

	Ray childRay;
	void Start()
	{
		childRay = ray.CreateChildRay();
		childRay.name = name + ":Ray";
		childRay.maxDistance = range;
		childRay.transform.parent = transform;
		childRay.transform.position = transform.position;
		childRay.transform.rotation = transform.rotation;
	}

	void Update()
	{
		childRay.maxDistance = range;
	}
}
