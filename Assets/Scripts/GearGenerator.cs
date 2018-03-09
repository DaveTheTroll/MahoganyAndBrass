using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class GearGenerator : MonoBehaviour
{
	public Material material;
	public int cogCount = 10;
	
	bool recreate = true;
	void OnValidate()
	{
		recreate = true;
	}

	const string cogName = "__COG__";

	void Update()
	{
		if (recreate)
		{
			recreate = false;

			for(int i=transform.childCount-1; i>=0; i--)
			{
				Transform child = transform.GetChild(i);
				if (child.gameObject.name == cogName)
				{
					child.parent = null;
					DestroyImmediate(child.gameObject);
				}
			}

			Cog cog = new Cog(cogCount);

			for(int i=0; i<cogCount; i++)
			{
				GameObject cogObj = new GameObject();
				MeshFilter filter = cogObj.transform.gameObject.AddComponent<MeshFilter>();
				MeshCollider collider = cogObj.transform.gameObject.AddComponent<MeshCollider>();
				MeshRenderer renderer = cogObj.transform.gameObject.AddComponent<MeshRenderer>();
				filter.sharedMesh = cog.Mesh;
				collider.sharedMesh = filter.sharedMesh;
				if (material != null)
				{
					renderer.material = material;
				}
				cogObj.transform.rotation = Quaternion.Euler(0, 0, i * 360 / cogCount);
				cogObj.transform.parent = transform;
				cogObj.name = cogName;
			}
		}
	}
}
