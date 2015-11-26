#region Declarations
using UnityEngine;
using System.Collections;

public class AlphaShip_Thrusters : Thrusters {
#endregion

	#region Initialize Values
	private float xOffset;
	private float yOffset;
	private float zOffset;
	private Vector3 oscillationSpeed;

	private Vector3 fixedTargetLocation;
	private Vector3 currentTarget;
	private Vector3 offsetLocation;
	private bool isOnRightSide;

	private float aimRotationSpeed;

	void Awake(){
		aimRotationSpeed = 0.2f;
		oscillationSpeed = new Vector3 (1f,0f,1f);
		ChooseLeftOrRight();
	}

	void ChooseLeftOrRight(){
		if (myShip.GetTheUniterShip){
			isOnRightSide = myShip.transform.position.x 
				< myShip.GetTheUniterShip.transform.position.x ?
					true : false;
		}
		
		fixedTargetLocation = isOnRightSide ?
				new Vector3(6f,0f,10f) :
				new Vector3(-6f,0f,10f);
	}

	#endregion
	
	#region Move To The Edges
	void Update () {
		MoveAndHide();
		RotateInaccurately();
	}

	void MoveAndHide(){
		if (MyShip.GetRigidBody){
			if (MyShip.GetRigidBody.velocity.magnitude < MaxSpeed){
				if (MyShip.GetTheUniterShip){
					MoveDir = ChooseTargetLocation();
					MyShip.GetRigidBody.AddForce(MoveDir * MoveForce);
				}
			}
		}
	}

	Vector3 ChooseTargetLocation(){
		xOffset = Mathf.Cos (Time.timeSinceLevelLoad * Mathf.PI * 2f * oscillationSpeed.x);
		yOffset = 0f;
		zOffset = Mathf.Sin (Time.timeSinceLevelLoad * Mathf.PI * 2f * oscillationSpeed.z);
		offsetLocation = new Vector3 ( xOffset, yOffset, zOffset);
		currentTarget = fixedTargetLocation + offsetLocation + MyShip.GetTheUniterShip.transform.position;
		float  distanceMultiplier = Mathf.Clamp01(Vector3.Distance(currentTarget, MyShip.transform.position));
		return (currentTarget - MyShip.transform.position).normalized * distanceMultiplier;
	}

	void OnDrawGizmos(){
		Gizmos.DrawSphere (currentTarget,2f);
	}

	#endregion

	#region Rotate Inaccurately at TheUniter
	void RotateInaccurately(){
		if (MyShip.GetTheUniterShip){
			if (!MyShip.IsBeingEmpowered){
				Vector3 offset = new Vector3 (Mathf.Sin (Time.realtimeSinceStartup * 2 * Mathf.PI),Mathf.Sin (Time.realtimeSinceStartup * 2 * Mathf.PI),0f);
				Quaternion targetQuat = Quaternion.LookRotation(offset + MyShip.GetTheUniterShip.transform.position-MyShip.transform.position);

				Quaternion.Slerp(MyShip.transform.rotation, targetQuat, aimRotationSpeed);
			}
		}
	}
	#endregion
}
