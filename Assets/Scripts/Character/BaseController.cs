using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseController : MonoBehaviour
{
    protected Rigidbody2D _rigidbody;
    protected AnimationHandler animationHandler;
    protected StatHandler statHandler;

    [SerializeField] private SpriteRenderer characterRenderer;
    [SerializeField] private Transform weaponPivot;


    protected Vector2 movementDirection = Vector2.zero;
    public Vector2 MovementDirection { get => movementDirection; }

    protected float jumpForce = 5f;
    public float JumpForce { get => jumpForce; }

    protected Vector2 lookDirection = Vector2.zero;
    public Vector2 LookDirection { get => lookDirection; }

    private Vector2 knockback = Vector2.zero;
    private float knockbackDuration = 0.0f;

    private bool isJumping = false;


    [SerializeField] public WeaponHandler WeaponPrefab;
    protected WeaponHandler weaponHandler;

    protected bool isAttacking;
    private float timeSinceLastAttack = float.MaxValue;



    protected virtual void Awake()
    {
       
        _rigidbody = GetComponent<Rigidbody2D>();
        animationHandler = GetComponent<AnimationHandler>();
        statHandler = GetComponent<StatHandler>();

        if (WeaponPrefab != null)
            weaponHandler = Instantiate(WeaponPrefab, weaponPivot);
        else
            weaponHandler = GetComponentInChildren<WeaponHandler>();
    }

    protected virtual void Start()
    {

    }

    protected virtual void Update()
    {
        HandleAction();
        Rotate(lookDirection);
        HandleAttackDelay();


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
        direction = direction * statHandler.Speed;
        if(knockbackDuration > 0.0f)
        {
            direction *= 0.2f;
            direction += knockback;
        }

        _rigidbody.velocity = direction;
        animationHandler.Move(direction);
    }

    private void Rotate(Vector2 direction)
    {
        // Atna2 탄젠트 높이와 밑변을 알면 각을 구할 수 있음. 그리고 거기에 Rad2Deg를 곱해서 라디안을 도로 변환
        float rotZ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        // 90도만 크면 캐릭터 방향이 반대로 돌아간걸로 볼 수 있음.

        bool isLeft = Mathf.Abs(rotZ) > 90f;

        characterRenderer.flipX = isLeft;

        if(weaponPivot != null)
        {
            weaponPivot.rotation = Quaternion.Euler(0, 0, rotZ);
        }

        weaponHandler?.Rotate(isLeft);

    }

    public void ApplyKnockback(Transform other, float power, float duration)
    {
        knockbackDuration = duration;
        knockback = -(other.position - transform.position).normalized*power;
    }

    
    public IEnumerator JumpEffect()
    {

        if (isJumping)
            yield break; // 이미 점프 중이면 실행하지 않음

        isJumping = true;

        float jumpHeight = 0.5f;
        float jumpDuration = 0.2f;
        Vector3 originalPos = transform.position;
        Vector3 jumpPos = originalPos + new Vector3(0, jumpHeight, 0);

        // 위로 이동
        float t = 0;
        while (t < 1)
        {
            transform.position = Vector3.Lerp(originalPos, jumpPos, t);
            t += Time.deltaTime / jumpDuration;
            yield return null;
        }

        // 아래로 이동
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


    private void HandleAttackDelay()
    {

        if (weaponHandler == null)
            return;

        if (timeSinceLastAttack <= weaponHandler.Delay)
        {
            timeSinceLastAttack += Time.deltaTime;
        }

        // 여기서 공격키 받으면 공격
        if (isAttacking && timeSinceLastAttack > weaponHandler.Delay)
        {           
            timeSinceLastAttack = 0;
            Attack();
        }
    }

    protected virtual void Attack()
    {

        if (lookDirection != Vector2.zero)
        {        
            weaponHandler?.Attack();
        }
    }

    public virtual void Death()
    {
        _rigidbody.velocity = Vector3.zero;

        foreach (SpriteRenderer renderer in transform.GetComponentsInChildren<SpriteRenderer>())
        {
            Color color = renderer.color;
            color.a = 0.3f;
            renderer.color = color;
        }

        foreach (Behaviour component in transform.GetComponentsInChildren<Behaviour>())
        {
            component.enabled = false;
        }

        Destroy(gameObject, 2f);
    }






}
