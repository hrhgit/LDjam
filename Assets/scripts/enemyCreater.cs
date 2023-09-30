using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyCreater : MonoBehaviour
{
    public Transform player;
    public GameObject enemy;
    public int enemyCount=1;
    public float enemyInterval=2f;
    private float scopex = 13f;
    private float scopey = 13f;
    private float timer = 1f;
    void Start()
    {
        float aspectRatio = Screen.width / (float)Screen.height;
        // 计算屏幕中可见的世界空间大小
        scopey = 2.0f * 6f;
        scopex = scopey * aspectRatio;

    }

    // Update is called once per frame
    void Update()
    {
        if (timer <= 0)
        {
            for (int i = 0; i < enemyCount; i++)
            {
                createEnemy();
            }
            timer = enemyInterval;
        }
        timer = timer -= Time.deltaTime;
        
    }

    void createEnemy()
    {
        int c = Random.Range(0, 2);
        float x = c*(Random.Range(0, 2)*2 - 1) * (scopex / 2f - 0.1f)+(1-c)*(Random.Range(0.1f,scopex-0.1f)-scopex / 2f);
        float y = (1-c)*(Random.Range(0, 2)*2 - 1) * (scopey / 2f - 0.1f)+c*(Random.Range(0.1f,scopey-0.1f)-scopey / 2f);
        GameObject ene=Instantiate(enemy, new Vector2(x, y), Quaternion.identity);
        ene.GetComponent<enemy>().player = player;
    }
}
