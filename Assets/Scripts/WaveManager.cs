using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [SerializeField] WaveConfig[] waves;
    [SerializeField] float timeToStart = 1.5f;
    [SerializeField] float timeBetweenWaves = 3f;
    [SerializeField] bool shouldLoop = false;

    int activeTargets = 0;
    int activeWave = 0;

    int gameScore = 0;
    int streak = 0;

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
        streak = 0;
        // TODO: multiplier back to 1
        FinishHandleTargetEscapedOrDestroyed();
    }

    void HandleTargetShot(Target target) {
        // Add points to total points
        gameScore += target.GetPoints();
        print(gameScore);
        // Add 1 to current streak
        streak = target.GetIsFriendly() ? 0 : streak + 1;
        print(streak);
        // TODO: If reached multiple of X, update multiplier
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
