using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TailTrigger : MonoBehaviour {

    private void OnTriggerExit(Collider other) {
        if (other.gameObject.CompareTag("Tail")) {
            GetComponentInParent<Snake>().Gain2(this.transform);
            Destroy(this.gameObject);
        }
    }

}
