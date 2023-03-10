using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [SerializeField] GameManager gameManager;
    [SerializeField] WaveConfig[] waves;
    [SerializeField] float timeToStart = 1.5f;
    [SerializeField] float timeBetweenWaves = 3f;
    [SerializeField] bool shouldLoop = false;

    int activeTargets = 0;
    int activeWave = 0;

    private void Start() {
        StartCoroutine(RestartGame());    
    }

    IEnumerator RestartGame() {
        yield return new WaitForSeconds(timeToStart);
        StartCoroutine(SpawnWave(waves[0]));
    }

    IEnumerator SpawnWave(WaveConfig wave) {
        Target[] targets = wave.GetTargets();
        activeTargets = targets.Length;
        for(int idx = 0; idx < targets.Length; idx++) { 
            List<Transform> wayPoints = wave.GetWaypoints();
            Vector3 startingPosition = wayPoints[0].position;
            // Set z to index so newer targets will be behind older ones in the case they overlap
            startingPosition.z = idx;
            Target target = Instantiate(targets[idx], startingPosition, wayPoints[0].rotation);
            target.SetWaveConfig(wave);
            target.OnTargetEscaped += HandleTargetEscaped;
            target.OnTargetShot += HandleTargetShot;
            yield return new WaitForSeconds(wave.GetTimeBetweenTargets());
        }
    }


    void HandleTargetEscaped() {
        gameManager.HandleTargetEscaped();
        FinishHandleTargetEscapedOrDestroyed();
    }

    void HandleTargetShot(Target target) {
        gameManager.HandleTargetShot(target);
        FinishHandleTargetEscapedOrDestroyed();
    }

    void FinishHandleTargetEscapedOrDestroyed() {
        activeTargets--;
        if(activeTargets == 0) {
            if(activeWave < waves.Length-1) {
                activeWave++;
                StartCoroutine(DelayedSpawn(waves[activeWave]));
            } else if(shouldLoop) {
                activeWave = 0;
                StartCoroutine(DelayedSpawn(waves[activeWave]));
            }
        }
    }

    IEnumerator DelayedSpawn(WaveConfig wave) {
        yield return new WaitForSeconds(timeBetweenWaves);
        StartCoroutine(SpawnWave(wave));
    }
}
