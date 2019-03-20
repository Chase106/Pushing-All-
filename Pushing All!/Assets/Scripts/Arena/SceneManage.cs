using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManage : MonoBehaviour
{
    public int enemyCount;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyCount == 8)
        {
            Cursor.lockState = CursorLockMode.None;
            SceneManager.LoadScene("TitleScreen");
        }
    }
}
