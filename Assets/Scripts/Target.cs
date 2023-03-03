using System;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    [SerializeField]
    bool isFriendly = false;

    [SerializeField]
    int points = 500;

    public Action<Target> OnTargetShot;
    public Action OnTargetEscaped;

    public WaveConfig waveConfig;
    private List<Transform> wayPoints;
    int wayPointIndex = 0;
    bool deadOrEscaped = false;


    private void Update() {
        if (waveConfig != null && !deadOrEscaped) {
            Move();
        }
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
            Destroy(gameObject);
        }
        else {
            Vector3 wayPointWithMyZ = wayPoints[wayPointIndex].position;
            wayPointWithMyZ.z = transform.position.z;
            transform.position = Vector3.MoveTowards(transform.position, wayPointWithMyZ, waveConfig.GetSpeed() * Time.deltaTime);
        }
    }

    public bool GetIsFriendly() {
        return isFriendly;
    }

    public int GetPoints() {
        return points;
    }

    public void SetWaveConfig(WaveConfig waveConfig) {
        this.waveConfig = waveConfig;
        wayPoints = waveConfig.GetWaypoints();
    }

    public void TargetShot() {
        if(OnTargetShot != null) {
            OnTargetShot.Invoke(this);
        }
        Destroy(gameObject);
    }

    

}
