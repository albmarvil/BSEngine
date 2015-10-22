using UnityEngine;
using System.Collections;
using BSEngine;



namespace BSEngine.Input
{

    public class MouseState
    {

        #region Private params

        private Vector3 m_Abs;

        private Vector2 m_deltaScroll;

        #endregion

        #region Public methods

        public void Update()
        {
            ///TODO, update the data ccording to the actual cfg
            ///CurrentState.InputSet.MouseCfg.......


            m_Abs = UnityEngine.Input.mousePosition;
            m_deltaScroll = UnityEngine.Input.mouseScrollDelta;
        }

        public Vector3 AbsolutePosition
        {
            get { return m_Abs; }
        }

        public Vector3 RelativePosition
        {
            get
            {
                return new Vector3(m_Abs.x / UnityEngine.Screen.width, m_Abs.y / UnityEngine.Screen.height, 0.0f);
            }
        }

        public Vector2 DeltaScroll
        {
            get { return m_deltaScroll; }
        }

        #endregion
    }
}