using UnityEngine;

public class Shoot : MonoBehaviour
{
    public float damage = 10f;
    public float range = 100f;
    public float pushForce;
    public Camera fpsCam;
    public LayerMask Enemy;
    public GameObject impactEffect;
    public GameObject player;
    public Animator anim;
    public int animLayer = 0;
    //public Animator playerAnimator;
    // Start is called before the first frame update


    // Update is called once per frame
    void Update()
    {
        if (GetComponent<ArmControllerScript>().currentAmmo != 0) {
            if (Input.GetButtonDown("Fire1") && player.GetComponent<Player_Movement2>().AbleToShoot == true && isPlaying(anim, "Idle"))
            {
                Shooting();
                print(gameObject.name);
                //playerAnimator.SetTrigger("Shoot");
            }
        }
        
    }

    void Shooting()
    {
        RaycastHit hit;

        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range, Enemy))
        {
            Debug.Log(hit.transform.name);
            Target target = hit.transform.GetComponent<Target>();
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            //if (target != null)
            //{
                hit.rigidbody.AddForceAtPosition(ray.direction * pushForce, hit.point);
            //target.TakeDamage(damage);
            //}
            GameObject impactGO = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(impactGO,2f);

        }
    }
    bool isPlaying(Animator anim, string stateName)
    {
        if (anim.GetCurrentAnimatorStateInfo(animLayer).IsName(stateName) &&
                anim.GetCurrentAnimatorStateInfo(animLayer).normalizedTime < 1.0f)
            return true;
        else
            return false;
    }
}
