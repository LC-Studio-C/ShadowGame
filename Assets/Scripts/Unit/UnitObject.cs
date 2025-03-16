using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitObject : MonoBehaviour
{
    [Header("�ƶ�״̬")]
    public UnitMoveState UnitMoveState;

    [Header("���Խ��еĻ����")]
    public bool isPushEnable = false;
    public bool isDestroyEnable = false;
    public bool isThroughEnable = false;

    [Header("�����")]
    public float PushEnergyCost = 0f;
    public float DestroyEnergyCost = 0f;
    public float ThroughEnergyCost = 0f;

    public UnitInfo UnitInfo;
    private void Awake()
    {
        if (this.gameObject.tag != "UnitObject")
        {
            this.gameObject.tag = "UnitObject";
        }
        this.UnitInfo = UnitMethod.CreateUnitInfo(UnitMoveState, PushEnergyCost, DestroyEnergyCost, ThroughEnergyCost);
        this.UnitInfo.SetUnitActionType(isPushEnable,isDestroyEnable,isThroughEnable);
    }
}
