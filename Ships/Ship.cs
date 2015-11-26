
#region Using / Class Declaration
using UnityEngine;
using System.Collections;
using GenericFunctions;
using System;
using System.Collections.Generic;

public class Ship : MonoBehaviour {
#endregion

	#region Initialize Values

	#region Initialize Values
	//marker
	#endregion

		#region Define Variables
		//marker
		#endregion

			#region Public Classes
	public ShipType shipType;
	public MedicalBay medicalBay;
	public Collider shipCollider;
	public Gun gun;
	public CapsuleCollider beamCollider;
	public ShotDirector shotDirector;
	public MedicalBayHQ medicalBayHQ;
	public ComradManager comradManager;
	public Rigidbody shipBody;
	public Comrad comradClass;
	public GunScope gunScope;
			#endregion

			#region Private Classes
	private InputManager inputManager;
	public List<Ship> invitedShips;
	public Ship theUniterShip;
	private Camera_Thrusters cameraThrusters;
	private UniterRespawner uniterRespawner;
			#endregion

			#region Private floats / ints
	private float conversionTime;
	private float conversionTimeElapsed;
	private float elevationForce;
	private float rotationSpeed;
	private float maxElevationHeight;
	private float maxElevationSpeed;
	private float startHeight;

	private int rank;
	private int maxHealth;
			#endregion

			#region Private Booleans
	private bool isComrad;
	private bool isBeingEmpowered;
	private bool isRightSide;
	private bool isMovingToFaceForward;
			#endregion 


		#region Initialize Ship values
		//marker
		#endregion
			
			#region Awake Ship Setting
	void Awake(){
		invitedShips = new List<Ship>();
		startHeight = transform.position.y;
		if (GameObject.FindObjectOfType<ComradManager> ()){
			theUniterShip = GameObject.FindObjectOfType<ComradManager> ().theUniterShip;
		}
		else{
			Destroy(gameObject);
		}

		comradManager = theUniterShip.GetComradManager;
		if (shipType == ShipType.TheUniter) {
			#region TheUniterShip values
			isComrad = true;
			inputManager = Component.FindObjectOfType<InputManager>();
			cameraThrusters = Component.FindObjectOfType<Camera_Thrusters>();
			uniterRespawner = Component.FindObjectOfType<UniterRespawner>();
			if (uniterRespawner){
				uniterRespawner.SetUniter(this);
			}
			rotationSpeed = 0.05f;
			conversionTime = 5f;
			maxElevationHeight = 6f;

			Ship[] allShips = GameObject.FindObjectsOfType<Ship>();
			foreach (Ship ship in allShips){
				ship.GetTheUniterShip = this;
				ship.GetComradManager = comradManager;
				ship.ShotDirector = shotDirector;
				ship.MedicalBayHQ = medicalBayHQ;
			}
			#endregion
		}
		else if (shipType == ShipType.Normal) {
			#region NormalShip values
			rotationSpeed = 0.1f;
			conversionTime = 1f;
			maxElevationHeight = 2f;
			#endregion
		}
		else if (shipType == ShipType.Alpha) {
			#region AlphaShip values
			rotationSpeed = 0.07f;
			conversionTime = 2f;
			maxElevationHeight = 4f;
			#endregion
		}
		else if (shipType == ShipType.Bravo) {
			#region BravoShip values
			rotationSpeed = 0.03f;
			conversionTime = 3f;
			maxElevationHeight = 5f;
			#endregion
		}
		elevationForce = 5f;
		maxElevationSpeed = 5f;
	}
			#endregion
	#endregion

	#region Handle Invitations/Deferrals

	#region Handle Invitations / Being Invited
	void OnTriggerEnter(Collider col){
		if (col.gameObject.layer == Constants.invitationBeam){
			if (!isBeingEmpowered){
				Ship inviterShip = col.GetComponent<InvitationBeam>().GetInvitationBeamShip;
				inviterShip.InviteANewShip(this);
				StartCoroutine(BeEmpowered(inviterShip));
			}
		}
	}

	public void InviteANewShip(Ship invitedShip){
		invitedShips.Add (invitedShip);
	}

	public IEnumerator BeEmpowered (Ship invitingShip){
		isBeingEmpowered = true;
		shipBody.constraints &= ~RigidbodyConstraints.FreezePositionY;
		if (!isComrad){
			while (isBeingEmpowered && shipBody && invitingShip && conversionTimeElapsed < conversionTime){
				conversionTimeElapsed += Time.deltaTime;
				if (shipBody.velocity.magnitude<maxElevationSpeed){
					Vector3 moveDir = (invitingShip.transform.position
					+ invitingShip.transform.forward*4f - transform.position).normalized;
					shipBody.AddForce(moveDir * elevationForce);
				}
				Vector3 currentEuler = transform.rotation.eulerAngles;
				transform.rotation = Quaternion.Lerp(transform.rotation,Quaternion.Euler(currentEuler + Vector3.up*90),rotationSpeed);
				yield return null;
			}
			if (isBeingEmpowered) {
				if (!isComrad){
					if (invitingShip){
						invitingShip.RemoveShipFromInviteList(this);
					}
					comradClass.BecomeAComrad (this);
				}
			} 
		}
		yield return null;
	}
	#endregion

	#region Handle Deferrals / Being Deferred
	void OnTriggerExit(Collider col){
		if (col.gameObject.layer == Constants.invitationBeam){
			Ship unInviterShip = col.GetComponent<InvitationBeam>().GetInvitationBeamShip;
			if (unInviterShip.GetInvitedShips.Contains(this)){
				GetReleasedFromTheInvitationBeam(unInviterShip);
			}
		}
	}

	void OnTriggerStay(Collider col){
		if (col.gameObject.layer == Constants.invitationBeam){
			Ship unInviterShip = col.GetComponent<InvitationBeam>().GetInvitationBeamShip;
			StartCoroutine(DetectBeamDeActivation(unInviterShip));
		}
	}
	//player releases the invitation beam
	IEnumerator DetectBeamDeActivation(Ship unInviterShip){
		yield return null;
		if (unInviterShip){
			if (unInviterShip.GetBeamCollider){
				if (!unInviterShip.GetBeamCollider.enabled && unInviterShip.GetInvitedShips.Contains(this)){
					GetReleasedFromTheInvitationBeam(unInviterShip);
				}
			}
		}
	}

	void GetReleasedFromTheInvitationBeam(Ship unInviterShip){
		unInviterShip.RemoveShipFromInviteList(this);
		bool keepMeInTheRunning = false;

		foreach (Ship fleetShip in comradManager.GetListOfAllShips){
			foreach (Ship stillInvitedShip in fleetShip.GetInvitedShips){
				if (this == stillInvitedShip){
					keepMeInTheRunning = true;
					break;
				}
			}
		}
		
		if (!keepMeInTheRunning){
			StartCoroutine(BeDeferred());
		}
	}

	public void RemoveShipFromInviteList (Ship deferredShip){
		invitedShips.Remove (deferredShip);
	}

	public IEnumerator BeDeferred(){
		isBeingEmpowered = false;
		isMovingToFaceForward = true;
		conversionTimeElapsed = 0f;

		while (!isBeingEmpowered && Mathf.Abs (transform.rotation.eulerAngles.y-180)>1f && Mathf.Abs (transform.position.y-startHeight)>.01f) {
			transform.rotation = Quaternion.Slerp(transform.rotation,Quaternion.Euler(0f,180f,0f),.1f);
			transform.position = Vector3.Slerp(transform.position, new Vector3 (transform.position.x,startHeight,transform.position.z),0.1f);
			yield return null;
		}
		isMovingToFaceForward = false;
		transform.rotation = Quaternion.Euler (0f, 180f, 0f);
		transform.position = new Vector3 (transform.position.x, startHeight, transform.position.z);
		shipBody.constraints = RigidbodyConstraints.FreezePositionY |
			RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
		yield return null;
	}
	#endregion

	#endregion

	#region Handle Invicibility
	public void BecomeInvincible(){
		shipCollider.enabled = false;
	}

	public void BecomeVincible(){
		shipCollider.enabled = true;
	}
	#endregion
	
	#region Return Ship Values

	#region Return Ship Values
	//marker
	#endregion

		#region Classes
	//ComradClass, GetInputManager, GetCubeGuns
	//GetShotDirector, GetComradManager, MedicalBay
	//MedicalBayHQ, GetTheUniterShip
	public ShotDirector ShotDirector{
		get{
			return shotDirector;
		}
		set{
			shotDirector = value;
		}
	}

	public UniterRespawner UniterRespawner{
		get{
			return uniterRespawner;
		}
	}

	public GunScope GetGunScope{
		get{
			return gunScope;
		}
	}

	public Camera_Thrusters GetCameraThrusters{
		get{
			return cameraThrusters;
		}
	}

	public Comrad ComradClass{
		get{
			return comradClass;
		}
		set{
			comradClass = value;
		}
	}

	public InputManager GetInputManager{
		get{
			return inputManager;
		}
		set{
			inputManager = value;
		}
	}
	
	public Gun Gun{
		get{
			return gun;
		}
		set{
			gun = value;
		}
	}
	
	public ShotDirector GetShotDirector{
		get{
			return shotDirector;
		}
		set{
			shotDirector = value;
		}
	}

	public ComradManager GetComradManager{
		get{
			return comradManager;
		}
		set{
			comradManager = value;
		}
	}
	
	public MedicalBay MedicalBay{
		get{
			return medicalBay;
		}
	}
	
	public MedicalBayHQ MedicalBayHQ{
		get{
			return medicalBayHQ;
		}
		set{
			medicalBayHQ = value;
		}
	}
	
	public Ship GetTheUniterShip{
		get{
			return theUniterShip;
		}
		set{
			theUniterShip = value;
		}
	}
		#endregion

		#region Booleans
	//IsRightSide, IsMovingToFaceForward, IsBeingEmpowered
	//IsComrad
	public bool IsRightSide{
		get{
			return isRightSide;
		}
		set{
			isRightSide = value;
		}
	}

	public bool IsMovingToFaceForward{
		get{
			return isMovingToFaceForward;
		}
		set{
			isMovingToFaceForward = value;
		}
	}
	
	public bool IsBeingEmpowered{
		get{
			return isBeingEmpowered;
		}
		set{
			isBeingEmpowered = value;
		}
	}
	
	public bool IsComrad{
		get{
			return isComrad;
		}
		set{
			isComrad = value;
		}
	}

	public bool GetShipColliderStatus{
		get{
			return shipCollider.enabled;
		}
	}
		#endregion

		#region Floats, Ints
	//ConversionTime, Rank
	
	public float ConversionTime{
		get{
			return conversionTime;
		}
		set{
			conversionTime = value;
		}
	}
	
	public int Rank{
		get{
			return rank;
		}
		set{
			rank = value;
		}
	}
		#endregion

		#region Other (Ship Collider, GetRigidBody, GetInvitedShips, BeamCollider)
	public ShipType ShipType{
		get{
			return shipType;
		}
	}

	public Collider ShipCollider{
		get{
			return shipCollider;
		}
	}

	public Rigidbody GetRigidBody{
		get{
			return shipBody;
		}
	}

	public List<Ship> GetInvitedShips{
		get{
			return invitedShips;
		}
	}
	
	public CapsuleCollider GetBeamCollider{
		get{
			return beamCollider;
		}
	}
		#endregion
	#endregion

	#region Stop Coroutines
	void OnDestroy(){
		StopAllCoroutines();
	}
}
	#endregion

