using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public FixedJoystick joystick;
    public float moveSpeed;

    float hInput, vInput;

    int score = 0;
    public GameObject winText;
    public int winScore;
    public Animator animator;

    [SerializeField] private AudioSource WalkSound;
    [SerializeField]
    private AudioSource BombSound;
    [SerializeField] private AudioSource WinSound;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        animator.SetFloat("Horizontal", joystick.Horizontal);
        animator.SetFloat("Vertical", joystick.Vertical);
        animator.SetFloat("Speed", joystick.Direction.magnitude);
    }

    private void FixedUpdate()
    {
        hInput = joystick.Horizontal * moveSpeed;
        vInput = joystick.Vertical * moveSpeed;

        if (hInput != 0 || vInput != 0)
        {
            if (!WalkSound.isPlaying)
            {
                WalkSound.Play();
            }
        }
        else
        {
            WalkSound.Stop();
        }

        transform.Translate(hInput, vInput, 0);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Bomb")
        {
            BombSound.Play();
            score++;
            Destroy(collision.gameObject);

            if (score >= winScore)
            {
                WinSound.Play();
                winText.SetActive(true);

                StartCoroutine(ResetGame());
            }
        }
    }

    IEnumerator ResetGame()
    {
        yield return new WaitForSeconds(3);
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }
}
