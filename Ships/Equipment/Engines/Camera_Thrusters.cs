
#region using / class declaration
using UnityEngine;
using System.Collections;

public class Camera_Thrusters : Thrusters {
#endregion

	#region Initialize Values
	private Vector3[] fixedTargetPositions;
	private Vector3 fixedTargetPosition;
	private Vector3 currentTargetPosition;
	private Vector3 lastUniterShipPosition;

	private Quaternion[] fixedTargetRotations;
	private Quaternion currentTargetRotation;
	private Quaternion fixedTargetRotation;

	private float rotationSpeed;
	private float moveSpeed;
	private float distanceAwayFromTargetPositon;
	private int[] numberOfShipsNeededForNewHeight;
	private bool resetPositions;

	void Awake(){
		fixedTargetPositions = new Vector3[]{
			new Vector3(0,2f,-4f),
			new Vector3(0,3f,-5f),
			new Vector3(0,5f,-6f),
			new Vector3(0,7f,-7f),
			new Vector3(0,8f,-8f),
			new Vector3(0,9f,-9f),
			new Vector3(0,10f,-10f),
			new Vector3(0,11,-11f),
			new Vector3(0,12f,-12f),
			new Vector3(0,13f,-13f)
		};
		fixedTargetRotations = new Quaternion[]{
			Quaternion.Euler(10f,0f,0f),
			Quaternion.Euler(15f,0f,0f),
			Quaternion.Euler(20f,0f,0f),
			Quaternion.Euler(25f,0f,0f),
			Quaternion.Euler(30f,0f,0f),
			Quaternion.Euler(31f,0f,0f),
			Quaternion.Euler(32f,0f,0f),
			Quaternion.Euler(33f,0f,0f),
			Quaternion.Euler(34f,0f,0f),
			Quaternion.Euler(35f,0f,0f)
		};
		numberOfShipsNeededForNewHeight = new int[]{
			1,
			2,
			4,
			7,
			11,
			16,
			22,
			29,
			37,
			46
		};
		rotationSpeed= 0.01f;
		//moveSpeed = 0.1f; by Lerp
		moveSpeed = 8f;
		SetPositions(0);
	}
	#endregion

	#region Handle Movement
	void Update(){
		HandleMovement();
		RotateProperly();
	}

	void HandleMovement(){
		FindTheTargetPosition();
		MoveToTheTargetPosition();
	}
	
	void FindTheTargetPosition(){
		if (MyShip.GetTheUniterShip){
			lastUniterShipPosition = MyShip.GetTheUniterShip.transform.position;
			currentTargetPosition = fixedTargetPosition + lastUniterShipPosition;
		}
		else{
			if (!resetPositions){
				resetPositions = true;
				currentTargetPosition = fixedTargetPosition + lastUniterShipPosition;
				SetPositions(0);
			}
		}
	}
	
	void MoveToTheTargetPosition(){
		if (MyShip.GetRigidBody.velocity.magnitude<MaxSpeed){
			MoveDir = (currentTargetPosition - transform.position).normalized;
			distanceAwayFromTargetPositon = Mathf.Clamp01(Vector3.Distance(MyShip.transform.position,currentTargetPosition));
			MyShip.transform.position = Vector3.Slerp (MyShip.transform.position, currentTargetPosition, 0.4f);
			//MyShip.GetRigidBody.velocity = MoveDir * distanceAwayFromTargetPositon * moveSpeed;
		}
	}

	#region Set FixedPositions
	public void CheckToMoveCamera(int largerShipCount){
		for (int i=fixedTargetPositions.Length-1; i>=0; i--){
			if (largerShipCount >= numberOfShipsNeededForNewHeight[i]){
				SetPositions(i);
				break;
			}
		}
	}
	
	void SetPositions(int positionIndex){
		fixedTargetPosition = fixedTargetPositions[positionIndex];
		fixedTargetRotation = fixedTargetRotations[positionIndex];
	}
	#endregion

	#endregion

	#region Handle Rotation	
	void RotateProperly(){
		currentTargetRotation = fixedTargetRotation;
		transform.rotation = Quaternion.Slerp(transform.rotation,currentTargetRotation,rotationSpeed);
	}

}
	#endregion