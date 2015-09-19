///----------------------------------------------------------------------
/// @file BSEngineEditorScript.cs
///
/// This file contains the declaration of BSEngineEditorScript class.
/// 
/// This script is used to create the base scene used in the engine
///
/// @author Alberto Martinez Villaran <tukaram92@gmail.com>
/// @date 15/9/2015
///----------------------------------------------------------------------



using UnityEditor;
using UnityEngine;
using System.Collections;

namespace BSEngine
{
    public class BSEngineEditorScript : MonoBehaviour
    {

        [MenuItem("BSEngine/Add BaseScene")]
        static void AddBaseSceneFunction()
        {
            GameObject BSEngineServers = GameObject.Find("BSEngineServers");
            if (BSEngineServers == null)
            {
                BSEngineServers = new GameObject("BSEngineServers");
                BSEngineServers.AddComponent<BSEngineLoader>();
            }


            GameObject Pool = GameObject.Find("Pool");
            if (Pool == null)
            {
                Pool = new GameObject("Pool");
            }
        }



        [MenuItem("BSEngine/Add Test Elements")]
        private static void AddTestSceneFunction()
        {
            GameObject BSEngineServers = GameObject.Find("BSEngineServers");
            if (BSEngineServers == null)
            {
                BSEngineServers = new GameObject("BSEngineServers");
                BSEngineLoader loader = BSEngineServers.AddComponent<BSEngineLoader>();
                loader.LoadStatesScenes = false;
            }


            GameObject Pool = GameObject.Find("Pool");
            if (Pool == null)
            {
                Pool = new GameObject("Pool");
            }
        }
    }


    


}
