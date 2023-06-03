using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameUI : MonoBehaviour
{
    GameManager gameplayManager;

    public TextMeshProUGUI ScoreDisplay, CoinsHUD;
    public Slider FuelBar;
    public GameObject GamePlayPanel;
    public GameObject MainMenuPanel;

    // Start is called before the first frame update
    void Start()
    {
        gameplayManager = GetComponent<GameManager>();
        MainMenuPanel.SetActive(true);
        GamePlayPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        ScoreDisplay.text = "" + Mathf.RoundToInt(gameplayManager.Score);
        CoinsHUD.text = gameplayManager.Player.Coins.ToString();
        FuelBar.value = gameplayManager.Player.Fuel;
    }

    public IEnumerator PlayButton()
    {
       
        yield return new WaitForSeconds(0.6f);

        MainMenuPanel.SetActive(false);
        GamePlayPanel.SetActive(true);
        StartCoroutine(gameplayManager.StartGame());
    }
}
