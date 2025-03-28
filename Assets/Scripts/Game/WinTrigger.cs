using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class WinTrigger : MonoBehaviour
{
    private void Start()
    {
        if (GetComponent<BoxCollider>().isTrigger == false)
        {
            GetComponent<BoxCollider>().isTrigger = true;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            GameControl.GameState = GameState.Win;
        }
    }
}
