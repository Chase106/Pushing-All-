using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI2 : MonoBehaviour
{
    public Transform player;
    private Animator anim;
    Vector3 direction;
    public LayerMask playerMask;
    public float range = 100f;
    public float pushForce = 500f;
    public Vector3 pushDirection;
    public float pushSpeed = 1000.0f;
    public float pushStoppingSpeed = 0.1f;
    public float maxPushTime = 1.0f;
    private float currentPushTime;
    public CharacterController playerController;
    public float attackRange;

    public AudioClip hurtSound;
    public AudioSource hurtSource;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        anim.SetTrigger("Draw");
        currentPushTime = maxPushTime;
        hurtSource.clip = hurtSound;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(player.position, this.transform.position) < 10 && Vector3.Distance(player.position, this.transform.position) > attackRange)
        {
            inRange();
        }
        if (Vector3.Distance(player.position, this.transform.position) < attackRange)
        {
            //this.transform.Translate(0, 0, -0.05f);
            Shooting();
            this.transform.Translate(0, 0, 0);
            anim.SetBool("isFire", true);
            anim.SetBool("Run", false);
            

        }

    }

    private void inRange()
    {
        if (Vector3.Distance(player.position, this.transform.position) < 10 && Vector3.Distance(player.position, this.transform.position) > attackRange)
        {
            direction = player.position - this.transform.position;
            direction.y = 0;
            this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(direction), 0.1f);
            anim.SetBool("Run", false);
            if (direction.magnitude < 7 && Vector3.Distance(player.position, this.transform.position) > attackRange)
            {
                this.transform.Translate(0, 0, 0.05f);
                anim.SetBool("Run", true);
                //anim.SetBool("isFire", false);

            }


        }
        else
        {
            anim.SetTrigger("Draw");
            anim.SetBool("Run", false);
            anim.SetBool("isFire", false);
        }
    }

    private void Shooting()
    {
        //RaycastHit hit;

        //if (Physics.Raycast(transform.position, player.position, out hit, range, playerMask))
        //{
        //Debug.Log(hit.transform.name);
        //Target target = hit.transform.GetComponent<Target>();
        //var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //if (target != null)
        //{
        //hit.rigidbody.AddForceAtPosition(ray.direction * pushForce, hit.point);
        //target.TakeDamage(damage);
        //}
        currentPushTime = 0.0f;
        hurtSource.Play();
        if (currentPushTime < maxPushTime)
        {
            pushDirection = player.transform.forward * -pushSpeed;
            currentPushTime += pushStoppingSpeed;
        }
        else
        {
            pushDirection = Vector3.zero;
        }
        playerController.Move(pushDirection * Time.deltaTime);

        //}
    }
}
