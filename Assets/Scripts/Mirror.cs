﻿using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MahoganyAndBrass
{
	public class Mirror : MonoBehaviour, IRayTarget
	{
		Dictionary<Ray, Ray> reflections = new Dictionary<Ray, Ray>();

		public void OnHit(Ray ray, RaycastHit hitInfo)
		{
			if (!reflections.ContainsKey(ray))
			{
				Ray newRefln = ray.CreateChildRay();
				newRefln.name = string.Format("{0}:{1}-Refln", ray.name, name);
				reflections.Add(ray, newRefln);
			}

			Ray refln = reflections[ray];

			refln.age = 0;
			refln.transform.position = hitInfo.point;
			Vector3 direction = Vector3.Reflect(ray.transform.rotation * Vector3.up, hitInfo.normal);
			refln.transform.rotation = Quaternion.FromToRotation(Vector3.up, direction);
			refln.maxDistance = ray.maxDistance - hitInfo.distance;

			refln.Update();
		}

		void Update()
		{
			foreach (Ray ray in reflections.Keys.ToList())
			{
				Ray refln = reflections[ray];
				refln.age++;
				if (refln.age >= 2)
				{
					Component.Destroy(refln.gameObject);
					reflections.Remove(ray);
				}
			}
		}
	}
}