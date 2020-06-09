using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ethan
{
	public class Pawn : MonoBehaviour
	{

		private Controller m_controller;

		public void SetController(Controller _controller)
		{
			m_controller = _controller;
		}

		public Controller GetController()
		{
			return m_controller;
		}

	}
}