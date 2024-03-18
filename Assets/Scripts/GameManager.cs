using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private GameObject tapToStartPanel;
    [SerializeField] private GameObject failPanel;
    [SerializeField] private GameObject winPanel;
    int currentSceneIdx = 0;

    private void Start()
    {
        currentSceneIdx = SceneManager.GetActiveScene().buildIndex;
        tapToStartPanel.SetActive(true);
        failPanel.SetActive(false);
        winPanel.SetActive(false);
    }

    public void StartGame()
    {
        tapToStartPanel.SetActive(false);
        player.StartGame();
    }

    public void FailGame()
    {
        failPanel.SetActive(true);
    }

    public void WinGame()
    {
        winPanel.SetActive(true);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(currentSceneIdx);
    }

    public void NextLevel()
    {
        currentSceneIdx++;
        SceneManager.LoadScene(currentSceneIdx);
    }

    public void PrevLevel() 
    {
        currentSceneIdx--;
        SceneManager.LoadScene(currentSceneIdx);
    }

}
