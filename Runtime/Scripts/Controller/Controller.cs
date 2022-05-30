using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using CharacterMovement;

public class Controller: MonoBehaviour {

    [SerializeField] 
    private GameObject avatarPrefab;

    private GameObject avatar;
    //private Team team;

    public string PlayerName { get => "Player"; }

    public GameObject SpawnAvatar(Vector3 spawnPoint)
    {
        // Spawn the avatar
        avatar = Instantiate(
            avatarPrefab,
            spawnPoint,
            Quaternion.identity, transform);


        GetComponent<MovementController>().Movement = avatar.GetComponent<Movement>();

        return avatar;
    }

    //public abstract void SetupAvatar(GameObject avatar, Color teamColor);
/*
    public void SetTeam(Team team)
    {
        this.team = team;
    }*/

    public GameObject Avatar()
    {
        return avatar;
    }

    public bool HasAvatar()
    {
        return avatar != null;
    }
}
