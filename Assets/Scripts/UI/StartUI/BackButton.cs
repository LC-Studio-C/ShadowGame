using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackButton : MonoBehaviour
{
    private Button backButton;
    void Start()
    {
        backButton = GetComponent<Button>();
        backButton.onClick.AddListener(() => { UIManagerMethod.OpenUIPanel("StartPanel"); });
    }
}
