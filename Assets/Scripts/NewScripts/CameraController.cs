using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CameraController : MonoBehaviour
{
    void Update()
    {
        GoingToPlayer();
    }
    private void GoingToPlayer()
    {
        float speed = 5f;
        float posX = Input.GetAxis("Horizontal");
        float posY = Input.GetAxis("Vertical");
        transform.Translate(Vector3.right * posX * speed * Time.deltaTime);
        transform.Translate(Vector3.up * posY * speed * Time.deltaTime);
    }
}
