using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{
    [SerializeField] private Transform[] _spawnPoints;
    [SerializeField] private GameObject zombie;

    private int _spawnIndex = 0;

    private void Awake()
    {
        StartCoroutine(Spawn());
    }

    private IEnumerator Spawn()
    {
        while (true)
        {
            yield return new WaitForSeconds(2);

            if (_spawnPoints.Length > _spawnIndex)
            {
                Instantiate(zombie, _spawnPoints[_spawnIndex].position, Quaternion.identity);

                _spawnIndex++;
                if (_spawnIndex >= _spawnPoints.Length)
                    _spawnIndex = 0;
            }
        }
    }
}
