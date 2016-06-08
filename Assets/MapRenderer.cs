using UnityEngine;
using System.Collections;

public class MapRenderer : MonoBehaviour {


    private int[,] map;
    public MapGenerator mpg;
    public Sprite sheet;
    public GameObject square;
    public bool mapIsDrawn;

    // Use this for initialization
    void Start () {
        if (mpg.map != null)
        {
            DrawMap();
        }
        else
        {
            print("no map available");
        }
	}
	
	// Update is called once per frame
	void Update () {
        if (mpg.map != null&& mapIsDrawn==false) 
        {
            mapIsDrawn = true;
            print("map found");
            DrawMap();

        }
	}

    private void DrawMap()
    {
        for(int i =0; i< mpg.width; i++)
        {
            for (int j = 0;j  < mpg.width; j++)
            {
                if (mpg.map[i, j] > 0)
                {
                    Vector3 pos = new Vector3(-mpg.width / 2 + i + .5f,mpg.height/2-j-.5f, 0);
                    GameObject tile = Instantiate(square, pos, Quaternion.identity) as GameObject;
                    var renderer = (SpriteRenderer)tile.GetComponent("SpriteRenderer");

                    renderer.sprite = Resources.Load<Sprite>("slice27_27");
                    tile.transform.SetParent(this.transform, false);
                }
                
            }
        }
    }
}
