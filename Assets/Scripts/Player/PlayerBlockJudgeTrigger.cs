using UnityEngine;

public enum Trigger
{
    up,
    down,
    left,
    right,
    other
}

public class PlayerBlockJudgeTrigger : MonoBehaviour
{
    //当前方向是否有障碍物
    public bool isBlock = false;
    //不同方向上的碰撞盒
    private Trigger trigger;
    //触发器位于的方向
    private Vector3 triggerForward = Vector3.zero;

    private GameObject player;
    //
    private bool isMove = true;
    //移动的量
    private int moveValue = 1;

    private void Start()
    {
        if (gameObject.name == "UpTrigger")
        {
            trigger = Trigger.up;
            triggerForward = new Vector3(-1, 0, 0);
        }
        if (gameObject.name == "DownTrigger")
        {
            trigger = Trigger.down;
            triggerForward = new Vector3(1, 0, 0);
        }
        if (gameObject.name == "LeftTrigger")
        {
            trigger = Trigger.left;
            triggerForward = new Vector3(0, 0, -1);
        }
        if (gameObject.name == "RightTrigger")
        {
            trigger = Trigger.right;
            triggerForward = new Vector3(0, 0, 1);
        }

        if (player == null)
        {
            player = GameObject.Find("Player");
        }
    }

    private void Update()
    {
        if (player.GetComponent<PlayerMove>().isLock && isMove)
        {
            if (player.GetComponent<PlayerMove>().willTrigger == trigger)
            {
                transform.Translate(triggerForward * moveValue, Space.World);
                isMove = false;
                isBlock = false;
            }
        }
        if (!player.GetComponent<PlayerMove>().isLock && !isMove)
        {
            transform.Translate(-triggerForward * moveValue, Space.World);
            isMove = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "UnitObject")
        {
            if (other.GetComponent<UnitObject>().UnitInfo.TryThisActionType(UnitActionType.Through) == false)
            {
                this.isBlock = true;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "UnitObject")
        {
            if (other.GetComponent<UnitObject>().UnitInfo.TryThisActionType(UnitActionType.Through) == false)
            {
                this.isBlock = false;
            }
        }
    }
}
