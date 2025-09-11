using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;
public enum LevelCam
{
    game, boss
}
public class CameraFollow : MonoBehaviour
{
    [Header("Variable")]
    private float followSpeed = 1.5f;
    private float yOffset = -0.5f; //0.3
    private float xOffset = 3f; //5
    private float initialY;
    private bool camLeft = false;
    public bool onGround = false;
    public LevelCam levelcam;

    public Transform target;
    public PlayerController player;
    
    private void Awake()
    {
        if(levelcam == LevelCam.game)
        {
            followSpeed = 1.5f;
        }
        else if(levelcam == LevelCam.boss)
        {
            followSpeed = 0f;
        }
        player = FindAnyObjectByType<PlayerController>();
        if (player != null)
        {
            target = player.transform;
        }
        else
        {
            Debug.Log("No player");
        }
        initialY = transform.position.y;
    }
    private void FixedUpdate()
    {
        if (target == null && player == null)
            return;
        if(!player._isFacingRight && !camLeft) 
        {
            xOffset = -3f;
            camLeft = true;
        }
        else if (player._isFacingRight && camLeft)
        {
            xOffset = 3f;
            camLeft = false;
        }
        float targetY = transform.position.y;
        if (onGround)
        {
            targetY = initialY;
        }
        else if (player.isOnPlatform)
        {
            targetY = target.position.y + yOffset;
        }
        else
        {
            targetY = initialY;
        }

        Vector3 newPos = new Vector3(target.position.x + xOffset, targetY, -10f);
        transform.position = Vector3.Slerp(transform.position, newPos, followSpeed * Time.deltaTime);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground"))
        {
            onGround = true;
            Debug.Log("Y");
        }
    }
}
