using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Head : MonoBehaviour {

    private bool _headCollide = true;
    private List<Collider> _c1 = new List<Collider>();
    private List<Collider> _c2 = new List<Collider>();
    private bool _isYellow;
    private bool _isPurple;
    private bool _isBlue;
    private bool _isBlack;


    private void OnTriggerEnter(Collider other) {

        if (other.gameObject.CompareTag("Solid Wall")) {
            GetComponentInParent<Move>().Die();
            if (_isBlack) {
                GetComponentInParent<Snake>().cameraBlock.SetActive(false);
                _isBlack = false;
            }
        }
        else if (other.gameObject.CompareTag("Breakable Wall")) {
            GetComponentInParent<Move>().Hit(6);
            Destroy(other.gameObject);
            if (_isYellow) {
                GetComponentInChildren<Shoot>().canShoot = false;
                _isYellow = false;
            }
            if (_isPurple) {
                _headCollide = true;
                _isPurple = false;
                CollidedBodies();
            }
            if (_isBlack) {
                GetComponentInParent<Snake>().cameraBlock.SetActive(false);
                _isBlack = false;
            }
        }
        else if (other.gameObject.CompareTag("Enemy")) {
            GetComponentInParent<Move>().Hit(4);
            Destroy(other.gameObject);
            if (_isYellow) {
                GetComponentInChildren<Shoot>().canShoot = false;
                _isYellow = false;
            }
            if (_isPurple) {
                _headCollide = true;
                _isPurple = false;
                CollidedBodies();
            }
            if (_isBlack) {
                GetComponentInParent<Snake>().cameraBlock.SetActive(false);
                _isBlack = false;
            }
        } 
        else if (other.gameObject.CompareTag("Body") || other.gameObject.CompareTag("Tail")) {
            if (_headCollide) {
                GetComponentInParent<Move>().Hit(8);
                if (_isYellow) {
                    GetComponentInChildren<Shoot>().canShoot = false;
                    _isYellow = false;
                }
                if (_isBlack) {
                    GetComponentInParent<Snake>().cameraBlock.SetActive(false);
                    _isBlack = false;
                }
                if (_isBlue) {
                    CollideWithBody(9, 10, 1);
                    CollideWithBody(9, 8, 1);
                    CollideWithBody(9, 11, 1);
                }
            } else {
                for (int i = 0; i < this.GetComponentsInParent<Collider>().Length; i++) {
                    UncollideWithBody(this.GetComponentInParent<Collider>(), other.GetComponent<Collider>());
                }
                UncollideBodies(other.gameObject);
            }
        } 
        else if (other.gameObject.CompareTag("Green Food")) {
            GetComponentInParent<Move>().Gain1();
            Destroy(other.gameObject);
        }
        else if (other.gameObject.CompareTag("Red Food")) {
            GetComponentInParent<Move>().Hit(2);
            Destroy(other.gameObject);
        }
        else if (other.gameObject.CompareTag("Orange Food")) {
            GetComponentInParent<Move>().FlipHead();
            Destroy(other.gameObject);
        }
        else if (other.gameObject.CompareTag("Yellow Food")) {
            _isYellow = true;
            StartCoroutine(ShootTime(10));
            GetComponentInChildren<Shoot>().StartShooting();
            Destroy(other.gameObject);
        }
        else if (other.gameObject.CompareTag("Blue Food")) {
            _isBlue = true;
            UncollideWithBody(9, 10, 10, 1);
            UncollideWithBody(9, 8, 10, 1);
            UncollideWithBody(9, 11, 10, 1);
            Destroy(other.gameObject);
        } 
        else if (other.gameObject.CompareTag("Purple Food")) {
            _isPurple = true;
            _headCollide = false;
            UncollideWithBody(11, 8, 5, 0);
            StartCoroutine(Wait(5));
            Destroy(other.gameObject);
        } 
        else if (other.gameObject.CompareTag("Black Food")) {
            _isBlack = true;
            CameraBlock();
            Destroy(other.gameObject);
        }

    }

    public void UncollideWithBody(Collider x , Collider y) {
        Physics.IgnoreCollision(x, y);
        _c1.Add(x);
        _c2.Add(y);
    }

    private IEnumerator Wait(float z) {
        yield return new WaitForSeconds(z);
        _headCollide = true;

        CollidedBodies();
    }

    public void CollidedBodies() {
        for (int i = 0; i < _c1.Count; i++) {
            if (_c1[i] != null && _c2[i] != null) {
                Physics.IgnoreCollision(_c1[i], _c2[i], false);
            }
        }
        _c1.Clear();
        _c2.Clear();
        _isPurple = false;
    }

    public void UncollideWithBody(int x, int y, float z, int i) {
        Physics.IgnoreLayerCollision(x, y);
        StartCoroutine(Wait(x, y, z, i));
    }
    public void CollideWithBody(int x, int y, int i) {
        Physics.IgnoreLayerCollision(x, y, false);
        if (i == 1) {
            _isBlue = false;
        }
    }

    private IEnumerator Wait(int x, int y, float z, int i) {
        yield return new WaitForSeconds(z);
        CollideWithBody(x, y, i);
    }

    private IEnumerator WaitTime(float z) {
        yield return new WaitForSeconds(z);
        GetComponentInParent<Snake>().cameraBlock.SetActive(false);
        _isBlack = false;
    }

    public void UncollideBodies(GameObject x) {
        List<GameObject> snakeBody = GetComponentInParent<Snake>().snakeBody;
        int index = 0;
        for (int i = 0; i < snakeBody.Count; i++) {
            if (snakeBody[i].gameObject == x) {
                index = i;
                break;
            }
        }

        for (int j = index + 1; j < snakeBody.Count; j++) {
            for (int k = 0; k < index; k++) {
                UncollideWithBody(snakeBody[j].GetComponent<Collider>(), snakeBody[k].GetComponent<Collider>()); 
            }
        }
        
    }

    public void CameraBlock() {
        GetComponentInParent<Snake>().cameraBlock.SetActive(true);
        StartCoroutine(WaitTime(3));
    }

    private IEnumerator ShootTime(float waitTime) {
        yield return new WaitForSeconds(waitTime);
        _isYellow = false;
    }
}
