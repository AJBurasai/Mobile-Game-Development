using UnityEngine;
using System;
using System.Collections;

public class enemySpawner : MonoBehaviour
{
    /*  This script is for handling enemy spawning and will be applied to the enemy spawner game object.
        Different enemy types will have different chances of appearing on screen and their spawn position
        will be randomly selected. This script will also handle the automatic difficulty increase. */

        
    // --- Variables --- 
    public GameObject enemy1;           // prefab for first enemy type, basic grunt
    public GameObject enemy2;           // prefab for second enemy type, elite flier
    public GameObject enemy3;           // prefab for third enemy type, shooter
    public GameObject enemy4;           // prefab for fourth enemy type, tank
    float spawnPosY;                    // enemy spawn position, y axis
    public float spawnTimer = 4.0f;     // time between enemy spawns, 4 sec by default
    public int enemyTypeSelector;       // random number that decides what enemy will be spawned next
    public GameObject nextEnemyType;    // the next enemy that will get spawned
    public int enemySpawnTotal = 0;     // tracks how many enemies have been spawned in total

    void Start()
    {
        StartCoroutine(Spawner());      // start random spawn position coroutine on game start
    }

    IEnumerator Spawner()       // coroutine to spawn enemies
    {   
        while (true)
        {
            enemyTypeSelector = UnityEngine.Random.Range(0, 13);    // randomise next enemy

            if (enemyTypeSelector < 5)      // chance next enemy will be basic grunt (enemy1)
            {
                nextEnemyType = enemy1;
            }

            else if (enemyTypeSelector < 9)      // chance next enemy will be elite flier (enemy2)
            {
                nextEnemyType = enemy2;
            }

            else if (enemyTypeSelector < 11)       // chance next enemy will be shooter (enemy3)
            {
                nextEnemyType = enemy3;
            }

            else if (enemyTypeSelector < 13)
            {
                nextEnemyType = enemy4;
            }


            if (enemySpawnTotal == 10)       // if 10, 20, 30 etc enemies have been spawned since game start, reduce respawn timer
            {
                spawnTimer = 3f;
            }
            else if (enemySpawnTotal == 20)  
            {
                spawnTimer = 2f;
            }
            else if (enemySpawnTotal == 30)
            {
                spawnTimer = 1.5f;
            }
            else if (enemySpawnTotal == 40)
            {
                spawnTimer = 1f;
            }
            else if (enemySpawnTotal == 50)
            {
                spawnTimer = 0.5f;
            }

            SpawnEnemy();
            enemySpawnTotal += 1;
            yield return new WaitForSeconds(spawnTimer);        // delay next enemy spawn
            
            
        }
    }

    void SpawnEnemy()      // function for spawning enemy
    {
        spawnPosY = (UnityEngine.Random.Range(-4.0f, 3.5f));    // randomises Y position
        Vector3 spawnPos = new Vector3(10f, spawnPosY, 0f);      // creates vector3 for spawn position using random Y
        Instantiate(nextEnemyType, spawnPos, Quaternion.identity);     // instantiate randomised enemy prefab
    }


}
