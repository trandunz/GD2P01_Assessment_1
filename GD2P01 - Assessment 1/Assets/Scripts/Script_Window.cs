using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_Window : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag is "Bullet")
        {
            Destroy(this.gameObject);
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag is "Bullet")
        {
            Destroy(this.gameObject);
        }
    }
}
