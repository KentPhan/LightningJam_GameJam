using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinMoveComponent : MonoBehaviour
{
    [SerializeField] private float m_Amplitude = 1.0f;
    [SerializeField] private float m_Frequency = 1.0f;

    private float m_CurrentTime;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        m_CurrentTime += Time.deltaTime;
        transform.position =
            transform.position + new Vector3(0.0f, Mathf.Sin(m_CurrentTime / m_Frequency) * m_Amplitude);
    }
}

