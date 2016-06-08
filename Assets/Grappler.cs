using UnityEngine;
using System.Collections;

public class Grappler : MonoBehaviour {

    public float lengthOfRope;
    public float maxLengthOfRope;
    public Vector3 headOfRope;
    public bool isHooked;
    // Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (this.gameObject.activeSelf)
        {
            lengthOfRope = Vector3.Distance(headOfRope, transform.position);
            this.GetComponent<LineRenderer>().SetPosition(0, transform.position);
            this.GetComponent<LineRenderer>().material.mainTextureScale = new Vector2(lengthOfRope, 1);
            if (lengthOfRope >= maxLengthOfRope)
            {
                Release();
            }
        }
	}

    public void Shoot(Vector3 startPosition, Vector3 endPosition, LayerMask whatIsGround)
    {
        RaycastHit2D hit = Physics2D.Raycast(startPosition, Vector3.Normalize(endPosition - startPosition), maxLengthOfRope, whatIsGround);
        //Ray ropeDetector = new Ray(transform.position, Vector3.Normalize(endPosition - startPosition) * maxLengthOfRope);
        //Debug.DrawRay(transform.position, Vector3.Normalize(endPosition - startPosition) * maxLengthOfRope);
        if (hit.collider != null)
        {
            Debug.Log("hit");
            this.gameObject.SetActive(true);
            isHooked = true;
            headOfRope = hit.point;
            lengthOfRope = Vector3.Distance(transform.position, hit.point);

            this.GetComponent<LineRenderer>().SetPosition(0, transform.position);
            this.GetComponent<LineRenderer>().SetPosition(1, transform.position + Vector3.Normalize(endPosition-startPosition) * lengthOfRope);

        }
    }

    public void Release()
    {
        this.gameObject.SetActive(false);
        isHooked = false;
    }
}
