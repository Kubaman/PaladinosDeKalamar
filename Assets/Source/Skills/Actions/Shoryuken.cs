using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Characters;

namespace Actions
{
	public class Shoryuken : Action 
	{
		// Use this for initialization
		void Start () 
		{
			UseAction (Characters.CharacterManager.GetController(transform.parent.gameObject));
		}
		
		// Update is called once per frame
		void Update () 
		{
			
		}

		public void UseAction (Characters.CharacterController caster)
		{	
			isActive = true;
			this.caster = caster;

			float range = 10f;
			float width = 5f;
			float speed = 5f;
			float hitSpeed = speed/2;
			float maxTime = 5f;

			int currentHits = 0;
			int maxHits = 4;//int.MaxValue;

			float hitCooldown = 0.4f;

			Vector3 hitPosition = new Vector3 (caster.transform.position.x + 1.5f, caster.transform.position.y + 0.7f, caster.transform.position.z);
			Vector3 velocity = caster.Direction * speed;
			Vector3 actionMoviment = new Vector3 (2, 6, 0);
			expirationTime = 2;

			coolDown = 0.1f;		

			beforeCastPrefab = "";
			afterCastPrefab = "";
			actionPrefab = "";

			MobilityShot mobilityShot = (MobilityShot)AddActionShoter(ShoterType.MOBILITY_SHOT, this, null, OnHit, OnTime, OnReach);

			mobilityShot.transform.parent = transform;

			mobilityShot.DelayRisingHit(hitPosition, width, maxHits, range, maxTime, speed, hitSpeed, hitCooldown, coolDown);

			this.caster.ApplyActionMoviment (actionMoviment);
		}

		public void OnHit(Characters.CharacterController target, GameObject shotGO)
		{
			target.PushCharacter(new Vector3(3, 7, 0));
			caster.MovingActionHitSpeed ();
			//target.ApplyDamage (DamageType.PHYSICAL, caster.Model.AttackDamage, EffectType.NONE, 0, 0f); 
		}

		public void OnReach(Vector3 position, GameObject shotGO)
		{
			//Destroy (currentShootGo);
		}

		public void OnTime(Vector3 position, GameObject shotGO)
		{
			//Destroy (currentShootGo);
		}
	}
}