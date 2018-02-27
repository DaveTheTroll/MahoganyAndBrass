using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayTargetTest : MonoBehaviour, IRayTarget
{
	public void OnHit(Ray ray, RaycastHit hitInfo)
	{
		Debug.LogFormat("Hit! by {0}", ray.name);
	}
}
