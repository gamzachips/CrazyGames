using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    [SerializeField]
    GameObject monsterPrefab;
    GameObject monster;

    [SerializeField]
    float spawnTime = 5f;

    float spawnTimer = 0f;

    private void Start()
    {
        SpawnMonster();
    }

    private void Update()
    {
        if(monster == null)
        {
            spawnTimer += Time.deltaTime;
            if(spawnTimer > spawnTime)
            {
                spawnTimer = 0f;
                SpawnMonster();
            }
        }
    }

    void SpawnMonster()
    {
        monster = Instantiate(monsterPrefab);
        monster.transform.position = transform.position;
    }
}
