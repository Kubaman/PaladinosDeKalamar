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
		private bool isRunning;
		private Vector3 currentDirection;
		private IDictionary<CommandChainType, SkillType> commandAllias = new Dictionary<CommandChainType, SkillType>();	
		
		void Start()
		{		
			isRunning = false;
			currentDirection = Vector3.zero;
			
			commandAllias.Add(CommandChainType.DOWN_FORWARD_ATTACK, SkillType.HADOUKEN);
			commandAllias.Add(CommandChainType.DOWN_BACKWARD_ATTACK, SkillType.HADOUKEN);		
			
			commandAllias.Add(CommandChainType.DOWN_FORWARD_ATTACK, SkillType.HADOUKEN);
			commandAllias.Add(CommandChainType.DOWN_BACKWARD_ATTACK, SkillType.HADOUKEN);
			
			commandAllias.Add(CommandChainType.FORWARD_DOWN_FORWARD_ATTACK, SkillType.SHORYUKEN);
			commandAllias.Add(CommandChainType.BACK_DOWN_BACKWARD_ATTACK, SkillType.SHORYUKEN);
			
			commandAllias.Add(CommandChainType.DOWN_FORWARD_DOWN_FORWARD_ATTACK, SkillType.SHINKU_HADOUKEN);
			commandAllias.Add(CommandChainType.DOWN_BACK_DOWN_BACKWARD_ATTACK, SkillType.SHINKU_HADOUKEN);
		}

		#region ReceivedCommands
		public void UseAction(ActionType action, Vector3 characterPos)
		{
			switch (action)
			{
				case ActionType.MOVE_UP:MoveTo (characterPos, new Vector3 (0, 0, 1)); break;
				case ActionType.MOVE_UP_FORWARD:MoveTo (characterPos, new Vector3 (1, 0, 1)); break;
				case ActionType.MOVE_FORWARD:MoveTo (characterPos, new Vector3 (1, 0, 0)); break;
				case ActionType.MOVE_FORWARD_DOWN:	MoveTo (characterPos, new Vector3 (1, 0, -1));break;
				case ActionType.MOVE_DOWN:	MoveTo (characterPos, new Vector3 (0, 0, -1)); break;
				case ActionType.MOVE_DOWN_BACKWARD:MoveTo (characterPos, new Vector3 (-1, 0, -1)); break;
				case ActionType.MOVE_BACKWARD:MoveTo (characterPos, new Vector3 (-1, 0, 0)); break;
				case ActionType.MOVE_BACKWARD_UP:MoveTo (characterPos, new Vector3 (-1, 0, 1)); break;
						
				case ActionType.RUN_UP:RunTo (characterPos, new Vector3 (0, 0, 1)); break;
				case ActionType.RUN_UP_FORWARD:RunTo (characterPos, new Vector3 (1, 0, 1)); break;
				case ActionType.RUN_FORWARD:RunTo (characterPos, new Vector3 (1, 0, 0)); break;
				case ActionType.RUN_FORWARD_DOWN:RunTo (characterPos, new Vector3 (1, 0, -1)); break;
				case ActionType.RUN_DOWN:RunTo (characterPos, new Vector3 (0, 0, -1)); break;
				case ActionType.RUN_DOWN_BACKWARD:RunTo (characterPos, new Vector3 (-1, 0, -1)); break;
				case ActionType.RUN_BACKWARD:RunTo (characterPos, new Vector3 (-1, 0, 0)); break;
				case ActionType.RUN_BACKWARD_UP:RunTo (characterPos, new Vector3 (-1, 0, 1)); break;
						
				case ActionType.NORMAL_ATTACK:Attack (characterPos, currentDirection); break;						
				case ActionType.FORCE_ATTACK:ForceAttack (characterPos, currentDirection); break;					
				case ActionType.DIRECTIONAL_ATTACK:DirectionalAttack (characterPos, currentDirection); break;				
				case ActionType.RUNNING_ATTACK:RunningAttack (characterPos, currentDirection); break;	
				case ActionType.NONE: isRunning = false; break;
			}
		}	
			
		public void UseSkill(CommandChainType command, Vector3 characterPos)
		{	
			SkillManager.UseSkill(commandAllias[command], this, characterPos, currentDirection);
		}	
		#endregion
		
		#region UsingActions
		private void Stop (Vector3 origin)
		{
			isRunning = false;
			// TODO Stop Character (Usando Rigidy Body e Velocity para ficar Smooth no Bullet Time)
			
			// TODO Play Idle Animation 
		}
		
		private void MoveTo (Vector3 origin, Vector3 direction)
		{		
			isRunning = false;
			currentDirection = direction;
			
			// TODO MOVE Character (Usando Rigidy Body e Velocity para ficar Smooth no Bullet Time)
			
			// TODO Play Moviment Animation 
		}
		
		private void RunTo (Vector3 origin, Vector3 direction)
		{		
			isRunning = true;
			currentDirection = direction;
		
			// TODO MOVE Character (Usando Rigidy Body e Velocity para ficar Smooth no Bullet Time)
			
			// TODO Play Run Animation 
		}
		
		private void Attack(Vector3 origin, Vector3 direction)
		{
			if (isRunning)
			{
				RunningAttack(origin, direction);
			}	
			else
			{		
				// TODO Attack on Direction
				
				// TODO Play Normal Attack Animation
			}
		}
		
		private void ForceAttack(Vector3 origin, Vector3 direction)
		{
			// TODO Force Attack on Direction
			
			// TODO Play Force Attack Animation
		}
		
		private void DirectionalAttack(Vector3 origin, Vector3 direction)
		{
			// TODO Directional Attack on Direction
			
			// TODO Play Directional Attack Animation
		}
		
		private void RunningAttack(Vector3 origin, Vector3 direction)
		{
			// TODO Running Attack on Direction
			
			// TODO Play Running Attack Animation
		}
		#endregion
	}
}