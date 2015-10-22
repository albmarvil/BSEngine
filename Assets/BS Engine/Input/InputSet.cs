///----------------------------------------------------------------------
/// @file InputSet.cs
///
/// This file contains the declaration of InputSet class.
/// 
/// An InputSet on Bird Soul Engine represents a set of keys related to logic orders.
/// 
/// One key can be binded to multiple logic orders.
/// 
/// InputSets are created by Dictionaries containing the bindings, it can also be created by files (XML) ***work in progress***
/// 
/// InputSets are related to a context defined by the Application State, so you can have multiple key bindings depending on the App context. 
///
/// @author Alberto Martinez Villaran <tukaram92@gmail.com>
/// @date 11/09/2015
///----------------------------------------------------------------------


using System.Collections.Generic;

namespace BSEngine
{
    namespace Input
    {
        public class InputSet
        {

            #region Private params

            /// <summary>
            /// Key bindings storage. One Key ---> Multiple logic orders
            /// </summary>
            protected Dictionary<BSKeyCode, List<string>> m_key2order = new Dictionary<BSKeyCode, List<string>>();

            /// <summary>
            /// Key listeners storege, indexed by logic orders
            /// </summary>
            private Dictionary<string, onOrderReceived> m_orderReceived = new Dictionary<string,onOrderReceived>();

            /// <summary>
            /// InputSet name. Usually the same as the context + "InputSet"
            /// </summary>
            private string m_name;


            private onMouseMoved m_mouseMoved;

            private MouseCfg m_mouseCfg;

            #endregion

            #region Public methods

            /// <summary>
            /// Temporary construction of the key bindings of the orders
            /// </summary>
            public InputSet(string name, Dictionary<BSKeyCode, List<string>> keyBindings, MouseCfg mouseCfg)
            {

                m_key2order = keyBindings;
                m_name = name;

                m_mouseMoved = null;

                m_mouseCfg = mouseCfg;
            }

            /// <summary>
            /// Public access to the InputSet's name
            /// </summary>
            public string Name
            {
                get { return m_name; }
            }

            /// <summary>
            /// Public access to the InputSet's key bindings
            /// </summary>
            public Dictionary<BSKeyCode, List<string>> KeyBindings
            {
                get { return m_key2order; }
            }

            /// <summary>
            /// Public access to the InputSet's listeners
            /// </summary>
            public Dictionary<string, onOrderReceived> OrdersListeners
            {
                get { return m_orderReceived; }
            }


            /// <summary>
            /// Method used to register a listener to events of a specific order
            /// </summary>
            /// <param name="order">specific order listening events to</param>
            /// <param name="listener">delegate function to call</param>
            public void RegisterOnOrderReceived(string order, onOrderReceived listener)
            {
                if(m_orderReceived.ContainsKey(order))
                {
                    m_orderReceived[order] += listener;
                }
                else
                {
                    onOrderReceived orderReceived = null;
                    orderReceived += listener;
                    m_orderReceived.Add(order, orderReceived);
                }
            }

            /// <summary>
            /// Method used to unregister a listener to events of a specific order
            /// </summary>
            /// <param name="order">specific order listening events to</param>
            /// <param name="listener">delegate function to call</param>
            public void UnregisterOnOrderReceived(string order, onOrderReceived listener)
            {
                if (m_orderReceived.ContainsKey(order))
                {
                    m_orderReceived[order] -= listener;
                }
            }

            /// <summary>
            /// Method used to release all the listeners
            /// </summary>
            public void ReleaseAllOrderListeners()
            {
                foreach (string key in m_orderReceived.Keys)
                {
                    m_orderReceived[key] = null;
                }

                m_orderReceived.Clear();
            }



            public void RegisterOnMouseMoved(onMouseMoved listener)
            {

                m_mouseMoved += listener;
               
            }


            public void UnregisterOnMouseMoved(onMouseMoved listener)
            {
                m_mouseMoved -= listener;
            }


            public void ReleaseAllMouseListeners()
            {
                m_mouseMoved = null;
            }

            public MouseCfg MouseCfg
            {
                get { return m_mouseCfg; }
            }

            public bool MouseSupported
            {
                get { return m_mouseCfg != null; }
            }


            public onMouseMoved MouseListeners
            {
                get { return m_mouseMoved; }
            }
            #endregion
        }
    }
}

