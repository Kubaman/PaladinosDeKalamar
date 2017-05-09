using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using Commands;

namespace Inputs
{
	public class InputController : MonoBehaviour
	{
		#region Atributos Internos
		private bool rightPressed;
		private bool leftPressed;
		private bool upPressed;
		private bool downPressed;

		private float holdTimer;
		private float holdTimerMax;
		private CommandType currentCommand;

		private CommandType lastKey;
		private CommandType currentKey;

		private CommandType currentHoldKey;
		
		private bool IsMovimentInput { get {return (upPressed || rightPressed || downPressed || leftPressed);}}
		#endregion
		
		#region MonoBehaviours
		/**
		 * Metodo padrão do "MonoBehaviour" do Unity3D, 
		 * ele é executado sempre que o GameObject que tenha esse script é Instanciado.
		 */
		void Start ()
		{
			rightPressed = false;
			leftPressed = false;	
			upPressed = false;	
			downPressed = false;

			holdTimerMax = 0.5f;
			holdTimer = holdTimerMax;

			currentKey = CommandType.NONE;
			currentCommand = CommandType.NONE;
			currentHoldKey = CommandType.NONE;
		}

		void Update()
		{
			VerifyMovimentInput ();
		}
		#endregion

		#region Metodos Internos
		/**
		 * Verifica e envia ao Hero os comandos de movimento detectados. 
		 * Enviando apenas os comandos "Novos", ou seja, deferentes do ultimo enviado.
		 * Mantendo assim uma movimentação constante ate que seja trocada a direção.
		 */ 
		private void VerifyMovimentInput()
		{	
			if (Input.GetKeyUp(KeyCode.RightArrow)) rightPressed = false;
			if (Input.GetKeyUp(KeyCode.LeftArrow)) leftPressed = false;
			if (Input.GetKeyUp(KeyCode.UpArrow)) upPressed = false;
			if (Input.GetKeyUp(KeyCode.DownArrow)) downPressed = false;
			
			if (Input.GetKeyDown (KeyCode.RightArrow)) rightPressed = true;
			if (Input.GetKeyDown(KeyCode.LeftArrow)) leftPressed = true;
			if (Input.GetKeyDown(KeyCode.UpArrow)) upPressed = true;
			if (Input.GetKeyDown(KeyCode.DownArrow)) downPressed = true;	

			if (Input.GetKeyUp (KeyCode.Keypad6))
			{
				rightPressed = false;
			}

			if (Input.GetKeyUp(KeyCode.Keypad4)) leftPressed = false;
			if (Input.GetKeyUp(KeyCode.Keypad8)) upPressed = false;
			if (Input.GetKeyUp(KeyCode.Keypad2)) downPressed = false;

			if (Input.GetKeyDown (KeyCode.Keypad6)) 
			{
				rightPressed = true;
			}
			if (Input.GetKeyDown(KeyCode.Keypad4)) leftPressed = true;
			if (Input.GetKeyDown(KeyCode.Keypad8)) upPressed = true;
			if (Input.GetKeyDown(KeyCode.Keypad2)) downPressed = true;	

			//Debug.Log ("ANY " + Input.anyKey + " up:" + upPressed + " down:" + downPressed + " left:" + leftPressed + " right:" + rightPressed);

			if (!Input.anyKey)
			{
				leftPressed = false;
				upPressed = false;
				rightPressed = false;
				downPressed = false;

				AddKey (CommandType.NONE);
				holdTimer = holdTimerMax;
			}
			else 
			{
				lastKey = currentKey;

				if (leftPressed)
				{
					if (upPressed) 
					{
						currentKey = CommandType.KEY_BACKWARD_UP;
					}

					if (downPressed)
					{
						currentKey = CommandType.KEY_DOWN_BACKWARD;
					}			

					if (!upPressed && !downPressed)
					{
						currentKey = CommandType.KEY_BACKWARD;
					}	
				}

				if (rightPressed)
				{
					if (upPressed) 
					{
						currentKey = CommandType.KEY_UP_FORWARD;
					}			

					if (downPressed) 
					{
						currentKey = CommandType.KEY_FORWARD_DOWN;
					}

					if (!upPressed && !downPressed)	
					{
						currentKey = CommandType.KEY_FORWARD;
					}
				}

				if (!rightPressed && !leftPressed)
				{
					if (upPressed)
					{
						currentKey = CommandType.KEY_UP;
					}

					if (downPressed)
					{
						currentKey = CommandType.KEY_DOWN;
					}
				}

				VerifyCommands ();
				VerifyHoldingKey ();
			}
		}		

		private void VerifyHoldingKey()
		{
			if (currentKey == lastKey) {
				holdTimer -= 0.1f;

				if (holdTimer < 0) {
					switch (currentKey) {
					case CommandType.KEY_UP:
						AddKey (CommandType.HOLD_KEY_UP);
						break;

					case CommandType.KEY_UP_FORWARD:
						AddKey (CommandType.HOLD_KEY_UP_FORWARD);
						break;

					case CommandType.KEY_FORWARD:
						AddKey (CommandType.HOLD_KEY_FORWARD);
						break;

					case CommandType.KEY_FORWARD_DOWN:
						AddKey (CommandType.HOLD_KEY_FORWARD_DOWN);
						break;

					case CommandType.KEY_DOWN:
						AddKey (CommandType.HOLD_KEY_DOWN);
						break;

					case CommandType.KEY_DOWN_BACKWARD:
						AddKey (CommandType.HOLD_KEY_DOWN_BACKWARD);
						break;

					case CommandType.KEY_BACKWARD:
						AddKey (CommandType.HOLD_KEY_BACKWARD);
						break;

					case CommandType.KEY_BACKWARD_UP:
						AddKey (CommandType.HOLD_KEY_BACKWARD_UP);
						break;						
					}
				}
			}
			else
			{
				holdTimer = holdTimerMax;
				AddKey (currentKey);
			}
		}

		private void AddKey(CommandType addedKey)
		{
			//Debug.Log ("Current Command: " + currentCommand + " addedCommand: " + addedCommand);
			if (currentCommand != addedKey) 
			{
				CommandManager.AddCommand (addedKey);
				currentKey = addedKey;
				currentCommand = addedKey;
			}
		}

		/**
		 * Verifica e cadastra os inputs de teclado nos CommandManager.
		 */ 
		private void VerifyCommands()
		{
			if (Input.GetKeyDown (KeyCode.Z))
			{
				currentKey = CommandType.KEY_ATTACK;
				AddKey (CommandType.KEY_ATTACK);		
			}
		}
		#endregion
	}	
}