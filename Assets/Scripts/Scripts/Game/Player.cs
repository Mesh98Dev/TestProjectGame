using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    [SerializeField] private PlayerConfiguration m_config;

    private void OnEnable()
    {
        CameraController.Instance.RegisterTarget(transform);
    }

    private void OnDisable()
    {
        if (CameraController.Instance != null)
            CameraController.Instance.UnregisterTarget(transform);
    }

    private void Update()
    {
        Vector2 movement = InputManager.Instance.GetPlayerMovement();
        Move(movement, m_config.MovementSpeed);
    }
}
