using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class MeleeWeaponHandler : WeaponHandler
{
    [Header("Melee Attack Info")]
    public Vector2 collideBoxSize = Vector2.one;

    protected override void Start()
    {
        base.Start();
        collideBoxSize = collideBoxSize * WeaponSize;
    }

    public override void Attack()
    {
        base.Attack();

        RaycastHit2D hit = Physics2D.BoxCast(
            transform.position + (Vector3)Controller.LookDirection * collideBoxSize.x, // �ڽ��� ���� ��ġ (origin)
            collideBoxSize, // �ڽ��� ũ�� (size)
            0,  // �ڽ��� ȸ���� (angle) - ���⼭�� 0�� (ȸ�� ����)
            Vector2.zero,  // ĳ������ ���� (direction) - ���⼭�� 0�����̹Ƿ� �̵����� ����
            0,      // ĳ���� �Ÿ� (distance) - 0�̹Ƿ� �ٷ� �� ��ġ������ �˻�
            target);    // ���̾� ����ũ (layerMask) - �˻��� ���̾� ����

        if (hit.collider != null)
        {
            ResourceController resourceController = hit.collider.GetComponent<ResourceController>();
            if (resourceController != null)
            {
                resourceController.ChangeHealth(-Power);
                if (IsOnKnockback)
                {
                    BaseController controller = hit.collider.GetComponent<BaseController>();
                    if (controller != null)
                    {
                        controller.ApplyKnockback(transform, KnockbackPower, KnockbackTime);
                    }
                }
            }
        }
    }
    public override void Rotate(bool isLeft)
    {
        if (isLeft)
            transform.eulerAngles = new Vector3(0, 180, 0);
        else
            transform.eulerAngles = new Vector3(0, 0, 0);
    }

}
