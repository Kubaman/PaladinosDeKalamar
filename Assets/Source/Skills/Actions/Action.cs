using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Characters;

public delegate void OnShot(Characters.CharacterController target, GameObject shotGO);
public delegate void OnHit(Characters.CharacterController target, GameObject shotGO);
public delegate void OnReach(Vector3 position, GameObject shotGO);
public delegate void OnTime(Vector3 position, GameObject shotGO);

namespace Actions
{
	public class Action : MonoBehaviour 
	{	
		internal bool isActive;
		internal bool isMainPlayerAction;
		internal int expirationTime;

		internal Vector3 currentPosition;

		internal float coolDown;
		internal float castDelay;
		internal float effectAmount;
		internal float effectDuration;

		internal string beforeCastPrefab;
		internal string afterCastPrefab;
		internal string actionPrefab;

		internal GameObject actionGO;

		internal IList<ActionShoter> actionShoters = new List<ActionShoter>();
		 
		internal Characters.CharacterController caster;

		void start()
		{
			
		}

		internal void CastAction(Characters.CharacterController caster, bool lockCharacter)
		{		
			this.caster = caster;
		}

		internal ActionShoter AddActionShoter (ShoterType type, Action source, OnShot onShot, OnHit onHit, OnTime onTime, OnReach onReach)
		{
			switch (type)
			{
				case ShoterType.SINGLE_SHOT:
					SingleShot singleShot = InstantiateSingleShot ();
					singleShot.PrepareSingleShot (source, onShot, onHit, onTime, onReach);						
					actionShoters.Add (singleShot);
					return singleShot;
				break;

				case ShoterType.LOCALIZED_SHOT:
					LocalizedShot localizedShot = InstantiateLocalizedShot ();
					localizedShot.PrepareLocalizedShot (source, onShot, onHit, onTime, onReach);						
					actionShoters.Add (localizedShot);
					return localizedShot;
				break;

				case ShoterType.MOBILITY_SHOT:
					MobilityShot mobilityShot = InstantiateMobilityShot ();
					mobilityShot.PrepareMobilityShot (source, onShot, onHit, onTime, onReach);						
					actionShoters.Add (mobilityShot);
					return mobilityShot;
				break;
			}

			return null;
		}

		private SingleShot InstantiateSingleShot()
		{
			string singleShotPrefab = "Actions/TargetTypes/SingleShot";
			GameObject returnedPrefab = ObjectManager.InstantiateObject(singleShotPrefab, this.transform.position);		
			SingleShot singleShot = returnedPrefab.GetComponentInChildren<SingleShot>();

			return singleShot;
		}

		private LocalizedShot InstantiateLocalizedShot()
		{
			string localizedShotPrefab = "Actions/TargetTypes/LocalizedShot";
			GameObject returnedPrefab = ObjectManager.InstantiateObject(localizedShotPrefab, this.transform.position);		
			LocalizedShot localizedShot = returnedPrefab.GetComponentInChildren<LocalizedShot>();

			return localizedShot;
		}

		private MobilityShot InstantiateMobilityShot()
		{
			string mobilityShotPrefab = "Actions/TargetTypes/MobilityShot";
			GameObject returnedPrefab = ObjectManager.InstantiateObject(mobilityShotPrefab, this.transform.position);		
			MobilityShot mobilityShot = returnedPrefab.GetComponentInChildren<MobilityShot>();

			return mobilityShot;
		}
	}
}	