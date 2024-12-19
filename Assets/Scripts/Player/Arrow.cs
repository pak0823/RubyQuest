using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using System.Linq;
using Unity.VisualScripting;

public class Arrow : MonoBehaviour
{
    private Player player;
    
    public float ArrowDamage = 0.0f; //����� ����, ���Ͱ� �ǰݽ� ȭ�� ���������� �ޱ� ����
    public float ArrowSpeed = 18.0f; // ȭ�� �̵� �ӵ�
    private float rotationSpeed = 10.0f; // ȸ�� �ӵ� ����
    private float detectRadius = 10f; // ȭ���� ���� ���� (���� �ִ��� ������ Ȯ��)
    private bool isSkill = false; // ��ų ��� ����
    private bool hit = false;    // ���� ������� Ȯ���ϴ� ����

    private Vector3 moveDirection = Vector3.right; // ȭ���� ���� ����
    public LayerMask islayer; // �浹 ������ �� ���̾�

    private BoxCollider2D Arrowcollider;
    private SpriteRenderer spriteRenderer;
    private Dictionary<Collider2D, bool> hitDict = new Dictionary<Collider2D, bool>(); // �̹� ������ ������� �������� ���θ� ����ϴ� Dictionary ����

    private float DestroyTime = 1.5f;  //ȭ�� ���� �ð�

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        Arrowcollider = GetComponent<BoxCollider2D>();
        player = Shared.player;

        ArrowStartSetting();
    }

    private void Start()
    {
        StartCoroutine(DestroyArrow());
        StartCoroutine(ChaseArrow());
    }

    void ArrowStartSetting()// �÷��̾ ���� ���⿡ �°� ȭ�� �߻�
    {
        isSkill = player.isSkill;
        
        if (player != null)
        {
            moveDirection = player.spriteRenderer.flipX ? Vector3.right : Vector3.left;
            spriteRenderer.flipX = !player.spriteRenderer.flipX;

            if(isSkill)
            {
                ArrowDamage = ArrowDamage * 2.5f;
                ArrowSpeed = 20f;
            }
                
        }

        ArrowDamage = (player.ATP + player.AtkPower + player.GridPower + player.VulcanPower) * player.WeaponsDmg[2];
    }

    // ���� ����� �� ã��
    Collider2D FindCollider(Collider2D[] colliders)
    {
        Collider2D closestCollider = null;
        float distance = float.MaxValue;
        foreach (Collider2D coll in colliders)
        {
            if (coll == null)
                continue;

            if ((islayer & (1 << coll.gameObject.layer)) != 0)
            {
                float tempDist = Vector2.Distance(transform.position, coll.transform.position);
                if (tempDist < distance)
                {
                    closestCollider = coll;
                    distance = tempDist;
                }
            }
            else
                return null;
        }
        return closestCollider;
    }

    // ���� ���� ȭ�� ���
    IEnumerator ChaseArrow()    //Update�Լ��� �����ߴ����� �ڷ�ƾ �Լ��� while���� �߰��Ͽ� Update�Լ��� ���� ȿ���� �־���.
    {
        while(true)
        {
            if (player.proLevel > 0 && !isSkill) // ���� �Ÿ� ���� ���� ������ ���� ����� ������ �̵�
            {
                Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, detectRadius, islayer);
                Collider2D closestCollider = FindCollider(hitColliders);

                ArrowSpeed = 13.0f;

                if (hitColliders.Length > 0)
                {
                    Vector2 direction = (closestCollider.transform.position - transform.position).normalized;

                    //���� ������ ��ǥ ���� ���
                    float targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                    float currentAngle = transform.rotation.eulerAngles.z;

                    //���� ���� ���
                    float angleDifference = Mathf.DeltaAngle(currentAngle, targetAngle);

                    //���� ���̱�
                    if (Mathf.Abs(angleDifference) > 0.1f)
                    {
                        float newAngle = currentAngle + Mathf.Sign(angleDifference) * rotationSpeed * 20f * Time.deltaTime;
                        transform.rotation = Quaternion.Euler(0, 0, newAngle);
                    }

                    //���� ������ �°� �̹��� ȸ��
                    spriteRenderer.transform.rotation = Quaternion.Euler(0, 0, transform.rotation.eulerAngles.z);

                    Vector2 pos2D = new Vector2(transform.position.x, transform.position.y);
                    pos2D += ArrowSpeed * Time.deltaTime * direction;
                    transform.position = new Vector3(pos2D.x, pos2D.y, transform.rotation.z);
                }
                else
                    transform.position += moveDirection * ArrowSpeed * Time.deltaTime;
            }
            else
            {
                transform.position += moveDirection * ArrowSpeed * Time.deltaTime; // ȭ�� ���� �̵�
            }

            yield return null; // ���� ���������� �̵�
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) //ȭ�� �浹 ó��
    {
        if (collision.tag == "Enemy" || collision.tag == "Boss")
        {
            if (isSkill == true && !hitDict.ContainsKey(collision)) // ��ų ��� ���̰�, �̹� ������ ������� ���� ��찡 �ƴ� ��
            {
                hitDict.Add(collision, true); // �� ������ Dictionary�� �߰�
            }
            else 
            {
                hit  = true;
                Destroy(gameObject);
            }
        }
        else if (collision.tag == "Wall" || collision.tag == "Tilemap")
        {
            if(!isSkill && player.proLevel < 1)
            {
                if (hit)
                {
                    return;
                }
                Destroy(gameObject);
            }
        }
    }

    IEnumerator DestroyArrow()  // ȭ�� ���� �Լ�
    {
        yield return new WaitForSeconds(DestroyTime);
        Destroy(gameObject);
    }

    private void OnDrawGizmos()
    {
        // ȭ���� ���� ��ġ���� detectRadius ������ �ð������� ǥ��
        Gizmos.color = Color.red; // ���� ���� ����
        Gizmos.DrawWireSphere(transform.position, detectRadius); // ���� ���� �׸���
    }
}


