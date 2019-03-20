using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public GameObject player;
    public float TargetDistance;
    public float AllowedRange = 10;
    public GameObject enemy;
    public float EnemySpeed;
    public int AttackTrigger;
    public RaycastHit Shot;


    // Update is called once per frame
    void Update()
    {
        transform.LookAt(player.transform);
        if (Physics.Raycast(transform.position,transform.TransformDirection(Vector3.forward), out Shot))
        {
            TargetDistance = Shot.distance;
            if(TargetDistance< AllowedRange)
            {
                EnemySpeed = 0.02f;
                if (AttackTrigger == 0)
                {
                    //enemy.GetComponent<Animator>().Play("Run");
                    //enemy.GetComponent<Animation>().Play("Run");
                    transform.position = Vector3.MoveTowards(transform.position, player.transform.position, EnemySpeed);
                }
            }else
            {
                EnemySpeed = 0;
                //enemy.GetComponent<Animator>().Play("Idle");
                //enemy.GetComponent<Animation>().Play("Idle");

            }
        } 
        if (AttackTrigger == 1)
        {
            //enemy.GetComponent<Animator>().Play("Fire");
            //enemy.GetComponent<Animation>().Play("Fire");
        }
    }

    
}
