using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerScript : MonoBehaviour
{
    [Header("GameObject References")]
    [SerializeField] public Camera camObj;

    [Header("Physics Object References")]
    //[SerializeField] public Rigidbody rb;

    [Header("Physics Variables")]
    [SerializeField] public int speed = 10;
    private int currSpeed;
    [SerializeField] public int sensitivity = 5;


    // Start is called before the first frame update
    void Start()
    {
        currSpeed = speed;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        var gamepad = Gamepad.current;
        if (gamepad == null)
        {
            Debug.Log("No Controller Connected");
            return;
        }

        if (gamepad.leftStickButton.isPressed)
        {
            currSpeed = speed * 2;
        }
        else
        {
            currSpeed = speed;
        }

        if (gamepad.xButton.isPressed)
        {
            SaveSystem.SavePersistentData(PersistentData.Instance);
        }

        if (gamepad.yButton.isPressed)
        {
            SerializablePersistentData sData = SaveSystem.LoadPersistentData();
            if (sData != null)
            {
                PersistentData.Instance.spawnMap = sData.spawnMap;
                PersistentData.Instance.spawnLoc = new Vector3(
                    sData.spawnLoc[0],
                    sData.spawnLoc[1],
                    sData.spawnLoc[2]
                );
                PersistentData.Instance.spawnRot = new Quaternion(
                    sData.spawnRot[0],
                    sData.spawnRot[1],
                    sData.spawnRot[2],
                    sData.spawnRot[3]
                );
                PersistentData.Instance.useWaypointSpawn = false;

                //Somehow reload scene
                SceneManager.LoadScene("_initscene", LoadSceneMode.Single);
                SceneManager.LoadScene(PersistentData.Instance.spawnMap, LoadSceneMode.Single);
            }
        }

        Vector2 camera = gamepad.rightStick.ReadValue();
        transform.Rotate(transform.up, camera.x * Time.deltaTime * sensitivity);
        camObj.transform.Rotate(camera.y * Time.deltaTime * sensitivity, 0.0f, 0.0f);

        Vector2 move = gamepad.leftStick.ReadValue();
        Vector3 realMovement = new Vector3(move.x, 0, move.y);
        realMovement.Normalize();
        transform.position += transform.right * realMovement.x * currSpeed * Time.deltaTime;
        transform.position += transform.forward * realMovement.z * currSpeed * Time.deltaTime;
        //rb.AddRelativeForce(realMovement * speed);
    }
}
