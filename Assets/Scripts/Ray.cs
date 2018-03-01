using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ray : MonoBehaviour
{
	public float maxDistance = 1000;

	Ray parentRay;
	internal int age;

	internal Ray CreateChildRay()
	{
		Ray child = Component.Instantiate<Ray>(this);
		child.parentRay = this;
		return child;
	}

	Ray Prefab
	{
		get
		{
			return parentRay == null ? this : parentRay.Prefab; // A prefab is the only Ray that has no parentRay.
		}
	}
	void Update()
	{
		float distance;
		RaycastHit hitInfo;
		if (Physics.Raycast(transform.position, transform.rotation * Vector3.up, out hitInfo, maxDistance))
		{
			distance = hitInfo.distance;
			// TODO: Check for ray transparent

			IRayTarget target = hitInfo.collider.gameObject.GetComponent(typeof(IRayTarget)) as IRayTarget;
			if (target != null)
			{
				target.OnHit(this, hitInfo);
			}
		}
		else
		{
			distance = maxDistance;
		}
		transform.localScale = new Vector3(transform.localScale.x / transform.lossyScale.x,
			distance * transform.localScale.y / transform.lossyScale.y,
			transform.localScale.z / transform.lossyScale.z);
	}
}
