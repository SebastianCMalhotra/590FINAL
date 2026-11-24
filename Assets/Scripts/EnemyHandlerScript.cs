using UnityEngine;
using Unity.Collections;
using NUnit.Framework;

public class EnemyHandlerScript : MonoBehaviour
{

    public GameObject PlayerHead; // get player rotation using PlayerHead.transform.rotation.eulerAngles to get normal (x,y,z) angles
    [UnityEngine.Range(0, 10)]
    public int MaxLights = 10;
    [UnityEngine.Range(25, 60)]
    public int TotalDegreesPerLight = 30;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnLight()
    {
        Random.Range(0, 360);
    }

}
