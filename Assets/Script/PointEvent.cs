using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointEvent : MonoBehaviour
{
    public GameObject player;
    Rigidbody rigid;
    void Awake()
    {
        rigid = player.transform.GetComponent<Rigidbody>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Target")
        {
            rigid.velocity = Vector3.zero;
        }
    }
}
