
#region Using / Class Declaration
using UnityEngine;
using System.Collections;
using GenericFunctions;

public class ShieldEnergyOrb : MonoBehaviour {
	#endregion

	#region Initialize Values
	private Ship targetShip;
	private Ship theUniterShip;
	private Transform targetOfHealing;

	public Rigidbody orbBody;
	public Collider orbCollider;
	public GameObject healthBlipEffect;

	private Vector3 moveDir;
	private float verticalForceFactor;
	private float planeDistance;
	private float maxOrbSpeed;
	private float heightToReach;
	private bool reachedHeight;
	private bool moving;
	private float moveForce;
	private float brakeDistance;
	private float brakeForce;

	void Awake(){
		moving = true;
		maxOrbSpeed = 7f;
		moveForce = 40f;
		brakeDistance = 2f;
		brakeForce = 4f;
		heightToReach = Random.Range (3f, 4f);

		if (!Component.FindObjectOfType<ComradManager> ()){
			Destroy (gameObject);
		}
		else{
			theUniterShip = Component.FindObjectOfType<ComradManager> ().theUniterShip;
			FindHealingTargetInformation(theUniterShip.MedicalBayHQ.FindHighestRankingShipInNeedOfHealth);
		}
	}
	#endregion


	#region Target ship to heal
	//medicalbay that generates this orb
	//sends the targetRigidBody for this
	//orb to follow and heal
	void FindHealingTargetInformation(Ship incomingTargetShip){
		targetShip = incomingTargetShip;
		targetOfHealing = targetShip.transform;
		StartCoroutine (MoveTowardsTargetOfHealing ());
	} 

	IEnumerator MoveTowardsTargetOfHealing(){
		while (moving){
			if (targetOfHealing){
				moveDir = (targetOfHealing.position - transform.position).normalized;
				if (transform.position.y > heightToReach) {
					reachedHeight = true;
					orbCollider.enabled = true;
				}
				if (!reachedHeight){
					moveDir = new Vector3 (moveDir.x,heightToReach,moveDir.z);
				}
				else{
					if (Vector3.Distance(targetOfHealing.position,transform.position)<brakeDistance){
						orbBody.AddForce( -orbBody.velocity * brakeForce,ForceMode.Acceleration);
					}
				}
				
				
				if ((Mathf.Abs (orbBody.velocity.x) < maxOrbSpeed) || Mathf.Sign (moveDir.x) != Mathf.Sign (orbBody.velocity.x )) {
					orbBody.AddForce( moveDir.x * Vector3.right * moveForce,ForceMode.Acceleration);
				}
				if ((Mathf.Abs (orbBody.velocity.y) < maxOrbSpeed) || Mathf.Sign (moveDir.y) != Mathf.Sign (orbBody.velocity.y )) {
					orbBody.AddForce( moveDir.y * Vector3.up * moveForce ,ForceMode.Acceleration);
				}
				if ((Mathf.Abs (orbBody.velocity.z) < maxOrbSpeed) || Mathf.Sign (moveDir.z) != Mathf.Sign (orbBody.velocity.z )) {
					orbBody.AddForce( moveDir.z * Vector3.forward * moveForce,ForceMode.Acceleration);
				}
			}
			else{
				if (theUniterShip.MedicalBayHQ.FindHighestRankingShipInNeedOfHealth){
					FindHealingTargetInformation(theUniterShip.MedicalBayHQ.FindHighestRankingShipInNeedOfHealth);
				}
				else{
					Destroy(gameObject);
				}
			}
			yield return null;
		}
		yield return null;
	}
	#endregion

	#region Heal On Trigger
	void OnTriggerEnter(Collider col){
		if (col.GetComponent<Ship> ()) {
			if (col.GetComponent<Ship>() == targetShip) {
				moving =false;
				targetShip.MedicalBay.HealThisShip();
				Instantiate ( healthBlipEffect, transform.position, Quaternion.identity);
				Destroy(gameObject);
			}
		}
	}
}
#endregion