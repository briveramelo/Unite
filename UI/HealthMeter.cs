#region Declarations
using UnityEngine;
using System.Collections;

public class HealthMeter : MonoBehaviour {
#endregion

	#region Initialize Variables
	public Material[] healthBarMaterials;
	public Renderer myRenderer;
	private Vector3 startingScale;

	void Awake(){
		startingScale = transform.localScale;
	}
	#endregion

	#region Shrink/Grow Health Bar
	public void ShrinkHealthBar(float currentHealth, float maxHealth){
		if (currentHealth>0){
			myRenderer.material = healthBarMaterials[Mathf.RoundToInt(maxHealth-currentHealth)];
		}
		transform.localScale = new Vector3 ( startingScale.x * (currentHealth / maxHealth), startingScale.y, startingScale.z);
	}
	#endregion
	
}
