using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject[] plugs, cameras, nodes;
    public GameObject player;
    public Canvas pauseMenu;
    public Slider detectionBar;
    public Text winText, loseText, scoreText;

    private int terminals = 0, maxTerminals = 0, random;
    private float detection = 100;
    private float detectionSpeed = 0.25f;

    // Start is called before the first frame update
    void Start()
    {
        int plugSeed = Random.Range(0, 4);
        // Debug.Log(plugSeed);
        plugs[plugSeed].gameObject.SetActive(false);
        for (int x = 0; x < cameras.Length; x++)
        {
            random = Random.Range(0, 2);
            if (random == 0)
            {
                cameras[x].SetActive(false);
            }
        }
        for (int x = 0; x < nodes.Length; x++)
        {
            random = Random.Range(0, 2);
            if (random == 0)
            {
                nodes[x].SetActive(false);
            } else
            {
                maxTerminals += 1;
            }
        }
        scoreText.text = "Terminals Accessed\n" + terminals + "/" + maxTerminals;
    }

    // Update is called once per frame
    void Update()
    {
        detection -= detectionSpeed * Time.deltaTime;
        if (detection <= 0)
        {
            loseGame();
        } else
        {
            detectionBar.value = detection;
        }


        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pauseGame();
        }
    }

    public void pauseGame()
    {
        if (Time.timeScale == 1)
        {
            Time.timeScale = 0;
            player.GetComponent<PlayerController>().setLookSpeed(0f, 0f);
            pauseMenu.gameObject.SetActive(true);
        }
        else if (Time.timeScale == 0)
        {
            Time.timeScale = 1;
            player.GetComponent<PlayerController>().setLookSpeed(5f, 5f);
            pauseMenu.gameObject.SetActive(false);
        }
    }

    public void restartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        pauseGame();
    }

    public void quitGame()
    {
        Application.Quit();
    }

    public void winGame()
    {
        pauseGame();
        //Debug.Log("WIN");
        winText.gameObject.SetActive(true);
    }

    public void loseGame()
    {
        pauseGame();
        //Debug.Log("LOSE");
        loseText.gameObject.SetActive(true);
    }

    public void setDetectionSpeed(float x)
    {
        detectionSpeed = x;
    }

    public void terminalAccessed()
    {
        terminals += 1;
        scoreText.text = "Terminals Accessed\n" + terminals + "/" + maxTerminals;
    }
}
