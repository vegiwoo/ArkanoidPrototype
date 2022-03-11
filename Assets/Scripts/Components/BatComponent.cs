using UnityEngine;

namespace Arkanoid
{
    public class BatComponent : MonoBehaviour
    {
        private Camera batCamera;
        public Rigidbody Rigidbody { get; private set; }

        public Vector3 cameraPosition = Vector3.zero;

        private void Awake()
        {
            Rigidbody = GetComponent<Rigidbody>();
        }

        private void Start()
        {
            CameraSettings();
        }

        private void Update()
        {
            var indent = 0.2f;
            cameraPosition = new Vector3(transform.position.x + indent, transform.position.y + indent, transform.position.z + indent);
            batCamera.transform.position = cameraPosition;
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

                    cameraPosition = batCamera.transform.position;
                }
            }
        }
    }
}