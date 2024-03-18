using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float horizontalSpeed = 5f;
    [SerializeField] private Transform[] wayTransforms;
    private Vector3 destination;
    private int currentWayIdx;
    private float maxSpeed = 15f;
    private float minSpeed = 5f;

    public int Lives = 3;
    public int Score = 0;
    public bool cheatOn = false;

    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text cheatText;
    [SerializeField] private Image heart1;
    [SerializeField] private Image heart2;
    [SerializeField] private Image heart3;
    [SerializeField] private GameManager gameManager;
    
    private void Start()
    {
        // CalculatePlayerPosition();
        moveSpeed = 0f;
        scoreText.text = "Score: 0";
        Lives = 3;
        currentWayIdx = 1;
        destination = wayTransforms[1].position;
    }


    private void Update()
    {
        // Cheat shortcut for the finish line
        if (Input.GetKeyDown(KeyCode.C))
        {
            ToggleCheat();
        }

        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            MoveLeft();
            transform.DOMoveX(destination.x, horizontalSpeed);
        }
        else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            MoveRight();
            transform.DOMoveX(destination.x, horizontalSpeed);
        }
        
        transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Obstacle"))
        {   
            if (Lives == 1)
            {
                moveSpeed = 0f;
                gameObject.GetComponent<Animator>().SetBool("IsDead", true);
                gameManager.FailGame();
            }
            else
            {
                moveSpeed = 5f;
                gameObject.GetComponent<Animator>().SetTrigger("IsHurt");
            }
            Lives--;
            UpdateLives();
        }
        else if (other.gameObject.CompareTag("Finish"))
        {
            moveSpeed = 0;
            gameManager.WinGame();
        }
        else if (other.gameObject.CompareTag("CoinGold"))
        {
            Score += 100;
            scoreText.text = "Score: " + Score;
        }
        else if (other.gameObject.CompareTag("CoinSilver"))
        {
            Score += 50;
            scoreText.text = "Score: " + Score;
        }
        else if (other.gameObject.CompareTag("SpeedUp"))
        {
            Score += 100;
            if (moveSpeed < maxSpeed)
            {
                moveSpeed += 5;
            }
        }
        else if (other.gameObject.CompareTag("SpeedDown"))
        {
            Score += 50;
            if (moveSpeed > minSpeed)
            {
                moveSpeed -= 5;
            }
        }
        else if (other.gameObject.CompareTag("Life"))
        {
            Score += 100;
            if (Lives == 3) return;
            Lives++;
            UpdateLives();
        }
    }


    private void MoveLeft()
    {
        if (currentWayIdx == 0) return;

        currentWayIdx--;
        destination = wayTransforms[currentWayIdx].position;
    }


    private void MoveRight() 
    {
        if (currentWayIdx == wayTransforms.Length - 1) return;

        currentWayIdx++;
        destination = wayTransforms[currentWayIdx].position;
    }


    private void UpdateLives()
    {
        switch (Lives) 
        {
            case 3:
                heart1.gameObject.SetActive(true);
                heart2.gameObject.SetActive(true);
                heart3.gameObject.SetActive(true);
                break;
            case 2:
                heart1.gameObject.SetActive(true);
                heart2.gameObject.SetActive(true);
                heart3.gameObject.SetActive(false);
                break;
            case 1:
                heart1.gameObject.SetActive(true);
                heart2.gameObject.SetActive(false);
                heart3.gameObject.SetActive(false);
                break;
            case 0:
                heart1.gameObject.SetActive(false);
                heart2.gameObject.SetActive(false);
                heart3.gameObject.SetActive(false);
                break;
        }
    }


    public void StartGame()
    {
        moveSpeed = 5f;
    }


    private void ToggleCheat()
    {
        cheatOn = !cheatOn;

        if (cheatOn)
        {
            cheatText.text = "Cheat: On";
            gameObject.GetComponent<Collider>().enabled = false;
            moveSpeed = 50f;
        }
        else 
        {
            cheatText.text = "Cheat: Off";
            gameObject.GetComponent<Collider>().enabled = true;
            moveSpeed = 5f;
        }
    }


    private void OnDestroy()
    {
        transform.DOKill();
    }
}
