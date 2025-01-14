using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    public float jumpForce;

    float xInput;
    float yInput;

    Rigidbody rb;

    int coinsCollected;
    public int totalCoins; 

    public AudioClip coinSound;
    private AudioSource audioSource;

    public float gameDuration = 60f; 
    private float timeRemaining;

    public TMP_Text timerText; 
    public TMP_Text endGameText; 

    private bool gameEnded = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
        timeRemaining = gameDuration;

        endGameText.text = ""; 
    }

    private void Update()
    {
        if (gameEnded) return;

        timeRemaining -= Time.deltaTime;
        if (timeRemaining <= 0)
        {
            timeRemaining = 0;
            EndGame(); 
        }

        UpdateTimerUI();

        xInput = Input.GetAxis("Horizontal");
        yInput = Input.GetAxis("Vertical");

        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, jumpForce, rb.linearVelocity.z);
        }

        if (transform.position.y <= -5f)
        {
            SceneManager.LoadScene(0); 
        }
    }

    private void FixedUpdate()
    {
        if (!gameEnded)
        {
            rb.AddForce(xInput * moveSpeed, 0, yInput * moveSpeed);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Coin")
        {
            audioSource.PlayOneShot(coinSound);
            coinsCollected++;
            other.gameObject.SetActive(false);

            if (coinsCollected >= totalCoins)
            {
                EndGame();
            }
        }
    }

    private void EndGame()
    {
        gameEnded = true;
        rb.linearVelocity = Vector3.zero; 

        if (coinsCollected >= totalCoins)
        {
            endGameText.text = "You Won!";
        }
        else
        {
            endGameText.text = "Game Over!";
        }

        Invoke("RestartGame", 5f);
    }

    private void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void UpdateTimerUI()
    {
        timerText.text = "Time: " + Mathf.Ceil(timeRemaining).ToString() + "s";
    }
}
