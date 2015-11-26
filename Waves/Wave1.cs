
#region Using / Class Declaration
using UnityEngine;
using System.Collections;
using GenericFunctions;

public class Wave1 : ThreatGenerator {
#endregion	

	#region Initialize Values
	private float[] emotionalIntensityCheckPointValues;
	private int currentCheckPoint;
	private int[][] shipTypeRange;

	void Awake () {
		emotionalIntensityCheckPointValues = new float[]{
			1f,  //1
			3f,  //2
			1f,  //3
			3f,  //4
			10f, //5
			3f,  //6
			9f,  //7
			25f, //8
		};
		shipTypeRange = new int[][]{
			new int[]{1, 1}, //1
			new int[]{1, 1}, //2
			new int[]{2, 2}, //3
			new int[]{2, 2}, //4
			new int[]{1, 2}, //5
			new int[]{3, 3}, //6
			new int[]{3, 3}, //7
			new int[]{1, 3}, //8
		};
		StartCoroutine (CommenceWave1());
	}
	#endregion

	#region Wave1 Logic
	IEnumerator CommenceWave1(){
		while ( currentCheckPoint < emotionalIntensityCheckPointValues.Length ){
			while ( EmotionalIntensity < emotionalIntensityCheckPointValues[currentCheckPoint] ) {
				if (UniterRespawner.TheUniterShip){
					ShipType nextShipToSpawn = (ShipType) Random.Range(shipTypeRange[currentCheckPoint][0],shipTypeRange[currentCheckPoint][1]);
					StartCoroutine( SpawnShip( nextShipToSpawn ) );
					yield return StartCoroutine(WaitBasedOnShipType(nextShipToSpawn));
				}
				yield return null;
			}
			yield return StartCoroutine(WaitUntilAllThreatsAreGone());
			currentCheckPoint++;
			yield return null;
		}
		yield return null;
	}
}
	#endregion
