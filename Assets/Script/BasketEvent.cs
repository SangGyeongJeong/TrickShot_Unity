using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasketEvent : MonoBehaviour
{
    // Start is called before the first frame update
    public Player player;
    Rigidbody rigid;
    void Awake()
    {
        rigid = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if(player.isShoot)
        {
            rigid.useGravity = true;
        }
    }
}
