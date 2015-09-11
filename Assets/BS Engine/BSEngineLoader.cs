using UnityEngine;
using System.Collections.Generic;

namespace BSEngine
{
    public class BSEngineLoader : MonoBehaviour
    {

        #region Public params



        #endregion

        #region Private params

        /// <summary>
        /// States to be loaded into the engine
        /// </summary>
        private Dictionary<string, State> m_initializedStates = new Dictionary<string,State>();

        #endregion


        #region Public Methods

        /// <summary>
        /// Porperty to acces to the states to load
        /// </summary>
        public Dictionary<string, State> States
        {
            get { return m_initializedStates; }
        }

        #endregion


        #region MonoBehavior Calls

        /// <summary>
        /// At the start, this script will load all the states into the engine and start al the engine functionality.
        /// 
        /// Also it will load the data needed for the PoolManager cache.
        /// </summary>
        private void Start()
        {
            ///GAME CODE
            ///
            ///Creation of the states. Example:
            ///
            ///State st = new GameState();
            ///m_initializedStates.Add(st.Name, st);
            State st = new GameState();
            m_initializedStates.Add(st.Name, st);

            st = new MenuState();
            m_initializedStates.Add(st.Name, st);

            ///ENGINE CODE
            GameMgr.Init(this);
        }


        private void OnDestroy()
        {
            ///ENGINE CODE
            GameMgr.Release();

            ///GAME CODE
        }

        private void Update()
        {
            ///GAME CODE
            

            ///ENGINE CODE
            GameMgr.Singleton.Update();
        }

        #endregion

    }
}

