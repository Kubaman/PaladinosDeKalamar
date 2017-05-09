using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Characters;

namespace Actions
{
	public class SingleShot : ActionShoter
	{		
		public int sourceActionID;	
		public GameObject actionPrefabGO;		
		public int maxExpirationTime = 500;

		void FixedUpdate()
		{
			//maxExpirationTime--;
			//if (maxExpirationTime < 0)
			//	DestroyObject (this.gameObject);

			/*
			if(isActive)
			{			
				float currentDistance = (transform.position - origPosition).magnitude;

				if(currentDistance >= sourceAction.range)
					ReachRange();

				if(actionPrefabGO != null)
				{
					actionPrefabGO.transform.position = transform.position;
				}
			}*/
		}

		#region Public API 
		public void PrepareSingleShot(Action sourceAction, OnShot onShot, OnHit onHit, OnTime onTime, OnReach onReach)
		{
			this.sourceAction = sourceAction;

			this.onShot = onShot;
			this.onHit = onHit;
			this.onTime = onTime;
			this.onReach = onReach;
		}

		public void DelayShotSingleHit(Vector3 origPosition, Vector3 direction, float width, float maxDistance, float speed, float castDelay)
		{
			StartCoroutine (ShotSingleHit (origPosition, direction, width, maxDistance, speed, castDelay));
		}

		public void DelayShotMultipleHit(Vector3 origPosition, Vector3 direction, float width, int maxHits, float maxDistance, float maxTime, float normalSpeed, float hitedSpeed, float hitCoolDown, float shotDelay)
		{
			StartCoroutine (ShotMultipleHit (origPosition, direction, width, maxHits, maxDistance, maxTime, normalSpeed, hitedSpeed, hitCoolDown, shotDelay));
		}

		public void DelayShotBeanHit(Vector3 origPosition, Vector3 direction, float width, int maxHits, float maxDistance, float maxTime, float normalSpeed, float hitedSpeed, float hitCoolDown, float shotDelay)
		{
			StartCoroutine (ShotBeanHit (origPosition, direction, width, maxHits, maxDistance, maxTime, normalSpeed, hitedSpeed, hitCoolDown, shotDelay));
		}

		public void DelayShotPiercingHit(Vector3 origPosition, Vector3 direction, float width, int maxHits, float maxDistance, float maxTime, float normalSpeed, float hitedSpeed, float hitCoolDown, float shotDelay)
		{
			StartCoroutine (ShotPiercingHit (origPosition, direction, width, maxHits, maxDistance, maxTime, normalSpeed, hitedSpeed, hitCoolDown, shotDelay));
		}
		#endregion

		#region Interal Delay Methods 
		IEnumerator ShotSingleHit(Vector3 origPosition, Vector3 direction, float width, float maxDistance, float speed, float shotDelay)
		{
			yield return StartCoroutine (ShotMultipleHit (origPosition, direction, width, 1, maxDistance, -1, speed, -1, -1, shotDelay));
		}	

		IEnumerator ShotMultipleHit(Vector3 origPosition, Vector3 direction, float width, int maxHits, float maxDistance, float maxTime, float normalSpeed, float hitedSpeed, float hitCoolDown, float shotDelay)
		{
			yield return new WaitForSecondsRealtime (shotDelay);

			string singleShotPrefab = "Actions/TargetTypes/SphereHit";
			GameObject returnedPrefab = ObjectManager.InstantiateObject(singleShotPrefab, origPosition);	
			returnedPrefab.transform.localScale = returnedPrefab.transform.localScale * width;
			HitTarget hitTarget = returnedPrefab.GetComponentInChildren<HitTarget>();	
			hitTarget.PrepareShot (this, direction, maxHits, maxDistance, maxTime, normalSpeed, hitedSpeed, hitCoolDown, this.onShot, this.onHit, this.onTime, this.onReach);
			hitTargets.Add(hitTarget);

			hitTarget.Shot();
		}

		IEnumerator ShotBeanHit(Vector3 origPosition, Vector3 direction, float width, int maxHits, float maxDistance, float maxTime, float normalSpeed, float hitedSpeed, float hitCoolDown, float shotDelay)
		{
			yield return new WaitForSecondsRealtime (shotDelay);

			string singleShotPrefab = "Actions/TargetTypes/BeanHit";
			GameObject returnedPrefab = ObjectManager.InstantiateObject(singleShotPrefab, origPosition);	
			returnedPrefab.transform.localScale = returnedPrefab.transform.localScale * width;
			HitTarget hitTarget = returnedPrefab.GetComponentInChildren<HitTarget>();	
			hitTarget.PrepareShot (this, direction, maxHits, maxDistance, maxTime, normalSpeed, hitedSpeed, hitCoolDown, this.onShot, this.onHit, this.onTime, this.onReach);
			hitTargets.Add(hitTarget);

			hitTarget.Bean();

			yield return new WaitForSecondsRealtime (maxTime);
			Destroy (returnedPrefab);
		}

		IEnumerator ShotPiercingHit(Vector3 origPosition, Vector3 direction, float width, int maxHits, float maxDistance, float maxTime, float normalSpeed, float hitedSpeed, float hitCoolDown, float shotDelay)
		{
			yield return new WaitForSecondsRealtime (shotDelay);

			string singleShotPrefab = "Actions/TargetTypes/SphereHit";
			GameObject returnedPrefab = ObjectManager.InstantiateObject(singleShotPrefab, origPosition);	
			returnedPrefab.transform.localScale = returnedPrefab.transform.localScale * width;
			HitTarget hitTarget = returnedPrefab.GetComponentInChildren<HitTarget>();	
			hitTarget.PrepareShot (this, direction, maxHits, maxDistance, maxTime, normalSpeed, hitedSpeed, hitCoolDown, this.onShot, this.onHit, this.onTime, this.onReach);
			hitTargets.Add(hitTarget);

			hitTarget.Pierce();
		}
		#endregion
	}
}
