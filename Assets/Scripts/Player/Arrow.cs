using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using System.Linq;
using Unity.VisualScripting;

public class Arrow : MonoBehaviour
{
    private Player player;
    
    public float ArrowDamage = 0.0f; //대미지 변수, 몬스터가 피격시 화살 데미지값을 받기 위해
    public float ArrowSpeed = 18.0f; // 화살 이동 속도
    private float rotationSpeed = 10.0f; // 회전 속도 변수
    private float detectRadius = 10f; // 화살의 추적 범위 (적이 있는지 없는지 확인)
    private bool isSkill = false; // 스킬 사용 여부
    private bool hit = false;    // 적을 맞췄는지 확인하는 변수

    private Vector3 moveDirection = Vector3.right; // 화살의 시작 방향
    public LayerMask islayer; // 충돌 감지를 할 레이어

    private BoxCollider2D Arrowcollider;
    private SpriteRenderer spriteRenderer;
    private Dictionary<Collider2D, bool> hitDict = new Dictionary<Collider2D, bool>(); // 이미 적에게 대미지를 입혔는지 여부를 기록하는 Dictionary 변수

    private float DestroyTime = 1.5f;  //화살 생존 시간

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

    void ArrowStartSetting()// 플레이어가 보는 방향에 맞게 화살 발사
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

    // 가장 가까운 적 찾기
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

    // 몬스터 추적 화살 기능
    IEnumerator ChaseArrow()    //Update함수에 구현했던것을 코루틴 함수에 while문을 추가하여 Update함수와 같은 효과를 주었음.
    {
        while(true)
        {
            if (player.proLevel > 0 && !isSkill) // 일정 거리 내에 적이 있으면 가장 가까운 적으로 이동
            {
                Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, detectRadius, islayer);
                Collider2D closestCollider = FindCollider(hitColliders);

                ArrowSpeed = 13.0f;

                if (hitColliders.Length > 0)
                {
                    Vector2 direction = (closestCollider.transform.position - transform.position).normalized;

                    //현재 각도와 목표 각도 계산
                    float targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                    float currentAngle = transform.rotation.eulerAngles.z;

                    //각도 차이 계산
                    float angleDifference = Mathf.DeltaAngle(currentAngle, targetAngle);

                    //각도 줄이기
                    if (Mathf.Abs(angleDifference) > 0.1f)
                    {
                        float newAngle = currentAngle + Mathf.Sign(angleDifference) * rotationSpeed * 20f * Time.deltaTime;
                        transform.rotation = Quaternion.Euler(0, 0, newAngle);
                    }

                    //현재 각도와 맞게 이미지 회전
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
                transform.position += moveDirection * ArrowSpeed * Time.deltaTime; // 화살 직진 이동
            }

            yield return null; // 다음 프레임으로 이동
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) //화살 충돌 처리
    {
        if (collision.tag == "Enemy" || collision.tag == "Boss")
        {
            if (isSkill == true && !hitDict.ContainsKey(collision)) // 스킬 사용 중이고, 이미 적에게 대미지를 입힌 경우가 아닐 때
            {
                hitDict.Add(collision, true); // 적 정보를 Dictionary에 추가
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

    IEnumerator DestroyArrow()  // 화살 제거 함수
    {
        yield return new WaitForSeconds(DestroyTime);
        Destroy(gameObject);
    }

    private void OnDrawGizmos()
    {
        // 화살의 현재 위치에서 detectRadius 범위를 시각적으로 표시
        Gizmos.color = Color.red; // 원의 색상 설정
        Gizmos.DrawWireSphere(transform.position, detectRadius); // 원형 범위 그리기
    }
}


