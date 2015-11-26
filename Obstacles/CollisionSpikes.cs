#region Declarations
using UnityEngine;
using System.Collections;
using GenericFunctions;

public class CollisionSpikes : MonoBehaviour {
#endregion

	public MedicalBay myMedicalBay;

	#region Hurt Ship You Collide With
	void OnTriggerEnter(Collider col){
		//hurt opposing ship (you!) on colliding 
		if (col.gameObject.layer == Constants.meLayer || col.gameObject.layer == Constants.comradLayer) {
			col.GetComponent<Ship> ().MedicalBay.InflictDamage(1);
			myMedicalBay.CheckForDeath(true);
		} 
	}
	#endregion
}
