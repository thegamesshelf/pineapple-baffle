using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;
//access ui elements
using UnityEngine.UI;

public class gameOverController : MonoBehaviour
{
    [SerializeField] GameObject gameOverScreen;
    [SerializeField] GameObject player;
    [SerializeField] GameObject UICanvas;
    [SerializeField] Text secondsSurvivedUI;
    public bool gameOver = false;
    void Start()
    {
        FindObjectOfType<eggController>().eggsCollected += OnGameOver;
        gameOverScreen.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        if (gameOver)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene(1);
            }
        }

    }
    void OnGameOver()
    {
        //Destroy(player);
        player.SetActive(false);
        UICanvas.SetActive(false);
        gameOverScreen.SetActive(true);
        secondsSurvivedUI.text = "Score: "+(Time.timeSinceLevelLoad).ToString("F4") + " sec";
        Highscores.AddNewHighscore(levelLoader.playerNameSTR, Mathf.RoundToInt(float.Parse(Time.timeSinceLevelLoad.ToString("F4"))*100000f));
        gameOver = true;
    }
}
