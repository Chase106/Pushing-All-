using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyCount : MonoBehaviour
{
    //public GameObject gun;
    public GameObject enemyCount;
    //Text ammoText;
    Text enemyText;
    // Start is called before the first frame update
    void Awake()
    {
        //ammoText = GetComponent<Text>();
        enemyText = GetComponent<Text>();
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //ammoText.text = gun.GetComponent<ArmControllerScript>().currentAmmo.ToString();
        enemyText.text = enemyCount.GetComponent<SceneManage>().enemyCount.ToString();
    }
}

