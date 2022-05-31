    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidsGenerator : MonoBehaviour
{
    public Player player;
    public GameObject[] asteroids;
    public GameObject asteroidsList;
    public float maxX=200f;
    public float maxY=5f;
    public GameManager gm;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!gm.gameOver)
        {
            RandomGenerate();

        }

    }

    public void RandomGenerate()
    {
        GameObject obj = Instantiate(asteroids[Random.Range(0, 4)], RandomPos(), Quaternion.identity);
        obj.transform.parent = asteroidsList.transform;
    }

    public Vector3 RandomPos()
    {
        Vector3 pos = new Vector3(asteroidsList.transform.position.x + Random.Range(-maxX, maxX), 0, 80 );
        return pos;
    }


}
