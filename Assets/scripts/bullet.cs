using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    private Camera mainCamera;
    // Start is called before the first frame update
    private void Start()
    {
        mainCamera = Camera.main;
    }

    private void Update()
    {
        // 获取物体在世界坐标中的位置
        Vector3 objectPosition = transform.position;

        // 将物体的位置转换为屏幕坐标
        Vector3 screenPoint = mainCamera.WorldToViewportPoint(objectPosition);

        // 检查物体是否超出屏幕范围
        if (screenPoint.x < 0 || screenPoint.x > 1 || screenPoint.y < 0 || screenPoint.y > 1)
        {
            // 物体超出屏幕范围，销毁它
            Destroy(gameObject);
        }
    }
}
