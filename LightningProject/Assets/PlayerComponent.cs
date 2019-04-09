using System.Collections;
using System.Collections.Generic;
using Assets;
using UnityEngine;

public class PlayerComponent : MonoBehaviour
{
    [SerializeField] private float m_OffsetDistanceJump = 10.0f;
    [SerializeField] private float m_ForwardDistanceJump = 10.0f;
    [SerializeField] private float m_TravelDelayTime = 3.0f;
    [Range(0.1f, 1.0f)]
    [SerializeField] private float m_TravelDelayRatioReduction = 0.9f;
    [SerializeField] private float m_TravelDelayReductionTime = 5.0f;


    private float m_CurrentTravelDelayTimer;
    private float m_CurrentDelay;
    private Rigidbody m_RigidBody;


    private void Awake()
    {
        m_CurrentTravelDelayTimer = m_TravelDelayReductionTime;
        m_RigidBody = GetComponent<Rigidbody>();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float l_DeltaTime = Time.deltaTime;

        if (m_CurrentDelay <= 0)
        {
            // Check Input
            Vector3 l_NewPosition = transform.position + transform.forward * m_ForwardDistanceJump;

            float l_VerticalAxis = Input.GetAxis("Vertical");
            if (l_VerticalAxis > 0.0f)
            {
                l_NewPosition += transform.up * m_OffsetDistanceJump;
            }
            else if (l_VerticalAxis < 0.0f)
            {
                l_NewPosition += transform.up * -1.0f * m_OffsetDistanceJump;
            }


            float l_HorizontalAxis = Input.GetAxis("Horizontal");
            if (l_HorizontalAxis > 0.0f)
            {
                l_NewPosition += transform.right * m_OffsetDistanceJump;
            }
            else if (l_HorizontalAxis < 0.0f)
            {
                l_NewPosition += transform.right * -1.0f * m_OffsetDistanceJump;
            }

            m_RigidBody.MovePosition(l_NewPosition);
            m_CurrentDelay = m_TravelDelayTime;
        }

        m_CurrentDelay -= l_DeltaTime;

        // Adjusting Travel Delay

        if (m_CurrentTravelDelayTimer <= 0)
        {

            m_TravelDelayTime *= m_TravelDelayRatioReduction;
            m_CurrentTravelDelayTimer = m_TravelDelayReductionTime;
        }

        m_CurrentTravelDelayTimer -= l_DeltaTime;
    }


    public void OnTriggerEnter(Collider i_Collider)
    {
        if (i_Collider.CompareTag("Bottle"))
        {
            // You Win
            Destroy(this);
            GameManager.Instance.TriggerWin();

        }
        else
        {
            GameManager.Instance.RestartGame();
        }
    }
}
