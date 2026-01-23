using UnityEngine;

public class MousePosition : MonoBehaviour
{
    [SerializeField] private Camera maincamera;

    private void Update()
    {
       Ray ray = maincamera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit raycastHit))
        {
            transform.position = raycastHit.point;
        }
    }
}
