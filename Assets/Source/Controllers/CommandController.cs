using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Commands
{
	public class CommandController : MonoBehaviour 
	{
		#region Atributos Internos
		private float maxDelay;		
		private float lastCommandTime;
		private CommandType currentCommand;
		private CommandChainType currentCommandChain;
		
		private List<CommandType> commandList = new List<CommandType>();
		private IDictionary	<KeyCode, CommandType> commandAlias = new Dictionary<KeyCode, CommandType>();	
		
		public List<CharacterCommand> twoKeyCommands = new List<CharacterCommand>();
		public List<CharacterCommand> threeKeyCommands = new List<CharacterCommand>();
		public List<CharacterCommand> fourKeyCommands = new List<CharacterCommand>();
		public List<CharacterCommand> fiveKeyCommands = new List<CharacterCommand>();
		#endregion
		
		#region Propriedades
		public CommandType CurrentCommand {get {return currentCommand;} set {currentCommand = value;}}
		public CommandChainType CurrentCommandChain {get {return currentCommandChain;} set {currentCommandChain = value;}}
		#endregion

		void Start ()
		{
			commandList.Clear();
			lastCommandTime = 0;
			maxDelay = 0.3f;
			
			//commandAlias.Add(KeyCode.ArrowUp, CommandType.MOVE_UP);
			//commandAlias.Add(KeyCode.ArrowRight, CommandType.MOVE_RIGHT);
			//commandAlias.Add(KeyCode.ArrowDown, CommandType.MOVE_DOWN);
			//commandAlias.Add(KeyCode.ArrowLeft, CommandType.MOVE_LEFT);		
			
			// ====================== Adicionando os Double Command Chains =======================================		
			// Directional Attacks 
			twoKeyCommands.Add(new CharacterCommand(CommandChainType.UP_ATTACK, CommandType.KEY_UP, CommandType.KEY_ATTACK));
			twoKeyCommands.Add(new CharacterCommand(CommandChainType.FORWARD_ATTACK, CommandType.KEY_FORWARD, CommandType.KEY_ATTACK));
			twoKeyCommands.Add(new CharacterCommand(CommandChainType.DOWN_ATTACK, CommandType.KEY_DOWN, CommandType.KEY_ATTACK));
			twoKeyCommands.Add(new CharacterCommand(CommandChainType.BACKWARD_ATTACK, CommandType.KEY_BACKWARD, CommandType.KEY_ATTACK));		
			
			// Moviments Skills
			twoKeyCommands.Add(new CharacterCommand(CommandChainType.RUN_UP, CommandType.KEY_UP, CommandType.HOLD_KEY_UP));
			twoKeyCommands.Add(new CharacterCommand(CommandChainType.RUN_FORWARD, CommandType.KEY_FORWARD, CommandType.HOLD_KEY_FORWARD));
			twoKeyCommands.Add(new CharacterCommand(CommandChainType.RUN_DOWN, CommandType.KEY_DOWN, CommandType.HOLD_KEY_DOWN));
			twoKeyCommands.Add(new CharacterCommand(CommandChainType.RUN_BACKWARD, CommandType.KEY_BACKWARD, CommandType.HOLD_KEY_BACKWARD));		
			
			
			// ====================== Adicionando os Triple Command Chains ======================================= 
			// Oposite Directions Attacks 
			threeKeyCommands.Add(new CharacterCommand(CommandChainType.UP_DOWN_ATTACK, CommandType.KEY_UP, CommandType.KEY_DOWN, CommandType.KEY_ATTACK));
			threeKeyCommands.Add(new CharacterCommand(CommandChainType.DOWN_UP_ATTACK, CommandType.KEY_DOWN, CommandType.KEY_UP, CommandType.KEY_ATTACK));
			threeKeyCommands.Add(new CharacterCommand(CommandChainType.BACK_FORWARD_ATTACK, CommandType.KEY_BACKWARD, CommandType.KEY_FORWARD, CommandType.KEY_ATTACK));
			threeKeyCommands.Add(new CharacterCommand(CommandChainType.FORWARD_BACK_ATTACK, CommandType.KEY_FORWARD, CommandType.KEY_BACKWARD, CommandType.KEY_ATTACK));	

			// Hadoukens Attacks 
			threeKeyCommands.Add(new CharacterCommand(CommandChainType.DOWN_FORWARD_ATTACK, CommandType.KEY_DOWN, CommandType.KEY_FORWARD, CommandType.KEY_ATTACK));
			threeKeyCommands.Add(new CharacterCommand(CommandChainType.FORWARD_DOWN_ATTACK, CommandType.KEY_FORWARD, CommandType.KEY_DOWN, CommandType.KEY_ATTACK));
			threeKeyCommands.Add(new CharacterCommand(CommandChainType.DOWN_BACKWARD_ATTACK, CommandType.KEY_DOWN, CommandType.KEY_BACKWARD, CommandType.KEY_ATTACK));
			threeKeyCommands.Add(new CharacterCommand(CommandChainType.BACKWARD_DOWN_ATTACK, CommandType.KEY_BACKWARD, CommandType.KEY_DOWN, CommandType.KEY_ATTACK));	

			// Novos Hadoukens Attacks 
			threeKeyCommands.Add(new CharacterCommand(CommandChainType.UP_FORWARD_ATTACK, CommandType.KEY_UP, CommandType.KEY_FORWARD, CommandType.KEY_ATTACK));
			threeKeyCommands.Add(new CharacterCommand(CommandChainType.FORWARD_UP_ATTACK, CommandType.KEY_FORWARD, CommandType.KEY_UP, CommandType.KEY_ATTACK));
			threeKeyCommands.Add(new CharacterCommand(CommandChainType.UP_BACKWARD_ATTACK, CommandType.KEY_UP, CommandType.KEY_BACKWARD, CommandType.KEY_ATTACK));
			threeKeyCommands.Add(new CharacterCommand(CommandChainType.BACKWARD_UP_ATTACK, CommandType.KEY_BACKWARD, CommandType.KEY_UP, CommandType.KEY_ATTACK));	
			
			
			// ====================== Adicionando os Quadruple Command Chains =======================================
			// Hadoukens Completos Attacks 
			fourKeyCommands.Add(new CharacterCommand(CommandChainType.BACK_DOWN_FORWARD_ATTACK, CommandType.KEY_BACKWARD, CommandType.KEY_DOWN, CommandType.KEY_FORWARD, CommandType.KEY_ATTACK));		
			fourKeyCommands.Add(new CharacterCommand(CommandChainType.FORWARD_DOWN_BACK_ATTACK, CommandType.KEY_FORWARD, CommandType.KEY_DOWN, CommandType.KEY_BACKWARD, CommandType.KEY_ATTACK));
			fourKeyCommands.Add(new CharacterCommand(CommandChainType.BACK_UP_FORWARD_ATTACK, CommandType.KEY_BACKWARD, CommandType.KEY_UP, CommandType.KEY_FORWARD, CommandType.KEY_ATTACK));			
			fourKeyCommands.Add(new CharacterCommand(CommandChainType.FORWARD_UP_BACK_ATTACK, CommandType.KEY_FORWARD, CommandType.KEY_UP, CommandType.KEY_BACKWARD, CommandType.KEY_ATTACK));		
			
			// Novos Hadoukens Completos Attacks 
			fourKeyCommands.Add(new CharacterCommand(CommandChainType.UP_FORWARD_DOWN_ATTACK, CommandType.KEY_UP, CommandType.KEY_FORWARD, CommandType.KEY_DOWN, CommandType.KEY_ATTACK));		
			fourKeyCommands.Add(new CharacterCommand(CommandChainType.DOWN_FORWARD_UP_ATTACK, CommandType.KEY_DOWN, CommandType.KEY_FORWARD, CommandType.KEY_UP, CommandType.KEY_ATTACK));
			fourKeyCommands.Add(new CharacterCommand(CommandChainType.UP_BACKWARD_DOWN_ATTACK, CommandType.KEY_UP, CommandType.KEY_BACKWARD, CommandType.KEY_DOWN, CommandType.KEY_ATTACK));			
			fourKeyCommands.Add(new CharacterCommand(CommandChainType.DOWN_BACKWARD_UP_ATTACK, CommandType.KEY_DOWN, CommandType.KEY_BACKWARD, CommandType.KEY_UP, CommandType.KEY_ATTACK));
					
			// Shoryukens Attacks 
			fourKeyCommands.Add(new CharacterCommand(CommandChainType.FORWARD_DOWN_FORWARD_ATTACK, CommandType.KEY_FORWARD, CommandType.KEY_DOWN, CommandType.KEY_FORWARD, CommandType.KEY_ATTACK));			
			fourKeyCommands.Add(new CharacterCommand(CommandChainType.BACK_DOWN_BACKWARD_ATTACK, CommandType.KEY_BACKWARD, CommandType.KEY_DOWN, CommandType.KEY_BACKWARD, CommandType.KEY_ATTACK));	
			fourKeyCommands.Add(new CharacterCommand(CommandChainType.FORWARD_UP_FORWARD_ATTACK, CommandType.KEY_FORWARD, CommandType.KEY_UP, CommandType.KEY_FORWARD, CommandType.KEY_ATTACK));			
			fourKeyCommands.Add(new CharacterCommand(CommandChainType.BACK_UP_BACKWARD_ATTACK, CommandType.KEY_BACKWARD, CommandType.KEY_UP, CommandType.KEY_BACKWARD, CommandType.KEY_ATTACK));
			
			// Kame-Hame-Hás Attacks
			fourKeyCommands.Add(new CharacterCommand(CommandChainType.DOWN_BACK_FORWARD_ATTACK, CommandType.KEY_DOWN, CommandType.KEY_BACKWARD, CommandType.KEY_FORWARD, CommandType.KEY_ATTACK));			
			fourKeyCommands.Add(new CharacterCommand(CommandChainType.DOWN_FORWARD_BACK_ATTACK, CommandType.KEY_DOWN, CommandType.KEY_FORWARD, CommandType.KEY_BACKWARD, CommandType.KEY_ATTACK));	
			fourKeyCommands.Add(new CharacterCommand(CommandChainType.UP_BACK_FORWARD_ATTACK, CommandType.KEY_UP, CommandType.KEY_BACKWARD, CommandType.KEY_FORWARD, CommandType.KEY_ATTACK));			
			fourKeyCommands.Add(new CharacterCommand(CommandChainType.UP_FORWARD_BACK_ATTACK, CommandType.KEY_UP, CommandType.KEY_FORWARD, CommandType.KEY_BACKWARD, CommandType.KEY_ATTACK));
			
			
			// ====================== Adicionando os Quintuple Command Chains =======================================		
			// Double Hadoukens Attacks 
			fiveKeyCommands.Add(new CharacterCommand(CommandChainType.DOWN_FORWARD_DOWN_FORWARD_ATTACK, CommandType.KEY_DOWN, CommandType.KEY_FORWARD, CommandType.KEY_DOWN, CommandType.KEY_FORWARD, CommandType.KEY_ATTACK));
			fiveKeyCommands.Add(new CharacterCommand(CommandChainType.DOWN_BACK_DOWN_BACKWARD_ATTACK, CommandType.KEY_DOWN, CommandType.KEY_BACKWARD, CommandType.KEY_DOWN, CommandType.KEY_BACKWARD, CommandType.KEY_ATTACK));
			fiveKeyCommands.Add(new CharacterCommand(CommandChainType.FORWARD_DOWN_FORWARD_DOWN_ATTACK, CommandType.KEY_FORWARD, CommandType.KEY_DOWN, CommandType.KEY_FORWARD, CommandType.KEY_DOWN, CommandType.KEY_ATTACK));
			fiveKeyCommands.Add(new CharacterCommand(CommandChainType.BACK_DOWN_BACK_DOWN_ATTACK, CommandType.KEY_BACKWARD, CommandType.KEY_DOWN, CommandType.KEY_BACKWARD, CommandType.KEY_DOWN, CommandType.KEY_ATTACK));
			
			// Double Trás-Frentes Attacks 
			fiveKeyCommands.Add(new CharacterCommand(CommandChainType.BACK_FORWARD_BACK_FORWARD_ATTACK, CommandType.KEY_BACKWARD, CommandType.KEY_FORWARD, CommandType.KEY_BACKWARD, CommandType.KEY_FORWARD, CommandType.KEY_ATTACK));
			fiveKeyCommands.Add(new CharacterCommand(CommandChainType.FORWARD_BACK_FORWARD_BACKWARD_ATTACK, CommandType.KEY_FORWARD, CommandType.KEY_BACKWARD, CommandType.KEY_FORWARD, CommandType.KEY_BACKWARD, CommandType.KEY_ATTACK));
			fiveKeyCommands.Add(new CharacterCommand(CommandChainType.UP_DOWN_UP_DOWN_ATTACK, CommandType.KEY_UP, CommandType.KEY_DOWN, CommandType.KEY_UP, CommandType.KEY_DOWN, CommandType.KEY_ATTACK));
			fiveKeyCommands.Add(new CharacterCommand(CommandChainType.DOWN_UP_DOWN_UP_ATTACK, CommandType.KEY_DOWN, CommandType.KEY_UP, CommandType.KEY_DOWN, CommandType.KEY_UP, CommandType.KEY_ATTACK));
			
			// Double Novos Hadoukens Attacks 
			fiveKeyCommands.Add(new CharacterCommand(CommandChainType.UP_FORWARD_UP_FORWARD_ATTACK, CommandType.KEY_UP, CommandType.KEY_FORWARD, CommandType.KEY_UP, CommandType.KEY_FORWARD, CommandType.KEY_ATTACK));
			fiveKeyCommands.Add(new CharacterCommand(CommandChainType.UP_BACK_UP_BACKWARD_ATTACK, CommandType.KEY_UP, CommandType.KEY_BACKWARD, CommandType.KEY_UP, CommandType.KEY_BACKWARD, CommandType.KEY_ATTACK));
			fiveKeyCommands.Add(new CharacterCommand(CommandChainType.FORWARD_UP_FORWARD_UP_ATTACK, CommandType.KEY_FORWARD, CommandType.KEY_UP, CommandType.KEY_FORWARD, CommandType.KEY_UP, CommandType.KEY_ATTACK));
			fiveKeyCommands.Add(new CharacterCommand(CommandChainType.BACK_UP_BACK_UP_ATTACK, CommandType.KEY_BACKWARD, CommandType.KEY_UP, CommandType.KEY_BACKWARD, CommandType.KEY_UP, CommandType.KEY_ATTACK));
			
			// Hadouken Ida e Volta Attacks
			fiveKeyCommands.Add(new CharacterCommand(CommandChainType.DOWN_BACK_DOWN_FORWARD_ATTACK, CommandType.KEY_DOWN, CommandType.KEY_BACKWARD, CommandType.KEY_DOWN, CommandType.KEY_FORWARD, CommandType.KEY_ATTACK));
			fiveKeyCommands.Add(new CharacterCommand(CommandChainType.DOWN_FORWARD_DOWN_BACKWARD_ATTACK, CommandType.KEY_DOWN, CommandType.KEY_FORWARD, CommandType.KEY_DOWN, CommandType.KEY_BACKWARD, CommandType.KEY_ATTACK));
			fiveKeyCommands.Add(new CharacterCommand(CommandChainType.UP_BACK_UP_FORWARD_ATTACK, CommandType.KEY_UP, CommandType.KEY_BACKWARD, CommandType.KEY_UP, CommandType.KEY_FORWARD, CommandType.KEY_ATTACK));
			fiveKeyCommands.Add(new CharacterCommand(CommandChainType.UP_FORWARD_UP_BACKWARD_ATTACK, CommandType.KEY_UP, CommandType.KEY_FORWARD, CommandType.KEY_UP, CommandType.KEY_BACKWARD, CommandType.KEY_ATTACK));
			
			// HAO-Shokokens Attacks
			fiveKeyCommands.Add(new CharacterCommand(CommandChainType.FORWARD_BACK_DOWN_FORWARD_ATTACK, CommandType.KEY_FORWARD, CommandType.KEY_BACKWARD, CommandType.KEY_DOWN, CommandType.KEY_FORWARD, CommandType.KEY_ATTACK));
			fiveKeyCommands.Add(new CharacterCommand(CommandChainType.BACK_FORWARD_DOWN_BACKWARD_ATTACK, CommandType.KEY_BACKWARD, CommandType.KEY_FORWARD, CommandType.KEY_DOWN, CommandType.KEY_BACKWARD, CommandType.KEY_ATTACK));
			fiveKeyCommands.Add(new CharacterCommand(CommandChainType.FORWARD_BACK_UP_FORWARD_ATTACK, CommandType.KEY_FORWARD, CommandType.KEY_BACKWARD, CommandType.KEY_UP, CommandType.KEY_FORWARD, CommandType.KEY_ATTACK));
			fiveKeyCommands.Add(new CharacterCommand(CommandChainType.BACK_FORWARD_UP_BACK_ATTACK, CommandType.KEY_BACKWARD, CommandType.KEY_FORWARD, CommandType.KEY_UP, CommandType.KEY_BACKWARD, CommandType.KEY_ATTACK));		
		}
		
		#region Updates
		/**
		 * Metodo padrão do "MonoBehaviour" do Unity3D, 
		 * ele é executado uma vez a cada frame.
		 */
		void FixedUpdate () 
		{
			VerifyCommands();
			UpdateList();
		}
		
		private void UpdateList()
		{
			if (GameManager.GameTime - lastCommandTime >= maxDelay)
			{	
				if (commandList.Count > 0)
				{
					//Debug.Log("Clear ");
					commandList.Clear();
				}
			}
		}
		#endregion
		
		#region Metodos Publicos
		/**
		 * API principal de comandos, adicionando um comando a lista.
		 */ 	 
		public void AddCommand(KeyCode key)
		{
			AddCommand (commandAlias[key]);
		} 
		 
		public void AddCommand(CommandType command)
		{
			if (command != currentCommand)
			{
				currentCommand = command;
				lastCommandTime = GameManager.GameTime;		
				commandList.Add(command);
			}		
		}
		#endregion
		
		#region Metodos Internos
		/**
		 * Verifica a lista de comandos realizados com as listas de Todos os comandos.
		 * Seguindo a ordem de prioridade evitando que um comando invalide outro 
		 * (Ex: Hadouken - Shoryuken , Caso eu verifique o Hadouken antes do Shoryuken eu nunca acharei o Shoryuken
		 * Pois um Shoryuken nada mais é que Frete + Hadouken OBS: fica a dica pra quem sabe mandar Hadouken, mas sofre pra mandar Shoryuken !!!).
		 * 
		 * Ordem de prioridade:
		 * Comandos de 5 Teclas (Ex: Shinku-Hadouken = Baixo, Frente, Baixo, Frente, Ataque)
		 * Comandos de 4 Teclas (Ex: Shoryuken = Frente, Baixo, Frente, Ataque)
		 * Comandos de 3 Teclas (Ex: Hadouken = Baixo, Frente, Ataque)	 
		 * Comandos de 2 Teclas (Ex: Frente, Ataque)
		 * Comandos de Ataque Basico (Ex: Ataque)
	 	 */
		private void VerifyCommands()
		{
			for (int i = 0 ; i < commandList.Count ; i++)
			{
				if (commandList.Count > i + 4)
					Verify5KeyCommands(i);
				
				if (commandList.Count > i + 3)
					Verify4KeyCommands(i);
				
				if (commandList.Count > i + 2)
					Verify3KeyCommands(i);			
				
				if (commandList.Count > i + 2)
					Verify2KeyCommands(i);
				
				if (commandList.Count > i)
					VerifyAttack(i);	
			}		
		}
		
		/**
		 * Verifica comandos de 5 teclas, apagando a lista de comandos assim que encontre algo.
		 */ 
		private void Verify5KeyCommands(int index)
		{
			foreach (CharacterCommand entrance in fiveKeyCommands)
			{
				if (commandList[index] == entrance.command[0] && 
				    commandList[index+1] == entrance.command[1] && 
					commandList[index+2] == entrance.command[2] && 
					commandList[index+3] == entrance.command[3] && 
					commandList[index+4] == entrance.command[4])
				{
					CurrentCommandChain = entrance.type;
					CurrentCommand = CommandType.NONE;
					commandList.Clear();
					return;
				}	
			}
		}
		
		/**
		 * Verifica comandos de 4 teclas, apagando a lista de comandos assim que encontre algo.
		 */ 
		private void Verify4KeyCommands(int index)
		{
			foreach (CharacterCommand entrance in fourKeyCommands)
			{
				if (commandList[index] == entrance.command[0] && 
				    commandList[index+1] == entrance.command[1] && 
					commandList[index+2] == entrance.command[2] && 
					commandList[index+3] == entrance.command[3])
				{
					CurrentCommandChain = entrance.type;
					CurrentCommand = CommandType.NONE;
					commandList.Clear();
					return;
				}	
			}
		}
		
		/**
		 * Verifica comandos de 3 teclas, apagando a lista de comandos assim que encontre algo.
		 */ 
		private void Verify3KeyCommands(int index)
		{
			foreach (CharacterCommand entrance in threeKeyCommands)
			{		
				if (commandList[index] == entrance.command[0] && 
				    commandList[index+1] == entrance.command[1] && 
					commandList[index+2] == entrance.command[2])
				{
					CurrentCommandChain = entrance.type;
					CurrentCommand = CommandType.NONE;
					commandList.Clear();
					return;
				}	
			}
		}
		
		/**
		 * Verifica comandos de 2 teclas, apagando a lista de comandos assim que encontre algo.
		 */ 
		private void Verify2KeyCommands(int index)
		{
			foreach (CharacterCommand entrance in twoKeyCommands)
			{		
				if (commandList[index] == entrance.command[0] && 
				    commandList[index+1] == entrance.command[1])
				{
					CurrentCommandChain = entrance.type;
					CurrentCommand = CommandType.NONE;
					commandList.Clear();
					return;
				}	
			}
		}
		
		/**
		 * Verifica comandos de ataque, apagando a lista de comandos assim que encontre-o.
		 */ 
		private void VerifyAttack(int index)
		{
			if (commandList[index] == CommandType.KEY_ATTACK)
			{
				CurrentCommandChain = CommandChainType.NONE;
				CurrentCommand = CommandType.KEY_ATTACK;
				commandList.Clear();
				return;
			}
		}
		
		/**
		 * Envia o resultado da analise da "Command Chain", para o objeto responsavel.
		 */	
		private void ApplyCommand()
		{
			if (CurrentCommandChain != null)
			{
				switch (CurrentCommandChain)
				{		
					// Running Actions
					case CommandChainType.RUN_UP: GameManager.ApplyAction(Actions.ActionType.RUN_UP); break;
					case CommandChainType.RUN_UP_FORWARD:GameManager.ApplyAction (Actions.ActionType.RUN_UP_FORWARD); break;
					case CommandChainType.RUN_FORWARD:GameManager.ApplyAction (Actions.ActionType.RUN_FORWARD); break;
					case CommandChainType.RUN_FORWARD_DOWN:GameManager.ApplyAction (Actions.ActionType.RUN_FORWARD_DOWN); break;
					case CommandChainType.RUN_DOWN:GameManager.ApplyAction (Actions.ActionType.RUN_DOWN); break;
					case CommandChainType.RUN_DOWN_BACKWARD:GameManager.ApplyAction (Actions.ActionType.RUN_DOWN_BACKWARD); break;
					case CommandChainType.RUN_BACKWARD:GameManager.ApplyAction (Actions.ActionType.RUN_BACKWARD); break;
					case CommandChainType.RUN_BACKWARD_UP:GameManager.ApplyAction (Actions.ActionType.RUN_BACKWARD_UP); break;	
								 
					// DiCommandChainType.rectional Attacks
					case CommandChainType.UP_ATTACK: GameManager.ApplyAction(Actions.ActionType.DIRECTIONAL_ATTACK); break;	
					case CommandChainType.FORWARD_ATTACK: GameManager.ApplyAction(Actions.ActionType.DIRECTIONAL_ATTACK); break;	
					case CommandChainType.DOWN_ATTACK: GameManager.ApplyAction(Actions.ActionType.DIRECTIONAL_ATTACK); break;	
					case CommandChainType.BACKWARD_ATTACK: GameManager.ApplyAction(Actions.ActionType.DIRECTIONAL_ATTACK); break;

					// Skills
					GameManager.ApplyCommand(CurrentCommandChain);
				}
			}
			else
			{
				switch (CurrentCommand)
				{
					// Directional Keys
					case CommandType.KEY_UP: GameManager.ApplyAction(Actions.ActionType.MOVE_UP); break;
					case CommandType.KEY_UP_FORWARD:GameManager.ApplyAction (Actions.ActionType.MOVE_UP_FORWARD); break;
					case CommandType.KEY_FORWARD:GameManager.ApplyAction (Actions.ActionType.MOVE_FORWARD); break;
					case CommandType.KEY_FORWARD_DOWN:GameManager.ApplyAction (Actions.ActionType.MOVE_FORWARD_DOWN); break;
					case CommandType.KEY_DOWN:GameManager.ApplyAction (Actions.ActionType.MOVE_DOWN); break;
					case CommandType.KEY_DOWN_BACKWARD:GameManager.ApplyAction (Actions.ActionType.MOVE_DOWN_BACKWARD); break;
					case CommandType.KEY_BACKWARD:GameManager.ApplyAction (Actions.ActionType.MOVE_BACKWARD); break;
					case CommandType.KEY_BACKWARD_UP:GameManager.ApplyAction (Actions.ActionType.MOVE_BACKWARD_UP); break;	
								 
					// AtCommandType.tack keys
					case CommandType.KEY_ATTACK: GameManager.ApplyAction(Actions.ActionType.NORMAL_ATTACK); break;								
					case CommandType.HOLD_KEY_ATTACK: GameManager.ApplyAction(Actions.ActionType.FORCE_ATTACK); break;
				}
			}
		}
		#endregion
	}

	public enum CommandType
	{
		NONE,
		
		KEY_UP,
		KEY_UP_FORWARD,
		KEY_FORWARD,
		KEY_FORWARD_DOWN,
		KEY_DOWN,
		KEY_DOWN_BACKWARD,
		KEY_BACKWARD,	
		KEY_BACKWARD_UP,	
		
		HOLD_KEY_UP,
		HOLD_KEY_UP_FORWARD,
		HOLD_KEY_FORWARD,
		HOLD_KEY_FORWARD_DOWN,
		HOLD_KEY_DOWN,
		HOLD_KEY_DOWN_BACKWARD,
		HOLD_KEY_BACKWARD,	
		HOLD_KEY_BACKWARD_UP,
		
		KEY_ATTACK,
		HOLD_KEY_ATTACK
	}

	public enum CommandChainType
	{	
		NONE,

		// ===== Double Command Chains =====
		UP_ATTACK,
		FORWARD_ATTACK,
		DOWN_ATTACK,
		BACKWARD_ATTACK,
		
		RUN_UP,
		RUN_UP_FORWARD,
		RUN_FORWARD,
		RUN_FORWARD_DOWN,
		RUN_DOWN,
		RUN_DOWN_BACKWARD,
		RUN_BACKWARD,
		RUN_BACKWARD_UP,
		
		// ===== Triple Command Chains ===== 	
		// *** Trás-Frentes ***
		UP_DOWN_ATTACK,
		DOWN_UP_ATTACK,
		BACK_FORWARD_ATTACK,
		FORWARD_BACK_ATTACK,
		
		// *** Hadoukens ***
		DOWN_FORWARD_ATTACK,
		FORWARD_DOWN_ATTACK,
		DOWN_BACKWARD_ATTACK,
		BACKWARD_DOWN_ATTACK,
			
		// *** Novos Hadoukens ***
		UP_FORWARD_ATTACK,
		FORWARD_UP_ATTACK,
		UP_BACKWARD_ATTACK,
		BACKWARD_UP_ATTACK,
		
		// ===== Quadruple Command Chains ======	
		// *** Hadoukens Completos ***
		BACK_DOWN_FORWARD_ATTACK,
		FORWARD_DOWN_BACK_ATTACK,
		BACK_UP_FORWARD_ATTACK,
		FORWARD_UP_BACK_ATTACK,
		
		// *** Novos Hadoukens Completos ***
		UP_FORWARD_DOWN_ATTACK,
		DOWN_FORWARD_UP_ATTACK,
		UP_BACKWARD_DOWN_ATTACK,
		DOWN_BACKWARD_UP_ATTACK,
		
		// *** Shoryukens ***
		BACK_DOWN_BACKWARD_ATTACK,
		FORWARD_DOWN_FORWARD_ATTACK,
		BACK_UP_BACKWARD_ATTACK,
		FORWARD_UP_FORWARD_ATTACK,
		
		// *** Kame-Hame-Hás (Hyper Dimension)
		DOWN_BACK_FORWARD_ATTACK,
		UP_BACK_FORWARD_ATTACK,
		DOWN_FORWARD_BACK_ATTACK,
		UP_FORWARD_BACK_ATTACK,	
		
		// ===== Quintuple Command Chains ======	
		// *** Double Hadoukens ***
		DOWN_FORWARD_DOWN_FORWARD_ATTACK,
		DOWN_BACK_DOWN_BACKWARD_ATTACK,
		FORWARD_DOWN_FORWARD_DOWN_ATTACK,
		BACK_DOWN_BACK_DOWN_ATTACK,
		
		// *** Double Trás-Frentes ***
		UP_DOWN_UP_DOWN_ATTACK,
		DOWN_UP_DOWN_UP_ATTACK,
		BACK_FORWARD_BACK_FORWARD_ATTACK,
		FORWARD_BACK_FORWARD_BACKWARD_ATTACK,
		
		// *** Double Novos Hadoukens ***
		UP_FORWARD_UP_FORWARD_ATTACK,
		FORWARD_UP_FORWARD_UP_ATTACK,
		UP_BACK_UP_BACKWARD_ATTACK,
		BACK_UP_BACK_UP_ATTACK,
		
		// *** Hadouken Ida e Volta ***
		DOWN_BACK_DOWN_FORWARD_ATTACK,
		DOWN_FORWARD_DOWN_BACKWARD_ATTACK,
		UP_BACK_UP_FORWARD_ATTACK,
		UP_FORWARD_UP_BACKWARD_ATTACK,	
		
		// *** HAO-Shokokens ***
		FORWARD_BACK_DOWN_FORWARD_ATTACK,
		BACK_FORWARD_DOWN_BACKWARD_ATTACK,
		FORWARD_BACK_UP_FORWARD_ATTACK,
		BACK_FORWARD_UP_BACK_ATTACK,
	}
}