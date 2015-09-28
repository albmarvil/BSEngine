using UnityEngine;
using System.Collections;

public class NeonMovementController : MonoBehaviour {


    #region Public params

    public float m_baseMaxSpeed = 1.0f;

    public float m_baseAccel = -1.0f;

    public float m_threshold = 0.15f;

    #endregion

    #region Private params

    private Vector3 m_movementDir = Vector3.zero;

    private float m_currentMaxSpeed;

    private float m_currentSpeed;

    private float m_currentAccel;

    private Transform m_transform = null;

    #endregion

    #region Public methods

    public float MaxSpeed
    {
        get { return m_currentMaxSpeed; }
        set { m_currentMaxSpeed = value; }
    }

    public float Speed
    {
        get { return m_currentSpeed; }
        set { m_currentSpeed = value; }
    }

    public float Accel
    {
        get { return m_currentAccel; }
        set { m_currentAccel = value; }
    }

    public Vector3 MovementDir
    {
        get { return m_movementDir; }
        set
        {
            m_movementDir = value;
            m_movementDir.Normalize();
        }
    }



    public void OnCollisionEnter2D(Collision2D col)
    {
        BounceElement bounce = col.collider.gameObject.GetComponent<BounceElement>();

        if (bounce != null)
        {

            Vector3 normal = col.contacts[0].normal;

            //Vector3 point = col.contacts[0].point;

            Vector3 cross = Vector3.Cross(normal, m_movementDir);

            float angle = Vector3.Angle(normal, m_movementDir);

            //Debug.LogError(angle + " DOT:" + Vector3.Dot(normal, m_movementDir) + " CROSS:" + cross);

            Vector3 newDir = Quaternion.AngleAxis(angle-180, -cross) * normal;

            //Debug.DrawLine(point, normal + point, Color.green);
            //Debug.DrawLine(point, m_movementDir + point, Color.black);
            //Debug.DrawLine(point, newDir + point, Color.yellow);
            //Debug.DrawRay(point, normal, Color.green);
            //Debug.DrawRay(point, m_movementDir, Color.red);
            //Debug.DrawRay(point, newDir, Color.black);

            m_movementDir = newDir;

            m_currentMaxSpeed = m_currentMaxSpeed + m_currentMaxSpeed * bounce.MaxSpeedVariation;

            m_currentSpeed = m_currentSpeed + m_currentSpeed * bounce.SpeedVariation;

            m_currentAccel = m_currentAccel + Mathf.Abs(m_currentAccel) * bounce.AccelVariation;


        }
    }

    #endregion

    #region Private methods

    #endregion

    #region Monobehavior calls

    private void OnEnable()
    {
        m_currentAccel = m_baseAccel;
        m_currentMaxSpeed = m_baseMaxSpeed;

        m_currentSpeed = 0.0f;

        m_movementDir = Vector3.zero;

        m_transform = gameObject.GetComponent<Transform>();
    }

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
