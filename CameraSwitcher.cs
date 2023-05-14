using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitcher : MonoBehaviour
{
    public Camera[] cameras;
    private int activeCameraIndex = 0;

    void Start()
    {
        // ù ��° ī�޶� Ȱ��ȭ
        cameras[activeCameraIndex].gameObject.SetActive(true);
    }

    void Update()
    {
        // ���� Ű �Է��� �޾� ī�޶� ����
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SwitchCamera(0);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SwitchCamera(1);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SwitchCamera(2);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            SwitchCamera(3);
        }
    }

    private void SwitchCamera(int cameraIndex)
    {
        // ������ ī�޶� �̹� Ȱ��ȭ�� ���¸� ����
        if (cameraIndex == activeCameraIndex)
            return;

        // ��� ī�޶� ��Ȱ��ȭ
        for (int i = 0; i < cameras.Length; i++)
        {
            cameras[i].gameObject.SetActive(false);
        }

        // ������ ī�޶� Ȱ��ȭ
        cameras[cameraIndex].gameObject.SetActive(true);

        activeCameraIndex = cameraIndex;
    }
}
