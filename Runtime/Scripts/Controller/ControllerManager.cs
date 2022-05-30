using UnityEngine;

public class ControllerManager : MonoBehaviour
{
    [SerializeField] private GameObject playerControllerPrefab;

    private GameObject playerController;

    private void Start()
    {
        SpawnController();
    }

    public void SpawnController()
    {
        if (playerController)
            Destroy(playerController);

        playerController = Instantiate(playerControllerPrefab);
        playerController.transform.SetParent(transform);
    }

    public void DestroyController()
    {
        foreach (Transform child in transform)
            Destroy(child.gameObject);
    }
}
