using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//access ui elements
using UnityEngine.UI;

public class eggController : MonoBehaviour
{
    public static int eggCounter;
    public event System.Action eggsCollected;
    [SerializeField] Text currentEggCountUI;
    [SerializeField] private Transform Eggs;
    private int maxEggCount;
    private int currentEggCount;
    // Start is called before the first frame update
    void Start()
    {
        eggCounter = 0;
        currentEggCount = 0;
        currentEggCountUI.text = eggCounter + "/" + maxEggCount;
        maxEggCount = Eggs.transform.childCount;
        //Debug.Log("Egg Counter:" + eggCounter);
        Debug.Log("max Egg Count:" + maxEggCount);
    }

    private void LateUpdate()
    {
        if (currentEggCount != maxEggCount) {
            //print("Egg Count Changed to:" + eggCounter);
            currentEggCountUI.text = eggCounter + "/" + maxEggCount;
            currentEggCount = eggCounter;
        }
        else
        {
            if (FindObjectOfType<gameOverController>().gameOver == false)
            {
                eggsCollected();
            }
        }
    }
}
