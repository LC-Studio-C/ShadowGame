using UnityEngine;
using UnityEngine.UI;

public class HelpButton : MonoBehaviour
{
    private Button helpButton;
    private void Start()
    {
        helpButton = GetComponent<Button>();
        helpButton.onClick.AddListener(() => { UIManagerMethod.OpenUIPanel("HelpPanel"); });
    }
}
