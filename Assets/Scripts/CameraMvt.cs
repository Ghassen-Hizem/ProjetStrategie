using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEngine.GraphicsBuffer;

public class CameraMvt : MonoBehaviour
{

    private float moveSpeed = 0.1f;
    private Vector3 mousePos;

    private float maxPositionLeft;
    private float maxPositionRight;
    private float maxPositionUpX;
    private float maxPositionDownX;
    private int Arenaindex;
    [SerializeField] private Camera cam;

    private void Start()
    {
        Arenaindex = SceneManager.GetActiveScene().buildIndex;
        
        if (Arenaindex == 1)
        {
            maxPositionLeft = -135f;
            maxPositionRight = -153f;
            maxPositionUpX = 14f;
            maxPositionDownX = -60f;
        }
        else if (Arenaindex == 3)
        {
            maxPositionLeft = 30f;
            maxPositionRight = 12f;
            maxPositionUpX = -34f;
            maxPositionDownX = -68.5f;
        }

    }

    void Update()
    {

        mousePos = Input.mousePosition;
        var pos = new Vector3(mousePos.x / Screen.width, mousePos.y / Screen.height, mousePos.z);
        
        //if mouse not over UI
        if (Input.GetKey(KeyCode.A) || pos.x < 0.06)
        {
            if (transform.position.z < maxPositionLeft)
            {
                transform.Translate(-moveSpeed, 0, 0);
            }
        }

        if (Input.GetKey(KeyCode.D) || pos.x > 0.95)
        {
            if (transform.position.z > maxPositionRight)
            {
                transform.Translate(moveSpeed, 0, 0);
            }
        }

        if (Input.GetKey(KeyCode.W) || pos.y > 0.95)
        {
            if (transform.position.x < maxPositionUpX)
            {
                transform.Translate(0, moveSpeed, moveSpeed);
            }
        }

        if (Input.GetKey(KeyCode.S) || pos.y < 0.15)
        {
            if (transform.position.x > maxPositionDownX)
            {
                transform.Translate(0, -moveSpeed, -moveSpeed);
            }
        }
    }
}
