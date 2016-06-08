using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    public Text fpsText;
    // Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        float fps = 1 / Time.deltaTime;
        fpsText.text = fps.ToString();
	}
}
