
#region Declare Stuff
using UnityEngine;
using System.Collections;
using GenericFunctions;

public class Gun : MonoBehaviour {

#endregion

	#region Initialize Values
	public Ship myShip;
	public AudioSource[] bulletSounds;
	public GameObject bullet;
	private float fireSpeed;
	private int bulletIndex;

	void Awake(){
		fireSpeed = 30f;
	}
	#endregion

	#region Fire On Command

	#region Fire On Command
	//marker
	#endregion

		#region Detect FiringCommand
	void Update () {
		if (myShip.IsComrad && myShip.GetTheUniterShip.GetShotDirector.CanShootAgain) {
			if (Input.GetButtonDown (myShip.GetTheUniterShip.GetInputManager.FireBullets)) {
				Vector3 fireDirection = myShip.transform.forward;
				Fire (fireDirection.normalized);
			}
		}
	}
		#endregion

		#region FireThe Bullet
	public void Fire(Vector3 fireDirection){
		GameObject newBullet = (GameObject)Instantiate (bullet, transform.position, Quaternion.identity);
		Rigidbody bulletBody = newBullet.GetComponent<Rigidbody> ();
		Bullet bulletScript = newBullet.GetComponent<Bullet>();
		bulletScript.SetMaterial(myShip.ShipType);


		if (bulletBody){
			if (myShip.IsComrad && myShip.GetTheUniterShip){
				bulletBody.velocity = fireDirection * fireSpeed + myShip.GetTheUniterShip.GetRigidBody.velocity;
			}
			else if (myShip.GetRigidBody){
				bulletBody.velocity = fireDirection * fireSpeed + myShip.GetRigidBody.velocity;
			}
			else{
				bulletBody.velocity = fireDirection * fireSpeed;
			}

			if (myShip.ShipCollider.enabled && bulletScript.BulletCollider.enabled){
				Physics.IgnoreCollision(myShip.ShipCollider, bulletScript.BulletCollider);
			}
		}

		if (myShip.ShipType == ShipType.TheUniter){
			bulletSounds[bulletIndex].Play ();
			ReloadBulletSounds ();
		}
	}
		#endregion

		#region Reload The Gun Sound index
	public void ReloadBulletSounds(){
		bulletIndex++;
		if (bulletIndex > bulletSounds.Length - 1) {
			bulletIndex = 0;
		}
	}
		#endregion

	#endregion

	#region Return Values
	public float FireSpeed{
		get{
			return fireSpeed;
		}
		set{
			fireSpeed = value;
		}
	}
}
	#endregion
