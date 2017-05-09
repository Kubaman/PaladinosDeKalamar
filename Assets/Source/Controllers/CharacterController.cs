using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using Skills;
using Actions;
using Commands;

namespace Characters
{
	public class CharacterController : MonoBehaviour
	{
		private CharacterModel model;

		private bool isMoving;
		private bool isRunning;
		private bool isLocked;
		private bool isPushing;
		private bool isHited;
		private bool isHiting;
		private bool isMovingAction;

		private float speedFactor;
		public Vector3 currentDirection;
		private float originalY;

		public Vector3 movingActionDirection;

		private float timeToPush;
		private float timeToPushCoolDown;

		private float timeToResume;
		private float timeToResumeCoolDown;

		private IDictionary<CommandChainType, SkillType> commandAllias = new Dictionary<CommandChainType, SkillType> ();

		private Animator characterAnimator;
		private Rigidbody body;

		private bool testAttack;

		public Vector3 Direction
		{
			get
			{
				if (currentDirection == Vector3.zero)
					return new Vector3 (-1, 0, 0);
				
				return currentDirection;
			}
		}

		public CharacterModel Model
		{
			get
			{
				return model;
			}
		}

		void Start ()
		{					
			timeToPush = 0f;
			timeToResume = 0f;
			originalY = 0.5f;
			speedFactor = 1.8f;
			timeToPushCoolDown = 0.2f;
			timeToResumeCoolDown = 0.2f;
			body = GetComponent<Rigidbody> ();
			characterAnimator = GetComponent<Animator> ();

			//this.transform.LookAt (Camera.main.transform);

			Stop (this.transform.position);
			
			commandAllias.Add (CommandChainType.DOWN_FORWARD_ATTACK, SkillType.HADOUKEN);
			commandAllias.Add (CommandChainType.DOWN_BACKWARD_ATTACK, SkillType.HADOUKEN);		
			
			commandAllias.Add (CommandChainType.FORWARD_DOWN_FORWARD_ATTACK, SkillType.SHORYUKEN);
			commandAllias.Add (CommandChainType.BACK_DOWN_BACKWARD_ATTACK, SkillType.SHORYUKEN);

			commandAllias.Add (CommandChainType.FORWARD_DOWN_BACK_ATTACK, SkillType.ATTACK_DAS_CORUJA);
			commandAllias.Add (CommandChainType.BACK_DOWN_FORWARD_ATTACK, SkillType.ATTACK_DAS_CORUJA);
			
			commandAllias.Add (CommandChainType.DOWN_FORWARD_DOWN_FORWARD_ATTACK, SkillType.SHINKU_HADOUKEN);
			commandAllias.Add (CommandChainType.DOWN_BACK_DOWN_BACKWARD_ATTACK, SkillType.SHINKU_HADOUKEN);
		}

		void FixedUpdate ()
		{
			// TODO: Mover para controle de movimento
			if (isMoving && !isLocked && !isPushing && !isMovingAction)
			{
				body.velocity = currentDirection;
			}
			else
			{
				if (!isPushing && !isMovingAction)
				{
					body.velocity = Vector3.zero;
				}
			}

			if (testAttack)
			{
				testAttack = false;
				Jab ();
			}

			//TODO: Mover para controle de quedas
			//if (isPushing)
			//{
				
			//}
			//else
			//{
				
			//}

			//Debug.Log("timer: " + characterAnimator.GetTime());
			//Debug.Log("is Running: " + isRunning);
		}

		#region ReceivedCommands

		public void UseAction (ActionType action, Vector3 characterPos)
		{
			//Debug.Log("Action: " + action);

			if (!isLocked)
			{
				switch (action)
				{
					case ActionType.MOVE_UP:
						MoveTo (characterPos, new Vector3 (0, 0, -1));
					break;
					case ActionType.MOVE_UP_FORWARD:
						MoveTo (characterPos, new Vector3 (-1, 0, -1));
					break;
					case ActionType.MOVE_FORWARD:
						MoveTo (characterPos, new Vector3 (-1, 0, 0));
					break;
					case ActionType.MOVE_FORWARD_DOWN:
						MoveTo (characterPos, new Vector3 (-1, 0, 1));
					break;
					case ActionType.MOVE_DOWN:
						MoveTo (characterPos, new Vector3 (0, 0, 1));
					break;
					case ActionType.MOVE_DOWN_BACKWARD:
						MoveTo (characterPos, new Vector3 (1, 0, 1));
					break;
					case ActionType.MOVE_BACKWARD:
						MoveTo (characterPos, new Vector3 (1, 0, 0));
					break;
					case ActionType.MOVE_BACKWARD_UP:
						MoveTo (characterPos, new Vector3 (1, 0, -1));
					break;
							
					case ActionType.RUN_UP:
						RunTo (characterPos, new Vector3 (0, 0, -1));
					break;
					case ActionType.RUN_UP_FORWARD:
						RunTo (characterPos, new Vector3 (-1, 0, -1));
					break;
					case ActionType.RUN_FORWARD:
						RunTo (characterPos, new Vector3 (-1, 0, 0));
					break;
					case ActionType.RUN_FORWARD_DOWN:
						RunTo (characterPos, new Vector3 (-1, 0, 1));
					break;
					case ActionType.RUN_DOWN:
						RunTo (characterPos, new Vector3 (0, 0, 1));
					break;
					case ActionType.RUN_DOWN_BACKWARD:
						RunTo (characterPos, new Vector3 (1, 0, 1));
					break;
					case ActionType.RUN_BACKWARD:
						RunTo (characterPos, new Vector3 (1, 0, 0));
					break;
					case ActionType.RUN_BACKWARD_UP:
						RunTo (characterPos, new Vector3 (1, 0, -1));
					break;
							
					case ActionType.NORMAL_ATTACK:
						Attack (characterPos, currentDirection);
					break;						
					case ActionType.FORCE_ATTACK:
						ForceAttack (characterPos, currentDirection);
					break;					
					case ActionType.DIRECTIONAL_ATTACK:
						DirectionalAttack (characterPos, currentDirection);
					break;				
					case ActionType.RUNNING_ATTACK:
						RunningAttack (characterPos, currentDirection);
					break;	
					case ActionType.NONE:
						Stop (characterPos);
					break;	

				//default: ActionManager.UseAction(action); break;
				}
			}
		}

		public void ApplyCommand (CommandChainType command, Vector3 characterPos)
		{				
			if (!isLocked)
			{
				switch (command)
				{
				case CommandChainType.MOVE_UP:
					MoveTo (characterPos, new Vector3 (0, 0, -1));
					break;
				case CommandChainType.MOVE_UP_FORWARD:
					MoveTo (characterPos, new Vector3 (-1, 0, -1));
					break;
				case CommandChainType.MOVE_FORWARD:
					MoveTo (characterPos, new Vector3 (-1, 0, 0));
					break;
				case CommandChainType.MOVE_FORWARD_DOWN:
					MoveTo (characterPos, new Vector3 (-1, 0, 1));
					break;
				case CommandChainType.MOVE_DOWN:
					MoveTo (characterPos, new Vector3 (0, 0, 1));
					break;
				case CommandChainType.MOVE_DOWN_BACKWARD:
					MoveTo (characterPos, new Vector3 (1, 0, 1));
					break;
				case CommandChainType.MOVE_BACKWARD:
					MoveTo (characterPos, new Vector3 (1, 0, 0));
					break;
				case CommandChainType.MOVE_BACKWARD_UP:
					MoveTo (characterPos, new Vector3 (1, 0, -1));
					break;

				case CommandChainType.RUN_UP:
					RunTo (characterPos, new Vector3 (0, 0, -1));
					break;
				case CommandChainType.RUN_UP_FORWARD:
					RunTo (characterPos, new Vector3 (-1, 0, -1));
					break;
				case CommandChainType.RUN_FORWARD:
					RunTo (characterPos, new Vector3 (-1, 0, 0));
					break;
				case CommandChainType.RUN_FORWARD_DOWN:
					RunTo (characterPos, new Vector3 (-1, 0, 1));
					break;
				case CommandChainType.RUN_DOWN:
					RunTo (characterPos, new Vector3 (0, 0, 1));
					break;
				case CommandChainType.RUN_DOWN_BACKWARD:
					RunTo (characterPos, new Vector3 (1, 0, 1));
					break;
				case CommandChainType.RUN_BACKWARD:
					RunTo (characterPos, new Vector3 (1, 0, 0));
					break;
				case CommandChainType.RUN_BACKWARD_UP:
					RunTo (characterPos, new Vector3 (1, 0, -1));
					break;

				case CommandChainType.NORMAL_ATTACK:
					Attack (characterPos, currentDirection);
					break;						
				case CommandChainType.FORCE_ATTACK:
					ForceAttack (characterPos, currentDirection);
					break;					
				case CommandChainType.DIRECTIONAL_ATTACK:
					DirectionalAttack (characterPos, currentDirection);
					break;				
				case CommandChainType.RUNNING_ATTACK:
					RunningAttack (characterPos, currentDirection);
					break;	
				case CommandChainType.NONE:
					Stop (characterPos);
					break;	

					default: 
						if (commandAllias.ContainsKey(command))
						{
							SkillManager.UseSkill (commandAllias [command], this, transform.position, Direction);
						}
					break;
				}
			}
		}

		public void ApplyDamage (DamageType damageType, int amout, EffectType effectType, int effectAmount, float effetcDuration)
		{
			Debug.Log ("Apply Damage !!!");

			// TODO processar dano

			// TODO processar effeito
		}

		public void ApplyActionMoviment(Vector3 actionDirection)
		{
			if (!isMovingAction)
			{
				isMovingAction = true;
				body.useGravity = true;
				movingActionDirection = actionDirection;

				body.AddForce (actionDirection, ForceMode.Impulse);
			}
		}

		public void PushCharacter(Vector3 pushForce)
		{
			HitCharacter (pushForce);
		}

		public void LandCharacter()
		{
			Land ();
		}

		public void MovingActionHitSpeed()
		{
			if (!isHiting)
			{
				isHiting = true;
				movingActionDirection = body.velocity;
				body.useGravity = false;
				body.velocity = Vector3.zero;
			}

			timeToResume = GameManager.GameTime + timeToResumeCoolDown;
			StartCoroutine (DelayedResumeSpeed ());
		}

		#endregion

		#region UsingActions

		private void Stop (Vector3 origin)
		{
			//Debug.Log("Stop !!!");

			isMoving = false;
			isRunning = false;
			currentDirection = Vector3.zero;

			transform.position = new Vector3 (transform.position.x, originalY, transform.position.z);

			if (characterAnimator)
			{
				characterAnimator.StopPlayback ();
				characterAnimator.Play ("Idle");
			}

			// TODO Stop Character (Usando Rigidy Body e Velocity para ficar Smooth no Bullet Time)	
		}

		private void MoveTo (Vector3 origin, Vector3 direction)
		{	
			if (isRunning)
			{
				RunTo (origin, direction);
			}
			else
			{
				isMoving = true;
				currentDirection = direction.normalized;

				if (direction.x > 0 || direction.z > 0)
				{
					characterAnimator.Play ("WalkBackward");
					//Debug.Log("Move Backward " + direction);

				}

				if (direction.x < 0 || direction.z < 0)
				{
					characterAnimator.Play ("WalkForward");
					//Debug.Log("Move Forward " + direction);
				}
			}

			// TODO MOVE Character (Usando Rigidy Body e Velocity para ficar Smooth no Bullet Time)
		}

		private void RunTo (Vector3 origin, Vector3 direction)
		{		
			isMoving = true;
			isRunning = true;
			currentDirection = direction.normalized * speedFactor;

			if (direction.x > 0 || direction.z > 0)
			{
				characterAnimator.Play ("RunBackward");
				//Debug.Log("Run Backward " + direction);
			}

			if (direction.x < 0 || direction.z < 0)
			{
				characterAnimator.Play ("RunForward");
				//Debug.Log("Run Forward " + direction);
			}
		
			// TODO MOVE Character (Usando Rigidy Body e Velocity para ficar Smooth no Bullet Time)
		}

		private void Attack (Vector3 origin, Vector3 direction)
		{
			//Debug.Log("Attack !!!");
			
			if (isRunning)
			{
				RunningAttack (origin, direction);
			}
			else
			{		
				Jab ();
			}
		}

		private void ForceAttack (Vector3 origin, Vector3 direction)
		{
			// TODO Force Attack on Direction			
			// TODO Play Force Attack Animation
		}

		private void DirectionalAttack (Vector3 origin, Vector3 direction)
		{
			// TODO Directional Attack on Direction			
			// TODO Play Directional Attack Animation
		}

		private void RunningAttack (Vector3 origin, Vector3 direction)
		{
			// TODO Running Attack on Direction			
			// TODO Play Running Attack Animation
		}

		private void HitCharacter(Vector3 pushForce)
		{
			isHited = true;
			isPushing = false;
			timeToPush = GameManager.GameTime + timeToPushCoolDown;
			body.useGravity = false;
			body.velocity = Vector3.zero;

			StartCoroutine (DelayedPush(pushForce));
		}

		private void Push(Vector3 pushForce)
		{
			if (!isPushing)
			{
				isPushing = true;
				isHited = false;
				body.useGravity = true;
				body.AddForce (pushForce, ForceMode.Impulse);
			}
		}

		private void Land()
		{
			if (isPushing || isMovingAction)
			{
				isPushing = false;
				isMovingAction = false;
				body.useGravity = false;

				Stop (transform.position);
			}
		}

		private void MovingActionNormalSpeed()
		{
			timeToResume = float.MaxValue;
			isHiting = false;
			body.useGravity = true;
			body.velocity = movingActionDirection;
		}

		private void Jab()
		{
			characterAnimator.Play("Jab");
			ActionManager.UseAction (ActionType.NORMAL_ATTACK, this);
		}

		IEnumerator ShotAnimation (string anim, float lockTimer)
		{			
			yield return new WaitForSeconds (lockTimer);
			testAttack = true;

			//Debug.Log ("Start: " + anim);
			//characterAnimator.SetTrigger(anim);
			//characterAnimator.Play (anim);
			//ActionManager.UseAction (ActionType.NORMAL_ATTACK, this);
			//Stop (transform.position);
			//isLocked = true;
			//yield return new WaitForSeconds (lockTimer);
			//Debug.Log ("End: " + anim);
			//isLocked = false;
			//Stop (transform.position);

		}

		IEnumerator DelayedPush(Vector3 pushForce)
		{
			yield return new WaitForSeconds (timeToPushCoolDown);

			if (timeToPush < GameManager.GameTime)
			{
				Push (pushForce);
			}
		}

		IEnumerator DelayedResumeSpeed()
		{
			yield return new WaitForSeconds (timeToResumeCoolDown);

			if (timeToResume < GameManager.GameTime)
			{
				MovingActionNormalSpeed ();
			}
		}
		#endregion
	}
}