
#region Declarations
using UnityEngine;
using System.Collections;
using GenericFunctions;

public class SimpleSpawner : MonoBehaviour {
#endregion

	#region initialize Values
	public GameObject[] shipsToSpawn;
	public Transform theUniterShipTransform;

	public bool spawnNormal;
	public bool spawnAlpha;
	public bool spawnBravo;

	private bool startSpawningNormal;
	private bool startSpawningAlpha;
	private bool startSpawningBravo;

	void Awake(){
		startSpawningNormal = true;
		startSpawningAlpha = true;
		startSpawningBravo = true;
		theUniterShipTransform = GameObject.FindObjectOfType<ComradManager>().transform;
	}
	#endregion
	

	#region Handle Spawn Requests in Inspector
	void Update(){
		if (theUniterShipTransform){
			if (spawnNormal){
				if (startSpawningNormal){
					startSpawningNormal = false;
					StartCoroutine (SpawnNormal());
				}
			}
			else{
				startSpawningNormal = true;
			}
			
			if (spawnAlpha){
				if (startSpawningAlpha){
					startSpawningAlpha = false;
					StartCoroutine (SpawnAlpha());
				}
			}
			else{
				startSpawningAlpha = true;
			}
			
			if (spawnBravo){
				if (startSpawningBravo){
					startSpawningBravo = false;
					StartCoroutine (SpawnBravo());
				}
			}
			else{
				startSpawningBravo = true;
			}
		}
	}
	#endregion

	#region Spawn Threats

		#region Normal
	IEnumerator SpawnNormal(){
		yield return new WaitForSeconds(2f);
		if (spawnNormal && theUniterShipTransform){
			Vector3 spawnSpot = new Vector3 (Random.Range (-3f,3f), Constants.ySpawnSpot, Random.Range (12f, 18f));
			spawnSpot += theUniterShipTransform.position;
			Instantiate(shipsToSpawn[0], spawnSpot, Quaternion.Euler (0f,180f,0f));
			StartCoroutine (SpawnNormal());
		}
	}
		#endregion

		#region Alpha
	IEnumerator SpawnAlpha(){
		yield return new WaitForSeconds(2f);
		if (spawnAlpha && theUniterShipTransform){
			Vector3 spawnSpot = new Vector3 (Random.Range (-3f,3f), Constants.ySpawnSpot, Random.Range (12f, 18f));
			spawnSpot += theUniterShipTransform.position;
			Instantiate(shipsToSpawn[1], spawnSpot, Quaternion.Euler (0f,180f,0f));
			StartCoroutine (SpawnAlpha());
		}
	}
		#endregion

		#region Bravo
	IEnumerator SpawnBravo(){
		yield return new WaitForSeconds(2f);
		if (spawnBravo && theUniterShipTransform){
			Vector3 spawnSpot = new Vector3 (Random.Range (-3f,3f), Constants.ySpawnSpot, Random.Range (12f, 18f));
			spawnSpot += theUniterShipTransform.position;
			Instantiate(shipsToSpawn[2], spawnSpot, Quaternion.Euler (0f,180f,0f));
			StartCoroutine (SpawnBravo());
		}
	}
		#endregion
	#endregion

	#region Set Values
	public Transform UniterShipTansform{
		set{
			theUniterShipTransform = value;
		}
	}
	#endregion
}
