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

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        agent.SetDestination(player.position);
    }
}
