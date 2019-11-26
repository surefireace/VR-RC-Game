//By Donovan Colen
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// a script that handles the RC tank and what it does
/// </summary>
public class RC_Tank : MonoBehaviour
{
    // private variables
    private GameObject m_turret;
    private GameObject m_gun;
    private bool m_isReloading = false;
    private float m_gunDepression = -5.0f;
    private float m_gunElevation = 30.0f;
    private Quaternion m_defaultTurretRotation;
    private Quaternion m_defaultGunRotation;
    private WaitForSeconds m_delay;
    private float m_movementSpeed = 0f;
    private float m_rotationSpeed = 180f;
    private Vector2 m_touchAxis;
    private Vector2 m_touchAxisTwo;
    private float m_triggerAxis;
    private Rigidbody m_rb;
    private Vector3 m_defaultPosition;
    private Quaternion m_defaultRotation;
    private int m_markersLeft;


    [SerializeField] private float m_reloadTime = 1.0f;
    [SerializeField] private GameObject m_ammo = null;
    [SerializeField] private float m_force = 500.0f;
    [SerializeField] private float m_bbLifeSpan = 5.0f;
    [SerializeField] private float m_acceleration = 0.5f;
    [SerializeField] private float m_maxAcceleration = 3f;
    [SerializeField] private float m_maxSpeed = 5f;
    [SerializeField] private float m_gunAimTolerance = 0.2f;
    [SerializeField] private WheelCollider[] m_tread = new WheelCollider[4];
    [SerializeField] private GameObject m_marker = null;
    [SerializeField] private int m_markerNum = 10;


    private void Start()
    {
        m_turret = transform.Find("Turret").gameObject;
        m_markersLeft = m_markerNum;
        
        if(m_turret == null)
        {
            Debug.Log("NO TURRET");
        }

        if (m_marker == null)
        {
            Debug.Log("missing marker");
        }

        m_gun = m_turret.transform.Find("Gun").gameObject;

        if (m_gun == null)
        {
            Debug.Log("NO GUN");
        }

        if(m_ammo == null)
        {
            Debug.Log("NO AMMO");
        }

        foreach(var col in m_tread)
        {
            if (col == null)
            {
                Debug.Log("missing wheel collider ");
            }
        }

        m_defaultTurretRotation = m_turret.transform.rotation;
        m_defaultGunRotation = m_gun.transform.rotation;

        m_delay = new WaitForSeconds(m_reloadTime);
    }

    public void SetTouchAxis(Vector2 data)
    {
        m_touchAxis = data;
    }

    public void SetTouchAxisTwo(Vector2 data)
    {
        m_touchAxisTwo = data;
    }

    public void SetTriggerAxis(float data)
    {
        m_triggerAxis = data;
    }

    // resets the tank into default state. turret looking forward based off hull and no velocity
    public void ResetCar()
    {
        //transform.position = m_defaultPosition;
        transform.rotation = Quaternion.Euler(m_defaultRotation.eulerAngles.x, transform.rotation.eulerAngles.y, m_defaultRotation.eulerAngles.z);
        m_movementSpeed = 0;
        m_turret.transform.rotation = 
            Quaternion.Euler(m_defaultTurretRotation.eulerAngles.x, transform.rotation.eulerAngles.y, m_defaultTurretRotation.eulerAngles.z);
        m_gun.transform.rotation = 
            Quaternion.Euler(m_defaultGunRotation.eulerAngles.x, transform.rotation.eulerAngles.y, m_defaultGunRotation.eulerAngles.z);
        m_rb.velocity = Vector3.zero;
        m_rb.angularVelocity = Vector3.zero;
    }

    // spawns a marker to help person navigate the maze
    public void SpawnMarker()
    {
        if (m_marker != null && m_markersLeft > 0)
        {
            Vector3 pos = gameObject.transform.position;
            pos.y = m_marker.transform.position.y;
            pos -= (gameObject.transform.forward * .5f);
            Instantiate(m_marker, pos, Quaternion.identity);
            --m_markersLeft;
        }
    }

    public void CollectMarker()
    {
        ++m_markersLeft;
    }


    private void Awake()
    {
        m_rb = GetComponent<Rigidbody>();
        m_defaultPosition = transform.position;
        m_defaultRotation = transform.rotation;
    }

    private void CalculateSpeed()
    {
        if (m_touchAxis.y != 0f)
        {
            m_movementSpeed += (m_acceleration * m_touchAxis.y);
            m_movementSpeed = Mathf.Clamp(m_movementSpeed, -m_maxSpeed, m_maxSpeed);
        }
        else
        {
            Decelerate();
        }
    }

    private void Decelerate()
    {
        if (m_movementSpeed > 0)
        {
            m_movementSpeed -= Mathf.Lerp(m_acceleration, m_maxAcceleration, 0f);
        }
        else if (m_movementSpeed < 0)
        {
            m_movementSpeed += Mathf.Lerp(m_acceleration, -m_maxAcceleration, 0f);
        }
        else
        {
            m_movementSpeed = 0;
        }
    }

    private void Move()
    {
        for(int i = 0; i < m_tread.Length; ++i)
        {
            m_tread[i].motorTorque = m_movementSpeed;
        }
    }

    //turns the tank's hull
    private void Turn()
    {
        float turn = m_touchAxis.x * m_rotationSpeed * Time.deltaTime;
        transform.Rotate(new Vector3(0f, turn, 0f));
    }

    private void FixedUpdate()
    {
        CalculateSpeed();
        Move();
        Turn();
        TurnTurret();
        TiltGun();
        FireGun();
        SpeedToMPh();
    }

    // rotates the turret
    private void TurnTurret()
    {
        float turn = m_touchAxisTwo.x * m_rotationSpeed * Time.deltaTime;
        //Debug.Log(m_touchAxisTwo);
        m_turret.transform.Rotate(new Vector3(0, turn, 0));
    }

    // tilts the tank's gun barrel
    private void TiltGun()
    {
        //if (m_touchAxisTwo.x <= m_gunAimTolerance && m_touchAxisTwo.x >= -m_gunAimTolerance)
        {
            float tilt = -m_touchAxisTwo.y * (m_rotationSpeed / 2) * Time.deltaTime;
            float angle = m_gun.transform.localEulerAngles.x + tilt;

            // cap the up and down rotation
            if (angle <= -m_gunDepression || angle >= 360 - m_gunElevation)
            {
                m_gun.transform.Rotate(new Vector3(tilt, 0, 0), Space.Self);
            }

        }
    }

    // fires a projectile from the tank's gun
    private void FireGun()
    {
        if (!m_isReloading && m_triggerAxis > 0)
        {
            m_isReloading = true;
            //Debug.Log("FIRE");
            Vector3 pos = m_gun.transform.Find("FirePoint").gameObject.transform.position;
            GameObject temp = Instantiate(m_ammo, pos, Quaternion.identity);
            temp.GetComponent<Rigidbody>().AddForce(m_gun.transform.forward * m_force);
            Destroy(temp, m_bbLifeSpan);
            StartCoroutine(Reload());
        }
    }

    private IEnumerator Reload()
    {
        yield return m_delay;
        m_isReloading = false;
        //Debug.Log("Reloaded");
    }

    // calculates the speed in MPH
    private void SpeedToMPh()
    {
        float mph = 0;
        //for (int i = 0; i < m_tread.Length; ++i)
        {
            mph = (m_tread[0].radius * 2) * 3.281f * Mathf.PI * m_tread[0].rpm * 60 / 5280;
        }


    }

}
