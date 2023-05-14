using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Transform targetObject; // ī�޶� ������ ������Ʈ
    private Renderer targetRenderer; // Ŭ���� ������Ʈ�� Renderer
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

        // ������Ʈ�� ����Ǿ� ���� ���� ���, ������Ʈ�� Ŭ���ϸ� �ش� ������Ʈ�� ����
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
        // ������Ʈ�� ����Ǿ� �ִ� ���, ���콺 Ŭ������ ������ ����
        else if (targetObject != null && Input.GetMouseButtonDown(0))
        {
            targetObject = null;
            targetRenderer = null;
        }
        // ������Ʈ�� ����Ǿ� ������ �ش� ������Ʈ�� �߽����� ȸ��
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
