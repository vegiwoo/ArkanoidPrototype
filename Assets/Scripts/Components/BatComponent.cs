using UnityEngine;

namespace Arkanoid
{
    public class BatComponent : MonoBehaviour
    {
        private Camera batCamera;
        public Rigidbody Rigidbody { get; private set; }
        public Vector2 Movement { get; set; }

        private void Start()
        {
            Movement = Vector2.zero;

            CameraSettings();
            Rigidbody = this.GetComponent<Rigidbody>();

            if (Rigidbody != null)
            {
                Rigidbody.freezeRotation = true;
                Rigidbody.isKinematic = true;
            }
            else
            {
                Debug.LogError("Rigidbody in BatComponent is null!");
            }
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
                }
            }
        }
    }
}