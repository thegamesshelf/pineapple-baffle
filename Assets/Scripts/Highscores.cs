using System.Collections;
using System.Collections.Generic;
//using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.Networking;

public class Highscores : MonoBehaviour
{
    //http://dreamlo.com/lb/0xrMc3qiGE629enaUXaI-ghiQ3Erb7bU-VzNDXN7lmkA
    const string privateCode = "0xrMc3qiGE629enaUXaI-ghiQ3Erb7bU-VzNDXN7lmkA";
    const string publicCode = "5f994044eb371809c4bd0b10";
    const string webURL = "http://dreamlo.com/lb/";
    public Highscore[] highscoresList;
    DisplayHighscores highscoresDisplay;
    static Highscores instance;

    private void Awake()
    {
        instance = this;
        //AddNewHighscore("Tim",50);
        //AddNewHighscore("Mary", 80);
        //AddNewHighscore("Bob", 92);

        //DownloadHighscores();

        highscoresDisplay = GetComponent<DisplayHighscores>();

    }

    // static makes this accessable to any script wanting to upload a new score
    public static void AddNewHighscore(string username, int score) {
        instance.StartCoroutine(instance.UploadNewHighscore(username, score));
    }

    IEnumerator UploadNewHighscore(string username, int score) {
        UnityWebRequest wwwDelete = UnityWebRequest.Get(webURL + privateCode + "/delete/" + UnityWebRequest.EscapeURL(username));
        //print(webURL + privateCode + "/delete/" + UnityWebRequest.EscapeURL(username));
        yield return wwwDelete.SendWebRequest();
        UnityWebRequest wwwAdd = UnityWebRequest.Get(webURL + privateCode + "/add/" + UnityWebRequest.EscapeURL(username) + "/" + score);
        //print(webURL + privateCode + "/add/" + UnityWebRequest.EscapeURL(username) + "/" + score);
        yield return wwwAdd.SendWebRequest();
        if (string.IsNullOrEmpty(wwwAdd.error))
        {
            print("Upload Successful.");
            DownloadHighscores();
        }
        else {
            print("Error Uploading:" + wwwAdd.error);
        }
    }

    public void DownloadHighscores() {
        StartCoroutine("DownloadHighscoresFromDatabase");
    }
    IEnumerator DownloadHighscoresFromDatabase()
    {
        UnityWebRequest www = UnityWebRequest.Get(webURL + publicCode + "/pipe-asc");
        yield return www.SendWebRequest();
        if (string.IsNullOrEmpty(www.error))
        {
            //print(www.downloadHandler.text);
            FormatHighscores(www.downloadHandler.text);
            highscoresDisplay.OnHighscoresDownloaded(highscoresList);
        }
        else
        {
            print("Error Uploading:" + www.error);
        }
    }

    void FormatHighscores(string textStream) {
        string[] entries = textStream.Split(new char[] { '\n' }, System.StringSplitOptions.RemoveEmptyEntries);
        highscoresList = new Highscore[entries.Length];
        for (int i = 0; i < entries.Length; i++) {
            string[] entryInfo = entries[i].Split(new char[] { '|' });
            string username = entryInfo[0];
            string score = (int.Parse(entryInfo[1]) / 100000f).ToString();

            highscoresList[i] = new Highscore(username, score);
            //print(highscoresList[i].username + " - " + highscoresList[i].score);
        }
    }
}

// data structure
public struct Highscore {
    public string username;
    public string score;

    // constructor - autocomplete
    public Highscore(string _username, string _score) {
        username = _username;
        score = _score;
    }
}