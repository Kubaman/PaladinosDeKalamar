using UnityEngine;
using System.Collections;

namespace Commands
{
	public class CommandManager {

		private static CommandController _controller;

		public static CommandController Controller
		{
			get
			{
				if (_controller == null)
					_controller = GameObject.Find("CommandController").GetComponent<CommandController>();

				return _controller;
			}
		}

		public static void AddCommand(CommandType command){ Controller.AddCommand (command); }
	}
}