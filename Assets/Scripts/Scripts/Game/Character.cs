using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class Character : MonoBehaviour
{
    private Rigidbody m_rigidbody;
    private bool m_isMoving;
    private Vector3 m_direction;

    private void Awake()
    {
        m_rigidbody = GetComponent<Rigidbody>();
    }

    protected void Move(Vector3 direction, float speed)
    {
        direction = direction.normalized;
        m_rigidbody.velocity = direction * speed;

        m_isMoving = direction.magnitude > 0.1f;
        if (m_isMoving)
            m_direction = direction;
    }

    public bool IsShooting()
    {
        return false;
    }

    public CharacterWeapon GetWeapon()
    {
        return CharacterWeapon.Rifle;
    }

    public bool IsMoving()
    {
        return m_isMoving;
    }

    public Vector3 GetMovementDirection()
    {
        return m_direction;
    }
}
