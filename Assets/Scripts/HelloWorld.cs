using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class HelloWorld : MonoBehaviour
{
    public string winMessage = "You Win";
    int counter = 0;
    public int winAmount = 5;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            counter++;
            if(counter >= winAmount)
            {
                ShowMessage();
            }
        }
    }

    void ShowMessage()
    {
        Debug.Log(winMessage + " " + counter + " times");
    }
}
