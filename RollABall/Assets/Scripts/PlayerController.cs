using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{   
    public float speed = 0;
    public TextMeshProUGUI countText;
    public GameObject winTextObject;
    public GameObject lostTextObject;

    public float health = 14;
    public float maxHealth = 14;
    public Image healthBar;
    public float deltaHealth =2;

    private Rigidbody rb; 
    private int count;
    private float movementX;
    private float movementY;

    public float timeRemaining;
    public bool timerIsRunning;
    public TextMeshProUGUI timeText;

    private bool gameIsOver;

    void Start()
    {   
        rb = GetComponent <Rigidbody>();
        count = 0; 

        SetCountText();
        winTextObject.SetActive(false);
        lostTextObject.SetActive(false);

        timerIsRunning = true;
        gameIsOver = false;
        timeRemaining = 60;
    }

    private void UpdateHealthBar(){
        healthBar.fillAmount = health / maxHealth;
    }

    void OnCollisionEnter(Collision collision){
        if (gameIsOver == false){
        if (collision.gameObject.CompareTag("Meteor")){
            SoundManager._instance.coinSource.PlayOneShot(SoundManager._instance.meteorClip);
                health -= deltaHealth;
                if (health <= 0){
                    gameIsOver = true;
                    lostTextObject.SetActive(true);
                    Invoke("GameEnd", 2f);
                }
            }
        }
    }

    private void GameEnd(){
        SceneManager.LoadSceneAsync(0);
    }

    void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();
        movementX = movementVector.x; 
        movementY = movementVector.y;  
    }

    void SetCountText()
    {   
        if (gameIsOver == false){
            countText.text = "Pontos: " + count.ToString();
            if (count >= 12){
                gameIsOver = true;
                winTextObject.SetActive(true);
                Invoke("GameEnd", 2f);
            }
        }
    }

    private void FixedUpdate() 
    {   
        Vector3 movement = new Vector3 (movementX, 0.0f, movementY);
        rb.AddForce(movement * speed); 
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PickUp"))
        {   
            SoundManager._instance.coinSource.PlayOneShot(SoundManager._instance.coinClip);
            count++;
            SetCountText();
            other.gameObject.SetActive(false);
        }
    }

    void Update(){
        if (timerIsRunning){
            UpdateHealthBar();
            if (timeRemaining >= 0){
                //position 
                if (transform.position.y < 0){
                    lostTextObject.SetActive(true);
                    Invoke("GameEnd", 2f);
                }
                timeRemaining -= Time.deltaTime;
                DisplayTime(timeRemaining);}
            else{
                if (gameIsOver == false){
                    timeRemaining = 0;
                    gameIsOver = true;
                    timerIsRunning = false;
                    //adicionar voce perdeu
                    lostTextObject.SetActive(true);
                    Invoke("GameEnd", 2f);
                }
            }
        }
    }

    void DisplayTime(float timeToDisplay){
        timeToDisplay += 1;
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
