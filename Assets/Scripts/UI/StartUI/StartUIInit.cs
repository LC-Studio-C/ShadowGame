using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartUIInit : MonoBehaviour
{
    public GameObject StartPanel;
    public GameObject HelpPanel;

    private void Start()
    {
        UIManagerMethod.AddUIPanel("StartPanel", StartPanel);
        UIManagerMethod.AddUIPanel("HelpPanel", HelpPanel);
    }
}
