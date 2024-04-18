using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterVisual : MonoBehaviour
{
    [Serializable]
    public struct Directions
    {
        public GameObject Up;
        public GameObject Down;
        public GameObject Side;
    }

    [SerializeField] private Directions m_rifle;
    [SerializeField] private Directions m_rifleWalk;
    [SerializeField] private Directions m_scytheIdle;
    [SerializeField] private Directions m_scytheAttack;
    [SerializeField] private GameObject[] m_muzzleFlashes;

    private Character m_character;

    private void Awake()
    {
        m_character = GetComponentInParent<Character>();
    }

    private static void SetScaleX(Transform t, float x)
    {
        Vector3 scale = t.localScale;
        scale.x = x;
        t.localScale = scale;
    }

    private GameObject GetDirectionalSprite(Directions sprites, Vector3 dir)
    {
        if (dir.x < -0.5f)
        {
            SetScaleX(sprites.Side.transform, 1.0f);
            return sprites.Side;
        }
        else if (dir.x > 0.5f)
        {
            SetScaleX(sprites.Side.transform, -1.0f);
            return sprites.Side;
        }
        else if (dir.y < 0)
        {
            return sprites.Down;
        }
        else
        {
            return sprites.Up;
        }
    }

    private void EnableDirection(Directions directions, GameObject visible)
    {
        directions.Up.SetActive(directions.Up == visible);
        directions.Down.SetActive(directions.Down == visible);
        directions.Side.SetActive(directions.Side == visible);
    }

    private void Update()
    {
        Vector3 dir = m_character.GetMovementDirection();
        bool isMoving = m_character.IsMoving();

        Directions directions;
        switch (m_character.GetWeapon())
        {
            case CharacterWeapon.Scythe:
                directions = m_scytheIdle;
                break;

            case CharacterWeapon.Rifle:
                if (isMoving)
                    directions = m_rifleWalk;
                else
                    directions = m_rifle;
                break;

            default:
                directions = m_rifle;
                break;
        }

        GameObject sprite = GetDirectionalSprite(directions, dir);
        EnableDirection(m_rifle, sprite);
        EnableDirection(m_rifleWalk, sprite);
        EnableDirection(m_scytheIdle, sprite);
        EnableDirection(m_scytheAttack, sprite);

        bool isShooting = m_character.IsShooting();
        foreach (GameObject obj in m_muzzleFlashes)
            obj.SetActive(isShooting);
    }
}
