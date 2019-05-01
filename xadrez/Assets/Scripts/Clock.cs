using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clock : MonoBehaviour {

    [SerializeField, Range(1, 2)]
    private int player;

    public int minutes;
    public float seconds;

    public float totalSec;
    public bool invert;

    public bool timeOver;

    public UnityEngine.UI.Text minText;
    public UnityEngine.UI.Text secText;

    private GameManager gm;
        
        // Use this for initialization
    void Start () {
        gm = GameObject.FindObjectOfType(typeof(GameManager)) as GameManager;
        timeOver = false;
        if (minutes == 0 && seconds == 0)
        {
            minutes = int.Parse(totalSec + "") / 60;
            seconds = int.Parse(totalSec + "") % 60;
        }
        else
        {
            totalSec = (minutes * 60) + seconds;
        }
        regulaPrint();
        StartCoroutine(oi());
    }
	
    IEnumerator oi()
    {
        Debug.Log("hello world!!!");
        yield return null;
    }

	// Update is called once per frame
	void Update () {
        if (gm.getTurno() == player)
        {
            if (invert)
            {
                if (totalSec > 1)
                {
                    seconds -= Time.deltaTime;
                    totalSec -= Time.deltaTime;

                    if (seconds < 0)
                    {
                        seconds = 60;
                        minutes--;
                    }
                }
                else
                {
                    timeOver = true;
                    seconds = totalSec = 0;
                }
            }
            else
            {
                seconds += Time.deltaTime;
                totalSec += Time.deltaTime;
                timeOver = false;

                if (seconds > 60)
                {
                    seconds = 0;
                    minutes++;
                }
            }
            regulaPrint();
        }
    }

    
    void regulaPrint()
    {
        if (minutes < 10)
        {
            minText.text = "0" + minutes;
        }
        else
        {
            minText.text = "" + minutes;
        }

        if (seconds < 10)
        {
            secText.text = "0" + (int)seconds;
        }
        else
        {
            secText.text = "" + (int)seconds;
        }
    }
}
