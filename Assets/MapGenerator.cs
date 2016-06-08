using UnityEngine;
using System.Collections;

public class MapGenerator : MonoBehaviour {


    public int [,] map;
    public int width, height;
    public string seed;

    [Range(0, 100)]
    public int randomFillPercent;
    public bool randomSeed;

    public GameObject square;

    [Range(0, 5)]
    public int smoothIterations;
    private Sprite[] sprites; 
    enum list {TOPLEFT=1, MIDDLELEFT = 2, BOTTOMLEFT=4, TOPCENTER = 8, BOTTOMCENTER =32,TOPRIGHT =64,MIDDLERIGHT=128,BOTTOMRIGHT=256}

    [Header ("Debug")]
    public bool showGizmos = false;

    // Use this for initialization
    void Start () {
        map = new int[width, height];
        sprites = Resources.LoadAll<Sprite>("sheet");
        GenerateMap();
        
        
    }
	
	// Update is called once per frame
	void Update () {

        /*if (Input.GetMouseButtonDown(1))
        {
            print("Generate new map");
            GenerateMap();
        }*/
        ManageSquareActivity();
	}
    
    public void GenerateMap()
    {

        RandomFillMap();
        SmoothMap(smoothIterations);
        ClearSingleTiles();
        DrawMap();


    }
    private void RandomFillMap()
    {
        if (randomSeed)
        {
            seed = Time.time.ToString();
            print("Generate new seed");
        }

        System.Random rnd = new System.Random(seed.GetHashCode());

        for (var i = 0; i < width; i++)
        {
            for (var j = 0; j < height; j++)
            {
                if (i == 0 || i == (width - 1) || j == 0 || j == (height - 1))
                {
                    map[i, j] = 1;
                }
                else
                {
                    map[i, j] = (rnd.Next(0,100) < randomFillPercent) ? 1 : 0;                    
                }

            }
        }
    }
    private void SmoothMap(int iterations)
    {
        for(int k = 0; k < iterations; k++)
        {
            for (var i = 0; i < width; i++)
            {
                for (var j = 0; j < height; j++)
                {
                    int neightboursFill = GetNeighboursFilledTiles(i, j);
                    if (neightboursFill > 4)
                    {
                        map[i, j] = 1;
                    }
                    else if (neightboursFill < 4)
                    {
                        map[i, j] = 0;
                    }
                    
                    
                }
            }
        }
    }
    private void ClearSingleTiles()
    {
        for (var i = 0; i < width; i++)
        {
            for (var j = 0; j < height; j++)
            {
                if (map[i, j] == 0 && GetNeighboursFilledTiles(i, j) == 8) map[i, j] = 1;
                else if (map[i, j] == 1 && GetNeighboursFilledTiles(i, j) == 0) map[i, j] = 0;
            }
        }
    }
    
    private int GetNeighboursFilledTiles(int x,int y)
    {
        int nb = 0;
        for (int i = x - 1; i <= x + 1; i++)
        {
            for (int j = y - 1; j <= y + 1; j++)
            {
                if (i >= 0 && i < width && j >= 0 && j < height)
                {
                    if (i != x || j != y)
                    {
                        nb += map[i, j];
                    }
                }
                else
                {
                    nb++;
                }
            }
        }
        return nb;

    }
    void OnDrawGizmos()
    {
        if (map != null && showGizmos)
        {
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    Gizmos.color = (map[x, y] == 1) ? Color.black : Color.white;
                    Vector3 pos = new Vector3(-width / 2 + x + .5f, height / 2 - y - .5f,5 );
                    Gizmos.DrawCube(pos, Vector3.one);
                }
            }
        }
    }
    private void DrawMap()
    {
        if (map != null)
        {

            CleanMap();
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    if (map[i, j] > 0)
                    {
                        Vector3 pos = new Vector3(-width / 2 + i /*- .5f*/, height / 2 - j /*+ .5f*/, 0);
                        GameObject tile = Instantiate(square, pos, Quaternion.identity) as GameObject;
                        SpriteRenderer renderer = tile.GetComponent<SpriteRenderer>();
                        int spriteIndex = GetSpriteIndex(i, j);

                        if (spriteIndex == 1)
                        {
                            renderer.sprite = sprites[20];
                            
                        }
                        if (spriteIndex == 11)
                        {
                            renderer.sprite = sprites[18];
                            
                        }
                        int test = spriteIndex & 2;
                        if (test>0)
                        {
                            renderer.sprite = sprites[22];
                            //break;
                        }

                        test = spriteIndex & 8;
                        if (test>0)
                        {
                            renderer.sprite = sprites[6];
                        }
                        else {
                            test = spriteIndex & 32;
                            if (test > 0)
                            {
                                renderer.sprite = sprites[35];
                            }
                            else
                            {
                                test = spriteIndex & 128;
                                if (test > 0)
                                {
                                    renderer.sprite = sprites[23];
                                }
                                else
                                {
                                    renderer.sprite = sprites[32];
                                }
                                
                            }
                        }


                        tile.transform.SetParent(this.transform, false);
                    }

                }
            }
        }
           
    }
    private void CleanMap()
    {
        foreach (Transform obj in transform)
        {
            GameObject.Destroy(obj.gameObject);
        }
    }
    private int GetSpriteIndex(int x,int y)
    {
        int index = 0;
        int multiplicateurBinaire=1;
        for (int i = x - 1; i <= x + 1; i++)
        {
            for (int j = y - 1; j <= y + 1; j++)
            {
                if (i >= 0 && i < width && j >= 0 && j < height)
                {
                    if (i != x || j != y)
                    {
                        if (map[i, j] == 0)
                        {
                            index = index | multiplicateurBinaire;
                        }

                    }
                }
                multiplicateurBinaire *= 2;

            }
            
        }

        return index;
    }

    void ManageSquareActivity()
    {
        
        Vector3 topLeftCameraLimit = Camera.main.ViewportToWorldPoint(new Vector3(0, 1, -Camera.main.transform.position.z));

        Vector3 bottomRightCameraLimit = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, -Camera.main.transform.position.z));

        float cameraHeight = topLeftCameraLimit.y - bottomRightCameraLimit.y;
        float cameraWidth = bottomRightCameraLimit.x - topLeftCameraLimit.x;

        float deltaHeight = cameraHeight * .5f+1;
        float deltaWidth = cameraWidth * .5f+1;

        foreach (Transform square in transform)
        {
            if(square.position.y> Camera.main.transform.position.y - deltaHeight &&square.position.y < Camera.main.transform.position.y+deltaHeight
                    && square.position.x >Camera.main.transform.position.x - deltaWidth && square.position.x < Camera.main.transform.position.x + deltaWidth )
            {
                square.gameObject.SetActive(true);
            }else
            {
                square.gameObject.SetActive(false);
            }
        }

        /*            print(topLeftCameraLimit);
        print(bottomRightCameraLimit);*/
        
    }
    void DrawSelectedMapd()
    {

    }


}
