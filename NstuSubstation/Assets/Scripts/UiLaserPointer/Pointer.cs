using UnityEngine;
using UnityEngine.EventSystems;
using SceneController;
namespace UiLaserPointer
{
    public class Pointer : MonoBehaviour
    {
        public float defaultLength = 5.0f;
        public GameObject dot;
        public VRInputModule vrInputModule;

        private LineRenderer lineRenderer;
    
        private void Awake()
        {
            lineRenderer = GetComponent<LineRenderer>();
        }
        
        private void Update()
        {
            UpdateLine();
            ResetRotation();
        }

        private void UpdateLine()
        {
            PointerEventData data = vrInputModule.GetData();
        
            float targetLength = data.pointerCurrentRaycast.distance == 0 ? defaultLength : data.pointerCurrentRaycast.distance;

            RaycastHit hit = CreateRaycast();

            Vector3 endPosition = transform.position + (transform.forward * targetLength);

            if (hit.collider != null)
                endPosition = hit.point;

            dot.transform.position = endPosition;
        
            lineRenderer.SetPosition(0, transform.position);
            lineRenderer.SetPosition(1, endPosition);
        }

        private RaycastHit CreateRaycast()
        {
            RaycastHit hit;
            Ray ray = new Ray(transform.position, transform.forward);
            Physics.Raycast(ray, out hit, defaultLength);
        
            return hit;
        }

        public void ResetRotation()
        {
            gameObject.transform.localRotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
        }
    }
}
