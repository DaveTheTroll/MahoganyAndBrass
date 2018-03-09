using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cog
{
	public Cog(int cogCount, float thickness = 0.1f, float pitch = 24, float pressureAngle = 20 * Mathf.Deg2Rad)
	{
		this.pressureAngle = pressureAngle;
		this.cogCount = cogCount;
		this.pitch = pitch;
		this.thickness = thickness;
	}

	float pressureAngle;    // rad
	int cogCount;
	float pitch;    // /m
	float thickness;    // m

	int steps = 4;

	float PitchDiameter { get { return cogCount / pitch; } }
	float BaseDiameter { get { return PitchDiameter * Mathf.Cos(pressureAngle); } }
	float RootDiameter { get { return (cogCount - 2.5f) / pitch; } }
	float OuterDiameter { get { return PitchDiameter + 2 * Addendum; } }
	float Addendum { get { return 1 / pitch; } }

	float CogAngle { get { return 2 * Mathf.PI / cogCount; } }

	Vector2[] Curve
	{
		get
		{
			float c_halfCog = Mathf.Cos(-CogAngle / 4);
			float s_halfCog = Mathf.Sin(-CogAngle / 4);
			float r_b = BaseDiameter / 2;
			float r_o = OuterDiameter / 2;
			float thetaMax = Mathf.Sqrt((r_o * r_o) / (r_b * r_b) - 1);
			float dTheta = thetaMax / steps;
			List<Vector2> curve = new List<Vector2>();
			float x0 = Mathf.Min(r_b, RootDiameter / 2);
			curve.Add(new Vector2(x0 * c_halfCog, x0 * s_halfCog));
			for (int i = 0; i <= steps; i++)
			{
				float theta = i * dTheta;
				float c = Mathf.Cos(theta);
				float s = Mathf.Sin(theta);
				float x = r_b * (c + theta * s);
				float y = r_b * (s - theta * c);
				curve.Add(new Vector2(x * c_halfCog - y * s_halfCog, x * s_halfCog + y * c_halfCog));
			}

			// Reflect through x-axis
			for (int i = curve.Count - 1; i >= 0; i--)
			{
				curve.Add(new Vector2(curve[i].x, -curve[i].y));
			}

			return curve.ToArray();
		}
	}

	public Mesh Mesh
	{
		get
		{
			Vector2[] curve = Curve;

			Vector2[] uv = null; //  new Vector2[curve.Length * 2];
			Vector3[] vertices = new Vector3[curve.Length * 2];
			for (int i = 0; i < curve.Length; i++)
			{
				vertices[i] = new Vector3(curve[i].x, curve[i].y, thickness / 2);
				vertices[i + curve.Length] = new Vector3(curve[i].x, curve[i].y, -thickness / 2);
				//				uv[i] = new Vector2();
				//				uv[i + curve.Length] = new Vector2();
			}

			int[] triangles = new int[(curve.Length - 2) * 12 + 6];
			for (int i = 0; i < curve.Length / 2 - 1; i++)
			{
				// Front
				triangles[6 * i + 0] = i;
				triangles[6 * i + 1] = i + 1;
				triangles[6 * i + 2] = curve.Length - i - 1;

				triangles[6 * i + 3] = i + 1;
				triangles[6 * i + 4] = curve.Length - i - 2;
				triangles[6 * i + 5] = curve.Length - i - 1;
			}
			for (int i = 0; i < curve.Length / 2 - 1; i++)
			{
				// Back
				triangles[(curve.Length - 2) * 3 + 6 * i + 0] = 2 * curve.Length - i - 1;
				triangles[(curve.Length - 2) * 3 + 6 * i + 1] = curve.Length + i + 1;
				triangles[(curve.Length - 2) * 3 + 6 * i + 2] = curve.Length + i;

				triangles[(curve.Length - 2) * 3 + 6 * i + 3] = 2 * curve.Length - i - 1;
				triangles[(curve.Length - 2) * 3 + 6 * i + 4] = 2 * curve.Length - i - 2;
				triangles[(curve.Length - 2) * 3 + 6 * i + 5] = curve.Length + i + 1;
			}
			for (int i = 0; i < curve.Length - 1; i++)
			{
				triangles[(curve.Length - 2) * 6 + i * 6 + 0] = curve.Length + i;
				triangles[(curve.Length - 2) * 6 + i * 6 + 1] = i + 1;
				triangles[(curve.Length - 2) * 6 + i * 6 + 2] = i;

				triangles[(curve.Length - 2) * 6 + i * 6 + 3] = curve.Length + i;
				triangles[(curve.Length - 2) * 6 + i * 6 + 4] = curve.Length + i + 1;
				triangles[(curve.Length - 2) * 6 + i * 6 + 5] = i + 1;
			}

			Mesh mesh = new Mesh();
			mesh.vertices = vertices;
			mesh.uv = uv;
			mesh.triangles = triangles;
			mesh.RecalculateNormals();

			return mesh;
		}
	}
}
