using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using Commands;

namespace Inputs
{
	public class InputController 
	{
		#region Atributos Internos
		private bool rightPressed;
		private bool leftPressed;
		private bool upPressed;
		private bool downPressed;
		
		private bool IsMovimentInput { get {return (upPressed || rightPressed || downPressed || leftPressed);}}
		#endregion
		
		#region Start
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
			
			if (Input.GetKeyDown(KeyCode.RightArrow)) rightPressed = true;
			if (Input.GetKeyDown(KeyCode.LeftArrow)) leftPressed = true;
			if (Input.GetKeyDown(KeyCode.UpArrow)) upPressed = true;
			if (Input.GetKeyDown(KeyCode.DownArrow)) downPressed = true;	
			
			if (leftPressed)
			{
				if (upPressed) {CommandManager.AddCommand(CommandType.KEY_BACKWARD_UP);}			
				if (downPressed) {CommandManager.AddCommand(CommandType.KEY_DOWN_BACKWARD);}			
				if (!upPressed && !downPressed)	{CommandManager.AddCommand(CommandType.KEY_BACKWARD);}	
			}
			
			if (rightPressed)
			{
				if (upPressed) {CommandManager.AddCommand(CommandType.KEY_UP_FORWARD);}			
				if (downPressed) {CommandManager.AddCommand(CommandType.KEY_FORWARD_DOWN);}			
				if (!upPressed && !downPressed)	{CommandManager.AddCommand(CommandType.KEY_FORWARD);}	
			}
			
			if (!rightPressed && !leftPressed)
			{
				if (upPressed){CommandManager.AddCommand(CommandType.KEY_UP);}
				if (downPressed){CommandManager.AddCommand(CommandType.KEY_DOWN);}
			}
			
			if (!Input.anyKey)
			{
				leftPressed = false;
				upPressed = false;
				rightPressed = false;
				downPressed = false;
				
				CommandManager.AddCommand(CommandType.NONE);
			}
		}
		
		
		/**
		 * Verifica e cadastra os inputs de teclado nos CommandManager.
		 */ 
		private void VerifyCommands()
		{
			//if (Input.anyKey && !IsMovimentInput)
			//{			
			//	CommandManager.AddCommand(Input.Key);		
			//}
		}
		#endregion
	}	
}