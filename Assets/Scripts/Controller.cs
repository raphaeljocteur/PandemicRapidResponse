using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Ethan
{

    public class Controller : MonoBehaviour
    {
        
        #region Variables

        [SerializeField] protected Pawn m_startingPawn;
        protected Pawn m_pawn;

        #endregion

        protected virtual void Start()
        {
            Possess(m_startingPawn);
        }

        public void Possess(Pawn _pawn)
        {
            m_pawn = _pawn;
            m_pawn.SetController(this);
        }

        public Pawn GetPawn()
        {
            return m_pawn;
        }

        public void Unpossess()
        {
            m_pawn = null;
        }

    }

}
