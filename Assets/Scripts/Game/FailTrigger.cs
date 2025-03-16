using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class FailTrigger : MonoBehaviour
{
    public float fairTriggerMoveSpeed;

    private void Start()
    {
        if (GetComponent<BoxCollider>().isTrigger == false)
        {
            GetComponent<BoxCollider>().isTrigger = true;
        }
    }

    private void Update()
    {
        this.transform.position += transform.forward * fairTriggerMoveSpeed * Time.deltaTime;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            GameControl.GameState = GameState.Fail;
        }
    }
}
