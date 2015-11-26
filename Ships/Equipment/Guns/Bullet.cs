using UnityEngine;
using System.Collections;
using GenericFunctions;

public class Bullet : MonoBehaviour {

	public Collider bulletCollider;
	public Material[] bulletMaterials;
	public Renderer bulletRenderer;
	public GameObject bulletHitEffect;

	// Use this for initialization
	void Awake () {
		Destroy (gameObject, 7f);
	}

	public void SetMaterial(ShipType shipTypeThatFired){
		if (shipTypeThatFired == ShipType.TheUniter){
			bulletRenderer.material = bulletMaterials[0];
		}
		else if (shipTypeThatFired == ShipType.Normal){
			bulletRenderer.material = bulletMaterials[1];
		}
		else if (shipTypeThatFired == ShipType.Alpha){
			bulletRenderer.material = bulletMaterials[2];
		}
		else if (shipTypeThatFired == ShipType.Bravo){
			bulletRenderer.material = bulletMaterials[3];
		} 
	}
	
	void OnTriggerEnter(Collider col){
		if (col.gameObject.layer == Constants.threatLayer || col.gameObject.layer == Constants.meLayer || col.gameObject.layer == Constants.comradLayer) {
			col.GetComponent<MedicalBay>().InflictDamage(1);
			Instantiate (bulletHitEffect, transform.position, Quaternion.identity);
			Destroy(gameObject);
		}
	}

	public Collider BulletCollider{
		get{
			return bulletCollider;
		}
	}
	
}
