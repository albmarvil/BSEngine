///----------------------------------------------------------------------
/// @file MenuState.cs
///
/// This file contains the declaration of MenuState class.
///
/// @author Alberto Martinez Villaran <tukaram92@gmail.com>
/// @date 17/9/2015
///----------------------------------------------------------------------

using UnityEngine;
using System.Collections.Generic;
using BSEngine;
using BSEngine.Input;


public class MenuState : State
{
    /// <summary>
    /// State constructor. Should call the base class
    /// </summary>
    public MenuState()
        : base("Menu", createInputSet(), "Menu_Scene")
    {
        Debug.Log("Menu State created");
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
        orders.Add("START_GAME");
        keyBindings.Add(BSKeyCode.Return, orders);

        orders = new List<string>();
        orders.Add("EXIT");
        keyBindings.Add(BSKeyCode.Escape, orders);

        return new InputSet("MenuStateInputSet", keyBindings);
    }

    /// <summary>
    /// Called on Init step. Used for specific state code
    /// </summary>
    /// <returns>True if everything went ok</returns>
    protected override bool open()
    {
        Debug.Log("Menu state open");
        return true;
    }

    /// <summary>
    /// Called on Release step. Used for specific state code
    /// </summary>
    protected override void close()
    {
        Debug.Log("Menu state close");
    }

    /// <summary>
    /// Called on Activate step. Used for specific state code
    /// </summary>
    /// <returns>True if everything went ok</returns>
    protected override bool onActivate()
    {
        Debug.Log("Menu state activate");
        return true;
    }

    /// <summary>
    /// Called on Resume step. Used for specific state code
    /// </summary>
    /// <returns>True if everything went ok</returns>
    protected override bool onResume()
    {
        Debug.Log("Menu state resume");
        return true;
    }

    /// <summary>
    /// Called on Deactivate step. Used for specific state code
    /// </summary>
    protected override void onDeactivate()
    {
        Debug.Log("Menu state deactivate");
    }

    /// <summary>
    /// Called on Pause step. Used for specific state code
    /// </summary>
    protected override void onPause()
    {
        Debug.Log("Menu state pause");
    }
}
