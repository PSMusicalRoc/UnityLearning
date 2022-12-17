using UnityEngine;
using UnityEngine.SceneManagement;

public class WaypointScript : MonoBehaviour
{
    [Header("Waypoint Options")]
    [SerializeField] public int waypointID;
    private bool justTeleported;

    private int waypointCollisionCount = 0;

    [Header("Destination Options")]
    [SerializeField] public string destinationScene;
    [SerializeField] public int destinationID;

    // Start is called before the first frame update
    void Start()
    {
        justTeleported = true;
    }

    private void Update()
    {
        if (waypointCollisionCount== 0)
        {
            justTeleported = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        waypointCollisionCount++;
        if (!justTeleported)
            Teleport();
    }

    private void OnTriggerExit(Collider other)
    {
        waypointCollisionCount--;
    }

    void Teleport()
    {
        PersistentData.Instance.destinationWaypointID = destinationID;
        PersistentData.Instance.useWaypointSpawn = true;
        PersistentData.Instance.spawnMap = destinationScene;
        SceneManager.LoadScene(destinationScene, LoadSceneMode.Single);
    }
}
