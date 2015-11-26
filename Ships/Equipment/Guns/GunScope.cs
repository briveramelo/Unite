using UnityEngine;
using System.Collections;
using GenericFunctions;
public class GunScope : MonoBehaviour {

	#region Initialize Values
	public Ship myShip;
	private bool looking;
	private float lookSpeed;
	private Vector3 outRightDirection;
	private Vector3 outLeftDirection;

	void Awake(){
		lookSpeed = 5f;
		outRightDirection = new Vector3(-1f,0f,1.5f);
		outLeftDirection = new Vector3(1f,0f,1.5f);
	}
	#endregion

	#region Determine When/Where to Aim
	void Update(){
		ToggleAimSettings ();
	}

	void ToggleAimSettings(){
		if (myShip.GetTheUniterShip.GetShotDirector.NewAim) {
			ChooseAimDirection();
		}
	}

	public void ChooseAimDirection(){
		Transform aimingTransform = transform;
		Vector3 targetSpot = Vector3.zero;
		
		#region Set AimTransform and TargetSpot Depending on AimMode
		//forward
		if (myShip.GetTheUniterShip.GetShotDirector.GetAimMode == AimMode.Straight){
			aimingTransform = myShip.transform;
			targetSpot = myShip.GetTheUniterShip.transform.forward;
		}
		//center
		else if(myShip.GetTheUniterShip.GetShotDirector.GetAimMode== AimMode.Focus){
			aimingTransform = myShip.GetTheUniterShip.transform;
			targetSpot = myShip.GetTheUniterShip.transform.forward*10f;
		}
		//outward
		else if (myShip.GetTheUniterShip.GetShotDirector.GetAimMode == AimMode.Outward){
			if (myShip.IsRightSide){
				aimingTransform = myShip.transform;
				targetSpot = outRightDirection;
			}
			else{
				aimingTransform = myShip.transform;
				targetSpot = outLeftDirection;
			}
		}
		#endregion
		StartCoroutine(AimHere(aimingTransform,targetSpot));
	}
	#endregion


	IEnumerator AimHere(Transform aimingTransform, Vector3 targetSpot){
		looking = false;
		yield return null;
		looking = true;
		Quaternion targetQuat = Quaternion.LookRotation (
		                        aimingTransform.position + targetSpot - myShip.transform.position);

		while (looking) {
			if (Quaternion.Angle(myShip.transform.rotation,targetQuat)>2f){

				myShip.transform.rotation = Quaternion.Slerp (myShip.transform.rotation, 
				                            targetQuat, Time.deltaTime * lookSpeed);
			}
			else{
				myShip.transform.rotation = targetQuat;
				looking = false;
			}
			yield return null;
		}
		yield return null;
	}
}
