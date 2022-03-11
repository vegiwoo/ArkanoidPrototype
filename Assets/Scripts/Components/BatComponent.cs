using UnityEngine;

namespace Arkanoid
{
    public class BatComponent : MonoBehaviour
    {
        private Camera batCamera;
        public Rigidbody Rigidbody { get; private set; }

        private void Awake()
        {
            Rigidbody = GetComponent<Rigidbody>();
        }

        private void Start()
        {
            CameraSettings();
        }

        /// <summary>Настраивает дочерний компонент камеры.</summary>
        private void CameraSettings()
        {
            GameObject cameraObject = gameObject.transform.Find("Camera").gameObject;
            if (cameraObject != null)
            {
                batCamera = cameraObject.GetComponent<Camera>();

                if (batCamera != null)
                {
                    bool isBat02 = name == "Bat02";

                    // Назначение ИНДЕКСА целевого дисплея 
                    batCamera.targetDisplay = isBat02 ? 1 : 0;

                    if (isBat02) batCamera.transform.Rotate(180, 180, 0);

                    AudioListener audioListener = batCamera.GetComponent<AudioListener>();

                    if (audioListener != null && audioListener.isActiveAndEnabled)
                    {
                        audioListener.gameObject.SetActive(false);
                    }

                    batCamera.usePhysicalProperties = true;
                    batCamera.fieldOfView = 80;
                }
            }
        }

        /// <summary>Определяет является ли переданный объект дочерним относительно текущего.</summary>
        /// <param name="intendedChild"></param>
        /// <returns></returns>
        public bool CheckIsObjectChild(Transform intendedChild)
        {
            return intendedChild.IsChildOf(transform);
        }

        /// <summary>Назначает или удаляет текущий компонент в качестве родителя для переданного.</summary>
        /// <param name="isAssignParent">Флаг назначания.</param>
        /// <param name="child">Дочерний компонент.</param>
        public void SetComponentAsParent(bool isAssignParent, Transform child)
        {
            Transform targret = gameObject.transform;

            child.transform.parent = isAssignParent ? targret : null;
            child.transform.position = new Vector3(targret.position.x, targret.position.y, targret.position.z + 1.2f);
        }
    }
}