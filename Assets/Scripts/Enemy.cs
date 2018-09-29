using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    [Range(0, 100)]
    public float positiveRange;
    [Range(-100, 0)]
    public float negativeRange;
    [Range(0, 100)]
    public float speed;
    [Range(0, 180)]
    public float angle;
    public bool isMovingTowardNegative;

    private float _xPosition;
    private float _yPosition;
    private Rigidbody _rb;
    private float currentRange;
    private bool _isMoving;
    private bool _isFalling;

    void Start() {
        transform.eulerAngles = new Vector3(0, angle, 0);
        _rb = GetComponent<Rigidbody>();
        currentRange = 0;
        _isMoving = false;
    }

    void Update() {
        _isMoving = FindObjectOfType<Snake>().isMoving;
        _isFalling = FindObjectOfType<Snake>().isFalling;
        if (_isMoving && !_isFalling) {
            if (isMovingTowardNegative) {

                if (currentRange < positiveRange) {
                    _rb.velocity = transform.forward * (speed/5);
                    currentRange += (speed / 20);
                } else {
                    isMovingTowardNegative = false;
                }

            } else {

                if (currentRange > negativeRange) {
                    _rb.velocity = transform.forward * (-speed/5);
                    currentRange -= (speed / 20);
                } else {
                    isMovingTowardNegative = true;
                }

            }
        } else {
            _rb.velocity = Vector3.zero;
        }

    }

    private void OnCollisionEnter(Collision collision) {
        if (isMovingTowardNegative) {
            isMovingTowardNegative = false;
        } else {
            isMovingTowardNegative = true;
        }
    }

}
