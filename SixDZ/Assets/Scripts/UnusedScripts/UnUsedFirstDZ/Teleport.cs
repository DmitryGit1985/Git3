using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    public int periodInSeconds;
    public GameObject gameobject;
    // Start is called before the first frame update
    void Start()
    {
        //таймер запускеается сразу.
        int num = 0;
        // устанавливаем метод обратного вызова
        TimerCallback tm = new TimerCallback(CreateDelete);
        // создаем таймер
        Timer timer = new Timer(tm, num, 0, periodInSeconds*1000);
    }
    public void CreateDelete(object obj)
    {
        Debug.Log("Test");
        gameobject = (GameObject)obj;
        gameobject.SetActive(false);
        float x = Random.Range(-25, 26);
        float y = Random.Range(-25, 26); 
        float z = Random.Range(-25, 26);
        Vector3 newPosition = new Vector3(x, y, z);
        gameobject.transform.position = newPosition;
        gameobject.SetActive(true);
    }
        void FixedUpdate()
        {
        }
    
}
