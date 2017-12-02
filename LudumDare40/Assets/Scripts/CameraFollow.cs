using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    [SerializeField]
    private Transform target;
    private Camera theCamera;

	// Use this for initialization
	private void Start () {
        theCamera = GetComponent<Camera>();
	}
	
	// Update is called once per frame
	private void Update () {
        transform.position = new Vector3(target.position.x, target.position.y, transform.position.z);

        //Don't allow the camera to go past the edge of the level.
        Vector3 topLeft = theCamera.ViewportToWorldPoint(new Vector3(0, 1, theCamera.nearClipPlane));
        Vector3 bottomRight = theCamera.ViewportToWorldPoint(new Vector3(1, 0, theCamera.nearClipPlane));
        GameManager.LevelBounds levelBounds = GameManager.instance.levelBounds;
        float newX = transform.position.x;
        float newY = transform.position.y;
        if (topLeft.x < levelBounds.left)
        {
            float distToLeft = transform.position.x - topLeft.x;
            newX = levelBounds.left + distToLeft;
        }
        if(topLeft.y > levelBounds.top)
        {
            float distToTop = transform.position.y - topLeft.y;
            newY = levelBounds.top + distToTop;
        }
        if (bottomRight.x > levelBounds.right)
        {
            float distToRight = bottomRight.x - transform.position.x;
            newX = levelBounds.right - distToRight;
        }
        if (bottomRight.y < levelBounds.bottom)
        {
            float distToBottom = bottomRight.y - transform.position.y;
            newY = levelBounds.bottom - distToBottom;
        }

        transform.position = new Vector3(newX, newY, transform.position.z);
    }
}
