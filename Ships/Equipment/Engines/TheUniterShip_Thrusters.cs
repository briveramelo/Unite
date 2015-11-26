
#region Using / Class Declaration
using UnityEngine;
using System.Collections;
using GenericFunctions;

[RequireComponent (typeof (Rigidbody))]
public class TheUniterShip_Thrusters : Thrusters {

	#endregion

	#region Initialize Movement Values
	
	private float brakeRate;
	private bool frozen;
	[Range(0,1)]
	private float xForceMultiplierBasedOnSpeed;
	private float zForceMultiplierBasedOnSpeed;

	// Use this for initialization
	void Awake () {
		brakeRate = 6f;
		BroadcastMessage ("ResetShipTarget", SendMessageOptions.DontRequireReceiver);
	}
	#endregion

	#region Move and Brake 
	void LateUpdate () {
		MoveAround ();
		Brake ();
	}

	void MoveAround(){


		if (Mathf.Abs (Input.GetAxisRaw (myShip.GetInputManager.Horizontal))> 0.05f && ((Mathf.Abs (myShip.GetRigidBody.velocity.x) < MaxSpeed) || Mathf.Sign (Input.GetAxisRaw (myShip.GetInputManager.Horizontal)) != Mathf.Sign (myShip.GetRigidBody.velocity.x ))) {
			//xForceMultiplierBasedOnSpeed = DetermineForceMultiplier(MyShip.GetRigidBody.velocity.x);
			//AddCorrectedForce(Vector3.right, Input.GetAxisRaw(myShip.GetInputManager.Horizontal), xForceMultiplierBasedOnSpeed);
		}

		if (Mathf.Abs (Input.GetAxisRaw (myShip.GetInputManager.Vertical))> 0.05f && ((Mathf.Abs (myShip.GetRigidBody.velocity.z) < MaxSpeed) || Mathf.Sign (Input.GetAxisRaw (myShip.GetInputManager.Vertical)) != Mathf.Sign (myShip.GetRigidBody.velocity.z ))) {
			//zForceMultiplierBasedOnSpeed = DetermineForceMultiplier(MyShip.GetRigidBody.velocity.z);
			//AddCorrectedForce(Vector3.forward, Input.GetAxisRaw(myShip.GetInputManager.Vertical), zForceMultiplierBasedOnSpeed);
		}

		MoveThePlayer();

	}

	float DetermineForceMultiplier(float directionalSpeed){
		return ( 1f + 1f/350f ) - ( 1f/350f * Mathf.Exp(Mathf.Abs(directionalSpeed) / MaxSpeed * 5.860787f)); 
	}

//	float DetermineForceMultiplier(float directionalSpeed){
//		return Mathf.Exp(-Mathf.Abs(directionalSpeed) / MaxSpeed * 5f); 
//	}

//	float DetermineForceMultiplier(float directionalSpeed){
//		return 1f;
//	}

	void AddCorrectedForce(Vector3 moveDir, float axisInput, float forceMultiplier){
		myShip.GetRigidBody.AddForce(  moveDir * axisInput * forceMultiplier *  MoveForce, ForceMode.Acceleration);
	}

	void MoveThePlayer(){
		myShip.GetRigidBody.velocity = new Vector3(
			Input.GetAxisRaw(myShip.GetInputManager.Horizontal) *  MoveForce,
		    0f,
			Input.GetAxisRaw(myShip.GetInputManager.Vertical) *  MoveForce);
	}

	void Brake(){
		if(Mathf.Abs (Input.GetAxisRaw (myShip.GetInputManager.Horizontal)) < 0.1f){
			myShip.GetRigidBody.AddForce( -myShip.GetRigidBody.velocity.x * brakeRate * Vector3.right);
		}

		if(Mathf.Abs (Input.GetAxisRaw (myShip.GetInputManager.Vertical)) <0.1f){
			myShip.GetRigidBody.AddForce( -myShip.GetRigidBody.velocity.z * brakeRate * Vector3.forward);
		}
	}

}
#endregion