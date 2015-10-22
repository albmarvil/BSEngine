using UnityEngine;
using System.Collections;
using BSEngine;

namespace BSEngine.Input
{


    public class MouseCfg
    {

        #region Private params

        private bool m_invertedX;

        private bool m_invertedY;

        private float m_sensivity;

        #endregion


        #region Public methods

        public MouseCfg(bool invertX, bool invertY, float sensivity)
        {
            m_invertedX = invertX;
            m_invertedY = invertY;
            m_sensivity = sensivity;
        }


        public bool InvertedXAxis
        {
            get { return m_invertedX; }
        }

        public bool InvertedYAxis
        {
            get { return m_invertedY; }
        }

        public float Sensivity
        {
            get { return m_sensivity; }
        }


        #endregion
    }
}