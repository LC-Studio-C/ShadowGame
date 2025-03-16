using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CurrentEnergyDisplay : MonoBehaviour
{
    private TMP_Text textMesh;
    void Start()
    {
        textMesh = GetComponent<TMP_Text>();
    }

    void Update()
    {
        textMesh.text = GameControl.GetInstance().CurrentEnergyNumber.ToString();
    }
}
