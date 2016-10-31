///----------------------------------------------------------------------
/// @file GameState.cs
///
/// This file contains the declaration of GameState class.
///
/// @author Alberto Martinez Villaran <tukaram92@gmail.com>
/// @date 17/9/2015
///----------------------------------------------------------------------


using UnityEngine;
using System.Collections.Generic;
using BSEngine;
using BSEngine.Input;


public class GameState : State
{
    /// <summary>
    /// State constructor. Should call the base class
    /// </summary>
    public GameState()
        : base("Game", "Game_Scene")
    {
        Debug.Log("Game State created");
    }

    /// <summary>
    /// method used to create the InputSet from the Config file loaded
    /// </summary>
    private InputSet createInputSet()
    {

        if (StorageMgr.Blackboard.ContainsKey("CFG"))
        {
            DataTable cfg = StorageMgr.Blackboard.Get<DataTable>("CFG");

            if (cfg.ContainsKey("GameStateInputSet"))
            {
                DataTable inputSetData = cfg.Get<DataTable>("GameStateInputSet");

                return new InputSet(inputSetData);
            }
            else
            {
                InputSet res = createDefaultInputSet();
                DataTable inputSetData = res.ToDataTable();
                StorageMgr.Blackboard.Get<DataTable>("CFG").Set<DataTable>("GameStateInputSet", inputSetData);
                return res;
            }
        }
        else
        {
            return createDefaultInputSet();
        }
    }

    /// <summary>
    /// Method to create the default Input Set in case there is no CFG file loaded
    /// </summary>
    private InputSet createDefaultInputSet()
    {
		
        Dictionary<BSKeyCode, List<string>> keyBindings = new Dictionary<BSKeyCode, List<string>>();

        List<string> orders = new List<string>();
        orders.Add("MOVE_UP");
        keyBindings.Add(BSKeyCode.UpArrow, orders);

        orders = new List<string>();
        orders.Add("MOVE_DOWN");
        keyBindings.Add(BSKeyCode.DownArrow, orders);

        orders = new List<string>();
        orders.Add("MOVE_LEFT");
        keyBindings.Add(BSKeyCode.LeftArrow, orders);

        orders = new List<string>();
        orders.Add("MOVE_RIGHT");
        keyBindings.Add(BSKeyCode.RightArrow, orders);

        orders = new List<string>();
        orders.Add("PAUSE");
        keyBindings.Add(BSKeyCode.Pause, orders);
        keyBindings.Add(BSKeyCode.P, orders);

        orders = new List<string>();
        orders.Add("EXIT_GAME");
        keyBindings.Add(BSKeyCode.Escape, orders);

        //TO DO MouseCfg config

        MouseCfg cfg = new MouseCfg(false, false, 10.0f);


        return new InputSet("GameStateInputSet", keyBindings, cfg);
    }

    /// <summary>
    /// Called on Init step. Used for specific state code
    /// </summary>
    /// <returns>True if everything went ok</returns>
    protected override bool open()
    {
        //Debug.Log("Game state open");
        m_inputSet = createInputSet();
        return true;
    }

    /// <summary>
    /// Called on Release step. Used for specific state code
    /// </summary>
    protected override void close()
    {
        //Debug.Log("Game state close");
    }

    /// <summary>
    /// Called on Activate step. Used for specific state code
    /// </summary>
    /// <returns>True if everything went ok</returns>
    protected override bool onActivate()
    {
        //Debug.Log("Game state activate");
        return true;
    }

    /// <summary>
    /// Called on Resume step. Used for specific state code
    /// </summary>
    /// <returns>True if everything went ok</returns>
    protected override bool onResume()
    {
        //Debug.Log("Game state resume");
        return true;
    }

    /// <summary>
    /// Called on Deactivate step. Used for specific state code
    /// </summary>
    protected override void onDeactivate()
    {
        //Debug.Log("Game state deactivate");
    }

    /// <summary>
    /// Called on Pause step. Used for specific state code
    /// </summary>
    protected override void onPause()
    {
        //Debug.Log("Game state pause");
    }
}
