using UnityEngine;
using System.Collections;

public class Gizmo_Sphere : MonoBehaviour {

	[Range(0,2)]
	public float sphereRadius = 0.2f;
	public Color sphereColor = Color.red;

	void OnDrawGizmos(){
		Gizmos.color = sphereColor;
		Gizmos.DrawSphere (transform.position, sphereRadius);
	}
}
