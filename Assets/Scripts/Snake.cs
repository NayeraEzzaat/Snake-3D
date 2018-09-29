using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour {

    public List<GameObject> snakeBody;
    public GameObject gameManager;
    public GameObject tailTrigger;
    public GameObject tail;
    [HideInInspector]
    public float speed;
    [HideInInspector]
    public bool isMoving;
    [HideInInspector]
    public bool isDead;
    [HideInInspector]
    public bool isFalling;

    void Start () {
        isMoving = false;
        isDead = false;
    }

    void Update() {
        speed = gameManager.GetComponent<Variables>().snakeSpeed;
	}

    public void Gain2(Transform _spawnTransform) {
        List<GameObject> snakeBody = GetComponentInParent<Snake>().snakeBody;
        GameObject _tail = Instantiate(tail);
        _tail.transform.SetParent(this.transform);
        _tail.transform.position = _spawnTransform.position;
        snakeBody.Add(_tail);
        _tail.GetComponent<Follow>().target = snakeBody[snakeBody.Count - 2].transform;
        snakeBody[snakeBody.Count - 2].tag = "Body";
    }
}
