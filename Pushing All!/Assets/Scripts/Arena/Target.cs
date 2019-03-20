using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    public GameObject SceneManage;
    void Update()
    {
       if (transform.position.y < -20)
        {
            Destroy(gameObject);
            SceneManage.GetComponent<SceneManage>().enemyCount++;
        } 

    }

    

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("KillerBuilding"))
        {
            SceneManage.GetComponent<SceneManage>().enemyCount++;
            Destroy(gameObject);
            
        }
    }

}
    