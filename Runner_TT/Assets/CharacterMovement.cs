using UnityEngine;
using TMPro;
using System.Collections;
using System.Collections.Generic;

public class CharacterMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5f;
    Rigidbody rb;
    [SerializeField] GameObject[] laneCenters;
    int currentLane = 1;
    float targetX;
    [SerializeField] float laneChangeSpeed = 5f;
    [SerializeField] float jumpForce = 6f;
    [SerializeField] float modify;
    Animator am;

    [SerializeField] GameObject menu;
    [SerializeField] float score;
    [SerializeField] TMP_Text scoreText;
    [SerializeField] TMP_Text moveSpeedText;

     AudioSource audioSource;
    [SerializeField] AudioClip[] runSound;
    [SerializeField] AudioClip jumpSound;
    [SerializeField] AudioClip pickupSound;
    [SerializeField] float stepDelay = 0.3f; 
    private float nextStepTime = 0f;

    [SerializeField] ParticleSystem pickupEffect;
    [SerializeField] ParticleSystem jumpEffect;
    public GameObject[] lifeArray = new GameObject[3];
    private int lifePoint = 3;
    [SerializeField] ParticleSystem damageEffect;
    [SerializeField] AudioClip damageSound;
    [SerializeField] AudioClip loseSound;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        transform.position = laneCenters[currentLane].transform.position;
        targetX = transform.position.x;
        am = GetComponent<Animator>();
        Physics.gravity = new Vector3(0, -9.81f * 3.7f, 0);
        menu.SetActive(false);
        Time.timeScale = 1;
        audioSource = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A) && currentLane > 0)
        {
            currentLane--;
            targetX = laneCenters[currentLane].transform.position.x;
        }
        else if (Input.GetKeyDown(KeyCode.D) && currentLane < 2)
        {
            currentLane++;
            targetX = laneCenters[currentLane].transform.position.x;
        }
        if (transform.position.y < 2.7f)
        {
            am.SetBool("Jump", false);
            if (Input.GetKeyDown(KeyCode.Space))
            {
                rb.linearVelocity = new Vector3(rb.linearVelocity.x, jumpForce, rb.linearVelocity.z);
                audioSource.Stop();
                audioSource.PlayOneShot(jumpSound);
                Instantiate(jumpEffect, transform.position + new Vector3(0,0,moveSpeed / 5), Quaternion.identity);
            }
        }
        else
        {
            am.SetBool("Jump", true);
        }
        moveSpeed += Time.deltaTime * modify;
        if (Time.time >= nextStepTime)
        {
            audioSource.PlayOneShot(runSound[Random.Range(0, runSound.Length)]);
            nextStepTime = Time.time + stepDelay;
        }
    }
    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + transform.forward * moveSpeed * Time.fixedDeltaTime);
        float direction = targetX - rb.position.x;
        rb.linearVelocity = new Vector3(direction * laneChangeSpeed, rb.linearVelocity.y, rb.linearVelocity.z);
        score += transform.position.z * Time.deltaTime;
        scoreText.text = score.ToString("f0");
        moveSpeedText.text = moveSpeed.ToString("f0");
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Obstacle")
        {
            if (lifePoint > 1)
            {
                lifeArray[lifePoint - 1].SetActive(false);
                lifePoint--;
                Instantiate(damageEffect, transform.position + new Vector3(0, 0, moveSpeed / 5), Quaternion.identity);
                audioSource.Stop();
                audioSource.PlayOneShot(damageSound);
            }
            else
            {
                lifeArray[lifePoint - 1].SetActive(false);
                lifePoint--;
                Time.timeScale = 0;
                menu.SetActive(true);
                Instantiate(damageEffect, transform.position + new Vector3(0, 0, moveSpeed / 5), Quaternion.identity);
                audioSource.Stop();
                audioSource.PlayOneShot(loseSound);
            }
        }
        if (collision.gameObject.tag == "Slow")
        {
            moveSpeed = moveSpeed / 5.5f;
            Debug.Log(collision.gameObject.name);
            Destroy(collision.gameObject);
            audioSource.Stop();
            audioSource.PlayOneShot(pickupSound);
            Instantiate(pickupEffect, collision.transform.position, Quaternion.identity);
        }
    }
}
