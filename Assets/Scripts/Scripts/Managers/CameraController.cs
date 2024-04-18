using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private static CameraController m_instance;
    public static CameraController Instance { get { return m_instance; } }

    private Camera m_camera;
    private List<Transform> m_targets;
    private float m_z;

    private void Awake()
    {
        m_instance = this;
        m_camera = Camera.main;
        m_targets = new List<Transform>();
        m_z = m_camera.transform.position.z;
    }

    private void OnDestroy()
    {
        m_instance = null;
    }

    public void RegisterTarget(Transform target)
    {
        m_targets.Add(target);
    }

    public void UnregisterTarget(Transform target)
    {
        m_targets.Remove(target);
    }

    private void LateUpdate()
    {
        if (m_targets.Count > 0)
        {
            Vector3 position = m_targets[0].position;
            position.z = m_z;
            m_camera.transform.position = position;
        }
    }
}
