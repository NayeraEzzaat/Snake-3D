using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour {

    public GameObject bullet;
    [Range (0,200)]
    public float bulletSpeed;
    [HideInInspector]
    public bool canShoot;

	void Start () {
        canShoot = false;
	}
	
	void Update () {
        if (canShoot) {
            if (Input.GetKeyDown(KeyCode.Space)) {
                GameObject _bullet = Instantiate(bullet);
                _bullet.transform.SetParent(this.GetComponentInParent<Transform>());
                _bullet.transform.position = this.transform.position;
                _bullet.transform.parent = null;
                _bullet.GetComponent<Rigidbody>().velocity = (transform.right * (-bulletSpeed));
            }
        }
	}

    public void StartShooting() {
        canShoot = true;
        StartCoroutine(Wait(10));
    }

    private IEnumerator Wait(float waitTime) {
        yield return new WaitForSeconds(waitTime);
        canShoot = false;
        
    }
}
