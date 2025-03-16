using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefaultUnitInfo : UnitInfo
{
    public DefaultUnitInfo(UnitMoveState _unitMoveState, float _pushEnergyCost, float _destroyEnergyCost, float _throughEnergyCost) : 
        base(_unitMoveState, _pushEnergyCost, _destroyEnergyCost, _throughEnergyCost)
    {

    }
}
