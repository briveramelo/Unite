using UnityEngine;
using System.Collections;

public class NormalShip_Thrusters : Thrusters {

	#region Initialize Values
	private bool hasPassedTheUniterShip;
	private bool hasSetToSelfDestruct;
	
	#endregion

	#region Keep OnKeeping on Until You've Passed On By
	// Update is called once per frame
	void Update () {
		if (!hasPassedTheUniterShip){
			MoveThisShipForward();
			DetectIfIvePassedTheUniterShip();
		}
		else{
			DestroyThisShip();
		}
	}

	void MoveThisShipForward(){
		if (MyShip.GetRigidBody){
			if (MyShip.GetRigidBody.velocity.magnitude<MaxSpeed){
				MyShip.GetRigidBody.AddForce(MoveForce * transform.forward);
			}
		}
	}

	void DetectIfIvePassedTheUniterShip(){
		if (MyShip.GetTheUniterShip){
			if (MyShip.transform.position.z < (MyShip.GetTheUniterShip.transform.position.z-1)){
				hasPassedTheUniterShip = true;
			}
		}
	}

	void DestroyThisShip(){
		if (!hasSetToSelfDestruct){
			hasSetToSelfDestruct = true;
			Destroy(gameObject,3f);
		}
	}
	#endregion
}
