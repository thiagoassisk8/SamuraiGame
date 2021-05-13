
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    // public GameObject[] objectSpawn;

    struct SpawnTime {
        public float instantiateTime;
        public float interval;
        public float variation;
    }

    // public Sprite[] cactusSprites;

    // public GameObject cactusPrefabRef;
    public GameObject flyingPrefabRef;

    // SpawnTime cactus;
    SpawnTime flying;

    public bool stopSpawn = false;

    float flyingMax = 4;
    float flyingMin = -1;

    public float speed = 4;

    void Start() {
        

        flying.instantiateTime = 0;
        flying.interval = 0.7f;
        flying.variation = 0.5f;
        // InvokeRepeating("Spawn", flying.interval, flying.variation);

    }
    
    // void Spawn(){
        // int randIndex = (int)Random.Range(0,objectSpawn.Length);
        // Instantiate(objectSpawn[randIndex], transform.position, Quaternion.identity);
    // }

    void Update() {
        

        // spawn flying
        if (Time.time > flying.instantiateTime && !stopSpawn) {
            GameObject obj = Instantiate(flyingPrefabRef, new Vector3(5, Random.Range(flyingMax, flyingMin)), Quaternion.identity);
            flying.instantiateTime = Time.time + Random.Range(flying.interval - flying.variation, flying.interval + flying.variation);
            // Start();
            // Spawn();
        }
        

    }
    
}
