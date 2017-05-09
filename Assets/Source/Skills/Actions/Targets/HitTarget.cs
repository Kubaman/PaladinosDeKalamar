using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Characters;

// TODO Organizar
namespace Actions
{
	public class HitTarget : MonoBehaviour 
	{
		private bool isActive;
		private bool isCollinding;

		private int maxHits;
		private int currentHits;

		private float maxDistance;
		private float currentDistance;

		private float maxTime;
		private float currentTime;

		private float hitedSpeed;
		private float normalSpeed;
		private float hitCoolDown;

		private Vector3 direction;
		private Vector3 origPosition;

		private IList<GameObject> hitedList = new List<GameObject>();

		private ActionShoter sourceTarget;		
		
		private OnShot onShot;
		private OnHit onHit;
		private OnTime onTime;
		private OnReach onReach;

		private bool isBean;
		private Vector3 beanDirection;

		private bool isExpanding;
		private Vector3 expandSpeed;
		private float maxExpansion;

		private bool isPierce;

		// Use this for initialization
		void Start ()
		{
			beanDirection = new Vector3 (0.1f, 0, 0);
		}
		
		// Update is called once per frame
		void FixedUpdate () 
		{
			VerifyDistance ();

			// TODO mover
			if (isBean) 
			{
				if (transform.localScale.x < maxDistance)
				{
					transform.localScale = transform.localScale + beanDirection * (normalSpeed / 2.5f);
				}
				else
				{
					GetComponent<Rigidbody> ().velocity = Vector3.zero;
				}					
			}

			if (isExpanding)
			{
				if (transform.localScale.magnitude < maxExpansion)
				{
					transform.localScale += expandSpeed;
				}
				else
				{
					if (onReach != null)
					{
						onReach (transform.position, this.gameObject);
					}
				}
			}
		}

		internal void PrepareShot(ActionShoter sourceTarget, Vector3 direction, int maxHits, float maxDistance, float maxTime, float normalSpeed, 
							      float hitedSpeed, float hitCoolDown, OnShot onShot, OnHit onHit, OnTime onTime, OnReach onReach)
		{
			this.sourceTarget = sourceTarget;

			currentHits = 0;

			this.direction = direction;
			this.maxHits = maxHits;
			this.maxDistance = maxDistance;
			this.maxTime = maxTime;

			this.normalSpeed = normalSpeed;
			this.hitedSpeed = hitedSpeed;
			this.hitCoolDown = hitCoolDown;

			this.onShot = onShot;
			this.onHit = onHit;
			this.onTime = onTime;
			this.onReach = onReach;
		}

		internal void Shot()
		{
			isBean = false;
			isActive = true;
			isPierce = false;
			isCollinding = true;
		
			hitedList.Clear ();
			origPosition = transform.position;

			GetComponent<Rigidbody>().AddRelativeForce(direction * normalSpeed, ForceMode.Impulse);		
		}

		internal void Bean()
		{
			isBean = true;
			isActive = true;
			isPierce = false;
			isCollinding = true;

			hitedList.Clear ();
			origPosition = transform.position;

			GetComponent<Rigidbody>().AddRelativeForce(direction * normalSpeed, ForceMode.Impulse);	
		}

		internal void Pierce()
		{
			isBean = false;
			isActive = true;
			isPierce = true;
			isCollinding = true;

			hitedList.Clear ();
			origPosition = transform.position;

			GetComponent<Rigidbody>().AddRelativeForce(direction * normalSpeed, ForceMode.Impulse);
		}

		internal void Expand(float width)
		{
			isExpanding = true;

			expandSpeed = new Vector3 (0.1f, 0.1f, 0.1f);
			expandSpeed *= normalSpeed;
			maxExpansion = width;
		}

		internal void SpecifcPosition(Vector3 position)
		{
			isActive = true;
			isCollinding = true;
			GetComponent<Rigidbody> ().isKinematic = true;
		}

		internal void Stop()
		{
			GetComponent<Rigidbody> ().velocity = Vector3.zero;
		}

		internal void DestroyTarget()
		{
			Destroy (this.gameObject);
		}

		internal void HitCollider(GameObject collider, Characters.CharacterController controller)
		{
			if (currentHits < maxHits)
			{				
				if(!hitedList.Contains(collider))
				{
					currentHits++;

					Debug.Log (currentHits + "Hits !!!");

					onHit (controller, this.gameObject);
					AddCollider(collider);
				}
			}
			else
			{
				Destroy(this.gameObject);
			}
		}


		internal void AddCollider(GameObject collider)
		{
			if(!isBean && !isPierce)
				GetComponent<Rigidbody>().velocity = direction * hitedSpeed;
			
			hitedList.Add(collider);
			StartCoroutine(RemoveCollider(collider));
		}

		IEnumerator RemoveCollider(GameObject collider)
		{			
			yield return new WaitForSeconds(hitCoolDown);
			GetComponent<Rigidbody>().velocity = direction * normalSpeed;

			if(!isPierce)
				hitedList.Remove(collider);
		}

		void OnTriggerStay(Collider other)
		{
			if(other.gameObject.tag.Equals("Player"))
			{
				Characters.CharacterController controller = Characters.CharacterManager.GetController(other.gameObject);

				if (sourceTarget.sourceAction.caster != controller) 
				{
					if (onHit != null)
					{
						if (isActive && isCollinding)
						{
							HitCollider (other.gameObject, controller);
						}
					}
				}
			}
		}

		private void VerifyDistance()
		{
			float distance = (origPosition - transform.position).magnitude;

			if (distance > maxDistance) 
			{
				if (onReach != null) 
				{
					onReach (transform.position, this.gameObject);
				}
			}
		}
	}



	public enum HitType
	{
		BOX,
		SPHERE,
		VERTICAL_CILINDER,
		HORIZONTAL_CILINDER
	}
}
