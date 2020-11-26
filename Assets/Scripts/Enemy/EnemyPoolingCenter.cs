using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPoolingCenter : MonoBehaviour
{

    public GameObject prefab;
    public int amount = 1;
    public int instanciateGap = 5;

    // Start is called before the first frame update
    void Start()
    {
        InitializePool();
        InvokeRepeating("GetEnemyFromPool", 1f, instanciateGap);

    }

    private void InitializePool()
    {
        for (int i = 0; i < amount; i++)
        {
            AddEnemyToPool();
        }
    }

    private void AddEnemyToPool()
    {
        GameObject enemy = Instantiate(prefab, this.transform.position, Quaternion.identity, this.transform);
        enemy.SetActive(false);
    }

    private GameObject GetEnemyFromPool()
    {
        GameObject enemy = null;

        for (int i = 0; i < this.transform.childCount; i++)
        {
            if (!this.transform.GetChild(i).gameObject.activeSelf)
            {
                enemy = transform.GetChild(i).gameObject;
                break;
            }
        }

        if (enemy == null)
        {
            AddEnemyToPool();
            enemy = this.transform.GetChild(transform.childCount - 1).gameObject;
        }

        enemy.transform.position = this.transform.position;
        enemy.transform.GetChild(2).gameObject.SetActive(true);
        enemy.SetActive(true);

        return enemy;
    }
}
