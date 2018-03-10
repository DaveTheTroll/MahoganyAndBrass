using UnityEngine;

namespace MahoganyAndBrass
{
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
			if (range > 0)
			{
				childRay.maxDistance = range;
			}
		}
	}
}