using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseController : MonoBehaviour
{
    protected Rigidbody2D _rigidbody;

    [SerializeField] private SpriteRenderer characterRenderer;
    [SerializeField] private Transform weaponPivor;


    protected Vector2 movementDirection = Vector2.zero;
    public Vector2 MovementDirection { get => movementDirection; }

    protected float jumpForce = 5f;
    public float JumpForce { get => jumpForce; }

    protected Vector2 lookDirection = Vector2.zero;
    public Vector2 LookDirection { get => lookDirection; }

    private Vector2 knockback = Vector2.zero;
    private float knockbackDuration = 0.0f;

    private bool isJumping = false;

    protected virtual void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    protected virtual void Start()
    {

    }

    protected virtual void Update()
    {
        HandleAction();
        Rotate(lookDirection);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(JumpEffect());
        }
    }

    protected virtual void FixedUpdate()
    {
        Movement(movementDirection);
        if(knockbackDuration > 0.0f)
        {
            knockbackDuration -= Time.fixedDeltaTime;
        }
    }

    protected virtual void HandleAction()
    {

    }


    private void Movement(Vector2 direction)
    {
        direction = direction * 5;
        if(knockbackDuration > 0.0f)
        {
            direction *= 0.2f;
            direction += knockback;
        }

        _rigidbody.velocity = direction;
    }

    private void Rotate(Vector2 direction)
    {
        // Atna2 ź��Ʈ ���̿� �غ��� �˸� ���� ���� �� ����. �׸��� �ű⿡ Rad2Deg�� ���ؼ� ������ ���� ��ȯ
        float rotZ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        // 90���� ũ�� ĳ���� ������ �ݴ�� ���ư��ɷ� �� �� ����.

        bool isLeft = Mathf.Abs(rotZ) > 90f;

        characterRenderer.flipX = isLeft;

        if(weaponPivor != null)
        {
            weaponPivor.rotation = Quaternion.Euler(0, 0, rotZ);
        }

    }

    public void ApplyKnockback(Transform other, float power, float duration)
    {
        knockbackDuration = duration;
        knockback = -(other.position - transform.position).normalized*power;
    }

    
    IEnumerator JumpEffect()
    {

        if (isJumping)
            yield break; // �̹� ���� ���̸� �������� ����

        isJumping = true;

        float jumpHeight = 0.5f;
        float jumpDuration = 0.2f;
        Vector3 originalPos = transform.position;
        Vector3 jumpPos = originalPos + new Vector3(0, jumpHeight, 0);

        // ���� �̵�
        float t = 0;
        while (t < 1)
        {
            transform.position = Vector3.Lerp(originalPos, jumpPos, t);
            t += Time.deltaTime / jumpDuration;
            yield return null;
        }

        // �Ʒ��� �̵�
        t = 0;
        while (t < 1)
        {
            transform.position = Vector3.Lerp(jumpPos, originalPos, t);
            t += Time.deltaTime / jumpDuration;
            yield return null;
        }

        transform.position = originalPos;

        isJumping = false;
    }

 




}
