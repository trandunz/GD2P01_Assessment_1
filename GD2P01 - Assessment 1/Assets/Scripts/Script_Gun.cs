// Bachelor of Software Engineering 
// Media Design School 
// Auckland 
// New Zealand 
// (c) Media Design School
// File Name : Script_Gun.cs 
// Description : Generic Gun Implementation File
// Author : William Inman
// Mail : william.inman@mds.ac.nz

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_Gun : MonoBehaviour
{
    #region Member Variables
    [SerializeField] GameObject m_Bullet;
    [SerializeField] AudioClip m_FireSound;
    [SerializeField] float m_FireRate_s = 0.25f;

    bool m_CanFire = true;
    float m_ShootTimer = 0;
    #endregion

    #region Public
    /// <summary>
    /// Starts the firing routine with a specified direction
    /// </summary>
    /// <param name="_direction"></param>
    public void Fire(Vector3 _direction)
    {
        if (m_CanFire)
            StartCoroutine(ShootRoutine(_direction.normalized));
    }
    /// <summary>
    /// Starts the firing routine with the direction equal to that of the guns forward vector
    /// </summary>
    public void Fire()
    {
        if (m_CanFire)
            StartCoroutine(ShootRoutine(transform.forward));
    }
    #endregion

    #region Private
    void Update()
    {
        HandleShootTimer();
    }
    /// <summary>
    /// Handles firerate / shoottimer
    /// </summary>
    void HandleShootTimer()
    {
        if (m_ShootTimer > 0)
            m_ShootTimer -= Time.deltaTime;
    }
    /// <summary>
    /// Fires a bullet in the specified direction and plays the sound
    /// Keeps thread open until firerate is ready . shoottimer == 0 using a delegate WaitUntil
    /// </summary>
    /// <param name="_direction"></param>
    /// <returns></returns>
    IEnumerator ShootRoutine(Vector3 _direction)
    {
        m_CanFire = false;
        m_ShootTimer = m_FireRate_s;

        // Create Bullet
        GameObject bullet = Instantiate(m_Bullet, transform.position, Quaternion.identity);
        GetComponent<AudioSource>().PlayOneShot(m_FireSound);
        bullet.GetComponent<Script_Bullet>().SetDirection(_direction);

        yield return new WaitUntil(() => m_ShootTimer <= 0);
        m_CanFire = true;
    }
    #endregion
}

