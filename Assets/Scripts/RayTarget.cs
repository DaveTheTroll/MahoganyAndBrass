using UnityEngine;

namespace MahoganyAndBrass
{
	public interface IRayTarget
	{
		void OnHit(Ray ray, RaycastHit hitInfo);
	}
}