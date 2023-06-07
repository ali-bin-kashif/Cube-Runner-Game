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

    // Start is called before the first frame update
    void Start()
    {
        gameplayManager = GetComponent<GameManager>();
        MainMenuPanel.SetActive(true);
        PausePanel.SetActive(false);
        GamePlayPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        ScoreDisplay.text = "" + Mathf.RoundToInt(gameplayManager.Score);
        CoinsHUD.text = gameplayManager.Player.Coins.ToString();
        FuelBar.value = gameplayManager.Player.Fuel;
    }

    public void PauseButton()
    {
        Time.timeScale = 0;
        PausePanel.SetActive(true);
        GamePlayPanel.SetActive(false);

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
}
