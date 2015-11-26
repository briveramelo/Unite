
#region Class Declaration
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EmotionalIntensityMeter : MonoBehaviour {
	#endregion

	#region Initialize Values
	public UniterRespawner uniterRespawner;
	private Ship[] allShips;
	private int[] numberOfThreatShipsAlive;
	private int[] numberOfComradShipsAlive;
	private int totalNumberOfThreatShipsAlive;
	private int totalNumberOfComradShipsAlive;
	private int shipIndex;
	private float[] dangerValue;
	private float[] comradValue;

	public float threatIntensity;
	public float uniterResilience;
	public float emotionalIntensity;

	void Awake(){
		numberOfThreatShipsAlive = new int[3];
		dangerValue = new float[]{
			1f,
			2f,
			2.5f
		};
		comradValue = new float[]{
			1f,
			2f,
			2.5f
		};
		InvokeRepeating("DetermineTheNumberOfAllShipsAlive",1f,2f);
	}
	#endregion

	#region Calculate Emotional Intensity
	public void DetermineTheNumberOfAllShipsAlive(){
		numberOfThreatShipsAlive = new int[3];
		numberOfComradShipsAlive = new int[3];
		totalNumberOfComradShipsAlive =0;
		totalNumberOfThreatShipsAlive =0;

		allShips = FindObjectsOfType<Ship>();
		foreach (Ship oneShip in allShips){
			if (oneShip.IsComrad){
				if (oneShip.ShipType == ShipType.Normal){
					shipIndex = 0;
				}
				else if (oneShip.ShipType == ShipType.Alpha){
					shipIndex = 1;
				}
				else if (oneShip.ShipType == ShipType.Bravo){
					shipIndex = 2;
				}
				numberOfComradShipsAlive[shipIndex]++;
				totalNumberOfComradShipsAlive++;
			}
			else{
				if (oneShip.ShipType == ShipType.Normal){
					shipIndex = 0;
				}
				else if (oneShip.ShipType == ShipType.Alpha){
					shipIndex = 1;
				}
				else if (oneShip.ShipType == ShipType.Bravo){
					shipIndex = 2;
				}
				numberOfThreatShipsAlive[shipIndex]++;
				totalNumberOfThreatShipsAlive++;
			}
		}
		DetermineEmotionalIntensity();
	}

	void DetermineEmotionalIntensity(){
		for (int i =0; i<numberOfThreatShipsAlive.Length; i++){
			threatIntensity += numberOfThreatShipsAlive[i] * dangerValue[i];
			uniterResilience += numberOfComradShipsAlive[i] * comradValue[i];
		}

		emotionalIntensity = uniterResilience - threatIntensity;
	}
	#endregion

	#region Return Values
	public UniterRespawner UniterRespawner{
		get{
			return uniterRespawner;
		}
	}

	public float EmotionalIntensity{
		get{
			return emotionalIntensity;
		}
	}

	public float ThreatsAlive{
		get{
			return totalNumberOfThreatShipsAlive;
		}
	}

	public float ComradsAlive{
		get{
			return totalNumberOfComradShipsAlive;
		}
	}


	#endregion

}
