using UnityEngine;

public class SpawnCarOnStart : MonoBehaviour
{
    [SerializeField]
    private GameObject _startPosition;
    public GameObject car;
    public bool shouldSpawnCar = true;
    void Start()
    {
        if (shouldSpawnCar)
        {
            SpawnCar();
        }
    }

    private void SpawnCar()
    {
        Instantiate(car, _startPosition.transform.position, transform.rotation);
    }
}
