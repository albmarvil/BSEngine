
using UnityEngine;
using BSEngine;
using BSEngine.Input;

using System.Collections.Generic;

public class MenuState : State
{
    /// <summary>
    /// State constructor. Should call the base class
    /// </summary>
    public MenuState()
        : base("menu", createInputSet(), "menu_scene")
    {
        Debug.Log("menu State created");
    }


    private static InputSet createInputSet()
    {
        ///TO DO: Input set creation
        Dictionary<BSKeyCode, List<string>> keyBindings = new Dictionary<BSKeyCode, List<string>>();

        List<string> orders = new List<string>();
        orders.Add("MENU_UP");
        keyBindings.Add(BSKeyCode.UpArrow, orders);

        orders = new List<string>();
        orders.Add("MENU_DOWN");
        keyBindings.Add(BSKeyCode.DownArrow, orders);


        orders = new List<string>();
        orders.Add("MENU_LEFT");
        keyBindings.Add(BSKeyCode.LeftArrow, orders);

        orders = new List<string>();
        orders.Add("MENU_RIGHT");
        keyBindings.Add(BSKeyCode.RightArrow, orders);


        orders = new List<string>();
        orders.Add("TO_GAME");
        keyBindings.Add(BSKeyCode.Return, orders);

        orders = new List<string>();
        orders.Add("EXIT");
        keyBindings.Add(BSKeyCode.Escape, orders);


        return new InputSet("MenuInputSet", keyBindings);
    }

    /// <summary>
    /// Called on Init step. Used for specific state code
    /// </summary>
    /// <returns>True if everything went ok</returns>
    protected override bool open()
    {
        Debug.Log("menu state open");
        return true;
    }

    /// <summary>
    /// Called on Release step. Used for specific state code
    /// </summary>
    protected override void close()
    {
        Debug.Log("menu state close");
    }

    /// <summary>
    /// Called on Activate step. Used for specific state code
    /// </summary>
    /// <returns>True if everything went ok</returns>
    protected override bool onActivate()
    {
        Debug.Log("menu state activate");
        return true;
    }

    /// <summary>
    /// Called on Resume step. Used for specific state code
    /// </summary>
    /// <returns>True if everything went ok</returns>
    protected override bool onResume()
    {
        Debug.Log("menu state resume");
        return true;
    }

    /// <summary>
    /// Called on Deactivate step. Used for specific state code
    /// </summary>
    protected override void onDeactivate()
    {
        Debug.Log("menu state deactivate");
    }

    /// <summary>
    /// Called on Pause step. Used for specific state code
    /// </summary>
    protected override void onPause()
    {
        Debug.Log("menu state pause");
    }
}
