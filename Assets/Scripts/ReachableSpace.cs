using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReachableSpace : MonoBehaviour {
    public int file;
    public int rank;

    void Start() {
        GameEvents.removeReachableGameObjects.AddListener(destroySelf);
        file = (int)transform.position.x;
        rank = (int)transform.position.y;
    }

    private void destroySelf() {
        Destroy(gameObject);
    }
}
