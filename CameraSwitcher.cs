using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSwitcher : MonoBehaviour
{
    public Camera[] cameras;
    private int activeCameraIndex = 0;

    void Start()
    {
        // 첫 번째 카메라만 활성화
        cameras[activeCameraIndex].gameObject.SetActive(true);
    }

    void Update()
    {
        // 숫자 키 입력을 받아 카메라 변경
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
        // 선택한 카메라가 이미 활성화된 상태면 무시
        if (cameraIndex == activeCameraIndex)
            return;

        // 모든 카메라를 비활성화
        for (int i = 0; i < cameras.Length; i++)
        {
            cameras[i].gameObject.SetActive(false);
        }

        // 선택한 카메라를 활성화
        cameras[cameraIndex].gameObject.SetActive(true);

        activeCameraIndex = cameraIndex;
    }
}
