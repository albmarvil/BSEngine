using UnityEngine;
using System.Collections;
using BSEngine.Input;

public class InputTest : MonoBehaviour {

    private bool registered = false;

	// Use this for initialization
	void Start () {

        registered = false;

	}
	
	// Update is called once per frame
	void Update () {

        if (!registered)
        {
            InputMgr.Singleton.RegisterOrderListener("menu", "MENU_UP", onOrderReceived);
            InputMgr.Singleton.RegisterOrderListener("menu", "MENU_DOWN", onOrderReceived2);
            registered = true;
        }
	}

    void OnDisable()
    {
        if (registered)
        {
            InputMgr.Singleton.UnregisterOrderListener("menu", "MENU_UP", onOrderReceived);
            InputMgr.Singleton.UnregisterOrderListener("menu", "MENU_DOWN", onOrderReceived2);
            registered = false;
        }
    }


    public void onOrderReceived(InputEvent e)
    {
        Debug.Log("Order from Input: " + e.Order + " - " + e.isOk);
    }

    public void onOrderReceived2(InputEvent e)
    {
        Debug.Log("Order from Input2: " + e.Order + " - " + e.isOk);
    }
}
