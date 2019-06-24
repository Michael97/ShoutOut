/// Michael Thomas
/// 16.02.19

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawnerScript : MonoBehaviour
{
    //array for spawn locations
    public Transform[] spawnPoints;

    //arrays for the gameobject prefabs used in the game
    public GameObject[] easyObstacles;
    public GameObject[] mediumObstacles;
    public GameObject[] hardObstacles;

    //arrays for the obstacles in each sigma
    private GameObject[] OneSigma;
    private GameObject[] TwoSigma;
    private GameObject[] ThreeSigma;

    //ObjectPool script
    public ObjectPoolScript ObjectPoolScript_;

    //The next obstacle to be spawned
    private GameObject ChosenObstacle;

    [SerializeField] private int m_maxObjects = 30;
    private int currentObjCount = 0;
    //Run when the game level starts
    void Awake()
    {
        //Call SpawnNewObject after 2 secs, every 5 secs
        InvokeRepeating("SpawnNewObject", 15.0f, 5.0f);

        //Call MediumLevel after 120 secs
        Invoke("MediumLevel", 20f);

        //Call HardLevel after 240 secs
        Invoke("HardLevel", 50f);

        EasyLevel();
    }

    //Cancels all invokes on this script
    public void w()
    {
        CancelInvoke("SpawnNewObject");
        CancelInvoke("MediumLevel");
        CancelInvoke("HardLevel");
    }

    //Choose and spawn the next object
    private void SpawnNewObject()
    {
        if (currentObjCount < m_maxObjects)
        {
            ThreeSigmaRule();
            Instantiate(ChosenObstacle, spawnPoints[Random.Range((int)0, (int)26)].position,transform.rotation);
            currentObjCount++;
        }
    }

    //Spawns the correct obstacle using the three sigma rule
    private void ThreeSigmaRule()
    {
        float num = MarsagliaNormalDistribution();

        bool chosen = false;

        do
        {
            if (num <= 1 && num >= -1)
            {
                ChosenObstacle = OneSigma[Random.Range(0, OneSigma.Length)];
                //Debug.Log("easy");
                chosen = true;
            }
            else if (num <= 2 && num >= -2)
            {
                ChosenObstacle = TwoSigma[Random.Range(0, TwoSigma.Length)];
                //Debug.Log("medium");
                chosen = true;
            }
            else if (num <= 3 && num >= -3)
            {
                ChosenObstacle = ThreeSigma[Random.Range(0, ThreeSigma.Length)];
                //Debug.Log("hard");
                chosen = true;
            }
            else
                num = MarsagliaNormalDistribution();
        }
        while (!chosen);

    }


    static float r0;
    static bool generate = true;
    //Generates a random number with marsaglia normal distribution
    public static float MarsagliaNormalDistribution(float mean = 0f, float standardDev = 1f)
    {
        float u, v, s;

        generate = !generate;

        if (generate)
            return r0 * standardDev + mean;

        do
        {
            u = 2.0f * Random.value - 1.0f;
            v = 2.0f * Random.value - 1.0f;
            s = u * u + v * v;
        }
        while (s >= 1.0f || s == 0.0f);

        float fac = Mathf.Sqrt(-2.0f * Mathf.Log(s) / s);
        r0 = v * fac;

        return u * fac * standardDev + mean;
    }

    //Functions to change the difficulty procedurally.
    private void EasyLevel()
    {
        OneSigma = easyObstacles;
        TwoSigma = mediumObstacles;
        ThreeSigma = hardObstacles;
    }

    private void MediumLevel()
    {
        OneSigma = mediumObstacles;
        TwoSigma = easyObstacles;
        ThreeSigma = hardObstacles;
    }

    private void HardLevel()
    {
        OneSigma = hardObstacles;
        TwoSigma = mediumObstacles;
        ThreeSigma = easyObstacles;
    }

}
