using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Commands
{
	public class CommandController : MonoBehaviour 
	{
		#region Atributos Internos
		private float maxDelay;		
		private float currentDelay;

		private float lastCommandTime;
		private CommandType currentCommand;

		private string fullCommandChainString;
		private string currentCommandChainString;
		private CommandChainType currentCommandChain;
		private List<CommandType> inacceptablesAttackCommands = new List<CommandType>();

		//private List<CommandType> commandList = new List<CommandType>();
		private IDictionary	<KeyCode, CommandType> commandAlias = new Dictionary<KeyCode, CommandType>();	
		
		public List<CharacterCommand> twoKeyCommands = new List<CharacterCommand>();
		public List<CharacterCommand> threeKeyCommands = new List<CharacterCommand>();
		public List<CharacterCommand> fourKeyCommands = new List<CharacterCommand>();
		public List<CharacterCommand> fiveKeyCommands = new List<CharacterCommand>();

		private int maxKeys;
		private IDictionary	<string, CommandChainType> commandStringsAlias = new Dictionary<string, CommandChainType>();	
		#endregion
		
		#region Propriedades
		public CommandType CurrentCommand {get {return currentCommand;} set {currentCommand = value;}}
		public CommandChainType CurrentCommandChain {get {return currentCommandChain;} set {currentCommandChain = value;}}
		#endregion

		void Start ()
		{
			maxKeys = 7;
			maxDelay = 2.5f;
			currentDelay = maxDelay;
			lastCommandTime = 0;

			//commandList.Clear();

			CurrentCommandChain = CommandChainType.NONE;
			fullCommandChainString = "";
			currentCommandChainString = "";

			inacceptablesAttackCommands.Add (CommandType.NONE);
			inacceptablesAttackCommands.Add (CommandType.KEY_UP_FORWARD);
			inacceptablesAttackCommands.Add (CommandType.KEY_BACKWARD_UP);
			inacceptablesAttackCommands.Add (CommandType.KEY_FORWARD_DOWN);
			inacceptablesAttackCommands.Add (CommandType.KEY_DOWN_BACKWARD);

			// Preenche todos o dicionario de command chain string
			foreach (CommandChainType command in Enum.GetValues(typeof(CommandChainType))) 
			{
				if (!commandStringsAlias.ContainsKey (command.ToCommandString ())) 
				{
					commandStringsAlias.Add (command.ToCommandString (), command);
				}
			}

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
			// Running Moviments
			fourKeyCommands.Add(new CharacterCommand(CommandChainType.RUN_UP, CommandType.KEY_UP, CommandType.NONE, CommandType.KEY_UP, CommandType.HOLD_KEY_UP));
			fourKeyCommands.Add(new CharacterCommand(CommandChainType.RUN_FORWARD, CommandType.KEY_FORWARD, CommandType.NONE, CommandType.KEY_FORWARD, CommandType.HOLD_KEY_FORWARD));
			fourKeyCommands.Add(new CharacterCommand(CommandChainType.RUN_DOWN, CommandType.KEY_DOWN, CommandType.NONE, CommandType.KEY_DOWN, CommandType.HOLD_KEY_DOWN));
			fourKeyCommands.Add(new CharacterCommand(CommandChainType.RUN_BACKWARD, CommandType.KEY_BACKWARD, CommandType.NONE, CommandType.KEY_BACKWARD, CommandType.HOLD_KEY_BACKWARD));		

			// Hadoukens Attacks 
			fourKeyCommands.Add(new CharacterCommand(CommandChainType.DOWN_FORWARD_ATTACK, CommandType.KEY_DOWN, CommandType.HOLD_KEY_FORWARD_DOWN, CommandType.KEY_FORWARD, CommandType.KEY_ATTACK));
			fourKeyCommands.Add(new CharacterCommand(CommandChainType.FORWARD_DOWN_ATTACK, CommandType.KEY_FORWARD, CommandType.HOLD_KEY_FORWARD_DOWN, CommandType.KEY_DOWN, CommandType.KEY_ATTACK));
			fourKeyCommands.Add(new CharacterCommand(CommandChainType.DOWN_BACKWARD_ATTACK, CommandType.KEY_DOWN, CommandType.HOLD_KEY_DOWN_BACKWARD, CommandType.KEY_BACKWARD, CommandType.KEY_ATTACK));
			fourKeyCommands.Add(new CharacterCommand(CommandChainType.BACKWARD_DOWN_ATTACK, CommandType.KEY_BACKWARD, CommandType.HOLD_KEY_DOWN_BACKWARD, CommandType.KEY_DOWN, CommandType.KEY_ATTACK));	

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
			if (currentDelay < 0)
			{
				ClearCommandList ();
			}
			else
			{
				currentDelay -= 0.1f;
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
			//Debug.Log ("Current Command " + currentCommand + " != command " + command);

			if (command != currentCommand) {
				//Debug.Log ("Current Command " + currentCommand + " != command " + command);
				//lastCommandTime = GameManager.GameTime;	

				if(!inacceptablesAttackCommands.Contains(command))
				{
					//Debug.Log ("Command " + command + " != NONE");

					if (currentCommandChainString.Length > 0)
					{
						currentCommandChainString += "|";

						currentCommandChainString += "" + command;
						string[] commands = currentCommandChainString.Split ('|');

						if (commands.Length > maxKeys) 
						{
							currentCommandChainString = commands [1] + "|" + commands [2] + "|" + commands [3] + "|" + commands [4] + "|" + commands [5] + "|" + commands [6] + "|" + command;
						}
					} 
					else 
					{
						currentCommandChainString += "" + command;
					}
				}

				if (fullCommandChainString.Length > 0)
				{
					fullCommandChainString += "|";

					fullCommandChainString += "" + command;
					string[] commands = fullCommandChainString.Split ('|');

					//if (commands.Length > (maxKeys * 2)) 
					//{
					//	int indexOf = fullCommandChainString.IndexOf ('|') + 1;
					//	int end = fullCommandChainString.Length - indexOf;

					//	fullCommandChainString = fullCommandChainString.Substring (indexOf, end); 
					//}
				} 
				else 
				{
					fullCommandChainString += "" + command;
				}

				currentDelay = maxDelay;	

				//commandList.Add (command);

				ApplySingleCommand (command);
				currentCommand = command;

				//Debug.Log ("currentCommandChainString " + currentCommandChainString);
				//Debug.Log ("fullCommandChainString " + fullCommandChainString);
				//Debug.Log ("Current Command " + currentCommand + " Lista de Comandos: " + commandList.Count);
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
			VerifyCommandChainString ();
			VerifyMovimentChain();
			ApplyCommand (currentCommand);

			/*
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

				ApplyCommand (currentCommand);
			}	
			*/
		}
		
		/**
		 * Verifica comandos de 5 teclas, apagando a lista de comandos assim que encontre algo.
		 * 
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
					ClearCommandList();
					//ApplyCommand (currentCommand);
					return;
				}	
			}
		}
		
		/**
		 * Verifica comandos de 4 teclas, apagando a lista de comandos assim que encontre algo.
		 * 
		private void Verify4KeyCommands(int index)
		{		
			foreach (CharacterCommand entrance in fourKeyCommands)
			{
				if (commandList[index] == entrance.command[0] && 
				    commandList[index+1] == entrance.command[1] && 
					commandList[index+2] == entrance.command[2] && 
					commandList[index+3] == entrance.command[3])
				{
					//Debug.Log ("Verified Command " + entrance.type);
					CurrentCommandChain = entrance.type;
					ClearCommandList();
					//ApplyCommand (currentCommand);
					return;
				}	
			}
		}
		
		/**
		 * Verifica comandos de 3 teclas, apagando a lista de comandos assim que encontre algo.
		 * 
		private void Verify3KeyCommands(int index)
		{
			foreach (CharacterCommand entrance in threeKeyCommands)
			{		
				if (commandList[index] == entrance.command[0] && 
				    commandList[index+1] == entrance.command[1] && 
					commandList[index+2] == entrance.command[2])
				{
					CurrentCommandChain = entrance.type;
					ClearCommandList();
					//ApplyCommand (currentCommand);
					return;
				}	
			}
		}
		
		/**
		 * Verifica comandos de 2 teclas, apagando a lista de comandos assim que encontre algo.
		 *
		private void Verify2KeyCommands(int index)
		{
			foreach (CharacterCommand entrance in twoKeyCommands)
			{		
				if (commandList[index] == entrance.command[0] && 
				    commandList[index+1] == entrance.command[1])
				{
					CurrentCommandChain = entrance.type;
					ClearCommandList();
					//ApplyCommand (currentCommand);
					return;
				}	
			}
		}
		
		/**
		 * Verifica comandos de ataque, apagando a lista de comandos assim que encontre-o.
		 * 
		private void VerifyAttack(int index)
		{
			if (commandList[index] == CommandType.KEY_ATTACK)
			{
				CurrentCommandChain = CommandChainType.NONE;
				CurrentCommand = CommandType.KEY_ATTACK;
				ClearCommandList();
				//ApplyCommand (currentCommand);
				return;
			}
		}*/ 

		private void VerifyCommandChainString()
		{
			// Inicia a verificação da sequencia ao executar um ataque
			if (currentCommandChainString.Contains ("KEY_ATTACK"))
			{
				VerifyAttackChain ();
			}
		}

		private void VerifyMovimentChain()
		{
			string[] commands = fullCommandChainString.Split ('|');

			// Percorre toda a sequencia buscando comandos de movimento com 4 teclas.
			for (int i = 0; i < commands.Length - 3; i++) 
			{
				string segment = commands [i] + "|" + commands [i + 1] + "|" + commands [i + 2] + "|" + commands [i + 3]; 
				VerifyChainSegment (segment);
			}
		}

		private void VerifyAttackChain()
		{
			if (currentCommandChainString.Length > 0) 
			{
				string[] commands = currentCommandChainString.Split ('|');
				string analiseString = "";

				// Constroi a String a ser verificada
				for (int i = commands.Length; i > 0; i--) 
				{
					if (i == commands.Length)
					{
						analiseString += commands [i - 1];
					} 
					else
					{
						analiseString = commands [i - 1] + "|" + analiseString;
					}
				}

				// Verifica se existe algum comando cadastrado com essa sequencia analisada
				if (commandStringsAlias.ContainsKey (analiseString))
				{
					GameManager.ApplyCommand (commandStringsAlias [analiseString]);
					ClearCommandList ();
				}
				else
				{
					// Caso não exista nenhum comando cadastrado, re-analisa a sequencia com -1 comando
					int indexOf = currentCommandChainString.IndexOf ('|') + 1;
					int end = currentCommandChainString.Length - indexOf;

					currentCommandChainString = currentCommandChainString.Substring (indexOf, end); 
					VerifyAttackChain ();
				}
			}
		}

		private void VerifyChainSegment(string segment)
		{
			if (commandStringsAlias.ContainsKey (segment)) 
			{
				GameManager.ApplyCommand (commandStringsAlias [segment]);
				ClearCommandList ();
			}
		}

		/**
		 * Envia o resultado da analise da "Command Chain", para o objeto responsavel.
		 */	
		private void ApplyCommand(CommandType command)
		{
			/*
			if (CurrentCommandChain != CommandChainType.NONE && command != CommandType.NONE) 
			{
				//Debug.Log ("Current Command Chain " + CurrentCommandChain);

				CommandChainType analysingChain = CurrentCommandChain;
				CurrentCommandChain = CommandChainType.NONE;

				switch (analysingChain) 
				{												
					// Running Actions
					case CommandChainType.RUN_UP:GameManager.ApplyAction (Actions.ActionType.RUN_UP);return; 
					case CommandChainType.RUN_UP_FORWARD:GameManager.ApplyAction (Actions.ActionType.RUN_UP_FORWARD);return;
					case CommandChainType.RUN_FORWARD:GameManager.ApplyAction (Actions.ActionType.RUN_FORWARD);return;
					case CommandChainType.RUN_FORWARD_DOWN:GameManager.ApplyAction (Actions.ActionType.RUN_FORWARD_DOWN);return;
					case CommandChainType.RUN_DOWN:GameManager.ApplyAction (Actions.ActionType.RUN_DOWN);return;
					case CommandChainType.RUN_DOWN_BACKWARD:GameManager.ApplyAction (Actions.ActionType.RUN_DOWN_BACKWARD);return;
					case CommandChainType.RUN_BACKWARD:GameManager.ApplyAction (Actions.ActionType.RUN_BACKWARD);return;
					case CommandChainType.RUN_BACKWARD_UP:GameManager.ApplyAction (Actions.ActionType.RUN_BACKWARD_UP);return;	
									 
					// Directional Attacks
					case CommandChainType.UP_ATTACK:GameManager.ApplyAction (Actions.ActionType.DIRECTIONAL_ATTACK);return;	
					case CommandChainType.FORWARD_ATTACK:GameManager.ApplyAction (Actions.ActionType.DIRECTIONAL_ATTACK);return;	
					case CommandChainType.DOWN_ATTACK:GameManager.ApplyAction (Actions.ActionType.DIRECTIONAL_ATTACK);return;	
					case CommandChainType.BACKWARD_ATTACK:GameManager.ApplyAction (Actions.ActionType.DIRECTIONAL_ATTACK);return;
				}

				// Skills
				GameManager.ApplyCommand (CurrentCommandChain);
			} 
			else
			{
				if (CurrentCommand != command) 
				{					
					//Debug.Log ("Current Command " + command);
					switch (command)
					{
						case CommandType.NONE:GameManager.ApplyAction (Actions.ActionType.NONE);return;
						
						// Directional Keys
						case CommandType.HOLD_KEY_UP:GameManager.ApplyAction (Actions.ActionType.MOVE_UP);return;
						case CommandType.HOLD_KEY_UP_FORWARD:GameManager.ApplyAction (Actions.ActionType.MOVE_UP_FORWARD);return;
						case CommandType.HOLD_KEY_FORWARD:GameManager.ApplyAction (Actions.ActionType.MOVE_FORWARD);return;
						case CommandType.HOLD_KEY_FORWARD_DOWN:GameManager.ApplyAction (Actions.ActionType.MOVE_FORWARD_DOWN);return;
						case CommandType.HOLD_KEY_DOWN:GameManager.ApplyAction (Actions.ActionType.MOVE_DOWN);return;
						case CommandType.HOLD_KEY_DOWN_BACKWARD:GameManager.ApplyAction (Actions.ActionType.MOVE_DOWN_BACKWARD);return;
						case CommandType.HOLD_KEY_BACKWARD:GameManager.ApplyAction (Actions.ActionType.MOVE_BACKWARD);return;
						case CommandType.HOLD_KEY_BACKWARD_UP:GameManager.ApplyAction (Actions.ActionType.MOVE_BACKWARD_UP);return;	
											 
						// AtCommandType.tack keys
						case CommandType.KEY_ATTACK:GameManager.ApplyAction (Actions.ActionType.NORMAL_ATTACK);return;								
						case CommandType.HOLD_KEY_ATTACK:GameManager.ApplyAction (Actions.ActionType.FORCE_ATTACK);return;
					}
				}
			}*/
		}

		private void ApplySingleCommand(CommandType command)
		{			
			switch (command) 
			{
				case CommandType.NONE:
					GameManager.ApplyAction (Actions.ActionType.NONE);
					return;

				// Directional Keys
				case CommandType.HOLD_KEY_UP:
					GameManager.ApplyAction (Actions.ActionType.MOVE_UP);
					return;
				case CommandType.HOLD_KEY_UP_FORWARD:
					GameManager.ApplyAction (Actions.ActionType.MOVE_UP_FORWARD);
					return;
				case CommandType.HOLD_KEY_FORWARD:
					GameManager.ApplyAction (Actions.ActionType.MOVE_FORWARD);
					return;
				case CommandType.HOLD_KEY_FORWARD_DOWN:
					GameManager.ApplyAction (Actions.ActionType.MOVE_FORWARD_DOWN);
					return;
				case CommandType.HOLD_KEY_DOWN:
					GameManager.ApplyAction (Actions.ActionType.MOVE_DOWN);
					return;
				case CommandType.HOLD_KEY_DOWN_BACKWARD:
					GameManager.ApplyAction (Actions.ActionType.MOVE_DOWN_BACKWARD);
					return;
				case CommandType.HOLD_KEY_BACKWARD:
					GameManager.ApplyAction (Actions.ActionType.MOVE_BACKWARD);
					return;
				case CommandType.HOLD_KEY_BACKWARD_UP:
					GameManager.ApplyAction (Actions.ActionType.MOVE_BACKWARD_UP);
					return;	

				// AtCommandType.tack keys
				case CommandType.KEY_ATTACK:
					GameManager.ApplyAction (Actions.ActionType.NORMAL_ATTACK);
					return;								
				case CommandType.HOLD_KEY_ATTACK:
					GameManager.ApplyAction (Actions.ActionType.FORCE_ATTACK);
					return;
			}
		}

		private void ClearCommandList()
		{
			fullCommandChainString = "";
			currentCommandChainString = "";
			currentDelay = maxDelay;

			/*
			if (commandList.Count > 0) 
			{
				commandList.Clear ();
				currentDelay = maxDelay;
				Debug.Log ("Clear: " + commandList.Count);
			}
			*/
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

		NORMAL_ATTACK,	
		FORCE_ATTACK,
		DIRECTIONAL_ATTACK,
		RUNNING_ATTACK,

		MOVE_UP,
		MOVE_UP_FORWARD,
		MOVE_FORWARD,
		MOVE_FORWARD_DOWN,
		MOVE_DOWN,
		MOVE_DOWN_BACKWARD,
		MOVE_BACKWARD,
		MOVE_BACKWARD_UP,

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

	public static class Command
	{
		public static string ToCommandString(this CommandChainType me)
		{
		    switch(me)
		    {
				case CommandChainType.NONE: return "NONE";

				case CommandChainType.NORMAL_ATTACK: return "KEY_ATTACK";

				case CommandChainType.MOVE_BACKWARD: return "HOLD_KEY_BACKWARD";
				case CommandChainType.MOVE_DOWN_BACKWARD: return "HOLD_KEY_DOWN_BACKWARD";
				case CommandChainType.MOVE_DOWN: return "HOLD_KEY_DOWN";
				case CommandChainType.MOVE_FORWARD_DOWN: return "HOLD_KEY_FORWARD_DOWN";
				case CommandChainType.MOVE_FORWARD: return "HOLD_KEY_FORWARD";
				case CommandChainType.MOVE_UP_FORWARD: return "HOLD_KEY_UP_FORWARD";
				case CommandChainType.MOVE_UP: return "HOLD_KEY_UP";
				case CommandChainType.MOVE_BACKWARD_UP: return "HOLD_KEY_BACKWARD_UP";

				case CommandChainType.RUN_BACKWARD: return "KEY_BACKWARD|NONE|KEY_BACKWARD|HOLD_KEY_BACKWARD";
				case CommandChainType.RUN_DOWN_BACKWARD: return "KEY_DOWN_BACKWARD|NONE|KEY_DOWN_BACKWARD|HOLD_KEY_DOWN_BACKWARD";
				case CommandChainType.RUN_DOWN: return "KEY_DOWN|NONE|KEY_DOWN|HOLD_KEY_DOWN";
				case CommandChainType.RUN_FORWARD_DOWN: return "KEY_FORWARD_DOWN|NONE|KEY_FORWARD_DOWN|HOLD_KEY_FORWARD_DOWN";
				case CommandChainType.RUN_FORWARD: return "KEY_FORWARD|NONE|KEY_FORWARD|HOLD_KEY_FORWARD";
				case CommandChainType.RUN_UP_FORWARD: return "KEY_UP_FORWARD|NONE|KEY_UP_FORWARD|HOLD_KEY_UP_FORWARD";
				case CommandChainType.RUN_UP: return "KEY_UP|NONE|KEY_UP|HOLD_KEY_UP";
				case CommandChainType.RUN_BACKWARD_UP: return "KEY_BACKWARD_UP|NONE|KEY_BACKWARD_UP|HOLD_KEY_BACKWARD_UP";

				case CommandChainType.DOWN_FORWARD_ATTACK: return "KEY_DOWN|KEY_FORWARD|KEY_ATTACK";
				case CommandChainType.DOWN_BACKWARD_ATTACK: return "KEY_DOWN|KEY_BACKWARD|KEY_ATTACK";

				case CommandChainType.FORWARD_DOWN_FORWARD_ATTACK: return "KEY_FORWARD|KEY_DOWN|KEY_FORWARD|KEY_ATTACK";
				case CommandChainType.BACK_DOWN_BACKWARD_ATTACK: return "KEY_BACKWARD|KEY_DOWN|KEY_BACKWARD|KEY_ATTACK";

				case CommandChainType.FORWARD_DOWN_BACK_ATTACK: return "KEY_FORWARD|KEY_DOWN|KEY_BACKWARD|KEY_ATTACK";
				case CommandChainType.BACK_DOWN_FORWARD_ATTACK: return "KEY_BACKWARD|KEY_DOWN|KEY_FORWARD|KEY_ATTACK";
				
				case CommandChainType.DOWN_FORWARD_DOWN_FORWARD_ATTACK: return "KEY_DOWN|KEY_FORWARD|KEY_DOWN|KEY_FORWARD|KEY_ATTACK";
				case CommandChainType.DOWN_BACK_DOWN_BACKWARD_ATTACK: return "KEY_DOWN|KEY_BACKWARD|KEY_DOWN|KEY_BACKWARD|KEY_ATTACK";
					
				default:
					return "";
		    }
		}
	}
}