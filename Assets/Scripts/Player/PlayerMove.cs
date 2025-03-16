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
    //player当前的移动状态
    [Header("移动状态")]
    [SerializeField]
    private PlayerState PlayerState = PlayerState.NormalMove;

    //player当前的移动方向
    [Header("移动方向")]
    [SerializeField]
    private PlayerMoveDirection PlayerMoveDirection = PlayerMoveDirection.Forward;

    //player当前的移动速度
    [Header("移动速度")]
    public float moveSpeed = 1f;

    //player当前的移动时间间隔，移动冷却
    [Header("移动时间间隔")]
    public float moveIntervalTime = 0f;

    //player的移动冷却计时器
    private float currentTime = 0f;
    private bool isMoveEnable = false;

    private GameObject player = null;

    //player的检测体
    private GameObject unitTrigger = null;
    //被检测的物体
    public UnitObject UnitObject = null;
    public bool IsUnitTrigger = false;
    //UnitObject的父物体
    private GameObject parentUnitObject = null;

    //碰撞盒
    private GameObject blockJudgeTrigger = null;
    //四个方向是否有障碍物的信息
    public bool[] isWitchBlock = new bool[] { false, false, false, false };
    public bool isBlock = false;
    //要改变的碰撞盒
    public Trigger willTrigger;
    //锁定移动状态下背后是否有障碍物
    private bool isBackBlock = false;
    //正常移动player的上下左右是否有障碍物
    //private bool isUpBlock = false;
    //private bool isDownBlock = false;
    //private bool isLeftBlock = false;
    //private bool isRightBlock = false;

    //player是否处于锁定移动状态
    public bool isLock = false;

    //玩家输入
    private bool isPlayerInput = false;

    //锁定移动状态下的前后移动
    private bool isForward = false;

    //玩家音频
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

        //找到检查体
        if (unitTrigger == null)
        {
            unitTrigger = GameObject.Find("UnitTrigger");
            if (unitTrigger == null)
            {
                Debug.Log("UnitTrigger不存在！");
            }
            else
            {
                unitTrigger.AddComponent<UnitTrigger>();
                unitTrigger.GetComponent<UnitTrigger>().InitUnitTrigger(player, this);
            }
        }

        //找到parentUnitObject
        if (parentUnitObject == null)
        {
            parentUnitObject = GameObject.Find("ParentUnitObject");
        }

        //找到碰撞盒
        if (blockJudgeTrigger == null)
        {
            blockJudgeTrigger = GameObject.Find("BlockJudgeTrigger");
        }

        GetComponent<AudioSource>().Stop();
    }

    private void Update()
    {
        //能量小于零玩家就不能移动
        if (EnergyMethod.IsMoveCondition() == false)
        {
            PlayerState = PlayerState.StopMove;
            UnitMethod.TryUnlockUnitObject(UnitObject, parentUnitObject);
            return;
        }

        //锁定物体
        PlayerState = UnitMethod.LockInputCheck(UnitObject, IsUnitTrigger);
        if (Input.GetMouseButtonDown(0))
        {
            UnitMethod.TryLockUnitObject(UnitObject, player);
        }
        if (Input.GetMouseButtonUp(0))
        {
            UnitMethod.TryUnlockUnitObject(UnitObject, parentUnitObject);
        }

        //四周是否有障碍物
        //PlayerAction.IsAroundBlockJudgeing(isWitchBlock, ref isUpBlock, ref isDownBlock, ref isLeftBlock, ref isRightBlock);

        //玩家输入
        isPlayerInput = PlayerAction.MoveInput(ref PlayerMoveDirection, PlayerState, ref isForward);
        //获取四方障碍物的信息
        PlayerAction.TriggerInfoTranslate(blockJudgeTrigger, ref isWitchBlock);
        //当前方向是否有障碍物
        isBlock = PlayerAction.IsBlockJudgeing(PlayerMoveDirection, PlayerState, ref willTrigger, ref isLock, ref isBackBlock, isWitchBlock);
        //玩家移动冷却是否完成
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

        //尝试销毁物体
        isDestroy =  UnitMethod.TryDestroyUnitObject(UnitObject, parentUnitObject);
        if (isDestroy)
        {
            GetComponent<AudioSource>().clip = DestroyUnitClip;
            GetComponent<AudioSource>().Play();
            isDestroy = false;
        }
    }
}
