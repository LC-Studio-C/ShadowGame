using UnityEngine;

public class UnitMethod
{
    public static UnitInfo CreateUnitInfo(UnitMoveState unitMoveState, float PushEnergyCost, float DestroyEnergyCost, float ThroughEnergyCost)
    {
        return new DefaultUnitInfo(unitMoveState, PushEnergyCost, DestroyEnergyCost, ThroughEnergyCost);
    }

    public static PlayerState LockInputCheck(UnitObject unitObject, bool isTrigger)
    {
        if (Input.GetMouseButton(0))
        {
            if (unitObject != null && isTrigger)
            {
                return PlayerState.LockMove;
            }
            else
            {
                return PlayerState.NormalMove;
            }
        }
        else
        {
            return PlayerState.NormalMove;
        }
    }

    public static bool TryLockUnitObject(UnitObject unitObject, GameObject player)
    {
        if (unitObject == null)
        {
            return false;
        }
        if (unitObject.UnitInfo.TryThisActionType(UnitActionType.Push) == false)
        {
            return false;
        }
        if (unitObject.UnitInfo.GetUnitMoveState() == UnitMoveState.Ready)
        {
            unitObject.UnitInfo.SetUnitMoveState(UnitMoveState.Lock);
            unitObject.gameObject.transform.SetParent(player.transform);
            return true;
        }
        Debug.Log("Ëø¶¨UnitObjectÊ§°Ü!");
        return false;
    }

    public static bool TryUnlockUnitObject(UnitObject unitObject, GameObject parentUnitObject)
    {
        if (unitObject == null)
        {
            return false;
        }
        if (unitObject.UnitInfo.GetUnitMoveState() == UnitMoveState.Lock)
        {
            unitObject.UnitInfo.SetUnitMoveState(UnitMoveState.Ready);
            unitObject.gameObject.transform.SetParent(parentUnitObject.transform);
            return true;
        }
        return false;
    }

    public static bool TryDestroyUnitObject(UnitObject unitObject,GameObject parentUnitObject)
    {
        if (unitObject != null)
        {
            if (unitObject.UnitInfo.TryThisActionType(UnitActionType.Destroy) == true)
            {
                if (Input.GetMouseButtonDown(1))
                {
                    TryUnlockUnitObject(unitObject, parentUnitObject);
                    unitObject.gameObject.transform.position = new Vector3(10000, 10000, 10000);
                    EnergyMethod.SetEnergyNumber(unitObject.UnitInfo.DestroyEnergyCost);
                    return true;
                }
            }
        }
        return false;
    }
}
