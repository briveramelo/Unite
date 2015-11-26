#region using / class declaration
using UnityEngine;
using System.Collections;
using GenericFunctions;

public class Comrad : MonoBehaviour {
#endregion

	#region Define ship; movement/rotation speeds
	public Ship myShip;
	public Renderer myRenderer;
	public Material transparentShipMaterial;
	private float moveSetSpeed;

	#endregion

	#region Initialize ComradShip Qualities
	public void BecomeAComrad(Ship invitingShip){
		myShip.IsComrad = true;
		myRenderer.material = transparentShipMaterial;
		moveSetSpeed = 0.1f;
		EliminateThreats ();
		ActivateEquipment ();
		DefinePositionInFleet ();
		myShip.GetComradManager.RequestToCelebrateJoining ();
	}

	#region Fleet Welcome Package (BecomeAComrad functions)
	public void EliminateThreats(){
		if (GetComponent<Rigidbody> ()){
			Destroy (GetComponent<Rigidbody> ());
		}
		if (GetComponent<CollisionSpikes> ()){
			Destroy (GetComponent<CollisionSpikes> ());
		}
		if (GetComponent<NormalShip_Thrusters>()){
			Destroy (GetComponent<NormalShip_Thrusters>());
		}
		if (GetComponent<AlphaShip_Thrusters> ()){
			Destroy (GetComponent<AlphaShip_Thrusters> ());
		}
		if (GetComponent<BravoShip_Thrusters> ()){
			Destroy (GetComponent<BravoShip_Thrusters> ());
		}
		if (transform.GetChild(0).GetComponent<ShotPulser>()){
			Destroy(transform.GetChild(0).GetComponent<ShotPulser>());
		}
	}

	public void DefinePositionInFleet(){
		//make a request to the senior manager
		//for an available position claim it,
		//celebrate with the boys, and get in
		//place
		if (myShip.GetTheUniterShip){
			transform.parent = myShip.GetTheUniterShip.transform;
			myShip.GetComradManager.RequestToJoinTheRanks(myShip);
			StartCoroutine (GetIntoPlace ());
		}
	}

	void ActivateEquipment(){
		gameObject.layer = Constants.comradLayer;
		myShip.ComradClass = this;
		myShip.MedicalBay.ActivateHealthGenerators ();
		gameObject.transform.GetChild (0).gameObject.SetActive (true);
		gameObject.transform.GetChild (1).gameObject.SetActive (true);

		myShip.Gun.enabled = true;
		myShip.GetGunScope.enabled = true;

	}
	#endregion
	#endregion


	#region GetInto Place
	public IEnumerator GetIntoPlace(){
		float distanceAwayFromSetSpot = 10f;
		myShip.BecomeInvincible ();
		myShip.GetGunScope.ChooseAimDirection();
		while (distanceAwayFromSetSpot>0.1f) {
			distanceAwayFromSetSpot = Vector3.Distance(transform.position,
                                      myShip.GetTheUniterShip.GetComradManager.transform.position
			                          + myShip.GetTheUniterShip.GetComradManager.GetSetPosition(myShip));
			transform.localPosition = Vector3.MoveTowards(transform.localPosition,
			                          myShip.GetTheUniterShip.GetComradManager.GetSetPosition(myShip),
			                          moveSetSpeed);
			yield return null;
		}
		transform.localPosition = myShip.GetTheUniterShip.GetComradManager.GetSetPosition(myShip);
		myShip.GetGunScope.ChooseAimDirection();
		myShip.BecomeVincible ();
		yield return null;
	}

	public void OrderComradIntoPlace(){
		StartCoroutine (GetIntoPlace ());
	}
	#endregion

	#region StopCoroutines
	void OnDestroy(){
		StopAllCoroutines ();
	}
	#endregion

	
}
