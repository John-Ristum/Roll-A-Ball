using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowlingPin : MonoBehaviour
{
    bool knockedOver = false;
    PlayerController playerController;

    // Start is called before the first frame update
    void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.up.y < 0.5f && !knockedOver)
        {
            playerController.PinFall();
            knockedOver = true;
        }
    }
}
