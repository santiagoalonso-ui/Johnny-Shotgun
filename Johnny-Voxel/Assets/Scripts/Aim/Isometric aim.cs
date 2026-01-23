using UnityEngine;
using UnityEngine.InputSystem;

namespace BarthaSzabolcs.IsometricAiming
{
    public class IsometricAiming : MonoBehaviour
    {
        [SerializeField] private LayerMask groundMask;
        [SerializeField] private Camera mainCamera;

        private void Awake()
        {
            // Forma segura (no usar Camera.main si puedes evitarlo)
            if (mainCamera == null)
                mainCamera = Camera.main;
        }

        private void Update()
        {
            Aim();
        }

        private void Aim()
        {
            var (success, position) = GetMousePosition();
            if (!success) return;

            Vector3 direction = position - transform.position;
            direction.y = 0;

            if (direction.sqrMagnitude > 0.001f)
                transform.forward = direction;
        }

        private (bool success, Vector3 position) GetMousePosition()
        {
            if (Mouse.current == null || mainCamera == null)
                return (false, Vector3.zero);

            Vector2 mouseScreenPosition = Mouse.current.position.ReadValue();
            Ray ray = mainCamera.ScreenPointToRay(mouseScreenPosition);

            if (Physics.Raycast(ray, out RaycastHit hit, 1000f, groundMask))
            {
                return (true, hit.point);
            }

            return (false, Vector3.zero);
        }
    }
}
