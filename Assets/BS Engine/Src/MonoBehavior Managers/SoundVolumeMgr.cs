///----------------------------------------------------------------------
/// @file SoundVolumeMgr.cs
///
/// This file contains the declaration of SoundVolumeMgr class.
/// 
/// This class is a MonoBheavior singleton. Desgined for volume or sound level management. It keeps control of the different busses
/// defined by the FMOD Studio tool.
/// 
/// This class has code that is considered GAME SPECIFIC. so feel free to modify if you need new features or manage more channels or audio busses.
///
/// @author Alberto Martinez Villaran <tukaram92@gmail.com>
/// @date 12/01/2016
///----------------------------------------------------------------------



using UnityEngine;
using System.Collections.Generic;

using BSEngine;
using BSEngine.Utils;


public class SoundVolumeMgr : MonoBehaviour {

	    
    #region Singleton

    /// <summary>
    /// Singleton instance of the class
    /// </summary>
    private static SoundVolumeMgr m_instance = null;

    /// <summary>
    /// Property to get the singleton instance of the class.
    /// </summary>
    public static SoundVolumeMgr Singleton { get { return m_instance; } }

    // Explicit static constructor to tell C# compiler not to mark type as beforefieldinit
    static SoundVolumeMgr() { }

    /// <summary>
    /// This is like the Init but done by the MonoBehaviour
    /// </summary>
    private void Awake()
    {
        if (m_instance == null)
            m_instance = this;
        else
        {
            Debug.LogError("Someone is trying to create various SoundVolumeMgr [" + name + "]");
            this.enabled = false;
        }
    }
	
	/// <summary>
    /// This is like the Release but done by the MonoBehaviour
    /// </summary>
    private void OnDestroy()
    {
        if (m_instance == this)
            m_instance = null;
    }

    #endregion


    #region Private params

    /// <summary>
    /// Reference to the FMOD Studio system
    /// </summary>
    private FMOD.Studio.System m_system;

    /// <summary>
    /// Reference to the DataTable that is located at the BSEngine_CFG file, that contains the sarialized volume values for each bus
    /// </summary>
    private DataTable m_soundOptions;

    /// <summary>
    /// References to the different busses
    /// </summary>
    private Dictionary<string, FMOD.Studio.Bus> m_buses;

    #endregion

    #region Public methods

    /// <summary>
    /// Gets the volume from the specified bus
    /// </summary>
    public float getBusVolume(string busName)
    {
        return m_soundOptions.Get<float>(busName);
    }

    /// <summary>
    /// Sets the volume for the specified bus. This changed will be serialized on the BSEngine_CFG file
    /// </summary>
    /// <param name="busName"></param>
    /// <param name="volumeLevel"></param>
    public void setBusVolume(string busName, float volumeLevel)
    {
        float finalLevel =  Mathf.Clamp01(volumeLevel);
        m_soundOptions.Set<float>(busName,finalLevel);
        m_buses[busName].setFaderLevel(finalLevel);
    }

    #endregion

    #region Private methods

    /// <summary>
    /// Method used to create the default definition of the audio busses.
    /// 
    /// This method is called when the data table "SoundVolumeOptions" ins't found in the BSEngine_CFG file.
    /// It usually happens when the game is launched for fisrt time.
    /// 
    /// CONTAINS GAME SPECIFIC CODE
    /// </summary>
    private DataTable createDefaultSoundOptions()
    {
        DataTable options = new DataTable("SoundVolumeOptions", SerializationMode.NONE, false);

        ///GAME CODE
        ///*********Make sure that your busses names match the buses names defined in FMOD Studio
        options.Set<float>("Master", 1.0f);
        options.Set<float>("Music", 1.0f);
        options.Set<float>("Ambience", 1.0f);
        options.Set<float>("Dialogs", 1.0f);
        options.Set<float>("Effects", 1.0f);

        ///ENGINE CODE
        StorageMgr.Blackboard.Get<DataTable>("CFG").Set<DataTable>("SoundVolumeOptions", options);

        return options;
    }

    #endregion

    #region Monobehavior calls

    /// <summary>
    /// This method tries to retrieve the required info from the BSEngine_CFG file. Then will retrieve all the busses from FMOD system.
    /// </summary>
    private void Start()
    {
        m_system = FMODUnity.RuntimeManager.StudioSystem;


        if (StorageMgr.Blackboard.Get<DataTable>("CFG").ContainsKey("SoundVolumeOptions"))
        {
            m_soundOptions = StorageMgr.Blackboard.Get<DataTable>("CFG").Get<DataTable>("SoundVolumeOptions");

            ///Make sure that all the volume values are set between 0 & 1
            DataTable aux = new DataTable("aux", SerializationMode.NONE, false);
            foreach (string key in m_soundOptions.Keys)
            {
               aux.Set<float>(key, Mathf.Clamp01(m_soundOptions.Get<float>(key)));
            }

            foreach (string key in aux.Keys)
            {
                m_soundOptions.Set<float>(key, aux.Get<float>(key));
            }
        }
        else
        {
            m_soundOptions = createDefaultSoundOptions();
        }

        m_buses = new Dictionary<string, FMOD.Studio.Bus>();


        foreach (string busName in m_soundOptions.Keys)
        {
            Debug.Log(busName);
            FMOD.Studio.Bus bus = null;
            if (busName == "Master")
            {
                m_system.getBus("bus:/", out bus);
            }
            else
            {
                m_system.getBus("bus:/" + busName, out bus);
            }

            Assert.assert(bus != null, "FMOD "+busName+" bus not found!");
            bus.setFaderLevel(m_soundOptions.Get<float>(busName));
            m_buses.Add(busName, bus);
        }
        
    }

    #endregion

}
