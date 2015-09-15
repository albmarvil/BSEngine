using UnityEngine;
using System.Collections.Generic;
using BSEngine;
using BSEngine.Input;

public class InputTest : MonoBehaviour {

    public GameObject prefab = null;

    private List<GameObject> created = new List<GameObject>();


    private bool registered = false;

	// Use this for initialization
	void Start () {

        registered = false;

	}
	
	// Update is called once per frame
	void Update () {

        if (!registered)
        {
            InputMgr.Singleton.RegisterOrderListener("menu", "EXIT", onExitReceived);
            InputMgr.Singleton.RegisterOrderListener("menu", "TO_GAME", onToGameReceived);
            InputMgr.Singleton.RegisterOrderListener("game", "TO_MENU", onToMenuReceived);
            InputMgr.Singleton.RegisterOrderListener("game", "NEXT_LEVEL", onNextLevelReceived);
            InputMgr.Singleton.RegisterOrderListener("game", "UNLOAD_LEVEL", onUnloadLevelReceived);
            InputMgr.Singleton.RegisterOrderListener("game", "MOVE_UP", onMoveUpReceived);
            registered = true;
        }
	}

    void OnDisable()
    {
        if (registered)
        {
            InputMgr.Singleton.UnregisterOrderListener("menu", "EXIT", onExitReceived);
            InputMgr.Singleton.UnregisterOrderListener("menu", "TO_GAME", onToGameReceived);
            InputMgr.Singleton.UnregisterOrderListener("game", "TO_MENU", onToMenuReceived);
            InputMgr.Singleton.UnregisterOrderListener("game", "NEXT_LEVEL", onNextLevelReceived);
            InputMgr.Singleton.UnregisterOrderListener("game", "UNLOAD_LEVEL", onUnloadLevelReceived);
            InputMgr.Singleton.UnregisterOrderListener("game", "MOVE_UP", onMoveUpReceived);
            registered = false;
        }
    }

    public void onExitReceived(InputEvent e)
    {
        if (e.isOk)
        {
            Debug.Log("MENU: Exit received");
            Application.Quit();
        }
    }

    public void onToGameReceived(InputEvent e)
    {
        if (e.isOk)
        {
            Debug.Log("MENU: To game received");
            GameMgr.Singleton.ChangeState("game");
        }
    }

    public void onToMenuReceived(InputEvent e)
    {
        if (e.isOk)
        {
            Debug.Log("GAME: to menu received");
            //GameMgr.Singleton.ChangeState("menu");

            PoolMgr.Singleton.Destroy(created);
            created.Clear();
        }
    }


    public void onNextLevelReceived(InputEvent e)
    {
        if (e.isOk)
        {
            Debug.Log("GAME: to next level received");
            SceneMgr.Singleton.LoadSubSceneAdditive("level_02");
        }
    }

    public void onUnloadLevelReceived(InputEvent e)
    {
        if (e.isOk)
        {
            Debug.Log("GAME: Unload received");
            SceneMgr.Singleton.UnloadSubScene("level_02");
        }
    }

    public void onMoveUpReceived(InputEvent e)
    {
        if (e.isOk)
        {
            Debug.Log("GAME: UPreceived");
            created.Add(PoolMgr.Singleton.Instatiate(prefab, gameObject.transform));
        }
    }
    
}
