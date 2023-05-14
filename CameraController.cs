using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Transform targetObject; // 카메라가 집중할 오브젝트
    private Renderer targetRenderer; // 클릭한 오브젝트의 Renderer
    private float xRotate, yRotate, xRotateMove, yRotateMove;
    public float rotateSpeed = 500.0f;
    public float Rotate1;
    public float Rotate2;

    void Update()
    {
        xRotateMove = -Input.GetAxis("Mouse Y") * Time.deltaTime * rotateSpeed;
        yRotateMove = Input.GetAxis("Mouse X") * Time.deltaTime * rotateSpeed;

        yRotate = transform.eulerAngles.y + yRotateMove;
        xRotate = xRotate + xRotateMove;
        xRotate = Mathf.Clamp(xRotate, Rotate1, Rotate2);

        transform.eulerAngles = new Vector3(xRotate, yRotate, 0);

        // 오브젝트가 저장되어 있지 않은 경우, 오브젝트를 클릭하면 해당 오브젝트를 저장
        if (targetObject == null && Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                targetRenderer = hit.collider.GetComponent<Renderer>();
                if (targetRenderer != null)
                {
                    targetObject = hit.transform;
                }
            }
        }
        // 오브젝트가 저장되어 있는 경우, 마우스 클릭으로 시점을 해제
        else if (targetObject != null && Input.GetMouseButtonDown(0))
        {
            targetObject = null;
            targetRenderer = null;
        }
        // 오브젝트가 저장되어 있으면 해당 오브젝트를 중심으로 회전
        else if (targetObject != null)
        {
            float xRotateMove = -Input.GetAxis("Mouse Y") * Time.deltaTime * rotateSpeed;
            float yRotateMove = Input.GetAxis("Mouse X") * Time.deltaTime * rotateSpeed;

            float yRotate = transform.eulerAngles.y + yRotateMove;
            float xRotate = transform.eulerAngles.x + xRotateMove;
            xRotate = Mathf.Clamp(xRotate, Rotate1, Rotate2);

            transform.eulerAngles = new Vector3(xRotate, yRotate, 0);

            transform.LookAt(targetObject);
            transform.RotateAround(targetObject.position, Vector3.up, yRotateMove);
            transform.RotateAround(targetObject.position, transform.right, -xRotateMove);
        }
    }
}
