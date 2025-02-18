using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField] private float highPosY = 1f;
    [SerializeField] private float lowPosY = -1f;

    [SerializeField] private float holeSizeMin = 1f;
    [SerializeField] private float holeSizeMax = 3f;

    [SerializeField] private Transform topObject;
    [SerializeField] private Transform bottomObjetc;

    [SerializeField] private float widthPadding = 4f;


    private void Start()
    {
        if (topObject == null)
            Debug.LogError("TopObject is Null");

        if (bottomObjetc == null)
            Debug.LogError("bottomObject is Null");
    }

    public Vector3 SetRandomPlace(Vector3 lastPosition, int obstacleCount)
    {
        // 구멍 사이즈 결정
        float holeSize = Random.Range(holeSizeMin, holeSizeMax);
        float halfHoseSize = holeSize / 2f;
        topObject.localPosition = new Vector3(0, halfHoseSize);
        bottomObjetc.localPosition = new Vector3(0, -halfHoseSize);

        // 랜덤한 위치에 옵스타클 배치
        Vector3 placePosition = lastPosition + new Vector3(widthPadding, 0);
        placePosition.y = Random.Range(lowPosY, highPosY);

        transform.position = placePosition;

        return placePosition;
    }



}
