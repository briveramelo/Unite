
#region Declaration
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ComradManager : MonoBehaviour {

#endregion

	#region Initialize Variables / Slots
	public Ship theUniterShip;
	public AudioSource joinTheRanks;

	private List<Ship> allShips;
	private List<Ship> rightShips;
	private List<Ship> leftShips;
	private float spacingBetweenShips;
	private Vector3[] leftSlotPositions;
	private Vector3[] rightSlotPositions;
	private Vector3[] allSlotPositions;

	#region Awaken
	void Awake(){
		#region SetSpots
		leftSlotPositions = new Vector3[]{
			new Vector3 (1f,0f,0f), //1
			new Vector3 (1f,1f,0f), //2
			new Vector3 (2f,0f,0f), //3
			new Vector3 (1f,2f,0f), //4
			new Vector3 (2f,1f,0f), //5
			new Vector3 (3f,0f,0f), //6
			new Vector3 (1f,3f,0f), //7
			new Vector3 (2f,2f,0f), //8
			new Vector3 (3f,1f,0f), //9
			new Vector3 (4f,0f,0f), //10
			new Vector3 (1f,4f,0f), //11
			new Vector3 (2f,3f,0f), //12
			new Vector3 (3f,2f,0f), //13
			new Vector3 (4f,1f,0f), //14
			new Vector3 (5f,0f,0f), //15
			new Vector3 (1f,5f,0f), //16
			new Vector3 (2f,4f,0f), //17
			new Vector3 (3f,3f,0f), //18
			new Vector3 (4f,2f,0f), //19
			new Vector3 (5f,1f,0f), //20
			new Vector3 (6f,0f,0f), //21
			new Vector3 (1f,6f,0f), //22
			new Vector3 (2f,5f,0f), //23
			new Vector3 (3f,4f,0f), //24
			new Vector3 (4f,3f,0f), //25
			new Vector3 (5f,2f,0f), //26
			new Vector3 (6f,1f,0f), //27
			new Vector3 (7f,0f,0f), //28
			new Vector3 (1f,7f,0f), //29
			new Vector3 (2f,6f,0f), //30
			new Vector3 (3f,5f,0f), //31
			new Vector3 (4f,4f,0f), //32
			new Vector3 (5f,3f,0f), //33
			new Vector3 (6f,2f,0f), //34
			new Vector3 (7f,1f,0f), //35
			new Vector3 (8f,0f,0f), //36
			new Vector3 (1f,8f,0f), //37
			new Vector3 (2f,7f,0f), //38
			new Vector3 (3f,6f,0f), //39
			new Vector3 (4f,5f,0f), //40
			new Vector3 (5f,4f,0f), //41
			new Vector3 (6f,3f,0f), //42
			new Vector3 (7f,2f,0f), //43
			new Vector3 (8f,1f,0f), //44
			new Vector3 (9f,0f,0f), //45
			new Vector3 (1f,9f,0f), //46
			new Vector3 (2f,8f,0f), //47
			new Vector3 (3f,7f,0f), //48
			new Vector3 (4f,6f,0f), //49
			new Vector3 (5f,5f,0f), //50
			new Vector3 (6f,4f,0f), //51
			new Vector3 (7f,3f,0f), //52
			new Vector3 (8f,2f,0f), //53
			new Vector3 (9f,1f,0f), //54
			new Vector3 (10f,0f,0f), //55
		};
		#endregion
		rightSlotPositions = new Vector3[leftSlotPositions.Length];
		allSlotPositions = new Vector3[leftSlotPositions.Length*2];
		allShips = new List<Ship>();
		rightShips = new List<Ship> ();
		leftShips = new List<Ship> ();
		spacingBetweenShips = 1f;
		InitializeSlots ();
	}

	void InitializeSlots(){
		for(int i = 0; i<leftSlotPositions.Length; i++) {
			leftSlotPositions[i] = leftSlotPositions[i] * spacingBetweenShips;
			rightSlotPositions[i] = new Vector3(-leftSlotPositions[i].x,leftSlotPositions[i].y,leftSlotPositions[i].z) * spacingBetweenShips;
		}
		bool isRight = true;
		int k = 0;
		for(int i = 0; i<allSlotPositions.Length; i++) {
			allSlotPositions[i] = isRight ? rightSlotPositions[k] : leftSlotPositions[k];
			if (!isRight){
				k++;
			}
			isRight = !isRight;
		}
	}
	#endregion
	#endregion

	#region Add/Remove/ReOrganize Ships
	public void RequestToJoinTheRanks(Ship newShip){
		allShips.Add (newShip);
		if (rightShips.Count <= leftShips.Count) {
			rightShips.Add (newShip);
			newShip.IsRightSide = true;
		}
		else {
			leftShips.Add(newShip);
			newShip.IsRightSide = false;
		}
		CheckWithCameraThrusters();
	}

	void CheckWithCameraThrusters(){
		int largerShipCount = rightShips.Count >= leftShips.Count ? 
			rightShips.Count : leftShips.Count;
		theUniterShip.GetCameraThrusters.CheckToMoveCamera(largerShipCount);
	}

	public void RequestToLeaveTheRanks(Ship valiantWarriorShip){
		allShips.Remove (valiantWarriorShip);
		if (valiantWarriorShip.IsRightSide) {
			rightShips.Remove(valiantWarriorShip);
		}
		else{
			leftShips.Remove(valiantWarriorShip);
		}
		CheckWithCameraThrusters();
		ReorganizeShips ();
	}

	void ReorganizeShips(){
		foreach (Ship shipToShuffle in allShips) {
			shipToShuffle.ComradClass.StartCoroutine(shipToShuffle.ComradClass.GetIntoPlace());
		}
	}
	#endregion

	#region Provide Ships / Positions
	public Vector3 GetSetPosition(Ship ship){
		List<Ship> setList;
		Vector3[] setSpots = rightSlotPositions;
		if (ship.IsRightSide) {
			setList = new List<Ship>(rightShips);
			setSpots = rightSlotPositions;
		}
		else {
			setList = new List<Ship>(leftShips);
			setSpots = leftSlotPositions;
		}
		return setSpots [setList.IndexOf(ship)];
	}


	public List<Ship> GetListOfAllShips{
		get{
			return allShips;
		}
	}

	public List<Ship> GetListOfRightShips{
		get{
			return rightShips;
		}
	}

	public List<Ship> GetListOfLeftShips{
		get{
			return leftShips;
		}
	}
	#endregion

	#region Join The Ranks noise
	public void RequestToCelebrateJoining(){
		if (joinTheRanks){
			joinTheRanks.Play ();
		}
	}
}
	#endregion
