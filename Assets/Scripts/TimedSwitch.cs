using UnityEngine;
using UnityEngine.SceneManagement;

public class TimedSwitch : MonoBehaviour
{

    private float timer = 0;
    private float maxTime = 5;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= maxTime)
        {
            //Fade Out
            SceneManager.LoadScene("mainscene");
        }
    }
}
