using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MapGenerator : MonoBehaviour {


    public int [,] map;
    public int width, height;
    public string seed;

    [Range(0, 100)]
    public int randomFillPercent;
    public bool randomSeed;

    private List<MapCoordonates> coordonatesAlreadyChecked = new List<MapCoordonates>();
    private List<Cave> cavesList = new List<Cave>();
    public GameObject square;
    public GameObject water;
    public GameObject lava;

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
        FindCaves();
        DrawMap();
        //PopulateCaves();

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
                    Vector3 pos = new Vector3(-width / 2 + x + 1, height / 2 - y - 1,5 );
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
                        Vector3 pos = new Vector3(-width / 2 + i, height / 2 - j, 0);
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
    private void FindCaves()
    {
        if (map != null)
        {           
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    if (map[i, j] == 0 && CheckIfInList(i,j,coordonatesAlreadyChecked)==false)
                    {
                        List<MapCoordonates> currentCaveAreaList = new List<MapCoordonates>();
                        List<MapCoordonates> coordonatesToCheckList = new List<MapCoordonates>();
                        MapCoordonates coord = new MapCoordonates(i, j);
                        coord.Origin = new MapCoordonates(0, 0);
                        coordonatesToCheckList.Add(coord);
                        while (coordonatesToCheckList.Count > 0)
                        {
                            MapCoordonates testCoord = coordonatesToCheckList[0];
                            //Test au dessus
                            if (map[testCoord.X, testCoord.Y-1] == 0 && CheckIfInList(testCoord.X, testCoord.Y - 1,currentCaveAreaList) ==false && CheckIfInList(testCoord.X, testCoord.Y - 1, coordonatesToCheckList)==false)
                            {
                                MapCoordonates newCoord = new MapCoordonates(testCoord.X, testCoord.Y - 1);
                                newCoord.Origin = testCoord;
                                coordonatesToCheckList.Add(newCoord);
                            }
                            //Test à droite
                            if (map[testCoord.X+1, testCoord.Y] == 0 && CheckIfInList(testCoord.X + 1, testCoord.Y,currentCaveAreaList) == false && CheckIfInList(testCoord.X + 1, testCoord.Y, coordonatesToCheckList) == false)
                            {
                                MapCoordonates newCoord = new MapCoordonates(testCoord.X + 1, testCoord.Y);
                                newCoord.Origin = testCoord;
                                coordonatesToCheckList.Add(newCoord);
                            }
                            //Test en bas
                            if (map[testCoord.X, testCoord.Y+1] == 0 && CheckIfInList(testCoord.X, testCoord.Y + 1,currentCaveAreaList) == false && CheckIfInList(testCoord.X, testCoord.Y + 1, coordonatesToCheckList) == false)
                            {
                                MapCoordonates newCoord = new MapCoordonates(testCoord.X, testCoord.Y + 1);
                                newCoord.Origin = testCoord;
                                coordonatesToCheckList.Add(newCoord);
                            }
                            //Test à gauche
                            if (map[testCoord.X-1, testCoord.Y] == 0 && CheckIfInList(testCoord.X - 1, testCoord.Y,currentCaveAreaList) == false && CheckIfInList(testCoord.X - 1, testCoord.Y, coordonatesToCheckList) == false)
                            {
                                MapCoordonates newCoord = new MapCoordonates(testCoord.X - 1, testCoord.Y);
                                newCoord.Origin = testCoord;
                                coordonatesToCheckList.Add(newCoord);
                            }
                            coordonatesToCheckList.Remove(testCoord);
                            currentCaveAreaList.Add(testCoord);
                            coordonatesAlreadyChecked.Add(testCoord);
                            
                        }
                        print(currentCaveAreaList.Count);
                        currentCaveAreaList.Sort(CompareHeight);
                        Cave cave = new Cave(currentCaveAreaList);
                        cavesList.Add(cave); 
                        
                    }
                }
            }
        }
        
        print(cavesList.Count);

    }

    public class Cave
    {
        private List<MapCoordonates> caveCoordonatesList;
        public Cave(List<MapCoordonates> list)
        {
            caveCoordonatesList = list;
        }
    }
    public class MapCoordonates
    {
        public int X, Y;
        private MapCoordonates origin;

        public MapCoordonates(int x, int y)
        {
            X = x;
            Y = y;
        }
        public bool isEqualTo(int i,int j)
        {
            if(X == i && Y == j)
            {
                return true;
            }else
            {
                return false;
            }
        }
        public MapCoordonates Origin { get { return origin; } set { origin = value; } }
    }
    private bool CheckIfInList(int i, int j, List<MapCoordonates> list)
    {
        foreach(MapCoordonates coordToCheck in list)
        {
            if (coordToCheck.isEqualTo(i,j))
            {
                return true;
            }
        }
        return false;
    }

    private int CompareHeight(MapCoordonates a, MapCoordonates b)
    {
        return b.Y - a.Y;
    }

    private void PolulateCaves()
    {
        foreach(Cave cave in cavesList)
        {

        }
    }

}
