///----------------------------------------------------------------------
/// @file VolumetricMultiLineBehavior.cs
///
/// This file contains the declaration of VolumetricMultiLineBehavior class.
///
/// @author Johannes Unterguggenberger <johannes.unterguggenberger@gmail.com>
/// @Unity Asset store profile: https://www.assetstore.unity3d.com/en/#!/publisher/11022/page=1/sortby=popularity
/// @author web: http://www.sphereless-games.com/
/// 
/// Based on the asset package "Volumetric Lines" (https://www.assetstore.unity3d.com/en/#!/content/29160)
/// 
/// I've only refactorized the Monobehavior scripts adapting them to my game architecture. 
/// 
/// All rights of this code are reserved to his owner
/// 
/// @refactor Alberto Martinez Villaran <tukaram92@gmail.com>
/// @date 17/9/2015
///----------------------------------------------------------------------



using UnityEngine;
using System.Collections;
using BSEngine;

namespace VolumetricLines
{
	
	public class VolumetricMultiLineBehavior : MonoBehaviour
    {

        #region Private params

        /// <summary>
        /// References to the volumetric lines that are in this multple line
        /// </summary>
        private VolumetricLineBehavior[] m_volumetricLines;

        #endregion

        #region Public params
        /// <summary>
		/// Prefab for a single volumetric line to be instantiated multiple times
		/// </summary>
        public GameObject m_volumetricLinePrefab;

		/// <summary>
		/// The vertices where 2 adjacent multi lines touch each other. 
		/// The end of line 1 is the start of line 2, etc.
		/// </summary>
		public Vector3[] m_lineVertices;

        /// <summary>
        /// Line Color
        /// </summary>
        public Color m_lineColor = Color.red;

        /// <summary>
        /// Line width
        /// </summary>
        public float m_lineWidth = 1.0f;

        #endregion


        #region Public Methods
        /// <summary>
		/// Gets or sets the color all line segments. This can be used during runtime
		/// regardless of SetLinePropertiesAtStart-property's value.
		/// </summary>
		public Color LineColor 
		{
			get 
			{
				// If that doesn't exist => just let it throw
                //return m_volumetricLines[0].LineColor;
                return m_lineColor;
			}
			set 
			{
                m_lineColor = value;
				foreach (var line in m_volumetricLines)
				{
					line.LineColor = m_lineColor;
				}
			}
		}
		
		/// <summary>
		/// Gets or sets the width of the line. This can be used during runtime
		/// regardless of SetLineColorAtStart-propertie's value.
		/// </summary>
		public float LineWidth 
		{
			get 
			{ 
				// If that doesn't exist => just let it throw
                //return m_volumetricLines[0].LineWidth;
                return m_lineWidth;
			}
			set 
			{
                m_lineWidth = value;
				foreach (var line in m_volumetricLines)
				{
					line.LineWidth = m_lineWidth;
				}
			}
		}

        /// <summary>
        /// Update the vertices of this multi line.
        /// </summary>
        /// <param name="newSetOfVertices">New set of vertices.</param>
        public void UpdateLineVertices(Vector3[] newSetOfVertices)
        {
            m_lineVertices = newSetOfVertices;
            for (int i = 0; i < m_lineVertices.Length - 1; ++i)
            {
                m_volumetricLines[i].SetStartAndEndPoints(m_lineVertices[i], m_lineVertices[i + 1]);
            }
        }

        #endregion


        #region Monobehavior calls
        /// <summary>
        /// Instatiates all the volumetricLines of the given prefab
        /// </summary>
		private void Start () 
		{
			m_volumetricLines = new VolumetricLineBehavior[m_lineVertices.Length - 1];
			for (int i=0; i < m_lineVertices.Length - 1; ++i)
			{
                GameObject go = PoolMgr.Singleton.Instantiate(m_volumetricLinePrefab, gameObject.transform);
                //GameObject go = GameObject.Instantiate(m_volumetricLinePrefab) as GameObject;
				go.transform.parent = gameObject.transform;
				go.transform.localPosition = Vector3.zero;
				go.transform.localRotation = Quaternion.identity;


                VolumetricLineBehavior volLine = go.GetComponent<VolumetricLineBehavior>();
                volLine.LineColor = m_lineColor;
                volLine.LineWidth = m_lineWidth;
				volLine.StartPos = m_lineVertices[i];
				volLine.EndPos = m_lineVertices[i+1];
				
				m_volumetricLines[i] = volLine;
			}
		}

		
		
		private void OnDrawGizmos()
		{
			Gizmos.color = m_lineColor;
			for (int i=0; i < m_lineVertices.Length - 1; ++i)
			{
				Gizmos.DrawLine(gameObject.transform.TransformPoint(m_lineVertices[i]), gameObject.transform.TransformPoint(m_lineVertices[i+1]));
			}
        }

        private void OnDestroy()
        {
            if (PoolMgr.Singleton != null)
            {
                for (int i = 0; i < m_volumetricLines.Length; ++i)
                {
                    PoolMgr.Singleton.Destroy(m_volumetricLines[i].gameObject);
                }
            }
        }

        #endregion
    }
}