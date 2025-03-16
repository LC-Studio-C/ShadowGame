using UnityEngine;

public enum PlayerState
{
    NormalMove,
    LockMove,
    StopMove
}

public enum PlayerMoveDirection
{
    Forward,
    Backward,
    Left,
    Right
}

public class PlayerMove : MonoBehaviour
{
    //player��ǰ���ƶ�״̬
    [Header("�ƶ�״̬")]
    [SerializeField]
    private PlayerState PlayerState = PlayerState.NormalMove;

    //player��ǰ���ƶ�����
    [Header("�ƶ�����")]
    [SerializeField]
    private PlayerMoveDirection PlayerMoveDirection = PlayerMoveDirection.Forward;

    //player��ǰ���ƶ��ٶ�
    [Header("�ƶ��ٶ�")]
    public float moveSpeed = 1f;

    //player��ǰ���ƶ�ʱ�������ƶ���ȴ
    [Header("�ƶ�ʱ����")]
    public float moveIntervalTime = 0f;

    //player���ƶ���ȴ��ʱ��
    private float currentTime = 0f;
    private bool isMoveEnable = false;

    private GameObject player = null;

    //player�ļ����
    private GameObject unitTrigger = null;
    //����������
    public UnitObject UnitObject = null;
    public bool IsUnitTrigger = false;
    //UnitObject�ĸ�����
    private GameObject parentUnitObject = null;

    //��ײ��
    private GameObject blockJudgeTrigger = null;
    //�ĸ������Ƿ����ϰ������Ϣ
    public bool[] isWitchBlock = new bool[] { false, false, false, false };
    public bool isBlock = false;
    //Ҫ�ı����ײ��
    public Trigger willTrigger;
    //�����ƶ�״̬�±����Ƿ����ϰ���
    private bool isBackBlock = false;
    //�����ƶ�player�����������Ƿ����ϰ���
    //private bool isUpBlock = false;
    //private bool isDownBlock = false;
    //private bool isLeftBlock = false;
    //private bool isRightBlock = false;

    //player�Ƿ��������ƶ�״̬
    public bool isLock = false;

    //�������
    private bool isPlayerInput = false;

    //�����ƶ�״̬�µ�ǰ���ƶ�
    private bool isForward = false;

    //�����Ƶ
    public AudioClip MoveClip = null;
    public AudioClip NoMoveableClip = null;
    public AudioClip DestroyUnitClip = null;

    private bool isDestroy =false;

    private void Start()
    {
        if (player == null)
        {
            if (this.gameObject.name == "Player" && this.gameObject.tag == "Player")
            {
                player = this.gameObject;
            }
            else
            {
                player = GameObject.Find("Player");
            }
        }
        player.transform.rotation = Quaternion.Euler(Vector3.zero);

        //�ҵ������
        if (unitTrigger == null)
        {
            unitTrigger = GameObject.Find("UnitTrigger");
            if (unitTrigger == null)
            {
                Debug.Log("UnitTrigger�����ڣ�");
            }
            else
            {
                unitTrigger.AddComponent<UnitTrigger>();
                unitTrigger.GetComponent<UnitTrigger>().InitUnitTrigger(player, this);
            }
        }

        //�ҵ�parentUnitObject
        if (parentUnitObject == null)
        {
            parentUnitObject = GameObject.Find("ParentUnitObject");
        }

        //�ҵ���ײ��
        if (blockJudgeTrigger == null)
        {
            blockJudgeTrigger = GameObject.Find("BlockJudgeTrigger");
        }

        GetComponent<AudioSource>().Stop();
    }

    private void Update()
    {
        //����С������ҾͲ����ƶ�
        if (EnergyMethod.IsMoveCondition() == false)
        {
            PlayerState = PlayerState.StopMove;
            UnitMethod.TryUnlockUnitObject(UnitObject, parentUnitObject);
            return;
        }

        //��������
        PlayerState = UnitMethod.LockInputCheck(UnitObject, IsUnitTrigger);
        if (Input.GetMouseButtonDown(0))
        {
            UnitMethod.TryLockUnitObject(UnitObject, player);
        }
        if (Input.GetMouseButtonUp(0))
        {
            UnitMethod.TryUnlockUnitObject(UnitObject, parentUnitObject);
        }

        //�����Ƿ����ϰ���
        //PlayerAction.IsAroundBlockJudgeing(isWitchBlock, ref isUpBlock, ref isDownBlock, ref isLeftBlock, ref isRightBlock);

        //�������
        isPlayerInput = PlayerAction.MoveInput(ref PlayerMoveDirection, PlayerState, ref isForward);
        //��ȡ�ķ��ϰ������Ϣ
        PlayerAction.TriggerInfoTranslate(blockJudgeTrigger, ref isWitchBlock);
        //��ǰ�����Ƿ����ϰ���
        isBlock = PlayerAction.IsBlockJudgeing(PlayerMoveDirection, PlayerState, ref willTrigger, ref isLock, ref isBackBlock, isWitchBlock);
        //����ƶ���ȴ�Ƿ����
        isMoveEnable = PlayerAction.MoveInterval(ref currentTime, moveIntervalTime);
        if (isPlayerInput && isMoveEnable)
        {
            PlayerAction.PlayerMove(PlayerMoveDirection, PlayerState, moveSpeed, player, blockJudgeTrigger, isBlock, isForward, isBackBlock);
            currentTime = 0f;
            isPlayerInput = false;
            isMoveEnable = false;

            if (PlayerState == PlayerState.LockMove)
            {
                EnergyMethod.SetEnergyNumber(UnitObject.UnitInfo.PushEnergyCost);
            }
        }

        //������������
        isDestroy =  UnitMethod.TryDestroyUnitObject(UnitObject, parentUnitObject);
        if (isDestroy)
        {
            GetComponent<AudioSource>().clip = DestroyUnitClip;
            GetComponent<AudioSource>().Play();
            isDestroy = false;
        }
    }
}
