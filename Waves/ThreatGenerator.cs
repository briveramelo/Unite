#region Declaration
using UnityEngine;
using System.Collections;
using GenericFunctions;

public class ThreatGenerator : EmotionalIntensityMeter {

	#endregion

	#region Initialize Variables
	public GameObject[] shipsToSpawn;
	#endregion

	#region Spawn Ships
	/// <summary>
	/// Spawns a ship relative to the Player positoin
	/// </summary>
	public IEnumerator SpawnShip(ShipType shipType){
		Vector3 spawnSpot = Vector3.zero;
		if (shipType == ShipType.Normal){
			spawnSpot = new Vector3(Random.Range(-3f,3f),Constants.ySpawnSpot,Random.Range(15f,20f));
		}
		else if (shipType == ShipType.Alpha){
			spawnSpot = new Vector3(Random.Range(-5f,5f),Constants.ySpawnSpot,Random.Range(15f,20f));
		}
		else if (shipType == ShipType.Bravo){
			float xSpot = (Random.insideUnitCircle.x>0) ? Random.Range(-6f,-3f) : Random.Range(3f,6f);
			spawnSpot = new Vector3(xSpot,Constants.ySpawnSpot,Random.Range(15f,20f));
		}

		spawnSpot += uniterRespawner.TheUniterShip.transform.position;
		Instantiate (shipsToSpawn[ShipTypeToGameObjectIndex (shipType)], spawnSpot, Quaternion.Euler (0f, 180f, 0f));
		yield return null;
	}

	public int ShipTypeToGameObjectIndex(ShipType shipType){
		int returnIndex = 0;
		if (shipType == ShipType.TheUniter) {
			returnIndex = 0;
		}
		else if (shipType == ShipType.Normal) {
			returnIndex = 1;
		}
		else if (shipType == ShipType.Alpha) {
			returnIndex = 2;
		}
		else if (shipType == ShipType.Bravo) {
			returnIndex = 3;
		}
		return returnIndex;
	}
	#endregion

	#region WaitUntil Functions
	public IEnumerator WaitBasedOnShipType(ShipType shipType){
		float time2Wait = 0;
		if (shipType == ShipType.Normal){
			time2Wait = 1;
		}
		else if (shipType == ShipType.Alpha){
			time2Wait = 2;
		}
		else if (shipType == ShipType.Bravo){
			time2Wait = 3;
		}

		yield return new WaitForSeconds(time2Wait);
	}

	public IEnumerator WaitUntilAllThreatsAreGone(){
		while(ThreatsAlive>0){
			yield return null;
		}
		yield return null;
	}
	#endregion


}
