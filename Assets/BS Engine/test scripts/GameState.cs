
using UnityEngine;
using BSEngine;
using BSEngine.Input;

using System.Collections.Generic;

public class GameState : State
{
    /// <summary>
    /// State constructor. Should call the base class
    /// </summary>
    public GameState()
        : base("game", createInputSet() , "scene")
    {
        Debug.Log("game State created");
    }

    /// <summary>
    /// Static method used to create the InputSet
    /// </summary>
    /// <returns></returns>
    private static InputSet createInputSet()
    {
        ///TO DO: Input set creation
        Dictionary<BSKeyCode, List<string>> keyBindings = new Dictionary<BSKeyCode, List<string>>();

        List<string> orders = new List<string>();
        orders.Add("MOVE_UP");
        keyBindings.Add(BSKeyCode.W, orders);
        keyBindings.Add(BSKeyCode.UpArrow, orders);

        orders = new List<string>();
        orders.Add("MOVE_DOWN");
        keyBindings.Add(BSKeyCode.S, orders);
        keyBindings.Add(BSKeyCode.DownArrow, orders);


        orders = new List<string>();
        orders.Add("MOVE_LEFT");
        keyBindings.Add(BSKeyCode.A, orders);
        keyBindings.Add(BSKeyCode.LeftArrow, orders);

        orders = new List<string>();
        orders.Add("MOVE_RIGHT");
        keyBindings.Add(BSKeyCode.D, orders);
        keyBindings.Add(BSKeyCode.RightArrow, orders);
        

        return new InputSet("GameInputSet", keyBindings);
    }

    /// <summary>
    /// Called on Init step. Used for specific state code
    /// </summary>
    /// <returns>True if everything went ok</returns>
    protected override bool open()
    {
        Debug.Log("game state open");
        return true;
    }

    /// <summary>
    /// Called on Release step. Used for specific state code
    /// </summary>
    protected override void close()
    {
        Debug.Log("game state close");
    }

    /// <summary>
    /// Called on Activate step. Used for specific state code
    /// </summary>
    /// <returns>True if everything went ok</returns>
    protected override bool onActivate()
    {
        Debug.Log("game state activate");
        return true;
    }

    /// <summary>
    /// Called on Resume step. Used for specific state code
    /// </summary>
    /// <returns>True if everything went ok</returns>
    protected override bool onResume()
    {
        Debug.Log("game state resume");
        return true;
    }

    /// <summary>
    /// Called on Deactivate step. Used for specific state code
    /// </summary>
    protected override void onDeactivate()
    {
        Debug.Log("game state deactivate");
    }

    /// <summary>
    /// Called on Pause step. Used for specific state code
    /// </summary>
    protected override void onPause()
    {
        Debug.Log("game state pause");
    }
}
