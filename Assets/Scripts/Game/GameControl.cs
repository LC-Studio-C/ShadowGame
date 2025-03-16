using UnityEngine;

public enum GameState
{
    Ready,
    Run,
    Pause,
    Win,
    Fail
}

public class GameControl : MonoBehaviour
{
    [Header("��Ϸ״̬")]
    public static GameState GameState;

    [Header("����������")]
    public GameObject WinTrigger = null;
    public GameObject FailTrigger = null;

    [Header("ʧ�ܴ������ٶȿ���")]
    public float MoveSpeed = 1.0f;

    [Header("������Ϣ")]
    public float MaxEnergyNumber = 50f;//�������ֵ
    //��ǰ����ֵ
    [SerializeField]
    private float currentEnergyNumber = 0f;
    //��ǰ����ֵ��װ
    [HideInInspector]
    public float CurrentEnergyNumber
    {
        get
        {
            return currentEnergyNumber;
        }
        set
        {
            if (value >= MaxEnergyNumber)
            {
                currentEnergyNumber = MaxEnergyNumber;
                return;
            }
            if (value <= 0f)
            {
                currentEnergyNumber = 0f;
                return;
            }
            currentEnergyNumber = value;
        }
    }
    //�ָ�����ֵ
    public float EnergyRecoverNumber = 1f;
    //�ָ��������ʱ��
    public float RecoverIntervalTime = 1f;
    //�ָ���ȴ��ʱ
    public float RecoverTime = 0f;

    private static GameControl instance;

    //UI
    [Header("UI")]
    public GameObject PlayingPanel;
    public GameObject PausePanel;
    public GameObject WinPanel;
    public GameObject FailPanel;

    //player
    [Header("Player")]
    public GameObject Player;

    //��ǰ��Ϸ״̬�Ƿ�Ϊ��ͣ
    private bool isPause = false;

    private void Awake()
    {
        GameState = GameState.Ready;
        if (instance != null)
        {
            Destroy(instance);
        }
        instance = this;
    }

    private void Start()
    {
        GameState = GameState.Run;
        UIManagerMethod.AddUIPanel("PlayingPanel", PlayingPanel);
        UIManagerMethod.AddUIPanel("PausePanel", PausePanel);
        UIManagerMethod.AddUIPanel("WinPanel", WinPanel);
        UIManagerMethod.AddUIPanel("FailPanel", FailPanel);
        UIManagerMethod.OpenUIPanel("PlayingPanel");
        if (WinTrigger == null)
        {
            if (GameObject.Find("WinTrigger") != null)
            {
                this.WinTrigger = GameObject.Find("WinTrigger");
            }
            else
            {
                Debug.Log("������û��WinTrigger!");
            }
        }
        if (FailTrigger == null)
        {
            if (GameObject.Find("FailTrigger") != null)
            {
                this.FailTrigger = GameObject.Find("FailTrigger");
            }
            else
            {
                Debug.Log("������û��FailTrigger!");
            }
        }

        if (WinTrigger != null && FailTrigger != null)
        {
            WinTrigger.AddComponent<WinTrigger>();
            FailTrigger.AddComponent<FailTrigger>();
            FailTrigger.GetComponent<FailTrigger>().fairTriggerMoveSpeed = this.MoveSpeed;
        }

        currentEnergyNumber = MaxEnergyNumber;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPause)
            {
                GameState = GameState.Run;
            }
            else
            {
                GameState = GameState.Pause;
            }
        }

        switch (GameState)
        {
            case GameState.Run:
                Run();
                break;
            case GameState.Win:
                Win();
                break;
            case GameState.Fail:
                Fail();
                break;
            case GameState.Pause:
                Pause();
                break;
        }

        EnergyMethod.SetEnergyRecoverNumber(ref RecoverTime, RecoverIntervalTime, EnergyRecoverNumber);
    }

    private void Run()
    {
        isPause = false;
        Time.timeScale = 1.0f;
        UIManagerMethod.OpenUIPanel("PlayingPanel");
        Player.GetComponent<PlayerMove>().enabled = true;
    }

    private void Win()
    {
        UIManagerMethod.OpenUIPanel("WinPanel");
        Player.GetComponent<PlayerMove>().enabled = false;
        Time.timeScale = 0f;
    }

    private void Fail()
    {
        UIManagerMethod.OpenUIPanel("FailPanel");
        Player.GetComponent<PlayerMove>().enabled = false;
        Time.timeScale = 0f;
    }

    private void Pause()
    {
        isPause = true;
        UIManagerMethod.OpenUIPanel("PausePanel");
        Player.GetComponent<PlayerMove>().enabled = false;
        Time.timeScale = 0f;
    }

    public static GameControl GetInstance()
    {
        return instance;
    }
}
