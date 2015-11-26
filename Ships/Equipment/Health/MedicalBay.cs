
#region using / Class Declaration
using UnityEngine;
using System.Collections;
using GenericFunctions;

public class MedicalBay : MonoBehaviour {
#endregion

	#region Initialize Values
	public Ship myShip;
	public GameObject shieldEnergyOrb;
	public GameObject explosion;

	private int maxHealth;
	private int currentHealth;
	private float minHealTime;
	private float maxHealTime;
	private HealthMeter healthMeterScript;

	private bool generatorsActive;

	void Awake(){
		#region Assign Health and HealTime Values
		if (myShip.ShipType == ShipType.TheUniter) {
			maxHealth = 5;
			currentHealth = maxHealth;
			minHealTime = 20f;
			maxHealTime = 25f;
			healthMeterScript = GameObject.FindObjectOfType<HealthMeter>();
			UpdateHealthMeter();
		}
		else if (myShip.ShipType == ShipType.Normal) {
			maxHealth = 1;
			minHealTime = 15f;
			maxHealTime = 20f;
		}
		else if (myShip.ShipType == ShipType.Alpha) {
			maxHealth = 2;
			minHealTime = 10f;
			maxHealTime = 20f;
		}
		else if (myShip.ShipType == ShipType.Bravo) {
			maxHealth = 3;
			minHealTime = 5f;
			maxHealTime = 10f;
		}
		currentHealth = maxHealth;
		#endregion
	}
	#endregion

	#region Activate health generation
	public void ActivateHealthGenerators(){
		if (!generatorsActive) {
			generatorsActive = true;
			ShareTheHealth ();
		}
	}

	void ShareTheHealth(){
		//find most senior comrad in need
		//give them health
		Instantiate (shieldEnergyOrb,transform.position,Quaternion.identity);
		//do it again
		Invoke ("ShareTheHealth", Random.Range (minHealTime, maxHealTime));
	}
	#endregion

	#region Heal / Hurt Ship
	public void HealThisShip(){
		if (currentHealth < maxHealth) {
			currentHealth++;
		}
		UpdateHealthMeter();
	}

	void UpdateHealthMeter(){
		if (healthMeterScript){
			healthMeterScript.ShrinkHealthBar(currentHealth, maxHealth);
		}
	}

	//this ship takes damage
	//(public verbage formed from inflicters perspective)
	public void InflictDamage(int damage){
		currentHealth -= damage;
		UpdateHealthMeter();
		CheckForDeath(false);
	}

	public void CheckForDeath(bool pullThePlug){
		if (currentHealth<=0 || pullThePlug){
			Instantiate (explosion, transform.position, Quaternion.identity);
			if (myShip.IsComrad){
				myShip.GetComradManager.RequestToLeaveTheRanks(myShip);
			}

			if (myShip.ShipType == ShipType.TheUniter ){
				myShip.UniterRespawner.CheckToSpawn();
			}
			Destroy(gameObject);
		}
	}
	#endregion

	#region Return Health Values
	public int CurrentHealth{
		get{
			return currentHealth;
		}
	}

	public int MaxHealth{
		get{
			return maxHealth;
		}
	}
	
	public float GetMinHealTime{
		get{
			return minHealTime;
		}
	}
	
	public float GetMaxHealTime{
		get{
			return maxHealTime;
		}
	}
}
	#endregion
