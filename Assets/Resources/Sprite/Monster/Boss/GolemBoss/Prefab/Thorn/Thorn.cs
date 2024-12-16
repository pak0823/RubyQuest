using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thorn : MonoBehaviour
{
    public float Damage = 50f;
    public AudioClip thorn;
    public AudioSource sfx;
    public SoundManager sm;

    private void Start()
    {
        float[] stats = { 0.8f, 1f, 1.2f };
        Damage = Damage * stats[Shared.mapMgr.Difficulty];
        sm = Shared.soundMgr;
        Invoke("thornSfxPlay", 1.2f);
        Destroy(gameObject, 1.8f);
    }

    private void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // �浹�� ������Ʈ�� �±װ� "Player"���� Ȯ���մϴ�.
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<Player>().Playerhurt(Damage, collision.transform.position);
        }
    }

    void thornSfxPlay()
    {
        sm.SFXPlay("thorn_sound",thorn);
    }
}