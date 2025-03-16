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
    //��ǰ�����Ƿ����ϰ���
    public bool isBlock = false;
    //��ͬ�����ϵ���ײ��
    private Trigger trigger;
    //������λ�ڵķ���
    private Vector3 triggerForward = Vector3.zero;

    private GameObject player;
    //
    private bool isMove = true;
    //�ƶ�����
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
