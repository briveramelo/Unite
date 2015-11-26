
#region Declaration
using UnityEngine;
using System.Collections;
using GenericFunctions;

public class UniterRespawner : MonoBehaviour {
#endregion

	#region Initialize Values
	private int numberOfLives;
	private int currentNumberOfLives;
	public Ship theUniterShip;
	public GameObject shipToSpawn;
	public GameObject uniterShipGameObject;
	public SimpleSpawner simpleSpawner;
	private Vector3 lastPosition;
	public LifeCountMeter lifeCountMeter;

	void Awake(){
		numberOfLives = 3;
		FindUniter();
	}
	#endregion

	void FindUniter(){
		theUniterShip = GameObject.FindObjectOfType<ComradManager>().GetComponent<Ship>();
	}

	#region Set Last position
	void Update(){
		if (theUniterShip){
			lastPosition = theUniterShip.transform.position;
		}
	}
	#endregion

	#region Check To Spawn Uniter OR Quit
	public void CheckToSpawn(){
		if (numberOfLives>=0){
			StartCoroutine( QueueUniter() );
		}
		else{
			Application.Quit();
		}
	}

	IEnumerator QueueUniter(){
		yield return new WaitForSeconds(2f);
		SpawnNewUniter();
	}

	public void SpawnNewUniter(){
		uniterShipGameObject = (GameObject)Instantiate (shipToSpawn, lastPosition, Quaternion.identity);
		numberOfLives--;
	}

	public void SetUniter(Ship uniterShip){
		theUniterShip = uniterShip;
		simpleSpawner.UniterShipTansform = theUniterShip.transform;

	}
	#endregion

	#region Return Values

	public int StartingNumberOfLives{
		get{
			return numberOfLives;
		}
	}

	public int CurrentNumberOfLives{
		get{
			return currentNumberOfLives;
		}
		set{
			currentNumberOfLives = value;
		}
	}

	public Ship TheUniterShip{
		get{
			return theUniterShip;
		}
	}

	public Vector3 LastUniterPosition{
		get{
			return lastPosition;
		}
	}

}

	#endregion