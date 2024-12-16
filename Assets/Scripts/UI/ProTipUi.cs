using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using static UnityEditor.Experimental.GraphView.GraphView;

public class ProTipUi : MonoBehaviour
{
    public Player player;
    // Start is called before the first frame update
    void Start()
    {
        player = Shared.player;
    }

    // Update is called once per frame
    void Update()
    {
        if (player.proSelectWeapon != 4)
        {
            Destroy(gameObject);
        }
        else
        {
            this.gameObject.SetActive(true);
        }
    }
}
