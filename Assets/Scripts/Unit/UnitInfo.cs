using System.Collections.Generic;
using UnityEngine;

public enum UnitMoveState
{
    NotInteractive,
    Ready,
    Lock
}

public enum UnitActionType
{
    Push,
    Destroy,
    Through
}

public abstract class UnitInfo
{
    private UnitMoveState unitMoveState = UnitMoveState.NotInteractive;

    public readonly float PushEnergyCost = 0f;
    public readonly float DestroyEnergyCost = 0f;
    public readonly float ThroughEnergyCost = 0f;

    protected List<UnitActionType> unitActionTypes = new List<UnitActionType>();

    protected UnitInfo(UnitMoveState _unitMoveState, float _pushEnergyCost, float _destroyEnergyCost, float _throughEnergyCost)
    {
        this.unitMoveState = _unitMoveState;

        this.PushEnergyCost = _pushEnergyCost;
        this.DestroyEnergyCost = _destroyEnergyCost;
        this.ThroughEnergyCost = _throughEnergyCost;
    }

    public void SetUnitMoveState(UnitMoveState _unitMoveState)
    {
        if (_unitMoveState == UnitMoveState.NotInteractive)
        {
            Debug.LogWarning("不可更改运动状态");
            return;
        }
        this.unitMoveState = _unitMoveState;
    }

    public UnitMoveState GetUnitMoveState()
    {
        return this.unitMoveState;
    }

    public void SetUnitActionType(bool isPushEnable, bool isDestroyEnable, bool isThroughEnable)
    {
        if (this.unitMoveState == UnitMoveState.NotInteractive)
        {
            unitActionTypes.Clear();
        }
        unitActionTypes.Clear();

        if (isPushEnable)
        {
            unitActionTypes.Add(UnitActionType.Push);
        }
        if (isDestroyEnable)
        {
            unitActionTypes.Add(UnitActionType.Destroy);
        }
        if (isThroughEnable)
        {
            unitActionTypes.Add(UnitActionType.Through);
        }
    }

    public bool TryThisActionType(UnitActionType _unitActionType)
    {
        if (unitActionTypes.Contains(_unitActionType))
        {
            return true;
        }
        return false;
    }
}
