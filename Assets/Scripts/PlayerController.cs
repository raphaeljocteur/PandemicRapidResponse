using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Ethan
{
    public class PlayerController : Controller
    {

        #region variables

        private Character m_character;
        private Vector3 m_move;
        private bool m_jump;
        private bool m_crouch;

        #endregion

        #region inputs

        public void OnFire()
        {
            m_character.Jump();
        }

        public void OnMove(InputValue value)
        {
            var input = value.Get<Vector2>();
            m_move = new Vector3(input.x, 0, input.y);
        }

        #endregion

        protected override void Start()
        {
            base.Start();
            m_character = m_pawn as Character;
        }

        protected virtual void FixedUpdate()
        {
            m_character.Move(m_move, m_crouch, m_jump);
        }

        protected virtual void Update()
        {

        }

    }

}