///----------------------------------------------------------------------
/// @file VolumetricLineBehavior.cs
///
/// This file contains the declaration of VolumetricLineBehavior class.
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
using VolumetricLines.Utils;

namespace VolumetricLines
{
   
    [RequireComponent(typeof(MeshFilter))]
    [RequireComponent(typeof(Renderer))]
    public class VolumetricLineBehavior : MonoBehaviour
    {

        #region Private params
        /// <summary>
        /// Flag used to update the line color, modifying the material of this object.
        /// </summary>
        private bool m_updateLineColor = false;

        /// <summary>
        /// Flag used to update the line width, modifying the material of this object.
        /// </summary>
        private bool m_updateLineWidth = false;

        /// <summary>
        /// Reference to the material of this object
        /// </summary>
        private Material m_material = null;

        /// <summary>
        /// Reference to the gameObject transform
        /// </summary>
        private Transform m_transform = null;


        /// <summary>
        /// Configuration of the mesh
        /// </summary>
        private static readonly Vector2[] m_vline_texCoords = {
			new Vector2(1.0f, 1.0f),
			new Vector2(1.0f, 0.0f),
			new Vector2(0.5f, 1.0f),
			new Vector2(0.5f, 0.0f),
			new Vector2(0.5f, 0.0f),
			new Vector2(0.5f, 1.0f),
			new Vector2(0.0f, 0.0f),
			new Vector2(0.0f, 1.0f),
		};


        private static readonly Vector2[] m_vline_vertexOffsets = {
			 new Vector2(1.0f,	 1.0f),
			 new Vector2(1.0f,	-1.0f),
			 new Vector2(0.0f,	 1.0f),
			 new Vector2(0.0f,	-1.0f),
			 new Vector2(0.0f,	 1.0f),
			 new Vector2(0.0f,	-1.0f),
			 new Vector2(1.0f,	 1.0f),
			 new Vector2(1.0f,	-1.0f)
		};

        private static readonly int[] m_vline_indices =
		{
			2, 1, 0,
			3, 1, 2,
			4, 3, 2,
			5, 4, 2,
			4, 5, 6,
			6, 5, 7
		};

        #endregion



        

        #region Public params
        /// <summary>
        /// The start position relative to the GameObject's origin
        /// </summary>
        public Vector3 m_startPos;

        /// <summary>
        /// The end position relative to the GameObject's origin
        /// </summary>
        public Vector3 m_endPos = new Vector3(0.0f, 10.0f, 0.0f);

        /// <summary>
        /// Line Color
        /// </summary>
        public Color m_lineColor = Color.red;

        /// <summary>
        /// The width of the line
        /// </summary>
        public float m_lineWidth = 1.0f;
        
        #endregion

        #region Public methods
        /// <summary>
        /// Set or get the start position relative to the GameObject's origin
        /// </summary>
        public Vector3 StartPos
        {
            get { return m_startPos; }
            set
            {
                m_startPos = value;
                SetStartAndEndPoints(m_startPos, m_endPos);
            }
        }

        /// <summary>
        /// Set or get the end position relative to the GameObject's origin
        /// </summary>
        public Vector3 EndPos
        {
            get { return m_endPos; }
            set
            {
                m_endPos = value;
                SetStartAndEndPoints(m_startPos, m_endPos);
            }
        }

        /// <summary>
        /// Gets or sets the color of the line. This can be used during runtime
        /// regardless of SetLinePropertiesAtStart-property's value.
        /// </summary>
        public Color LineColor
        {
            get { return m_lineColor; }
            set { m_lineColor = value; m_updateLineColor = true; }
        }

        /// <summary>
        /// Gets or sets the width of the line. This can be used during runtime
        /// regardless of SetLineColorAtStart-propertie's value.
        /// </summary>
        public float LineWidth
        {
            get { return m_lineWidth; }
            set { m_lineWidth = value; m_updateLineWidth = true; }
        }

        /// <summary>
        /// Sets the start and end points - updates the data of the Mesh.
        /// </summary>
        public void SetStartAndEndPoints(Vector3 startPoint, Vector3 endPoint)
        {
            Vector3[] vertexPositions = {
				startPoint,
				startPoint,
				startPoint,
				startPoint,
				endPoint,
				endPoint,
				endPoint,
				endPoint,
			};

            Vector3[] other = {
				endPoint,
				endPoint,
				endPoint,
				endPoint,
				startPoint,
				startPoint,
				startPoint,
				startPoint,
			};

            var mesh = GetComponent<MeshFilter>().sharedMesh;
            if (null != mesh)
            {
                mesh.vertices = vertexPositions;
                mesh.normals = other;
            }
        }

        #endregion

        #region Monobehavior Calls
        
        /// <summary>
        /// Initialization of the mesh data and material data
        /// </summary>
        private void Start()
        {
            Vector3[] vertexPositions = {
				m_startPos,
				m_startPos,
				m_startPos,
				m_startPos,
				m_endPos,
				m_endPos,
				m_endPos,
				m_endPos,
			};

            Vector3[] other = {
				m_endPos,
				m_endPos,
				m_endPos,
				m_endPos,
				m_startPos,
				m_startPos,
				m_startPos,
				m_startPos,
			};

            // Need to set vertices before assigning new Mesh to the MeshFilter's mesh property
            Mesh mesh = new Mesh();
            mesh.vertices = vertexPositions;
            mesh.normals = other;
            mesh.uv = m_vline_texCoords;
            mesh.uv2 = m_vline_vertexOffsets;
            mesh.SetIndices(m_vline_indices, MeshTopology.Triangles, 0);

            gameObject.GetComponent<MeshFilter>().mesh = mesh;

            // Need to duplicate the material, otherwise multiple volume lines would interfere

            Renderer renderComp = gameObject.GetComponent<Renderer>();

            m_material = renderComp.material;


            m_material.color = m_lineColor;
            m_material.SetFloat("_LineWidth", m_lineWidth);

            m_material.SetFloat("_LineScale", transform.GetGlobalUniformScaleForLineWidth());

            m_updateLineColor = false;
            m_updateLineWidth = false;

            m_transform = gameObject.GetComponent<Transform>();
        }

        /// <summary>
        /// Update of the material info if needed
        /// </summary>
        private void Update()
        {
            if (m_transform.hasChanged)
            {
                m_material.SetFloat("_LineScale", transform.GetGlobalUniformScaleForLineWidth());
            }
            if (m_updateLineColor)
            {
                m_material.color = m_lineColor;
                m_updateLineColor = false;
            }
            if (m_updateLineWidth)
            {
                m_material.SetFloat("_LineWidth", m_lineWidth);
                m_updateLineWidth = false;
            }
        }

        /// <summary>
        /// Free memory of the material instance(avoiding memory leaks)
        /// </summary>
        private void OnDestroy()
        {
            DestroyImmediate(m_material);
        }

        /// <summary>
        /// Mthod used to draw the actual line
        /// </summary>
        private void OnDrawGizmos()
        {
            Gizmos.color = m_lineColor;
            Gizmos.DrawLine(gameObject.transform.TransformPoint(m_startPos), gameObject.transform.TransformPoint(m_endPos));
        }
        #endregion
    }
}