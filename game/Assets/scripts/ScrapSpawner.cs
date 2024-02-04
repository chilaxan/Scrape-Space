using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrapSpawner : MonoBehaviour {
    public float SPAWN_TIME = 1f;
    public GameObject scrapPrefab;
    
    public float timeToSpawn = 0;

    // Update is called once per frame
    void Update() {
        timeToSpawn -= Time.deltaTime;
        if (timeToSpawn <= 0) {
            timeToSpawn = SPAWN_TIME;
            SpawnScrap();
        }
    }
    void SpawnScrap() {
        GameObject scrap = Instantiate(scrapPrefab, transform.position, Quaternion.identity);
        Vector3 randomDirection = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized * 6f;
        scrap.transform.position += randomDirection;
        scrap.transform.parent = transform;
    }
}
