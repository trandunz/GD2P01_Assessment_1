using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_PlayerCamera : MonoBehaviour
{
    #region Member Variables
    [SerializeField] GameObject m_Target;
    #endregion

    #region Private
    /// <summary>
    /// Locks the camera with assigned script to target position so that it follows.
    /// </summary>
    void Update()
    {
        if (m_Target)
            transform.position = new Vector3(m_Target.transform.position.x, transform.position.y, m_Target.transform.position.z);
    }
    #endregion
}
