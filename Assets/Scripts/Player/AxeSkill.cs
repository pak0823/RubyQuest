using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeSkill : MonoBehaviour
{
    public Player player; // Player ��ũ��Ʈ�� ������ �ִ� GameObject
    public SpriteRenderer spriteRenderer;
    public Transform trans;



    private void Start()
    {
        player = Shared.player;
    }

    IEnumerator Destroy()
    {
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
}