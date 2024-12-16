using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class MonsterConstroller : MonoBehaviour
{

    public Enemy monster;
    public Transform target;

    
    void Start()
    {
        target = Shared.player.gameObject.transform;
        monster.InitSetting(Shared.mapMgr.Difficulty);
        monster.onetime();
    }

    void Update()
    {
        monster.Short_Monster(target);
    }
        
}
