using Cinemachine;
using UnityEngine;
using UnityEngine.XR;
using static UnityEngine.AudioSettings;

namespace MenteBacata.ScivoloCharacterControllerDemo
{
    public class OrbitingCamera : MonoBehaviour
    {
        public Transform target;

        public float verticalOffset = 0f;

        public float distance = 5f;

        [Header("Camera Settings")]
        [SerializeField] private float sensitivity = 1f;
        [SerializeField] float mobileCameraSens = 12f;
        [Header("Input Settings")]
        [SerializeField] private bool invertY = false;
        [SerializeField] private string mouseXInput = "Mouse X";
        [SerializeField] private string mouseYInput = "Mouse Y";

        [SerializeField] 
        private CinemachineFreeLook freeLookCamera;

        private bool _isMobile;
        private Vector2 _lastTouchPosition;
        private bool _isTouching;

        private void Start()
        {
#if UNITY_EDITOR
            // Somehow after updating to 2019.3, mouse axes sensitivity decreased, but only in the editor.
            sensitivity *= 10f;
#endif
            // Настраиваем оси ввода для Cinemachine
            SetupCinemachineInput();
            //SetStartCameraRotation();
        }

        private void SetupCinemachineInput()
        {
            // Отключаем стандартный ввод Cinemachine
            freeLookCamera.m_XAxis.m_InputAxisName = "";
            freeLookCamera.m_YAxis.m_InputAxisName = "";

            // Настраиваем ограничения для осей
            freeLookCamera.m_YAxis.m_MinValue = 0f;
            freeLookCamera.m_YAxis.m_MaxValue = 75f; // Как в вашем оригинальном скрипте
        }
        private void Update()
        {
            if (!_isMobile)
            {
                HandleMouseInput();
            }
            else
            {
                HandleTouchInput();
            }

            ApplyInputToCamera();
        }
        private void HandleMouseInput()
        {
            float mouseX = 0f;
            float mouseY = 0f;

            // Поддержка старой и новой системы ввода
#if ENABLE_INPUT_SYSTEM
        if (Mouse.current != null)
        {
            mouseX = Mouse.current.delta.x.ReadValue() * sensitivity * Time.deltaTime;
            mouseY = Mouse.current.delta.y.ReadValue() * sensitivity * Time.deltaTime;
        }
#else
            mouseX = Input.GetAxis(mouseXInput) * sensitivity * Time.deltaTime;
            mouseY = Input.GetAxis(mouseYInput) * sensitivity * Time.deltaTime;
#endif

            if (invertY) mouseY = -mouseY;

            freeLookCamera.m_XAxis.m_InputAxisValue = mouseX;
            freeLookCamera.m_YAxis.m_InputAxisValue = mouseY;
        }

        private void HandleTouchInput()
        {
            if (Input.touchCount == 1)
            {
                Touch touch = Input.GetTouch(0);

                //if (IsTouchBlocked(touch.position)) return;

                switch (touch.phase)
                {
                    case TouchPhase.Began:
                        _lastTouchPosition = touch.position;
                        _isTouching = true;
                        break;

                    case TouchPhase.Moved:
                        if (_isTouching)
                        {
                            Vector2 delta = touch.position - _lastTouchPosition;

                            float touchX = delta.x * mobileCameraSens * Time.deltaTime;
                            float touchY = -delta.y * mobileCameraSens * Time.deltaTime;

                            freeLookCamera.m_XAxis.m_InputAxisValue = touchX;
                            freeLookCamera.m_YAxis.m_InputAxisValue = touchY;

                            _lastTouchPosition = touch.position;
                        }
                        break;

                    case TouchPhase.Ended:
                    case TouchPhase.Canceled:
                        _isTouching = false;
                        freeLookCamera.m_XAxis.m_InputAxisValue = 0f;
                        freeLookCamera.m_YAxis.m_InputAxisValue = 0f;
                        break;
                }
            }
            else
            {
                freeLookCamera.m_XAxis.m_InputAxisValue = 0f;
                freeLookCamera.m_YAxis.m_InputAxisValue = 0f;
            }
        }

        //private bool IsTouchBlocked(Vector2 touchPosition)
        //{
        //    foreach (var zone in blockedTouchZones)
        //    {
        //        if (zone == null) continue;

        //        Vector2 zoneCenter = zone.position;
        //        Vector2 zoneSize = zone.rect.size;
        //        Rect zoneRect = new Rect(
        //            zoneCenter.x - zoneSize.x / 2,
        //            zoneCenter.y - zoneSize.y / 2,
        //            zoneSize.x,
        //            zoneSize.y
        //        );

        //        // Добавляем небольшой отступ
        //        zoneRect = zoneRect.Expand(50f);

        //        if (zoneRect.Contains(touchPosition))
        //            return true;
        //    }

        //    return false;
        //}

        private void ApplyInputToCamera()
        {
            // Cinemachine автоматически применяет значения осей
            // Можно добавить дополнительные модификации здесь
        }

        public void SetMobile(bool state)
        {
            _isMobile = state;

            // Сбрасываем значения при переключении
            freeLookCamera.m_XAxis.m_InputAxisValue = 0f;
            freeLookCamera.m_YAxis.m_InputAxisValue = 0f;
        }

        public void SetTarget(Transform newTarget)
        {
            freeLookCamera.LookAt = newTarget;
            freeLookCamera.Follow = newTarget;
        }

        public void ResetCamera()
        {
            // Сброс камеры в начальную позицию
            freeLookCamera.m_XAxis.Value = 0f;
            freeLookCamera.m_YAxis.Value = 0.5f; // Средняя позиция по вертикали

            // Можно настроить под ваши начальные значения
            // Например: xRot = 30f соответсвует определенному значению YAxis
            // YAxis от 0 (низ) до 1 (верх)
            float normalizedXRot = 30f / 75f; // 75f - ваш MaxValue
            freeLookCamera.m_YAxis.Value = normalizedXRot;
        }
    }

    // Расширение для Rect
    public static class RectExtensions
    {
        public static Rect Expand(this Rect rect, float amount)
        {
            return new Rect(
                rect.x - amount,
                rect.y - amount,
                rect.width + amount * 2,
                rect.height + amount * 2
            );
        }
    }
}
   
