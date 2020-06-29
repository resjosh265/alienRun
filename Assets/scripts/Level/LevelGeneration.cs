using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGeneration : MonoBehaviour {

	public GameObject tile;
	public GameObject groundMiddle;
	public GameObject bridge;
	public GameObject spikes;
	public GameObject finish;

	public int minTileSize = 1;
	public int maxTileSize = 10;
	[Range (0.0f, 1f)]
	public float emptyChance = .5f;
	[Range (0.0f, 1f)]
	public float bridgeChance = .1f;
	public int maxEmptySize = 3;
	public int maxHeightSize = 3;
	public int maxDropSize = -3;

	public int platforms = 100;
	//ADD
	public float hostilePlatformChance = 50f;
	[Range (0.0f, 1f)]
	public float enemyChance = 0.3f;
	[Range (0.0f, 1f)]
	public float fishChance = 0.3f;

	private float height;
	private float tileSize;
	private bool isHazzard;
	private int blockNum = 1;

	private int lastSpace;

	private bool isEnemyPlatform;

	// Use this for initialization
	void Start ()
    {
        GenerateStartingTiles();

        //map generation
        //Generate each platform
        for (int i = 1; i < platforms; i++)
        {
            GetIfNextIsHazard(i);

            //generate the platform tile size
            tileSize = Mathf.Round(Random.Range(minTileSize, maxTileSize));

            for (int b = 0; b < tileSize; b++)
            {
                if (!isHazzard)
                {
                    //if the space isnt empty what is the new height of the tiles
                    height = height + (Random.Range(maxDropSize, maxHeightSize));

                    if (i == platforms - 1)
                    {
                        Instantiate(finish, new Vector2(blockNum, height + 1.2f), Quaternion.identity);
                    }

                    float spaceIs = Random.value;
                    /*WHAT IS THE EMPTY SPACE
					* 1 - Bridge
					* 2 - block
					* 3 - 
					*/
                    if (spaceIs < bridgeChance)
                    {
                        GenerateBridge();
                    }
                    else
                    {
                        GenerateLand();
                    }
                }
                else
                {
                    //isHazzard
                    GenerateHazard();
                }
            }

            isEnemyPlatform = false;
        }
    }

    private void GetIfNextIsHazard(int i)
    {
        //If last space was empty then next space MUST be occupied Otherwise find out if next space is empty
        if (!isHazzard)
        {
            if (Random.value > emptyChance)
            {
                isHazzard = false;
            }
            else
            {
                if (i == platforms - 1)
                {
                    isHazzard = false;
                }
                else
                {
                    isHazzard = true;
                }
            }
        }
        else
        {
            isHazzard = false;
        }
    }

    private void GenerateHazard()
    {
        tileSize = Mathf.Round(Random.Range(1, maxEmptySize));
        for (int a = 0; a < tileSize; a++)
        {
            Instantiate(spikes, new Vector2(blockNum, height - 2), Quaternion.identity);
            //generate tiles below top tile
            for (int c = 1; c < 5; c++)
            {
                Instantiate(groundMiddle, new Vector2(blockNum, (height - 2) - c), Quaternion.identity);
            }
            blockNum++;
        }
    }

    private void GenerateLand()
    {
        tileSize = Mathf.Round(Random.Range(minTileSize, maxTileSize));

        //GENERATE ENEMYS
        if (tileSize >= 3)
        {
            GenerateEnemy();
        }

        GenerateCoin(0.8f);

        //generate top tile
        for (int a = 0; a < tileSize; a++)
        {
            Instantiate(tile, new Vector2(blockNum, height), Quaternion.identity);
            //generate tiles below top tile
            for (int c = 1; c < 10; c++)
            {
                Instantiate(groundMiddle, new Vector2(blockNum, height - c), Quaternion.identity);
            }

            //GENERATE OBJECTS
            if (a > 0 && a < tileSize)
            {
                GenerateObject(0.2f);
            }

            blockNum++;
        }
    }

    private void GenerateObject(float chance)
    {
        if (Random.value < chance)
        {
            GetComponent<ObjectGeneration>().PlaceObject(new Vector2(blockNum, height), isEnemyPlatform);
        }
    }

    private void GenerateEnemy()
    {
        if (Random.value < enemyChance)
        {
            GetComponent<EnemyGeneration>().PlaceEnemy(new Vector2(blockNum + 2, height + 0.15f));
            isEnemyPlatform = true;
        }
    }

    private void GenerateBridge()
    {
        GenerateCoin(0.5f);

        //make a bridge
        tileSize = Mathf.Round(Random.Range(minTileSize, maxTileSize));

        GenerateFish();

        for (int brid = 0; brid < tileSize; brid++)
        {
            if (brid == 0 || brid == (tileSize - 1))
            {
                Instantiate(tile, new Vector2(blockNum, height), Quaternion.identity);
                for (int c = 1; c < 10; c++)
                {
                    Instantiate(groundMiddle, new Vector2(blockNum, height - c), Quaternion.identity);
                }
            }
            else
            {
                Instantiate(bridge, new Vector2(blockNum, height), Quaternion.identity);
            }
            blockNum++;
        }
    }

    private void GenerateFish()
    {
        if (Random.value <= fishChance)
        {
            GetComponent<EnemyGeneration>().PlaceFish(new Vector2(blockNum + (int)Random.Range(1, tileSize - 1), height - 5));
        }
    }

    private void GenerateCoin(float chance)
    {
        //GENERATE COINS
        if (Random.value < chance)
        {
            int startTile = (int)Random.Range(1, tileSize);
            GetComponent<ObjectGeneration>().PlaceCoin(new Vector2(blockNum, height), (int)Random.Range(1, tileSize - startTile), startTile);
        }
    }

    private void GenerateStartingTiles()
    {
        //begining tile
        Instantiate(tile, new Vector2(0, 0), Quaternion.identity);
        for (int c = 1; c < 8; c++)
        {
            Instantiate(groundMiddle, new Vector2(0, 0 - c), Quaternion.identity);
        }

        for (int c = 1; c < 4; c++)
        {
            Instantiate(tile, new Vector2(-c, 0), Quaternion.identity);
            for (int d = 1; d < 8; d++)
            {
                Instantiate(groundMiddle, new Vector2(-c, 0 - d), Quaternion.identity);
            }
        }

        for (int a = 4; a < 14; a++)
        {
            Instantiate(tile, new Vector2(-a, 8), Quaternion.identity);
            for (int c = 1; c < 16; c++)
            {
                Instantiate(groundMiddle, new Vector2(-a, 8 - c), Quaternion.identity);
            }
        }
    }

    // Update is called once per frame
    void Update () {


		
	}
}
	
