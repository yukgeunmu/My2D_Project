using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerFlappy : MonoBehaviour
{
    Animator animator = null;
    Rigidbody2D _rigidbody = null;

    [SerializeField] private float flapForce = 6f;
    public float FlapForce { get => flapForce; }

    [SerializeField] private float forwardSpeed = 3f;
    public float ForwardSpeed { get => forwardSpeed; }

    public bool isDead = false;
    
    float deathCooldown = 0f;
    bool isFlap = false;

    public bool godMode = false;

    public GameUI gameUI;

    public GameManager gameManager = null;

    public bool isTime = false;

    private bool isGameOver = false;




    private void Start()
    {
        Time.timeScale = 0;

        gameManager = GameManager.Instance;
        gameManager.cureentScore = 0;

        animator = transform.GetComponentInChildren<Animator>();
        _rigidbody = transform.GetComponent<Rigidbody2D>();

        if (animator == null)
            Debug.LogError("Not Founded Animator");

        if (_rigidbody == null)
            Debug.LogError("Not Founded Rigidbody");
    }


    private void Update()
    {    
        if (isTime)
            Time.timeScale = 1;

        if (isDead)
        {
            if (deathCooldown <= 0 && !isGameOver)
            {
                isGameOver = true;
                gameManager.UIManager.SetGameOver();
                Time.timeScale = 0;                           
            }
            else
            {
                deathCooldown -= Time.deltaTime;
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
            {
                isFlap = true;
            }
        }
    }

    public void FixedUpdate()
    {
        if (isDead)
            return;

        Vector3 velocity = _rigidbody.velocity;

        velocity.x = forwardSpeed;

        if(isFlap)
        {
            velocity.y += flapForce;
            isFlap = false;
        }

        _rigidbody.velocity = velocity;

        // 회전 각도 제한
        float angle = Mathf.Clamp((_rigidbody.velocity.y * 10f), -90, 90);

        // 오브젝트 움직임을 부드럽게 보간하기 위해 lerp를 사용
        float lerpAngle = Mathf.Lerp(_rigidbody.velocity.y, angle, Time.fixedDeltaTime * 5f);

        transform.rotation = Quaternion.Euler(0, 0, lerpAngle);
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (godMode)
            return;
        if(isDead)
            return;

        animator.SetInteger("IsDie", 1);
        isDead = true;
        deathCooldown = 1f;

    }

}
