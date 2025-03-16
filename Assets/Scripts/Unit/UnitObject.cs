using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitObject : MonoBehaviour
{
    [Header("移动状态")]
    public UnitMoveState UnitMoveState;

    [Header("可以进行的活动类型")]
    public bool isPushEnable = false;
    public bool isDestroyEnable = false;
    public bool isThroughEnable = false;

    [Header("活动消耗")]
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
