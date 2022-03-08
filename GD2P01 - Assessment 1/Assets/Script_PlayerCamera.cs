using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_PlayerCamera : MonoBehaviour
{
    [SerializeField] GameObject m_Target;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(m_Target.transform.position.x, transform.position.y, m_Target.transform.position.z);
    }
}
