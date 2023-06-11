using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameUI : MonoBehaviour
{
    GameManager gameplayManager;

    public TextMeshProUGUI ScoreDisplay, CoinsHUD;
    public Slider FuelBar;
    public GameObject GamePlayPanel;
    public GameObject MainMenuPanel;
    public GameObject PausePanel;

    public GameObject GameOverPanel;
    public TextMeshProUGUI endScore, endCoins;

    public GameObject QuickTipPanel;
    public TextMeshProUGUI QuickTipsText;
    [SerializeField]
    string[] quickTips;
    

    bool hasRun;

    // Start is called before the first frame update
    void Start()
    {
        gameplayManager = GetComponent<GameManager>();
        MainMenuPanel.SetActive(true);
        PausePanel.SetActive(false);
        GamePlayPanel.SetActive(false);
        GameOverPanel.SetActive(false);

        QuickTipsText.text = quickTips[Random.Range(0, quickTips.Length)];
    }

    // Update is called once per frame
    void Update()
    {
        ScoreDisplay.text = "" + Mathf.RoundToInt(gameplayManager.Score);
        CoinsHUD.text = gameplayManager.Player.Coins.ToString();
        FuelBar.value = gameplayManager.Player.Fuel;

        if(gameplayManager.gameOver && !hasRun)
        {
            hasRun = true;
            StartCoroutine(GameOver());
        }
    }

    public void PauseButton()
    {
        Time.timeScale = 0;
        PausePanel.SetActive(true);
        GamePlayPanel.SetActive(false);
        QuickTipPanel.SetActive(false);

    }

    public void ResumeButton()
    {
        Time.timeScale = 1;
        PausePanel.SetActive(false);
        GamePlayPanel.SetActive(true);
    }

    public void MainMenuButton()
    {
        SceneManager.LoadScene(0);
    }

    public IEnumerator PlayButton()
    {
       
        yield return new WaitForSeconds(0.6f);

        MainMenuPanel.SetActive(false);
        GamePlayPanel.SetActive(true);
        StartCoroutine(gameplayManager.StartGame());
    }

    public IEnumerator GameOver()
    {
        yield return new WaitForSeconds(3f);

        GamePlayPanel.SetActive(false);
        GameOverPanel.SetActive(true);

        endScore.text = "" + Mathf.RoundToInt(gameplayManager.Score);
        endCoins.text = "" + gameplayManager.Player.Coins;



    }
}
