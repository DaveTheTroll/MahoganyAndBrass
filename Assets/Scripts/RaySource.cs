using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaySource : MonoBehaviour
{
	public GameObject ray;

	void Start()
	{
		Ray childRay = GameObject.Instantiate(ray).GetComponent<Ray>();
		childRay.name = name + ":Ray";
		childRay.transform.parent = transform;
		childRay.transform.position = transform.position;
		childRay.transform.rotation = transform.rotation;
	}
}
