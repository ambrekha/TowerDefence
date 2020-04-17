using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{

    [SerializeField] float movementPeriod = 0.5f;
    [SerializeField] ParticleSystem goalParticle;

    // Start is called before the first frame update
    void Start()
    {
        PathFinder pathfinder = FindObjectOfType<PathFinder>();
        var path = pathfinder.getPath();
        StartCoroutine(FollowPath(path));
    }

    IEnumerator FollowPath(List<Waypoint> path) // coroutine -> remplace l'utilisation de Update()
    {
        // IEnumerator pour utiliser des queues dans Unity, pour mettre une série d'instructions ou de temps, comme un minuteur
        foreach(Waypoint waypoint in path)
        {
            transform.position = waypoint.transform.position;
            yield return new WaitForSeconds(movementPeriod); // attend une seconde, stoppe l'exécution de la méthode et retourne à Start() puis rappelle FollowPath et fait se déplacer l'ennmi toutes les X secondes
                                                             // Avec la coroutine, l'ennemi courant se déplace selon le path spécifié
        }
        SelfDestruct();
    }

    private void SelfDestruct()
    {
        var vfx = Instantiate(goalParticle, transform.position, Quaternion.identity);
        vfx.Play();
        float particleDuration = vfx.main.duration;

        Destroy(vfx.gameObject, particleDuration);
        Destroy(gameObject); // enemy is destroyed when reaching the goal in an explosion
    }
}
