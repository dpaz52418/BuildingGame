using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DinosaurHopCacti : MonoBehaviour
{
    public GameObject cactusPrefab;
    public float spawnRate = 2f;
    public Transform spawnPoint;

    private float spawnTimer;

    void Update()
    {
        spawnTimer += Time.deltaTime;
        if (spawnTimer >= spawnRate)
        {
            spawnTimer = 0f;
            Vector3 pos = spawnPoint.position;
            Quaternion rot = Quaternion.identity;
            Instantiate(cactusPrefab, pos, rot, transform);
        }
    }
}
