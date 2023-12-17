using UnityEngine;

public class FireBallSpawner : MonoBehaviour
{
    public GameObject fireballPrefab;
    public Transform player;
    public float fireballSpeed = 5f;
    public float distanceAway = 2f;
    public float spawnDelay = 2f;
    public float repeatRate = 3f; // Interval between fireball spawns

    void Start()
    {
        InvokeRepeating("SpawnFireball", spawnDelay, repeatRate);
    }
    void SpawnFireball()
    {
        if (player != null && fireballPrefab != null)
        {
            Vector3 directionToPlayer = player.position - transform.position;
            directionToPlayer.Normalize();

            Vector3 spawnPosition = player.position + (directionToPlayer * distanceAway);

            GameObject newFireball = Instantiate(fireballPrefab, spawnPosition, Quaternion.identity);

            Rigidbody fireballRB = newFireball.GetComponent<Rigidbody>();
            if (fireballRB != null)
            {
                fireballRB.velocity = directionToPlayer * fireballSpeed;
            }
        }
    }
}