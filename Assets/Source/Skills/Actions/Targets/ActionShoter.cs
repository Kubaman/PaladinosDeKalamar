using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Characters;

namespace Actions
{
	public class ActionShoter : MonoBehaviour 
	{			
		internal OnShot onShot;
		internal OnHit onHit;
		internal OnTime onTime;
		internal OnReach onReach;

		internal Action sourceAction;
		internal Vector3 origPosition;

		internal IList<HitTarget> hitTargets = new List<HitTarget>();

		internal string prefab;
		internal string actionPrefab;
		
		void Start()
		{			
			
		}
	
		void FixedUpdate()
		{
			// TODO controla cooldown de hits
		}

		internal HitTarget InstantiateHitTarget(HitType type, Vector3 originalPosition, float width)
		{
			string singleShotPrefab = "Actions/TargetTypes/";

			switch (type)
			{
				case HitType.BOX:
					singleShotPrefab += "BoxHit";
				break;

				case HitType.SPHERE:
					singleShotPrefab += "SphereHit";
				break;

				case HitType.VERTICAL_CILINDER:
					singleShotPrefab += "VerticalCylinderHit";
				break;

				case HitType.HORIZONTAL_CILINDER:
					singleShotPrefab += "HorizontalCylinderHit";
				break;
			}

			GameObject returnedPrefab = ObjectManager.InstantiateObject(singleShotPrefab, originalPosition);	
			returnedPrefab.transform.localScale = returnedPrefab.transform.localScale * width;
			return returnedPrefab.GetComponentInChildren<HitTarget>();	
		}
	}

	public enum ShoterType
	{
		TARGET_SELF,
		SINGLE_SHOT,
		LOCALIZED_SHOT,
		MOBILITY_SHOT
	}
}
