using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class BoarController : MonoBehaviour
{

    public Enemy monster;
    public Transform target;


    void Start()
    {
        target = Shared.player.gameObject.transform;
        monster.InitSetting(Shared.mapMgr.Difficulty);
        monster.boarOntime();
    }

    void Update()
    {
        monster.Boar(target);
    }

}
