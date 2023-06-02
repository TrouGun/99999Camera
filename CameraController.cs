using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Transform targetObject; // ī�޶� ������ ������Ʈ
    private Renderer targetRenderer; // Ŭ���� ������Ʈ�� Renderer
    private bool isDoubleClick = false; // ���� Ŭ�� ����
    private float doubleClickTime = 0.3f; // ���� Ŭ�� ����
    private float doubleClickTimer = 0f; // ���� Ŭ�� Ÿ�̸�
    private float xRotate, zRotate, xRotateMove, zRotateMove;
    public float rotateSpeed = 500.0f;
    public float Rotate1;
    public float Rotate2;
    public float orbitSpeed = 20.0f; // ������Ʈ ���� �ӵ�

    void Update()
    {
        // ���� Ŭ�� Ÿ�̸� ������Ʈ
        if (isDoubleClick)
        {
            doubleClickTimer += Time.deltaTime;
            if (doubleClickTimer >= doubleClickTime)
            {
                isDoubleClick = false;
                doubleClickTimer = 0f;
            }
        }

        // ī�޶� �������� �޾ƿ�
        float xMove = Input.GetAxis("Horizontal");
        float zMove = Input.GetAxis("Vertical");

        // ī�޶��� �̵� �ӵ� ����
        float moveSpeed = 5.0f;

        // ī�޶� �̵�
        Vector3 newPosition = transform.position + (transform.right * xMove + transform.forward * zMove) * moveSpeed * Time.deltaTime;
        newPosition.y = transform.position.y; // Y �� ����
        transform.position = newPosition;

        xRotateMove = -Input.GetAxis("Mouse Y") * Time.deltaTime * rotateSpeed;
        zRotateMove = Input.GetAxis("Mouse X") * Time.deltaTime * rotateSpeed;

        zRotate = transform.eulerAngles.z + zRotateMove;
        xRotate = xRotate + xRotateMove;
        xRotate = Mathf.Clamp(xRotate, Rotate1, Rotate2);

        transform.eulerAngles = new Vector3(xRotate, transform.eulerAngles.y, 0);

        // ������Ʈ�� ����Ǿ� ���� ���� ���, ������Ʈ�� Ŭ���ϸ� �ش� ������Ʈ�� ����
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

        // ������Ʈ�� ����Ǿ� �ִ� ���, ���콺 Ŭ������ ������ ����
        else if (targetObject != null && Input.GetMouseButtonDown(0))
        {
            targetObject = null;
            targetRenderer = null;
            StopAllCoroutines();
        }

        // ������Ʈ�� ����Ǿ� ������ �ش� ������Ʈ�� �߽����� ȸ��
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

    // ������Ʈ ������ �ڵ����� ����
    IEnumerator OrbitObject()
    {
        while (true)
        {
            float orbitAngle = orbitSpeed * Time.deltaTime;
            transform.RotateAround(targetObject.position, Vector3.up, orbitAngle);
            transform.eulerAngles = new Vector3(xRotate, transform.eulerAngles.y, 0); // X �� ����
            yield return null;
        }
    }
}