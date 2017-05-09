using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Characters;

namespace Actions
{
	public class Explosion : Action 
	{
		private LocalizedShot localizedShot;

		void Start () 
		{
			UseAction (Characters.CharacterManager.GetController(transform.parent.gameObject));
		}

		public void UseAction (Characters.CharacterController caster)
		{	
			isActive = true;

			float range = 5f;
			float width = 8f;
			float speed = 4f;
			float hitSpeed = speed;
			float maxTime = 5f;

			int currentHits = 0;
			int maxHits = int.MaxValue;

			float hitCooldown = 0.1f;

			Vector3 velocity = caster.Direction * speed;
			expirationTime = 2;

			coolDown = 1.0f;			

			beforeCastPrefab = "";
			afterCastPrefab = "";
			actionPrefab = "";

			Vector3 orignalPosition = transform.position + new Vector3 (4, 0, 0);

			localizedShot = (LocalizedShot)AddActionShoter(ShoterType.LOCALIZED_SHOT, this, null, OnHit, OnTime, OnReach);

			//localizedShot.DelayShotSphereRiseHit (orignalPosition , Vector3.up, width, maxHits, range, maxTime, speed, hitSpeed, hitCooldown, 1.0f);
		
			//localizedShot.DelayShotVerticalCylinderHit (orignalPosition , Vector3.up, width, maxHits, range, maxTime, speed, hitSpeed, hitCooldown, 1.0f);

			orignalPosition = transform.position + new Vector3 (6, 0, 0);
			localizedShot.DelayLocalizedExplosionHit (orignalPosition , Vector3.up, width, maxHits, range, maxTime, speed, hitSpeed, hitCooldown, 1.0f);
		
			orignalPosition = transform.position + new Vector3 (8, 0, 0);
			localizedShot.DelayLocalizedExplosionHit (orignalPosition , Vector3.up, width, maxHits, range, maxTime, speed, hitSpeed, hitCooldown, 1.2f);

			orignalPosition = transform.position + new Vector3 (6, 0, 1);
			localizedShot.DelayLocalizedExplosionHit (orignalPosition , Vector3.up, width, maxHits, range, maxTime, speed, hitSpeed, hitCooldown, 1.4f);

			orignalPosition = transform.position + new Vector3 (8, 0, -1);
			localizedShot.DelayLocalizedExplosionHit (orignalPosition , Vector3.up, width, maxHits, range, maxTime, speed, hitSpeed, hitCooldown, 1.6f);

		}

		public void OnHit(Characters.CharacterController target, GameObject shotGO)
		{
			//target.ApplyDamage (DamageType.PHYSICAL, caster.Model.AttackDamage, EffectType.NONE, 0, 0f); 
		}

		public void OnReach(Vector3 position, GameObject shotGO)
		{
			Destroy (shotGO);
		}

		public void OnTime(Vector3 position, GameObject shotGO)
		{
			//Destroy (currentShootGo);
		}
	}
}
