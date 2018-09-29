using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Head : MonoBehaviour {

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Solid Wall")) {
            GetComponentInParent<Move>().Die();
        }
        else if (other.gameObject.CompareTag("Breakable Wall")) {
            GetComponentInParent<Move>().Hit(3);
            Destroy(other.gameObject);
        } else if (other.gameObject.CompareTag("Enemy")) {
            GetComponentInParent<Move>().Hit(3);
            Destroy(other.gameObject);
        } else if (other.gameObject.CompareTag("Body") || other.gameObject.CompareTag("Tail")) {
            GetComponentInParent<Move>().Hit(1);
        } else if (other.gameObject.CompareTag("Green Food")) {
            GetComponentInParent<Move>().Gain1();
            Destroy(other.gameObject);
        } else if (other.gameObject.CompareTag("Red Food")) {
            GetComponentInParent<Move>().Hit(2);
            Destroy(other.gameObject);
        } else if (other.gameObject.CompareTag("Orange Food")) {
            GetComponentInParent<Move>().FlipHead();
            Destroy(other.gameObject);
        }
    }
}
