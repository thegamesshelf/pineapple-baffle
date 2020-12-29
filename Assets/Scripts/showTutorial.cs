using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class showTutorial : MonoBehaviour
{
    public GameObject otherObject;
    private Animator animator;
    [SerializeField] AudioSource audioSource;
    // Start is called before the first frame update

    private void Start()
    {
        animator = otherObject.GetComponent<Animator>();
        animator.SetBool("a_startGame", false);
    }

    public void StartGame()
    {
        StartCoroutine(fadeAudioSource.StartFade(audioSource, 1.2f, 0f));
        StartCoroutine(fadeOut());
        //print("playerName:" + playerName.text);
        //levelLoader.playerNameSTR = playerName.text;
        //load
        //SceneManager.LoadScene(0);
    }

    IEnumerator fadeOut()
    {
        animator.SetBool("a_startGame", true);
        yield return new WaitForSeconds(1.2f);
        //load
        SceneManager.LoadScene(2);
    }
}
