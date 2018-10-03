using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawning : MonoBehaviour {

    public List<GameObject> floors;
    public GameObject snakeSkeleton;
    public GameObject foodSkeleton;
    public GameObject snake;
    public GameObject greenFood;
    public GameObject redFood;
    public GameObject orangeFood;
    public GameObject yellowFood;
    public GameObject purpleFood;
    public GameObject blueFood;
    public GameObject blackFood;

    private GameObject _floorSelected;
    private int _rotationSelected;
    private Vector3 _snakeSpawnPosition;
    private Vector3 _pickupSpawnPosition;
    private bool _methodCall;

    void Start () {
        //int rotationIndex = Random.Range(1, 5);
        //if (rotationIndex == 1) {
        //    _rotationSelected = 0;
        //} else if (rotationIndex == 2) {
        //    _rotationSelected = 90;
        //} else if (rotationIndex == 3) {
        //    _rotationSelected = 180; 
        //} else if (rotationIndex == 4) {
        //    _rotationSelected = 270;
        //}
        _methodCall = false;
    }
    private void Update() {
        if (snake.GetComponent<Snake>().isMoving && !_methodCall) {
            StartCoroutine(SpawnPickup(5));
            _methodCall = true;
        }
    }


    private IEnumerator SpawnPickup(int time) {
        yield return new WaitForSeconds(time);

        int floorIndex = Random.Range(0, floors.Count);
        _floorSelected = floors[floorIndex];
        float minX = _floorSelected.transform.position.x - (transform.localScale.x * 15);
        float maxX = _floorSelected.transform.position.x + (transform.localScale.x * 15);
        float minZ = _floorSelected.transform.position.z - (transform.localScale.z * 15);
        float maxZ = _floorSelected.transform.position.z + (transform.localScale.z * 15);

        float xValue = Random.Range(minX, maxX);
        float zValue = Random.Range(minZ, maxZ);

        int randomFood = Random.Range(0, 150);

        _pickupSpawnPosition = new Vector3(xValue, 0.25f , zValue);

        if (randomFood < 50) {
            Instantiate(greenFood, _pickupSpawnPosition, Quaternion.identity);
        } else if (randomFood >= 50 && randomFood < 90) {
            Instantiate(redFood, _pickupSpawnPosition, Quaternion.identity);
        } else if (randomFood >= 90 && randomFood < 120) {
            Instantiate(orangeFood, _pickupSpawnPosition, Quaternion.identity);
        } else if (randomFood >=120 && randomFood < 140) {
            randomFood = Random.Range(0, 2);
            if (randomFood == 0) {
                Instantiate(blueFood, _pickupSpawnPosition, Quaternion.identity);
            } else {
                Instantiate(purpleFood, _pickupSpawnPosition, Quaternion.identity);
            }
        } else if (randomFood >= 140 && randomFood < 150) {
            randomFood = Random.Range(0, 2);
            if (randomFood == 0) {
                Instantiate(blackFood, _pickupSpawnPosition, Quaternion.identity);
            } else {
                Instantiate(yellowFood, _pickupSpawnPosition, Quaternion.identity);
            }
        }

        StartCoroutine(SpawnPickup(time));


    }
}
