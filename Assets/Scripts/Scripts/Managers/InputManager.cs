using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private static InputManager m_instance;
    public static InputManager Instance { get { return m_instance; } }

    private PlayerActions m_inputControls;

    public PlayerActions actions => m_inputControls;
      private InputManager inputManager;

    private void Awake()
    {
        m_instance = this;
    }

    private void OnDestroy()
    {
        m_instance = null;
    }

    private void OnEnable()
    {
        m_inputControls = new PlayerActions();
        m_inputControls.Enable();
    }

    private void OnDisable()
    {
        m_inputControls.Disable();
    }

    public Vector3 GetPlayerMovement()
    {
        return m_inputControls.Player_PC.Move.ReadValue<Vector2>();
    }
}
