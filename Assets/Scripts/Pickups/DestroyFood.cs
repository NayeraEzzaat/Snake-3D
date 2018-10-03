using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyFood : MonoBehaviour {

    void Start() {
        StartCoroutine(DestroyFoodAfter(20));
    }

    private IEnumerator DestroyFoodAfter(int time) {
        yield return new WaitForSeconds(time);
        Destroy(this.gameObject);
    }
}
