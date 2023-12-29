using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


/*
 * References used to implement EnemyNavigation:
 * https://www.youtube.com/watch?v=u2EQtrdgfNs
 * https://www.youtube.com/watch?v=UjkSFoLxesw&t=5s
 */
public class EnemyNavigation : MonoBehaviour
{
    private Transform player;
    private NavMeshAgent agent;

    // Refresh Enemy path every .2 seconds
    [Header("Stats")]
    public float pathUpdateDelay = .2f;

    // Keep track when path was last updated
    private float pathLastUpdated;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        agent.speed = Random.Range(7f, 11f);
    }

    // Update is called once per frame
    void Update()
    {
        UpdatePath();
    }

    void UpdatePath()
    {
        if (Time.time >= pathLastUpdated)
        {
            pathLastUpdated = Time.time + pathUpdateDelay;
            agent.SetDestination(player.transform.position);
        }
    }
}
