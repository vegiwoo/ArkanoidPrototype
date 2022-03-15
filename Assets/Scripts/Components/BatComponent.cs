using UnityEngine;

namespace Arkanoid
{
    /// <summary>Компонент биты (платформы) игрока.</summary>
    public class BatComponent : MonoBehaviour, ISideble
    {
        /// <summary>Камера как дочерний объект биты.</summary>
        private Camera batCamera;
        /// <summary>Твердое тело биты.</summary>
        public Rigidbody Rigidbody { get; private set; }
        public SideOfConflict Side { get; set; }

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
            if (isAssignParent)
            {
                child.transform.position = transform.position;
                child.transform.parent = transform;

                switch (Side)
                {
                    case SideOfConflict.First:
                        child.transform.Translate(0.0f, 0.0f, 1.5f);
                        break;
                    case SideOfConflict.Second:
                        child.transform.Translate(0.0f, 0.0f, 1.5f);
                        break;
                }
            }
            else
            {
                child.transform.parent = null;
            }
        }
    }
}