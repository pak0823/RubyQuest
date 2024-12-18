using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Unity.VisualScripting;

public class Projective_Body : MonoBehaviour
{
    Rigidbody2D rigid;
    SpriteRenderer sprite;
    Player player;
    Enemy enemy;
    public float Power;
    public int Dir;
    public float Time;


    void Start()
    {
        rigid = this.GetComponent<Rigidbody2D>();
        sprite = this.GetComponent<SpriteRenderer>();
        DestoryObject();
    }

    private void Update()
    {
        Shot();
    }
    public void DestoryObject()
    {
        Destroy(gameObject, Time) ;
    }
    public void Shot()
    { 
        if (Dir == 1)
        {
            rigid.AddForce(transform.right * 0.3f, ForceMode2D.Impulse);
            sprite.flipX = false;
        }
        else if (Dir == -1)
        {
            rigid.AddForce(transform.right * -0.3f, ForceMode2D.Impulse);
            sprite.flipX = true;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Vector2 vector2 = new Vector2(Dir, 1);
            Shared.player.Playerhurt(Power, vector2);
            Destroy(gameObject);
        }
        else if (collision.tag == "Wall"){
            Destroy(gameObject);
        }
    }
}
