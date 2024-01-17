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
        
     
        if (Input.GetKey(KeyCode.A) || mousePos.x < 20)
        {
            if (transform.position.z < maxPositionLeft)
            {
                transform.Translate(-moveSpeed, 0, 0);
            }
        }

        if (Input.GetKey(KeyCode.D) || mousePos.x > 820)
        {
            if (transform.position.z > maxPositionRight)
            {
                transform.Translate(moveSpeed, 0, 0);
            }
        }

        if (Input.GetKey(KeyCode.W) || mousePos.y > 420)
        {
            if (transform.position.x < maxPositionUpX)
            {
                transform.Translate(0, moveSpeed, moveSpeed);
            }
        }

        if (Input.GetKey(KeyCode.S) || mousePos.y < 20)
        {
            if (transform.position.x > maxPositionDownX)
            {
                transform.Translate(0, -moveSpeed, -moveSpeed);
            }
        }
    }
}
