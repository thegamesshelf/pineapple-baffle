using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class levelLoader : MonoBehaviour
{
    public static string playerNameSTR;
    public Text playerName;
    [SerializeField] int charAmount = 7;
    const string glyphs = "abcdefghijklmnopqrstuvwxyz0123456789";
    // Start is called before the first frame update
    void Start()
    {
        if (string.IsNullOrEmpty(playerNameSTR))
        {
            for (int i = 0; i < charAmount; i++)
            {
                playerName.text += glyphs[Random.Range(0, glyphs.Length)];
            }
            playerNameSTR = playerName.text;
        }
        else
        {
            playerName.text = playerNameSTR;

        }

        print("Playing as: " + playerNameSTR);
    }

}
