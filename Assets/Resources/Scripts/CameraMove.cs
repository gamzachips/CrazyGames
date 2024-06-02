using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    [SerializeField]
    GameObject player;

    [SerializeField]
    GameObject background;

    float mapSizeX;
    float mapSizeY;

    private void Start()
    {
        mapSizeX = background.GetComponent<SpriteRenderer>().size.x;
        mapSizeY = background.GetComponent<SpriteRenderer>().size.y;
    }

    private void LateUpdate()
    {
        if (player == null)
            return;
        float cameraX = player.transform.position.x;
        float cameraY = player.transform.position.y;

        float cameraHeight = Camera.main.orthographicSize;
        float cameraWidth = Camera.main.orthographicSize * Screen.width / Screen.height;

        cameraX = Mathf.Clamp(cameraX, -mapSizeX/2 + cameraWidth, mapSizeX / 2 - cameraWidth);
        cameraY = Mathf.Clamp(cameraY, -mapSizeY / 2 + cameraHeight, mapSizeY / 2 - cameraHeight);
        transform.position = new Vector3(cameraX, cameraY, transform.position.z);
    }
}
