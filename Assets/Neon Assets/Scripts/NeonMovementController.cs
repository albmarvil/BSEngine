///----------------------------------------------------------------------
/// @file NeonMovementController.cs
///
/// This file contains the declaration of NeonMovementController class.
/// 
/// This MonoBehavior controls the neon movement, using a starting configuration.
/// 
/// -starting max speed
/// -Starting acceleration
/// -movement threshold to stop
/// 
/// Thos params can be modified during execution. To sum up, this component uses 4 main parameters
/// 
/// -Current movement speed
/// -Current Max movement speed
/// -Current Acceleration
/// -Current Movement direction
///
///
/// @author Alberto Martinez Villaran <tukaram92@gmail.com>
/// @date 29/9/2015
///----------------------------------------------------------------------


using UnityEngine;
using System.Collections;

public class NeonMovementController : MonoBehaviour {


    #region Public params

    /// <summary>
    /// Starting Max speed
    /// </summary>
    public float m_baseMaxSpeed = 1.0f;

    /// <summary>
    /// Starting acceleration
    /// </summary>
    public float m_baseAccel = -1.0f;

    /// <summary>
    /// Movement threshold. When speed is under this value, speed will be truncated  to 0.f
    /// </summary>
    public float m_threshold = 0.15f;

    #endregion

    #region Private params

    /// <summary>
    /// Current movement direction
    /// </summary>
    private Vector3 m_movementDir = Vector3.zero;

    /// <summary>
    /// Current max speed. The maximum speed that the gameObject can move. It can be different from the starting value
    /// </summary>
    private float m_currentMaxSpeed;

    /// <summary>
    /// Current speed of the gameObject. It never will be more than the Curent maximum speed value
    /// </summary>
    private float m_currentSpeed;

    /// <summary>
    /// Current acceleration of the gameObject. Positive or negative (usually negative, imitating friction)
    /// </summary>
    private float m_currentAccel;

    /// <summary>
    /// Refernce to the gameObject transform
    /// </summary>
    private Transform m_transform = null;

    #endregion

    #region Public methods

    /// <summary>
    /// Current max speed. The maximum speed that the gameObject can move. It can be different from the starting value
    /// </summary>
    public float MaxSpeed
    {
        get { return m_currentMaxSpeed; }
        set { m_currentMaxSpeed = value; }
    }


    /// <summary>
    /// Current speed of the gameObject. It never will be more than the Curent maximum speed value
    /// </summary>
    public float Speed
    {
        get { return m_currentSpeed; }
        set { m_currentSpeed = value; }
    }


    /// <summary>
    /// Current acceleration of the gameObject. Positive or negative (usually negative, imitating friction)
    /// </summary>
    public float Accel
    {
        get { return m_currentAccel; }
        set { m_currentAccel = value; }
    }

    /// <summary>
    /// Current movement direction
    /// </summary>
    public Vector3 MovementDir
    {
        get { return m_movementDir; }
        set
        {
            m_movementDir = value;
            m_movementDir.Normalize();
        }
    }

    /// <summary>
    /// Collision callback. Used when Neon hits something. If the collider object is a "BounceElement", then the movement parameters will be modified
    /// using the specific modifiers defined by the "BounceElement"
    /// 
    /// The new direction will be calculated using the following rule:
    /// 
    /// In angle == Out angle
    /// 
    /// As a reflection effect
    /// </summary>
    /// <param name="col">Collision object containing the colision info</param>
    public void OnCollisionEnter2D(Collision2D col)
    {
        BounceElement bounce = col.collider.gameObject.GetComponent<BounceElement>();

        if (bounce != null)
        {

            Vector3 normal = col.contacts[0].normal;

            Vector3 cross = Vector3.Cross(normal, m_movementDir);

            float angle = Vector3.Angle(normal, m_movementDir);

            Vector3 newDir = Quaternion.AngleAxis(angle-180, -cross) * normal;

            MovementDir = newDir;

            m_currentMaxSpeed = m_currentMaxSpeed + m_currentMaxSpeed * bounce.MaxSpeedVariation;

            m_currentSpeed = m_currentSpeed + m_currentSpeed * bounce.SpeedVariation;

            m_currentAccel = m_currentAccel + Mathf.Abs(m_currentAccel) * bounce.AccelVariation;


        }
    }

    #endregion

    #region Private methods

    #endregion

    #region Monobehavior calls

    /// <summary>
    /// Used to initiate all the movement values
    /// </summary>
    private void OnEnable()
    {
        m_currentAccel = m_baseAccel;
        m_currentMaxSpeed = m_baseMaxSpeed;

        m_currentSpeed = 0.0f;

        m_movementDir = Vector3.zero;

        m_transform = gameObject.GetComponent<Transform>();
    }

    /// <summary>
    /// Updates the speed and position of the gameObject
    /// </summary>
    private void Update()
    {
        //Update the current speed with the accel value
        if(m_currentAccel != 0.0f && m_currentSpeed >= m_threshold)
            m_currentSpeed = m_currentSpeed + m_currentAccel * Time.deltaTime;

        //cap current speed to the minimum threshold
        if (m_currentSpeed >= m_threshold)
        {
            //cap current speed to the current MAX movement speed
            m_currentSpeed = m_currentSpeed > m_currentMaxSpeed ? m_currentMaxSpeed : m_currentSpeed;
        }
        else
        {
            m_currentSpeed = 0.0f;
        }

        

        //calculate new position and set it to the gameObject transform
        Vector3 newPos = m_transform.position + (m_currentSpeed * m_movementDir) * Time.deltaTime;

        m_transform.position = newPos;

        //Debug.Log("Speed: " + m_currentSpeed * m_movementDir);
    }


    

    #endregion

}
