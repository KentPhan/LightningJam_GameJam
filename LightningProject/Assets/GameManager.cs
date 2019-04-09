using System;
using Cinemachine;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets
{
    public enum GameStates
    {
        START,
        PLAY,
        WIN
    }

    public class GameManager : MonoBehaviour
    {
        [SerializeField] private RectTransform m_StartScreen;
        [SerializeField] private RectTransform m_WinScreen;
        [SerializeField] private Transform m_StartPosition;
        [SerializeField] private PlayerComponent m_PlayerPrefab;
        [SerializeField] private CinemachineVirtualCamera m_CMCamera;

        private GameStates m_CurrentState;
        private PlayerComponent m_CurrentPlayer;

        public static GameManager Instance;

        private void Awake()
        {
            Instance = this;
            m_WinScreen.gameObject.SetActive(false);
        }

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Application.Quit();
                return;
            }
            if (Input.GetKeyDown(KeyCode.R))
            {
                RestartGame();
                return;
            }


            switch (m_CurrentState)
            {
                case GameStates.START:
                    if (Input.GetKeyDown(KeyCode.Return))
                    {
                        m_CurrentState = GameStates.PLAY;
                        m_CurrentPlayer = Instantiate(m_PlayerPrefab, m_StartPosition.position, Quaternion.identity, null);

                        m_CMCamera.Follow = m_CurrentPlayer.transform;

                        m_StartScreen.gameObject.SetActive(false);
                        return;
                    }
                    break;
                case GameStates.PLAY:
                    break;
                case GameStates.WIN:
                    if (Input.GetKeyDown(KeyCode.Return))
                    {
                        RestartGame();
                        return;
                    }
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }



        }


        public void RestartGame()
        {
            SceneManager.LoadScene(0);
        }

        public void TriggerWin()
        {
            m_CurrentState = GameStates.WIN;
            m_WinScreen.gameObject.SetActive(true);
        }
    }
}

