using UnityEngine;
using System.Collections;
using BSEngine.Input;

public class NeonThrowTest : MonoBehaviour {


    #region Public params

    #endregion

    #region Private params

    private NeonMovementController m_controller = null;

    #endregion

    #region Public methods

    public void onMoveReceived(InputEvent e)
    {
        if (e.isOk)
        {
            
            Vector3 dir = Vector3.zero;
            if (e.Order == "MOVE_UP")
            {
                dir.y = 1.0f;
            }
            else if (e.Order == "MOVE_DOWN")
            {
                dir.y = -1.0f;
            }
            else if (e.Order == "MOVE_LEFT")
            {
                dir.x = -1.0f;
            }
            else if (e.Order == "MOVE_RIGHT")
            {
                dir.x = 1.0f;
            }

            m_controller.MovementDir = dir;
            m_controller.Speed = float.MaxValue;
        }
    }


    public void onMouseMoved(MouseState ms)
    {
        Debug.Log(ms.AbsolutePosition + " - " + ms.RelativePosition + " - " + ms.DeltaScroll);
    }

    #endregion

    #region Private methods

   

    #endregion

    #region Monobehavior calls

    private void OnEnable()
    {
        m_controller = gameObject.GetComponent<NeonMovementController>();

        if (InputMgr.Singleton != null)
        {
            InputMgr.Singleton.RegisterOrderListener("Game", "MOVE_UP", onMoveReceived);
            InputMgr.Singleton.RegisterOrderListener("Game", "MOVE_DOWN", onMoveReceived);
            InputMgr.Singleton.RegisterOrderListener("Game", "MOVE_LEFT", onMoveReceived);
            InputMgr.Singleton.RegisterOrderListener("Game", "MOVE_RIGHT", onMoveReceived);
        }

        //InputMgr.Singleton.RegisterMouseListener("Game", onMouseMoved);


    }

    private void OnDisable()
    {
        if (InputMgr.Singleton != null)
        {
            InputMgr.Singleton.UnregisterOrderListener("Game", "MOVE_UP", onMoveReceived);
            InputMgr.Singleton.UnregisterOrderListener("Game", "MOVE_DOWN", onMoveReceived);
            InputMgr.Singleton.UnregisterOrderListener("Game", "MOVE_LEFT", onMoveReceived);
            InputMgr.Singleton.UnregisterOrderListener("Game", "MOVE_RIGHT", onMoveReceived);
        }

        //InputMgr.Singleton.UnregisterMouseListener("Game", onMouseMoved);
    }

    #endregion

}
