using UnityEngine;

using CharacterMovement;

public class Controller : MonoBehaviour
{
    [SerializeField] private GameObject avatarPrefab;
    [SerializeField] private bool spawnAvatarOnStart;

    private void Start()
    {
        if(spawnAvatarOnStart)
        {
            SpawnAvatar();
        }
    }

    public void SpawnAvatar()
    {
        var avatar = Instantiate(avatarPrefab);
        avatar.transform.SetParent(transform);

        var movementController = GetComponent<MovementController>();
        movementController.Movement = avatar.GetComponent<Movement>();
    }
}
