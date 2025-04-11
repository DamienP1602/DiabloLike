using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerComponent : MonoBehaviour
{
    [SerializeField] int limitSpawn;
    [SerializeField] float timeBetweenSpawn;
    [SerializeField] float range;
    float currentTime;
    [SerializeField] List<BaseEnemy> spawnedEnemy;
    [SerializeField] List<BaseEnemy> prefabToSpawn;

    private void Awake()
    {
        spawnedEnemy = new List<BaseEnemy>();
    }

    void Update()
    {
        currentTime += Time.deltaTime;
        if (currentTime >= timeBetweenSpawn)
        {
            currentTime = 0.0f;
            Spawn();
            CheckForNullInstance();
        }
    }

    void Spawn()
    {
        if (spawnedEnemy.Count == limitSpawn) return;
        if (prefabToSpawn.Count == 0) return;

        int _randomIndex = Random.Range(0,prefabToSpawn.Count);

        BaseEnemy _enemy = Instantiate(prefabToSpawn[_randomIndex]);
        float _x = Mathf.Cos(Time.time) * range;
        float _z = Mathf.Sin(Time.time) * range;
        _enemy.transform.position = transform.position + new Vector3(_x,transform.position.y,_z);

        spawnedEnemy.Add(_enemy);
    }

    public void CheckForNullInstance()
    {
        int _size = spawnedEnemy.Count;
        for (int _i = 0; _i < _size; _i++)
        {
            BaseEnemy _enemy = spawnedEnemy[_i];
            if (!_enemy)
            {
                spawnedEnemy.Remove(_enemy);
                CheckForNullInstance();
                return;
            }
        }
    }
}
