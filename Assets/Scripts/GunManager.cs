using System.Collections.Generic;
using UnityEngine;

public class GunManager : MonoBehaviour
{
    [SerializeField] GameObject gunPrefab;

    Transform player;
    List<Vector2> gunPositions = new List<Vector2>();

    int spawnedGuns = 0;

    void Start()
    {
        player = GameObject.Find("Player").transform;

        gunPositions.Add(new Vector2(-0.7f, -0.15f));
        gunPositions.Add(new Vector2(0.7f, -0.15f));

        gunPositions.Add(new Vector2(-0.5f, 0.7f));
        gunPositions.Add(new Vector2(0.5f, 0.7f));
        
        gunPositions.Add(new Vector2(-1.2f, 0));
        gunPositions.Add(new Vector2(1.2f, 0));

        // gunPositions.Add(new Vector2(-1.5f, -0.3f));
        // gunPositions.Add(new Vector2( 1.5f, -0.3f));

        // gunPositions.Add(new Vector2(-1.2f,  1.2f));
        // gunPositions.Add(new Vector2( 1.2f,  1.2f));

        // gunPositions.Add(new Vector2(-2.0f,  0.0f));
        // gunPositions.Add(new Vector2( 2.0f,  0.0f));

        AddGun();
        AddGun();
    }

    void Update()
    {
        //just for testing
        if (Input.GetKeyDown(KeyCode.G))
            AddGun();  
    }

    void AddGun()
    {
        var pos = gunPositions[spawnedGuns];

        var newGun = Instantiate(gunPrefab, pos, Quaternion.identity);

        newGun.GetComponent<Gun>().SetOffset(pos);
        spawnedGuns++;
    }
}
