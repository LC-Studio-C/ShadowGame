using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public static class UIManagerMethod
{
    public static UIManager GetInstance()
    {
        return UIManager.Instance;
    }

    public static void AddUIPanel(string panelName,GameObject panel)
    {
        UIManagerMethod.GetInstance().UIPanels.Add(panelName, panel);
    }

    public static bool TryPanelInUIPanels(string panelName)
    {
        if (UIManagerMethod.GetInstance().UIPanels.ContainsKey(panelName))
        {
            return true;
        }
        return false;
    }

    public static void OpenUIPanel(string panelName)
    {
        if (UIManagerMethod.TryPanelInUIPanels(panelName))
        {
            foreach (var panel in UIManagerMethod.GetInstance().UIPanels.Values)
            {
                panel.SetActive(panel.name == panelName);
            }
        }
    }

    public static void CloseUIPanels()
    {
        foreach (var panel in UIManagerMethod.GetInstance().UIPanels.Values)
        {
            panel.SetActive(false);
        }
    }

    /*public static void OtherSceneOpenPanel(string panelName)
    {
        if (UIManagerMethod.TryPanelInUIPanels(panelName))
        {
            foreach (var panel in UIManagerMethod.GetInstance().UIPanels.Values)
            {
                if (panel.name == panelName)
                {
                    panel.SetActive(true);
                }
            }
        }
    }*/

    public static void RemoveUIPanel(string panelName)
    {
        UIManagerMethod.GetInstance().UIPanels.Remove(panelName);
    }
}
