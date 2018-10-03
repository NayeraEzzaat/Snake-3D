using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeSkeleton : MonoBehaviour {

    private bool _spawn;
    [HideInInspector]
    public List<bool> _collide;

    private void Start() {
        _collide = new List<bool>();
    }

    void Update() {
        if (_collide.Count >= 5) {
            CheckCollision();
        }
    }

    public void CheckCollision() {
        _spawn = _collide[0] && _collide[1] && _collide[2] && _collide[3] && _collide[4];
        Debug.Log("Here");
        //if (_spawn) {
        //    GetComponentInParent<Spawning>().SpawnSnake();
        //} else {
        //    GetComponentInParent<Spawning>().RandomLocation();
        //}
        Destroy(this);
    }


}
