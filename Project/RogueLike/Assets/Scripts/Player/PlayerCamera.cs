using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public BoxCollider2D levelBoundsCollider;

    public Player Player;

    public Vector2 ViewSize
    {
        get
        {
            float viewHeight = Camera.main.orthographicSize/* * 2.0f*/;
            float viewWidth = viewHeight * Camera.main.aspect;
            return new Vector2(viewWidth, viewHeight);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Player.getState != EPlayerState.Dead)
        {
            SetCameraLocation(Player.transform.position);
        }
    }

    public void SetCameraLocation(Vector2 location)
    {
        Vector2 viewSize = ViewSize;
        Bounds levelBounds = levelBoundsCollider.bounds;

        float maxCameraX = levelBounds.max.x - viewSize.x;
        float minCameraX = levelBounds.min.x + viewSize.x;
        float maxCameraY = levelBounds.max.y - viewSize.y;
        float minCameraY = levelBounds.min.y + viewSize.y;

        Vector3 cameraPosition = Vector3.zero;
        cameraPosition.x = Mathf.Clamp(location.x, minCameraX, maxCameraX);
        cameraPosition.y = Mathf.Clamp(location.y, minCameraY, maxCameraY);
        cameraPosition.z = transform.position.z;

        transform.position = cameraPosition;
    }
}
