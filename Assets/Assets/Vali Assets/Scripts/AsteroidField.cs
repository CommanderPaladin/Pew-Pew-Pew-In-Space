using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidField : MonoBehaviour
{
    public Transform asteroidPrefab;
    public int fieldRadius = 100;
    public int asteroidCount = 500;
    public int innerRadius = 10;
    // Start is called before the first frame update
    void Start()
    {
        for (int count = 0; count < asteroidCount; count++)
        {
            Transform temp = Instantiate(asteroidPrefab, Random.insideUnitSphere * fieldRadius, Random.rotation);
            temp.localScale = temp.localScale * Random.Range(0.5f, 10);
         //   temp.Translate(new Vector3(temp.position.x*1.5f,0, temp.position.z*1.5f), Space.Self);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
