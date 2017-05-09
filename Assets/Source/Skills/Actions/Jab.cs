using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Characters;

namespace Actions
{
	public class Jab : Action 
	{
		private SingleShot singleShot;

		void Start () 
		{
			UseAction (Characters.CharacterManager.GetController(transform.parent.gameObject));
		}
		
		public void UseAction (Characters.CharacterController caster)
		{	
			this.caster = caster;
			isActive = true;
			
			float range = 0.4f;
			float speed = 15f;
			float width = 2f;

			int currentHits = 0;
			int maxHits = 10;

			Vector3 origPosition = new Vector3(transform.position.x, transform.position.y + 0.3f, transform.position.z);
			Vector3 velocity = caster.Direction * speed;
			expirationTime = 2;
			
			coolDown = 1.0f;			

			beforeCastPrefab = "";
			afterCastPrefab = "";
			actionPrefab = "";

			singleShot = (SingleShot)AddActionShoter(ShoterType.SINGLE_SHOT, this, null, OnHit, null, OnReach);

			singleShot.DelayShotSingleHit (origPosition, caster.Direction, width, range, speed, 0f);

			//CastAction (caster, true);
		}		

		public void OnHit(Characters.CharacterController target, GameObject shotGO)
		{
			//target.ApplyDamage (DamageType.PHYSICAL, caster.Model.AttackDamage, EffectType.NONE, 0, 0f); 
		}

		public void OnReach(Vector3 position, GameObject shotGO)
		{
			StartCoroutine (DelayedDestroy (0.04f, shotGO));
		}

		IEnumerator DelayedDestroy(float timeToDestroy, GameObject shotGO)
		{
			singleShot.hitTargets [0].Stop ();

			yield return new WaitForSeconds (timeToDestroy);
			Destroy (shotGO);
		}
	}
}