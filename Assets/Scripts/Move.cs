using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour {

    private int _direction;
    private Rigidbody _rb;
    private float _speed;
    private bool _isPaused;
    private bool _isMoving;
    private bool _isDead;
    private float _snakeRotation;
    private int _triggerCount;
    private Transform _spawnTransform;

    void Start() {
        _direction = 0;
        _triggerCount = 0;
        _rb = GetComponent<Rigidbody>();
    }

    void Update() {
        _isPaused = GetComponentInParent<Snake>().gameManager.GetComponent<Settings>()._isPaused;
        _isMoving = GetComponentInParent<Snake>().isMoving;
        _isDead = GetComponentInParent<Snake>().isDead; ;
        _snakeRotation = GetComponentInParent<Transform>().rotation.y;

        if (Input.GetKeyDown(KeyCode.D) && _direction != 2 && !_isPaused && !_isDead) {
            if (_isMoving) {
                transform.eulerAngles = new Vector3(0, 180, 0);
                _direction = 1;
            } else if (!(!_isMoving && (_snakeRotation == 0))) {
                _direction = 1;
                transform.eulerAngles = new Vector3(0, 180, 0);
                GetComponentInParent<Snake>().isMoving = true;
                GetComponentInParent<Snake>().gameManager.GetComponentInChildren<Score>().isMoving = true;
            }
        } else if (Input.GetKeyDown(KeyCode.A) && _direction != 1 && !_isPaused && !_isDead) {
            if (_isMoving) {
                transform.eulerAngles = new Vector3(0, 0, 0);
                _direction = 2;
            } else if (!(!_isMoving && (_snakeRotation == 180))) {
                _direction = 2;
                transform.eulerAngles = new Vector3(0, 0, 0);
                GetComponentInParent<Snake>().isMoving = true;
                GetComponentInParent<Snake>().gameManager.GetComponentInChildren<Score>().isMoving = true;
            }
        } else if (Input.GetKeyDown(KeyCode.W) && _direction != 4 && !_isPaused && !_isDead) {
            if (_isMoving) {
                transform.eulerAngles = new Vector3(0, 90, 0);
                _direction = 3;
            } else if (!(!_isMoving && (_snakeRotation == 270))) {
                _direction = 3;
                transform.eulerAngles = new Vector3(0, 90, 0);
                GetComponentInParent<Snake>().isMoving = true;
                GetComponentInParent<Snake>().gameManager.GetComponentInChildren<Score>().isMoving = true;
            }
        } else if (Input.GetKeyDown(KeyCode.S) && _direction != 3 && !_isPaused && !_isDead) {
            if (_isMoving) {
                transform.eulerAngles = new Vector3(0, 270, 0);
                _direction = 4;
            } else if (!(!_isMoving && (_snakeRotation == 90))) {
                _direction = 4;
                transform.eulerAngles = new Vector3(0, 270, 0);
                GetComponentInParent<Snake>().isMoving = true;
                GetComponentInParent<Snake>().gameManager.GetComponentInChildren<Score>().isMoving = true;
            }
        }

        _speed = GetComponentInParent<Snake>().speed;
        move();

    }

    void move() {

        if (GetComponentInParent<Snake>().isFalling) {
            _rb.velocity = new Vector3(0, -0.2f, 0) * _speed;
        } else if (GetComponentInParent<Snake>().isDead || !_isMoving) {
            _rb.velocity = Vector3.zero;
        }
        else if(_isMoving) {
            _rb.velocity = (transform.right * (-_speed / 10));
        }

    }

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Floor") {
            _triggerCount++;
        }
    }
    private void OnTriggerExit(Collider other) {
        if (other.tag == "Floor") {
            _triggerCount--;
        }
        if (_triggerCount == 0) {
            GetComponentInParent<Snake>().isFalling = true;
            GetComponentInParent<Snake>().gameManager.GetComponentInChildren<Score>().isFalling = true;
        }
    }

    public void Die() {
        GetComponentInParent<Snake>().isDead = true;
        GetComponentInParent<Snake>().gameManager.GetComponentInChildren<Score>().isDead = true;
        GetComponentInParent<Snake>().gameManager.GetComponentInChildren<Settings>().isDead = true;
        GetComponentInParent<Snake>().isMoving = false;
        GetComponentInParent<Snake>().gameManager.GetComponentInChildren<Score>().isMoving = false;
        Time.timeScale = 0;
    }

    public void Hit(int x) {
        int count = x;
        List<GameObject> snakeBody = GetComponentInParent<Snake>().snakeBody;
        while (count != 0) {
            if (snakeBody.Count > 2) {
                Destroy(snakeBody[snakeBody.Count - 1]);
                snakeBody.RemoveAt(snakeBody.Count - 1);
            } else {
                Die();
            }
            count--;
        }
        snakeBody[snakeBody.Count - 1].tag = "Tail";
        if (snakeBody.Count < 3) {
            Die();
        }
    }

    public void Gain1() {
        List<GameObject> snakeBody = GetComponentInParent<Snake>().snakeBody;
        GameObject tailTrigger = GetComponentInParent<Snake>().tailTrigger;
        _spawnTransform = snakeBody[snakeBody.Count - 1].transform;
        GameObject trigger = Instantiate(tailTrigger);
        trigger.transform.SetParent(this.transform.parent);
        trigger.transform.position = _spawnTransform.position;
    }

    public void FlipHead() {

        GetComponentInParent<Snake>().isMoving = false;

        List<GameObject> snakeBody = GetComponentInParent<Snake>().snakeBody;
        snakeBody[snakeBody.Count - 1].tag = "Head";
        snakeBody[snakeBody.Count - 2].tag = "Head";
        
        Vector3 tmp = snakeBody[0].transform.position;
        snakeBody[0].transform.position = snakeBody[snakeBody.Count - 1].transform.position;
        snakeBody[snakeBody.Count - 1].transform.position = tmp;
        if (snakeBody.Count > 3) {
            snakeBody[1].tag = "Body";
        }

        if (snakeBody[0].transform.localEulerAngles.y == 0) {
            transform.eulerAngles = new Vector3(0, 180, 0);
            _direction = 1;
        } else if (snakeBody[0].transform.localEulerAngles.y == 90) {
            transform.eulerAngles = new Vector3(0, 270, 0);
            _direction = 4;
        } else if (snakeBody[0].transform.localEulerAngles.y == 180) {
            transform.eulerAngles = new Vector3(0, 0, 0);
            _direction = 2;
        } else if (snakeBody[0].transform.localEulerAngles.y == 270) {
            transform.eulerAngles = new Vector3(0, 90, 0);
            _direction = 3;
        }
        
        for (int i = 1; i < (snakeBody.Count/2); i++) {
            GameObject temp;
            temp = snakeBody[i];
            snakeBody[i] = snakeBody[snakeBody.Count - 1 - i];
            snakeBody[snakeBody.Count - 1 - i] = temp;
        }

        for (int i = 1; i < snakeBody.Count; i++) {
            snakeBody[i].GetComponent<Follow>().target = snakeBody[i-1].transform;
        }
        snakeBody[snakeBody.Count - 1].tag = "Tail";
        GetComponentInParent<Snake>().isMoving = true;
    }

}
