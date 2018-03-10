using UnityEngine;

public class EditOnlyLight : MonoBehaviour
{
	void Start()
	{
		Light light = GetComponent<Light>();
		light.enabled = false;
	}
	void Update()
	{

	}
}
