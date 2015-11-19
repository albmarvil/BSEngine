
///----------------------------------------------------------------------
/// @file BounceElement.cs
///
/// This file contains the declaration of BounceElement class.
/// 
/// This component defines an element in which Neon can bounce. It has the definition of the bopunce properties over
/// neon movement properties
///
/// @author Alberto Martinez Villaran <tukaram92@gmail.com>
/// @date 28/9/2015
///----------------------------------------------------------------------


using UnityEngine;
using System.Collections;

public class BounceElement : MonoBehaviour
{

    #region Private params

    /// <summary>
    /// % of increment on Max Speed value
    /// </summary>
    [SerializeField]
    private float m_maxSpeedVariation = 0.0f;

    /// <summary>
    /// % of increment on Current Speed value
    /// </summary>
    [SerializeField]
    private float m_speedVariaton = 0.0f;

    /// <summary>
    /// % of increment on Current Accel value
    /// </summary>
    [SerializeField]
    private float m_accelVariation = 0.0f;

    #endregion

    #region Public methods

    /// <summary>
    /// % of increment on Max Speed value
    /// </summary>
    public float SpeedVariation
    {
        get { return m_speedVariaton; }
    }

    /// <summary>
    /// % of increment on Current Speed value
    /// </summary>
    public float MaxSpeedVariation
    {
        get { return m_maxSpeedVariation; }
    }

    /// <summary>
    /// % of increment on Current Accel value
    /// </summary>
    public float AccelVariation
    {
        get { return m_accelVariation; }
    }


    #endregion

}