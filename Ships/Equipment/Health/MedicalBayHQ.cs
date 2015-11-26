#region Declarations
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MedicalBayHQ : MonoBehaviour {
#endregion 

	public ComradManager comradManagerClass;
	public MedicalBay medicalBay;

	#region Find Ship In Need of Health
	public Ship FindHighestRankingShipInNeedOfHealth{
		get {
			Ship shipInNeed = comradManagerClass.theUniterShip;
			foreach (Ship shipToInspect in comradManagerClass.GetListOfAllShips){
				if (shipToInspect.MedicalBay.CurrentHealth < shipToInspect.MedicalBay.MaxHealth){
					shipInNeed = shipToInspect;
					break;
				}
			}
			return shipInNeed;
		}
	}
	#endregion


	#region Return Values
	public MedicalBay MedicalBay{
		get{
			return medicalBay;
		}
	}
	#endregion

}
