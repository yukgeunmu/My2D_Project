using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BgLooper : MonoBehaviour
{
    public int numBgCount = 5;
    public int obstacleCount = 0;
    public Vector3 obstacleLastPostion = Vector3.zero;

    private void Start()
    {
        Obstacle[] obstacles = GameObject.FindObjectsOfType<Obstacle>();
        obstacleLastPostion = obstacles[0].transform.position;
        obstacleCount = obstacles.Length;

        // 장매물 프리팹 갯수만큼 출력
        for(int i = 0; i < obstacleCount; i++)
        {
            obstacleLastPostion = obstacles[i].SetRandomPlace(obstacleLastPostion, obstacleCount);
        }
    }

    // BgLooper와 충돌하면 이어서 계속 생성 
    public void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Triggered: " + collision.name);

       
        if(collision.CompareTag("BackGround"))
        {
            float widthOfBgObject = ((BoxCollider2D)collision).size.x;
            Vector3 pos = collision.transform.position;

            pos.x += widthOfBgObject * numBgCount;
            collision.transform.position = pos;

            return;
        }


        Obstacle obstacle = collision.GetComponent<Obstacle>();

        if(obstacle)
        {
            obstacleLastPostion = obstacle.SetRandomPlace(obstacleLastPostion, obstacleCount);
        }
    }

}
