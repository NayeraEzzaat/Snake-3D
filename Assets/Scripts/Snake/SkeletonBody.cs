using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonBody : MonoBehaviour {

    private bool _spawn;

    void Start() {
        _spawn = false;
    }

    public void AddBool() {
        GetComponentInParent<SnakeSkeleton>()._collide.Add(_spawn);
    }

    private void OnTriggerEnter(Collider other) {
        Debug.Log("AddBool");
        _spawn = other.gameObject.CompareTag("Floor");
        AddBool();
    }

}
