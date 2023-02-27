using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Wave Config", fileName = "New Wave Config")]
public class WaveConfig : ScriptableObject {
    [SerializeField] Transform path;

    [SerializeField] Target[] targets;

    [SerializeField] float speed = 2f;

    [SerializeField] float timeBetweenTargets = 1.5f;

    public List<Transform> GetWaypoints() {
        List<Transform> waypoints = new List<Transform>();
        foreach(Transform transform in path) {
            waypoints.Add(transform);
        }

        return waypoints;
    }

    public Target[] GetTargets() {
        return targets;
    }


    public float GetSpeed() {
        return speed;
    }

    public float GetTimeBetweenTargets() {
        return timeBetweenTargets;
    }

   
}
