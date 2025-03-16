using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartButton : MonoBehaviour
{
    public GameObject LoadingText;
    public GameObject LoadingSlider;
    public GameObject TitleImage;
    public GameObject HelpButton;
    public GameObject ExitButton;

    private Button startButton;
    void Start()
    {
        startButton = GetComponent<Button>();
        startButton.onClick.AddListener(StartGame);

        LoadingSlider.SetActive(false);
        LoadingText.SetActive(false);
    }

    private void StartGame()
    {
        StartCoroutine(LoadGameScene());
        LoadingSlider.SetActive(true);
        LoadingText.SetActive(true);
        GetComponent<Image>().enabled = false;
        TitleImage.GetComponent<Image>().enabled = false;
        HelpButton.SetActive(false);
        ExitButton.SetActive(false);
    }

    private IEnumerator LoadGameScene()
    {
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync("GameScene");

        asyncOperation.allowSceneActivation = false;

        while (!asyncOperation.isDone)
        {
            float progress = Mathf.Clamp01(asyncOperation.progress / 0.9f);

            LoadingSlider.GetComponent<Slider>().value = progress;
            LoadingText.GetComponent<TMP_Text>().text = "正在载入..." + (progress * 100).ToString() + "%";

            if (asyncOperation.progress >= 0.9f)
            {
                LoadingText.GetComponent<TMP_Text>().text = "按任意键继续...";
                if (Input.anyKeyDown)
                {
                    //UIManagerMethod.CloseUIPanels();
                    UIManagerMethod.GetInstance().UIPanels.Clear();
                    asyncOperation.allowSceneActivation = true;
                }
            }
            yield return null;
        }
    }
}
