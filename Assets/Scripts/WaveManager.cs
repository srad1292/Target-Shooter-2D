using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [SerializeField] WaveConfig[] waves;
    [SerializeField] float timeToStart = 1.5f;
    [SerializeField] float timeBetweenWaves = 3f;

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
        foreach (Target targetPre in targets) {
            List<Transform> wayPoints = wave.GetWaypoints();
            Target target = Instantiate(targetPre, wayPoints[0].position, wayPoints[0].rotation);
            target.SetWaveConfig(wave);
            target.OnTargetEscaped += HandleTargetHiddenOrDestroyed;
            yield return new WaitForSeconds(wave.GetTimeBetweenTargets());
        }
    }

    void HandleTargetHiddenOrDestroyed() {
        activeTargets--;
        if(activeTargets == 0) {
            print("Wave completed!");
            if(activeWave < waves.Length-1) {
                activeWave++;
                StartCoroutine(DelayedSpawn(waves[activeWave]));
            }
        }
    }

    IEnumerator DelayedSpawn(WaveConfig wave) {
        yield return new WaitForSeconds(timeBetweenWaves);
        StartCoroutine(SpawnWave(wave));
    }
}
