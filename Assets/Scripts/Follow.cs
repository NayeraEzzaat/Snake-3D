using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour {

    public Transform target;
    private Rigidbody _rb;
    private float speed;

	void Start () {
        _rb = GetComponent<Rigidbody>();
	}
	
	void Update () {

        if (GetComponentInParent<Snake>().isMoving) {
            transform.LookAt(target);
            speed = GetComponentInParent<Snake>().speed;
            if (GetComponentInParent<Snake>().isFalling) {
                _rb.constraints = RigidbodyConstraints.None;
                _rb.velocity = (transform.forward * (speed / 5));
            } else if (GetComponentInParent<Snake>().isDead) {
                _rb.velocity = Vector3.zero;
            } else if (Vector3.Distance(transform.position, target.transform.position) > (transform.localScale.x + (transform.localScale.x * 0.01))) {
                _rb.velocity = (transform.forward * (speed / 5));
            } else {
                _rb.velocity = (transform.forward * (speed / 10));

            }
        } else {
            _rb.velocity = Vector3.zero;
        }

	}
}
