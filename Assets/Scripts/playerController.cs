using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class playerController : MonoBehaviour
{
    [SerializeField] private float playerSpeed;
    [SerializeField] private float jumpForce;
    [SerializeField] private LayerMask groundedLayer;
    [SerializeField] private float leftLimit;
    [SerializeField] private float rightLimit;
    [SerializeField] private float topLimit;
    [SerializeField] private float bottomLimit;
    [SerializeField] private Transform playerSkin;
    [SerializeField] AudioSource runningAudio;
    [SerializeField] AudioSource jumpAudio;
    [SerializeField] AudioSource fallenAudio;
    private Rigidbody2D rb2D;
    private CapsuleCollider2D cc2D;
    private bool isFalling;
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        rb2D = gameObject.GetComponent<Rigidbody2D>();
        cc2D = gameObject.GetComponent<CapsuleCollider2D>();
        animator = GetComponentInChildren<Animator>();
    }
    void OnGUI()
    {

        Event e = Event.current;

        if (e.isKey)
        {

            string key = e.keyCode.ToString();

            Debug.Log(key);

            print("Hi");

        }

    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R)) {
            SceneManager.LoadScene(1);
        }
        if (FindObjectOfType<cameraShake>().playerCanMove == true)
        {

            animator.SetBool("a_HasFallen", false);
            // move player
            float inputX = Input.GetAxisRaw("Horizontal");
            float velocity = inputX * playerSpeed;
            transform.Translate(Vector2.right * velocity * Time.deltaTime);

            // face direction of movement
            if (inputX == -1f)
            {
                //print("Face Left.");
                playerSkin.transform.eulerAngles = new Vector3(playerSkin.transform.rotation.x,
                    -180f,
                    playerSkin.transform.rotation.y);
                animator.SetBool("a_IsRunning", true);

                // play running sound
                if (runningAudio.isPlaying == false && IsGrounded()) {
                    runningAudio.Play();
                }
            }
            else if (inputX == 1f)
            {
                //print("Face Right.");
                playerSkin.transform.eulerAngles = new Vector3(playerSkin.transform.rotation.x,
                    0f,
                    playerSkin.transform.rotation.y);
                animator.SetBool("a_IsRunning", true);

                // play running sound
                if (runningAudio.isPlaying == false && IsGrounded())
                {
                    runningAudio.Play();
                }
            }
            else {
                animator.SetBool("a_IsRunning", false);
                // play running sound
                if (runningAudio.isPlaying == true)
                {
                    runningAudio.Stop();
                }
            }

            // jump player
            // /*Input.GetKeyDown(KeyCode.Space)*/
            if ( Input.GetButtonDown("Jump") && IsGrounded())
            {
                jumpAudio.Play();
                rb2D.velocity = Vector2.up * jumpForce;
                animator.SetBool("a_IsJumping", true);

            }

            // Limit the Players transform 
            transform.position = new Vector3(
                Mathf.Clamp(transform.position.x, leftLimit, rightLimit),
                Mathf.Clamp(transform.position.y, bottomLimit, topLimit),
                transform.position.z
            );

            // check if player is falling
            if (rb2D.velocity.y < -5 && !isFalling)
            {
                isFalling = true;
                animator.SetBool("a_IsJumping", false);
                animator.SetBool("a_IsFalling", true);
            }
            else if (rb2D.velocity.y == 0)
            {
                animator.SetBool("a_IsJumping", false);
                animator.SetBool("a_IsFalling", false);
            }
            if (rb2D.velocity.y < 0 && !IsGrounded())
            {
                animator.SetBool("a_IsFalling", true);
            }
        }
        else if (FindObjectOfType<cameraShake>().playerCanMove == false)
        {
            animator.SetBool("a_IsFalling", false);
            animator.SetBool("a_HasFallen", true);
            runningAudio.Stop();
            // play fallen sound
            if (fallenAudio.isPlaying == false)
            {
                fallenAudio.Play();
            }
        }

        if (!IsGrounded())
        {
            runningAudio.Stop();
        }

    }

    bool IsGrounded()
    {
        RaycastHit2D rch2D = Physics2D.BoxCast(cc2D.bounds.center, cc2D.bounds.size, 0f, Vector2.down, .5f, groundedLayer);
        return rch2D.collider != null;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Ground" && isFalling)
        {
            //print("We hit the ground ground");
            FindObjectOfType<cameraShake>().ShakeIt();
            isFalling = false;
        }
    }

    // add gizmos
    void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(new Vector2(leftLimit, topLimit), new Vector2(rightLimit, topLimit)); //top
        Gizmos.DrawLine(new Vector2(leftLimit, bottomLimit), new Vector2(rightLimit, bottomLimit)); //bottom
        Gizmos.DrawLine(new Vector2(leftLimit, topLimit), new Vector2(leftLimit, bottomLimit)); //left
        Gizmos.DrawLine(new Vector2(rightLimit, topLimit), new Vector2(rightLimit, bottomLimit)); //right   
    }
}
