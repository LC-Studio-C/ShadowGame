using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonClickMethod : MonoBehaviour
{
    public void BackButton()
    {
        GameControl.GameState = GameState.Run;
        UIManagerMethod.OpenUIPanel("PlayingPanel");
    }

    public void ExitButton()
    {
        StartCoroutine(LoadStartScene());
    }

    private IEnumerator LoadStartScene()
    {
        UIManagerMethod.GetInstance().UIPanels.Clear();
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync("StartScene");
        asyncOperation.allowSceneActivation = true;
        yield return null;
    }

    public void ReStartButton()
    {
        UIManagerMethod.GetInstance().UIPanels.Clear();
        SceneManager.LoadScene("GameScene");
    }

    public void IsBGMPlay()
    {
        GameObject bgm = GameObject.Find("BGM");
        GameObject isPlayingButton = GameObject.Find("IsPlayingButton");
        if (bgm.GetComponent<AudioSource>().isPlaying)
        {
            bgm.GetComponent<AudioSource>().Stop();
            isPlayingButton.GetComponent<Image>().color = Color.gray;
        }
        else
        {
            bgm.GetComponent<AudioSource>().Play();
            isPlayingButton.GetComponent<Image>().color = Color.white;
        }
    }
}
