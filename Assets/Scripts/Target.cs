using System;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    //public static Action OnTargetDestroyed;
    public Action OnTargetEscaped;

    public WaveConfig waveConfig;
    private List<Transform> wayPoints;
    int wayPointIndex = 0;
    bool deadOrEscaped = false;


    private void Update() {
        if(waveConfig != null && !deadOrEscaped)
            Move();
    }

    void Move() {
        bool madeItToCurrentWayPoint = Vector2.Distance(transform.position, wayPoints[wayPointIndex].position) < 0.001f;
        if (madeItToCurrentWayPoint && wayPointIndex < wayPoints.Count-1) {
            wayPointIndex++;
        } else if(madeItToCurrentWayPoint) {
            deadOrEscaped = true;
            if(OnTargetEscaped != null) {
                OnTargetEscaped.Invoke();
            }
        } else {
            transform.position = Vector2.MoveTowards(transform.position, wayPoints[wayPointIndex].position, waveConfig.GetSpeed() * Time.deltaTime);
        }
    }

    public void SetWaveConfig(WaveConfig waveConfig) {
        this.waveConfig = waveConfig;
        wayPoints = waveConfig.GetWaypoints();
    }

    

}
