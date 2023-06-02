using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Transform targetObject; // 카메라가 집중할 오브젝트
    private Renderer targetRenderer; // 클릭한 오브젝트의 Renderer
    private bool isDoubleClick = false; // 더블 클릭 여부
    private float doubleClickTime = 0.3f; // 더블 클릭 간격
    private float doubleClickTimer = 0f; // 더블 클릭 타이머
    private float xRotate, zRotate, xRotateMove, zRotateMove;
    public float rotateSpeed = 500.0f;
    public float Rotate1;
    public float Rotate2;
    public float orbitSpeed = 20.0f; // 오브젝트 공전 속도

    void Update()
    {
        // 더블 클릭 타이머 업데이트
        if (isDoubleClick)
        {
            doubleClickTimer += Time.deltaTime;
            if (doubleClickTimer >= doubleClickTime)
            {
                isDoubleClick = false;
                doubleClickTimer = 0f;
            }
        }

        // 카메라 움직임을 받아옴
        float xMove = Input.GetAxis("Horizontal");
        float zMove = Input.GetAxis("Vertical");

        // 카메라의 이동 속도 조정
        float moveSpeed = 5.0f;

        // 카메라 이동
        Vector3 newPosition = transform.position + (transform.right * xMove + transform.forward * zMove) * moveSpeed * Time.deltaTime;
        newPosition.y = transform.position.y; // Y 축 고정
        transform.position = newPosition;

        xRotateMove = -Input.GetAxis("Mouse Y") * Time.deltaTime * rotateSpeed;
        zRotateMove = Input.GetAxis("Mouse X") * Time.deltaTime * rotateSpeed;

        zRotate = transform.eulerAngles.z + zRotateMove;
        xRotate = xRotate + xRotateMove;
        xRotate = Mathf.Clamp(xRotate, Rotate1, Rotate2);

        transform.eulerAngles = new Vector3(xRotate, transform.eulerAngles.y, 0);

        // 오브젝트가 저장되어 있지 않은 경우, 오브젝트를 클릭하면 해당 오브젝트를 저장
        if (targetObject == null && Input.GetMouseButtonDown(0) && !isDoubleClick)
        {
            isDoubleClick = true;
            doubleClickTimer = 0f;
        }
        else if (isDoubleClick && doubleClickTimer < doubleClickTime && Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                targetRenderer = hit.collider.GetComponent<Renderer>();
                if (targetRenderer != null && hit.collider.CompareTag("Pigeon"))
                {
                    targetObject = hit.transform;
                    StartCoroutine(OrbitObject());
                }
            }
        }

        // 오브젝트가 저장되어 있는 경우, 마우스 클릭으로 시점을 해제
        else if (targetObject != null && Input.GetMouseButtonDown(0))
        {
            targetObject = null;
            targetRenderer = null;
            StopAllCoroutines();
        }

        // 오브젝트가 저장되어 있으면 해당 오브젝트를 중심으로 회전
        else if (targetObject != null)
        {
            float yRotateMove = Input.GetAxis("Mouse X") * Time.deltaTime * rotateSpeed;

            float yRotate = transform.eulerAngles.y + yRotateMove;

            transform.eulerAngles = new Vector3(xRotate, yRotate, 0);

            transform.LookAt(targetObject);
            transform.RotateAround(targetObject.position, Vector3.up, yRotateMove);
            transform.RotateAround(targetObject.position, transform.right, -xRotateMove);
        }
    }

    // 오브젝트 주위를 자동으로 공전
    IEnumerator OrbitObject()
    {
        while (true)
        {
            float orbitAngle = orbitSpeed * Time.deltaTime;
            transform.RotateAround(targetObject.position, Vector3.up, orbitAngle);
            transform.eulerAngles = new Vector3(xRotate, transform.eulerAngles.y, 0); // X 축 고정
            yield return null;
        }
    }
}