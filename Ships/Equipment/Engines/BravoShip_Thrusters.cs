using UnityEngine;
using System.Collections;

public class BravoShip_Thrusters : Thrusters {

	#region Initialize Values

	private bool hasPassedTheUniterShip;

	#endregion

	#region Approach The Uniter and Beyond
	// Update is called once per frame
	void Update () {
		if (MyShip.GetTheUniterShip){
			if ( MyShip.transform.position.z > (MyShip.GetTheUniterShip.transform.position.z-1)) {
				if (MyShip.GetRigidBody.velocity.magnitude<MaxSpeed){
					ApproachTheUniter();
				}
			}
			else{
				if (!hasPassedTheUniterShip){
					hasPassedTheUniterShip = true;
					Destroy(gameObject,3f);
				}
			}
		}
	}

	void ApproachTheUniter(){
		MoveDir = (MyShip.GetTheUniterShip.transform.position - transform.position).normalized;
		MyShip.GetRigidBody.AddForce (MoveDir * MoveForce);
	}
	#endregion


}
