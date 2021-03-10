using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Main : MonoBehaviour
{

    static public Main S; //A singleton for Main

    [Header("Set in Inspector")]
    public GameObject[] prefabEnemies; //Array of Enemy Prefabs
    public float enemySpawnPerSecond = 0.5f; //# of Enemies per second
    public float enemyDefaultPadding = 1.5f; //Padding for position

    private BoundsCheck _bndCheck;

    void Awake()
    {
        S = this;
        //Set bndChec to reference the BoundsChec component on this GameObject
        _bndCheck = GetComponent<BoundsCheck>();
        //Invoke SpawnEnemy() once (in 2 seconds, based on default value)
        Invoke("SpawnEnemy", 1f / enemySpawnPerSecond); //called every x seconds
    }

    public void SpawnEnemy() {
        //Pick a random Enemy prefab to instantiate
        int ndx = Random.Range(0, prefabEnemies.Length); //select a random index in array
        GameObject go = Instantiate<GameObject>(prefabEnemies[ndx]);
        //instantiate the GameObject

        //Position the enemy above the screen with a random x position
        float enemyPadding = enemyDefaultPadding;
        if (go.GetComponent<BoundsCheck>() != null)
        {
            enemyPadding = Mathf.Abs(go.GetComponent<BoundsCheck>().radius);
            //enemyPadding is set to BoundCheck padding
        }

        //Set the initial position for the spawned Enemy
        Vector3 pos = Vector3.zero;
        float xMin = -_bndCheck.camWidth + enemyPadding;
        float xMax = _bndCheck.camWidth - enemyPadding;
        //Random X position within screen width - padding on each side
        pos.x = Random.Range(xMin, xMax);
        //The height is the height of the cam + padding
        pos.y = _bndCheck.camHeight + enemyPadding;
        //Setting the postiion
        go.transform.position = pos;

        //INvoke SpawnEnemy() again
        //can theoretically shange spawn seconds with each iteration
        Invoke("SpawnEnemy", 1f / enemySpawnPerSecond);
    }

    public void DelayedRestart(float delay)
    {
        //Invoke the Restarted() method in delay seconds
        Invoke("Restart", delay);
    }

    public void Restart()
    {
        //Reload _Scene_0 to restart the game
        SceneManager.LoadScene("_Scene_0");
    }

}
