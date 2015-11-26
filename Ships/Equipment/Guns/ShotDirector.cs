
#region DeclarationStuff
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ShotDirector : MonoBehaviour {
#endregion

	#region InitializeValues
	public Ship myShip;
	public AimMode aimMode;
	public AudioSource[] aimSounds;
	private bool getFocused;
	private bool newAim;
	private bool canShootAgain;
	private bool canBeam;
	private bool isBeaming;
	private bool cancelBeam;

	private float timeSinceLastShot;
	public float beamPower;
	private float beamPowerRecoveryRate;
	private float beamPowerDrainRate;
	private float reloadTime;

	void Awake(){
		canShootAgain = true;
		canBeam = true;
		beamPower = 100;
		beamPowerDrainRate = 2f;
		beamPowerRecoveryRate = beamPowerDrainRate * 3f;
		reloadTime = 0.5f;
		if (GameObject.FindObjectOfType<BeamPowerMeter>()){
			GameObject.FindObjectOfType<BeamPowerMeter>().ShotDirector = this;
		}
	}
	#endregion

	#region Aim Ships and Restrict Gun Activity

	#region Update will AimShips and RestrictGunActivity
	void Update(){
		AimShips();
		RestrictGunActivity();
	}
	#endregion

		#region AimShips
	void AimShips(){
		newAim = false;
		if (Input.GetButtonDown (myShip.GetInputManager.AimShips)) {
			newAim = true;
			if (aimMode == AimMode.Straight){
				aimMode = getFocused ? AimMode.Focus : AimMode.Outward;
				getFocused = !getFocused;
			}
			else if (aimMode == AimMode.Focus || aimMode == AimMode.Outward){
				aimMode = AimMode.Straight;
			}

			PlayAimSounds();
		}
	}

	void PlayAimSounds(){
		if (myShip.GetComradManager.GetListOfAllShips.Count>0){
			if (aimMode == AimMode.Straight){
				aimSounds[0].Play();
			}
			else if (aimMode == AimMode.Focus){
				aimSounds[1].Play();
			}
			else if (aimMode == AimMode.Outward){
				aimSounds[2].Play();
			}
		}
	}

		#endregion

		#region Restrict Gun / Beam Activity
		//marker
		#endregion

			#region Queue Delay / Recharge Timers
	void RestrictGunActivity(){
		if (canShootAgain){
			if (Input.GetButtonDown (myShip.GetTheUniterShip.GetInputManager.FireBullets)){
				canShootAgain = false;
				StartCoroutine (StartBulletReloadDelay());
			}
		}

		if (canBeam){
			if (Input.GetButtonDown (myShip.GetTheUniterShip.GetInputManager.EngageInvitationBeam)){
				StartCoroutine (DrainBeamPower());
			}
			else if (Input.GetButtonUp (myShip.GetTheUniterShip.GetInputManager.EngageInvitationBeam)){
				StartCoroutine(ReChargeBeamPower());
			}
		}
	}
			#endregion

			#region Restrict Bullet Shooting

	IEnumerator StartBulletReloadDelay(){
		float currentWaitTime =0;
		while (currentWaitTime<reloadTime){
			currentWaitTime += Time.deltaTime;
			yield return null;
		}
		canShootAgain = true;
		yield return null;
	}

			#endregion

			#region Restrict Beaming
			//marker
			#endregion

				#region Drain Beam

	IEnumerator DrainBeamPower(){
		isBeaming = true;
		while (isBeaming && beamPower>0){
			beamPower -= beamPowerDrainRate;
			if (beamPower<=0){
				canBeam = false;
				cancelBeam = true;
				StartCoroutine (ReChargeBeamPower());

				yield return null;
				cancelBeam = false;
				yield return new WaitForSeconds(2f);
				canBeam = true;
			}
			yield return null;
		}
		yield return null;
	}
				#endregion

				#region ReCharge Beam
	IEnumerator ReChargeBeamPower(){
		isBeaming = false;
		while (!isBeaming && beamPower<100){
			beamPower += beamPowerRecoveryRate;
			yield return null;
		}
		yield return null;
	}
				#endregion

	#endregion

	#region Return Values
	public bool IsBeaming{
		get{
			return isBeaming;
		}
	}

	public float BeamPower{
		get{
			return beamPower;
		}
	}

	public bool CancelBeam{
		get{
			return cancelBeam;
		}
	}

	public bool CanBeam{
		get{
			return canBeam;
		}
	}

	public AimMode GetAimMode{
		get{
			return aimMode;
		}
	}

	public bool NewAim{
		get{
			return newAim;
		}
	}

	public bool CanShootAgain{
		get{
			return canShootAgain;
		}
	}
	#endregion

}
