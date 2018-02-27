using UnityEngine;

public interface IRayTarget
{
	void OnHit(Ray ray, RaycastHit hitInfo);
}
