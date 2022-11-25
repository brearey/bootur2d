using UnityEngine;

public class ParticlesSpawner : MonoBehaviour
{
    [System.Serializable]
    public struct SpawnableObject
    {
        public GameObject prefab;
    }

    public SpawnableObject particlePrefab;

    private GameObject question;

    public void Spawn()
    {
        Instantiate(particlePrefab.prefab, transform.position, transform.rotation);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("question");
            Spawn();
        }
    }
}
