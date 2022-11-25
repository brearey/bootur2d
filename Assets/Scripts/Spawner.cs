using UnityEngine;

public class Spawner : MonoBehaviour
{
    [System.Serializable]
    public struct SpawnableObject {
        public GameObject prefab;
        [Range(0f, 1f)]
        public float spawnChance;
    }

    public SpawnableObject[] objects;

    public float minSpawnRate = 1f;
    public float maxSpawnRate = 2f;

    private void OnEnable() {
        Invoke(nameof(Spawn), Random.Range(minSpawnRate, maxSpawnRate));
    }
    private void OnDisable() {
        CancelInvoke();
    }

    private void Spawn() {
        float spawnChance = Random.value;

        foreach (var obj in objects) {
            if (spawnChance < obj.spawnChance) {
                GameObject obstacle = Instantiate(obj.prefab);
                obstacle.transform.position += transform.position;

                if (obstacle.CompareTag("question"))
                {
                    obstacle.transform.position += new Vector3(0, 2f, 0);
                }
                break;
            }

            spawnChance -= obj.spawnChance;
        }

        Invoke(nameof(Spawn), Random.Range(minSpawnRate, maxSpawnRate));
    }
}
