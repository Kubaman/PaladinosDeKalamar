using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Actions
{
	public class MobilityShot : ActionShoter
	{
		// Use this for initialization
		void Start () 
		{
			
		}
		
		// Update is called once per frame
		void FixedUpdate () 
		{
			
		}

		#region Public API 
		public void PrepareMobilityShot(Action sourceAction, OnShot onShot, OnHit onHit, OnTime onTime, OnReach onReach)
		{
			this.sourceAction = sourceAction;

			this.onShot = onShot;
			this.onHit = onHit;
			this.onTime = onTime;
			this.onReach = onReach;
		}

		public void DelayRisingHit(Vector3 originalPosition, float width, int maxHits, float maxDistance, float maxTime, float normalSpeed, float hitedSpeed, float hitCoolDown, float shotDelay)
		{
			StartCoroutine (RisingHit (originalPosition, width, maxHits, maxDistance, maxTime, normalSpeed, hitedSpeed, hitCoolDown, shotDelay));
		}
		#endregion

		#region Interal Delay Methods 
		IEnumerator RisingHit(Vector3 originalPosition, float width, int maxHits, float maxDistance, float maxTime, float normalSpeed, float hitedSpeed, float hitCoolDown, float shotDelay)
		{
			yield return new WaitForSecondsRealtime (shotDelay);

			HitTarget hitTarget = InstantiateHitTarget (HitType.SPHERE, originalPosition, width);

			hitTarget.PrepareShot (this, Vector3.zero, maxHits, maxDistance, maxTime, normalSpeed, hitedSpeed, hitCoolDown, this.onShot, this.onHit, this.onTime, this.onReach);
			hitTargets.Add(hitTarget);

			hitTarget.gameObject.transform.parent = transform;

			hitTarget.SpecifcPosition (originalPosition);
		}
		#endregion
	}
}