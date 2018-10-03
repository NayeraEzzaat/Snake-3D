using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour {

    public List<GameObject> snakeBody;
    public GameObject gameManager;
    public GameObject tailTrigger;
    public GameObject tail;
    public GameObject cameraBlock;
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
        Physics.IgnoreLayerCollision(9, 10, false);
        Physics.IgnoreLayerCollision(9, 8, false);
        Physics.IgnoreLayerCollision(9, 11, false);
        Physics.IgnoreLayerCollision(8, 11, false);
        cameraBlock.SetActive(false);
    }

    void Update() {
        speed = gameManager.GetComponent<Variables>().snakeSpeed;

        if (snakeBody.Count < 3) {
            isDead = true;
            gameManager.GetComponentInChildren<Settings>().isDead = true;
            gameManager.GetComponentInChildren<Score>().isDead = true;
        }
        if (isFalling) {
            cameraBlock.SetActive(false);
        }
    }

    public void Gain2(Transform _spawnTransform) {
        List<GameObject> snakeBody = GetComponentInParent<Snake>().snakeBody;
        GameObject _tail = Instantiate(tail);
        _tail.transform.SetParent(this.transform);
        _tail.transform.position = _spawnTransform.position;
        _tail.layer = 8;
        snakeBody.Add(_tail);
        _tail.GetComponent<Follow>().target = snakeBody[snakeBody.Count - 2].transform;
        snakeBody[snakeBody.Count - 2].tag = "Body";
    }
}
