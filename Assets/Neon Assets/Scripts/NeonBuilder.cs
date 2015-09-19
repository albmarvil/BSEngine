using UnityEngine;
using System.Collections.Generic;
using VolumetricLines;

public class NeonBuilder : MonoBehaviour {


    #region Public params

    public VolumetricMultiLineBehavior m_volumetricLine = null;

    public int m_segments = 10;

    public float m_radius = 1.0f;

    #endregion

    #region Private params

    

    #endregion

    #region Public methods

    #endregion

    #region Private methods

    #endregion

    #region Monobehavior calls

    public void OnEnable()
    {
        List<Vector3> vertices = new List<Vector3>();

        Vector3 center = Vector3.zero;

        vertices.Add(new Vector3(0.0f, -m_radius, 0.0f));

        Vector3 dirReference = vertices[0] - center;
        dirReference.Normalize();

        float angle = 360.0f / m_segments;

        Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);

        for (int i = 1; i < m_segments; ++i)
        {
            dirReference = q * dirReference;

            Vector3 vertex = dirReference * m_radius + center;

            vertices.Add(vertex);
        }

        vertices.Add(new Vector3(0.0f, -m_radius, 0.0f));

        //m_volumetricLine.UpdateLineVertices(vertices.ToArray());
        m_volumetricLine.m_lineVertices = vertices.ToArray();

    }

    #endregion

}
