using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody rb;
    public float speed = 1;
    int pickupCount;
    int totalPickups;
    Timer timer;
    bool wonGame = false;

    [Header("UI")] //adds header in the inspecter
    public GameObject winPanel;
    public TMP_Text winTime;
    public GameObject inGamePanel;
    public TMP_Text timerText;
    public TMP_Text pickupText;

    GameObject resetPoint;
    bool resetting = false;
    Color originalColor;

    void Start()
    {
        Time.timeScale = 1;
        //Get the Rigidbidy component of the gameObject
        rb = GetComponent<Rigidbody>();
        //Get the number of pickups in our scene
        totalPickups = GameObject.FindGameObjectsWithTag("Pickup").Length;
        //set pickup count to total
        //pickupCount = totalPickups;
        CheckPickups();
        //Get the timer object
        timer = FindObjectOfType<Timer>();
        timer.StartTimer();
        //turn off our win panel
        //winPanel.SetActive(false);
        //turn on our in game pannel
        inGamePanel.SetActive(true);

        resetPoint = GameObject.Find("Reset Point");
        originalColor = GetComponent<Renderer>().material.color;
    }

    void LateUpdate()
    {
        if (resetting)
            return;

        if (wonGame == true)
            return;

        //Get the input value from our horizontal axis
        float moveHorizontal = Input.GetAxis("Horizontal");
        //Get the input value from our vertical axis
        float moveVertical = Input.GetAxis("Vertical");

        //Create a new vector 3 based on the horizontal and vertical values
        Vector3 movement = new Vector3(moveHorizontal, 0, moveVertical);

        //Add force to the rigidbody based on our movement vector
        rb.AddForce(movement * speed * Time.deltaTime);

        //Get TimerText to show time
        timerText.text = "Time: " + timer.GetTime().ToString("F3");
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Respawn"))
        {
            StartCoroutine(ResetPlayer());
        }
    }

    public IEnumerator ResetPlayer()
    {
        resetting = true;
        GetComponent<Renderer>().material.color = Color.black;
        rb.velocity = Vector3.zero;
        Vector3 startPos = transform.position;
        float resetSpeed = 2f;
        var i = 0.0f;
        var rate = 1.0f / resetSpeed;
        while (i < 1.0f)
        {
            i += Time.deltaTime * rate;
            transform.position = Vector3.Lerp(startPos, resetPoint.transform.position, i);
            yield return null;
        }
        GetComponent<Renderer>().material.color = originalColor;
        resetting = false;
    }

    void OnTriggerEnter(Collider other)
    {
        //If the other object contains the Pickup tag, destroy it
        if(other.CompareTag("Pickup"))
        {
            Destroy(other.gameObject);
            //Decriment the pickup count
            pickupCount += 1;
            CheckPickups();
        }
    }

    void CheckPickups()
    {
        //Debug.Log(pickupCount + " more remaining");
        //Get PickupText to show the number of pickups remaining
        pickupText.text = "Pickups: " + pickupCount + " / " + totalPickups;

        if (pickupCount == totalPickups)
        {
            WinGame();
        }
    }


    void WinGame()
    {
        wonGame = true;
        timer.StopTimer();
        //Debug.Log("YOU WIN! Time: " + timer.GetTime().ToString("F3"));
        //turn off our in game pannel
        inGamePanel.SetActive(false);
        //Set the timer on the text
        winTime.text = "Time: " + timer.GetTime().ToString("F3");
        //Turn on our win panel
        winPanel.SetActive(true);

        //Set te velocity of the ridgidbody to zero
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
    }

    //Temporary - Remove when doing moduals in A2

    public void RestartGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }

}
