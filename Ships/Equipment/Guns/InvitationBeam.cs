#region Using / Class Declaration
using UnityEngine;
using System.Collections;
using GenericFunctions;

public class InvitationBeam : MonoBehaviour {
#endregion

	#region Initialize Values

	#region Initialize Values
	//marker
	#endregion

		#region Variables
	public Ship myShip;
	public AudioSource invitationSound;
	public AudioSource overHeatedSound;
	public Transform beamRendererTransform;
	public CapsuleCollider invitationBeamCollider;
	public MeshRenderer beamRenderer;

	private float defaultBeamLength;
	private float beamLength;

	private bool alreadySetBeam;
	private bool fun;
		#endregion

		#region Awake Define BeamLength

	void Awake () {
		defaultBeamLength = 5f;

		if (myShip.ShipType == ShipType.TheUniter) {
			beamLength = 1f;
		}
		else if (myShip.ShipType == ShipType.Normal) {
			beamLength = 1f;
		}
		else if (myShip.ShipType == ShipType.Alpha) {
			beamLength = 2f;
		}
		else if (myShip.ShipType == ShipType.Bravo) {
			beamLength = 3f;
		}
		beamLength *= defaultBeamLength;
		SetBeamLength();

	}
		#endregion

		#region SetBeam Length
	void SetBeamLength(){
		if (!alreadySetBeam){
			alreadySetBeam = true;
			invitationBeamCollider.height = beamLength;
			invitationBeamCollider.center = Vector3.up * (invitationBeamCollider.height/2f - 1);
			beamRendererTransform.localScale = new Vector3 (1f,invitationBeamCollider.height/2,1f);
			beamRendererTransform.localPosition = invitationBeamCollider.center;
		}
	}
		#endregion

	#endregion

	#region ToggleBeam Activation

	#region Toggle Beam Activation
	//marker
	#endregion

		#region Detect On/Off Beam
	void Update(){
		if (Input.GetButtonDown (myShip.GetTheUniterShip.GetInputManager.EngageInvitationBeam) && myShip.GetTheUniterShip.GetShotDirector.CanBeam) {
			StartCoroutine (PowerUpTheInvitationBeam ());
		}
		else if ((Input.GetButtonUp (myShip.GetTheUniterShip.GetInputManager.EngageInvitationBeam) && invitationBeamCollider.enabled) || myShip.GetTheUniterShip.GetShotDirector.CancelBeam) {
			StartCoroutine (DeactivateTheInvitationBeam(myShip.GetTheUniterShip.GetShotDirector.CancelBeam));
		}
	}
		#endregion

		#region Power Up
	IEnumerator PowerUpTheInvitationBeam(){
		invitationBeamCollider.enabled = true;
		beamRenderer.enabled = true;

		if (myShip.ShipType == ShipType.TheUniter){
			invitationSound.Play ();
		}

		yield return null;
	}
		#endregion

		#region Power Down

	IEnumerator DeactivateTheInvitationBeam(bool playDeActivateNoise){
		invitationBeamCollider.enabled = false;
		beamRenderer.enabled = false;
		invitationSound.Stop ();

		if (myShip.ShipType == ShipType.TheUniter && playDeActivateNoise){
			overHeatedSound.Play();
		}

		yield return null;
	}
		#endregion

	#endregion

	#region Return Beam Things
	public Ship GetInvitationBeamShip{
		get{
			return myShip;
		}
	}
	
	public float GetBeamLength{
		get{
			return beamLength;
		}
	}
}
	#endregion
