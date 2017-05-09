using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Characters;

namespace Actions
{
	public class Hadouken : Action 
	{
		void Start () 
		{
			UseAction (Characters.CharacterManager.GetController(transform.parent.gameObject));
		}

		public void UseAction (Characters.CharacterController caster)
		{	
			isActive = true;

			float range = 10f;
			float width = 5f;
			float speed = 5f;
			float hitSpeed = speed/2;
			float maxTime = 5f;

			int currentHits = 0;
			int maxHits = 4;//int.MaxValue;

			float hitCooldown = 0.05f;

			Vector3 velocity = caster.Direction * speed;
			expirationTime = 2;

			coolDown = 0.1f;			

			beforeCastPrefab = "";
			afterCastPrefab = "";
			actionPrefab = "";

			//SingleShot singleShot = (SingleShot)AddActionShoter(ShoterType.SINGLE_SHOT, this, null, OnHit, OnTime, OnReach);

			//singleShot.DelayShotSingleHit (caster.Direction, width, range, speed, coolDown);

			//singleShot.DelayShotMultipleHit (caster.Direction, width, maxHits, range, maxTime, speed, hitSpeed, hitCooldown, coolDown);
		
			//singleShot.DelayShotBeanHit (caster.Direction, width, maxHits, range, maxTime, speed, hitSpeed, hitCooldown, coolDown);

			//singleShot.DelayShotPiercingHit (caster.Direction, width, maxHits, range, maxTime, speed, hitSpeed, hitCooldown, coolDown);
		}

		public void OnHit(Characters.CharacterController target, GameObject shotGO)
		{
			target.PushCharacter(new Vector3(3, 5, 0));
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
