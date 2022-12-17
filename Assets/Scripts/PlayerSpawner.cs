using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerSpawner : MonoBehaviour
{

    public static PlayerSpawner Instance;

    public GameObject PlayerPrefab;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
        SceneManager.activeSceneChanged += OnSceneChanged;
    }

    void OnSceneChanged(Scene current, Scene next)
    {
        SpawnPlayer();
    }

    private void SpawnPlayer()
    {
        SceneManager.LoadScene(PersistentData.Instance.spawnMap, LoadSceneMode.Single);

        if (PersistentData.Instance.useWaypointSpawn)
        {
            WaypointScript[] Waypoints = FindObjectsOfType<WaypointScript>();
            foreach (WaypointScript wp in Waypoints)
            {
                if (wp.waypointID == PersistentData.Instance.destinationWaypointID)
                {
                    PersistentData.Instance.spawnLoc = wp.transform.position;
                    PersistentData.Instance.spawnRot = wp.transform.rotation;
                    break;
                }
            }
        }

        Instantiate(
            PlayerPrefab,
            PersistentData.Instance.spawnLoc,
            PersistentData.Instance.spawnRot
        );
    }
}
