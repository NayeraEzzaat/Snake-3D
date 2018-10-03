using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

	// Use this for initialization
	void Start () {
        StartCoroutine(DestroyAfterTime(4));
    }

    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.CompareTag("Enemy")) {
            Destroy(collision.gameObject);
        }
        if (!collision.gameObject.CompareTag("Head")) {
            Destroy(this.gameObject);
        }
    }

    private IEnumerator DestroyAfterTime(float waitTime) {
        yield return new WaitForSeconds(waitTime);
        Destroy(this.gameObject);
    }
}
