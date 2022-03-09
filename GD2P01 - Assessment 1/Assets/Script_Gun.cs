using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_Gun : MonoBehaviour
{
    #region Member Variables
    [SerializeField] GameObject m_Bullet;
    [SerializeField] float m_FireRate_s = 0.25f;

    bool m_CanFire = true;
    float m_ShootTimer = 0;
    #endregion

    #region Public
    public void Fire()
    {
        if (m_CanFire)
            StartCoroutine(ShootRoutine());
    }
    #endregion

    #region Private
    void Update()
    {
        HandleShootTimer();
    }
    void HandleShootTimer()
    {
        if (m_ShootTimer > 0)
            m_ShootTimer -= Time.deltaTime;
    }
    IEnumerator ShootRoutine()
    {
        m_CanFire = false;
        m_ShootTimer = m_FireRate_s;

        // Create Bullet
        GameObject bullet = Instantiate(m_Bullet, transform.position, Quaternion.identity);
        bullet.GetComponent<Script_Bullet>().SetDirection(transform.forward);

        yield return new WaitUntil(() => m_ShootTimer <= 0);
        m_CanFire = true;
    }
    #endregion
}

