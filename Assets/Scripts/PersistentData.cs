using UnityEngine;
using UnityEngine.SceneManagement;


public class PersistentData : MonoBehaviour
{
    public static PersistentData Instance;

    [Header("Waypoints")]
    public bool useWaypointSpawn = false;
    public int destinationWaypointID;

    [Header("Player Spawn Information")]
    public string spawnMap = "";
    public Vector3 spawnLoc = Vector3.zero;
    public Quaternion spawnRot = Quaternion.identity;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public string GetCurrentSceneName()
    {
        return SceneManager.GetActiveScene().name;
    }
}
