using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMControl : MonoBehaviour
{
    private void Start()
    {
        GetComponent<AudioSource>().Play();
    }
}
