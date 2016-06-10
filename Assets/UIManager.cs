using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    public Text fpsText;
    public Transform livesContainer;

    void Awake()
    {
        Player player = FindObjectOfType<Player>();
        if (player!=null)
        {
            player.OnHealthModified += UpdateHealth;
        }
    }

    // Use this for initialization
	void Start () {
	    
	}

    private void UpdateHealth(int lives)
    {
        foreach(Transform obj in livesContainer)
        {
            
        }
        for (int i = 0; i < livesContainer.childCount; i++)
        {

            livesContainer.GetChild(i).gameObject.SetActive(i < lives ? true : false);
            
        }

        
    }
	
	// Update is called once per frame
	void Update () {
        float fps = 1 / Time.deltaTime;
        fpsText.text = fps.ToString();
	}
}
