using UnityEngine;
using System.Collections;

public class Thrusters : MonoBehaviour {

	#region Initialize Values
	public Ship myShip;

	private Vector3 moveDir;

	private float maxSpeed;
	private float moveForce;
	#endregion

	#region Return Values To RespectiveShips;

	public Ship MyShip{
		get{
			return myShip;
		}
	}

	public float MaxSpeed{
		get{
			float returnMaxSpeed = 0;
			if (myShip.shipType == ShipType.TheUniter){
				returnMaxSpeed = 5f;
			}
			else if (myShip.shipType == ShipType.Normal){
				returnMaxSpeed = 1f;
			}
			else if (myShip.shipType == ShipType.Alpha){
				returnMaxSpeed = 2f;
			}
			else if (myShip.shipType == ShipType.Bravo){
				returnMaxSpeed = 4f;
			} 
			else if (myShip.shipType == ShipType.Camera){
				returnMaxSpeed = 10f;
			}
			return returnMaxSpeed;
		}
	}

	public float MoveForce{
		get{
			float returnMoveForce = 0;
			if (myShip.shipType == ShipType.TheUniter){
				returnMoveForce = 3f;
			}
			else if (myShip.shipType == ShipType.Normal){
				returnMoveForce = 5f;
			}
			else if (myShip.shipType == ShipType.Alpha){
				returnMoveForce = 5f;
			}
			else if (myShip.shipType == ShipType.Bravo){
				returnMoveForce = 20f;
			}
			else if (myShip.shipType == ShipType.Camera){
				returnMoveForce = 11f;
			}
			return returnMoveForce;
		}
	}

	public Vector3 MoveDir{
		get{
			return moveDir;
		}
		set{
			moveDir = value;
		}
	}
	#endregion
}
