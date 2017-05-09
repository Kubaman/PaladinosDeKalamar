using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Characters;

namespace Actions
{
	public class LocalizedShot : ActionShoter
	{		
		public int sourceActionID;	
		public GameObject actionPrefabGO;		
		public int maxExpirationTime = 500;

		void FixedUpdate()
		{
			
		}

		#region Public API 
		public void PrepareLocalizedShot(Action sourceAction, OnShot onShot, OnHit onHit, OnTime onTime, OnReach onReach)
		{
			this.sourceAction = sourceAction;

			this.onShot = onShot;
			this.onHit = onHit;
			this.onTime = onTime;
			this.onReach = onReach;
		}

		public void DelayShotSphereRiseHit(Vector3 originalPosition, Vector3 direction, float width, int maxHits, float maxDistance, float maxTime, float normalSpeed, float hitedSpeed, float hitCoolDown, float shotDelay)
		{
			StartCoroutine (ShotSphereRiseHit (originalPosition, direction, width, maxHits, maxDistance, maxTime, normalSpeed, hitedSpeed, hitCoolDown, shotDelay));
		}

		public void DelayLocalizedExplosionHit(Vector3 originalPosition, Vector3 direction, float width, int maxHits, float maxDistance, float maxTime, float normalSpeed, float hitedSpeed, float hitCoolDown, float shotDelay)
		{
			StartCoroutine (ShotLocalizedExplosionHit (originalPosition, direction, width, maxHits, maxDistance, maxTime, normalSpeed, hitedSpeed, hitCoolDown, shotDelay));
		}

		public void DelayShotVerticalCylinderHit(Vector3 originalPosition, Vector3 direction, float width, int maxHits, float maxDistance, float maxTime, float normalSpeed, float hitedSpeed, float hitCoolDown, float shotDelay)
		{
			StartCoroutine (ShotVerticalCylinderHit (originalPosition, direction, width, maxHits, maxDistance, maxTime, normalSpeed, hitedSpeed, hitCoolDown, shotDelay));
		}

		public void DelayDestroy(float delay)
		{
			StartCoroutine (DestroyThis (delay));
		}

		public void StopAll()
		{
			foreach (HitTarget hitTarget in hitTargets)
			{
				hitTarget.Stop ();
			}
		}
		#endregion

		#region Interal Delay Methods 
		IEnumerator ShotSphereRiseHit(Vector3 originalPosition, Vector3 direction, float width, int maxHits, float maxDistance, float maxTime, float normalSpeed, float hitedSpeed, float hitCoolDown, float shotDelay)
		{
			yield return new WaitForSecondsRealtime (shotDelay);

			HitTarget hitTarget = InstantiateHitTarget (HitType.SPHERE, originalPosition, width);

			hitTarget.PrepareShot (this, direction, maxHits, maxDistance, maxTime, normalSpeed, hitedSpeed, hitCoolDown, this.onShot, this.onHit, this.onTime, this.onReach);
			hitTargets.Add(hitTarget);

			hitTarget.Shot();
		}

		IEnumerator ShotLocalizedExplosionHit(Vector3 originalPosition, Vector3 direction, float width, int maxHits, float maxDistance, float maxTime, float normalSpeed, float hitedSpeed, float hitCoolDown, float shotDelay)
		{
			yield return new WaitForSecondsRealtime (shotDelay);

			HitTarget hitTarget = InstantiateHitTarget (HitType.SPHERE, originalPosition, width);

			hitTarget.PrepareShot (this, direction, maxHits, maxDistance, maxTime, normalSpeed, hitedSpeed, hitCoolDown, this.onShot, this.onHit, this.onTime, this.onReach);
			hitTargets.Add(hitTarget);

			hitTarget.Expand(width);
		}

		IEnumerator ShotVerticalCylinderHit(Vector3 originalPosition, Vector3 direction, float width, int maxHits, float maxDistance, float maxTime, float normalSpeed, float hitedSpeed, float hitCoolDown, float shotDelay)
		{
			yield return new WaitForSecondsRealtime (shotDelay);

			HitTarget hitTarget = InstantiateHitTarget (HitType.VERTICAL_CILINDER, originalPosition, width);

			hitTarget.PrepareShot (this, direction, maxHits, maxDistance, maxTime, normalSpeed, hitedSpeed, hitCoolDown, this.onShot, this.onHit, this.onTime, this.onReach);
			hitTargets.Add(hitTarget);

			hitTarget.Shot();
		}

		IEnumerator DestroyThis(float delay)
		{
			yield return new WaitForSecondsRealtime (delay);

			foreach (HitTarget hitTarget in hitTargets)
			{
				hitTarget.DestroyTarget ();
			}

			Destroy (this);
		}
		#endregion
	}
}
