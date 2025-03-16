using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitTrigger : MonoBehaviour
{
    //public UnitObject UnitObject = null;
    //public bool IsUnitTrigger = false;

    private GameObject player;
    private PlayerMove playerMove;

    public void InitUnitTrigger(GameObject _player,PlayerMove _playerMove)
    {
        this.player = _player;
        this.playerMove = _playerMove;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "UnitObject")
        {
            if (other.gameObject.GetComponent<UnitObject>().UnitInfo.GetUnitMoveState() != UnitMoveState.NotInteractive)
            {
                playerMove.UnitObject = other.gameObject.GetComponent<UnitObject>();
                playerMove.IsUnitTrigger = true;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "UnitObject")
        {
            if (other.gameObject.GetComponent<UnitObject>().UnitInfo.GetUnitMoveState() != UnitMoveState.NotInteractive)
            {
                playerMove.UnitObject = null;
                playerMove.IsUnitTrigger = false;
            }
        }
    }
}
