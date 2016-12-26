using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/**
 * Classe auxiliar para construção de comandos com numero de teclas variaveis.
 */ 
namespace Commands
{	
	public class CharacterCommand
	{
		public CommandChainType type;
		public List<CommandType> command = new List<CommandType>();
		
		public CharacterCommand(CommandChainType type, CommandType command1, CommandType command2)
		{
			this.type = type;	
			command.Add(command1);
			command.Add(command2);
		}
				
		public CharacterCommand(CommandChainType type, CommandType command1, CommandType command2, CommandType command3)
		{
			this.type = type;	
			command.Add(command1);
			command.Add(command2);
			command.Add(command3);
		}

		public CharacterCommand(CommandChainType type, CommandType command1, CommandType command2, CommandType command3, CommandType command4)
		{
			this.type = type;	
			command.Add(command1);
			command.Add(command2);
			command.Add(command3);
			command.Add(command4);
		}
				
		public CharacterCommand(CommandChainType type, CommandType command1, CommandType command2, CommandType command3, CommandType command4, CommandType command5)
		{
			this.type = type;	
			command.Add(command1);
			command.Add(command2);
			command.Add(command3);
			command.Add(command4);
			command.Add(command5);
		}
	}
}