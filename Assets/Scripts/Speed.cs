using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Speed : MonoBehaviour {

    private float speed;
	
	void Start () {
        GetComponent<Slider>().value = GetComponentInParent<Variables>().snakeSpeed;
	}
	
	void Update () {
        GetComponentInParent<Variables>().snakeSpeed = GetComponent<Slider>().value;
    }
}
