using UnityEngine;
using UnityEngine.EventSystems;
using Valve.VR;

namespace UiLaserPointer
{
    public class VRInputModule : BaseInputModule
    {
        public new Camera camera;
        public SteamVR_Input_Sources targetSource;
        public SteamVR_Action_Boolean clickAction;

        private GameObject _currentObject;
        private PointerEventData _data;
    
        protected override void Awake()
        {
            base.Awake();

            _data = new PointerEventData(eventSystem);
        }

        public override void Process()
        {
            _data.Reset();
            _data.position = new Vector2(camera.pixelWidth / 2, camera.pixelHeight / 2);
        
            eventSystem.RaycastAll(_data, m_RaycastResultCache);
            _data.pointerCurrentRaycast = FindFirstRaycast(m_RaycastResultCache);
            _currentObject = _data.pointerCurrentRaycast.gameObject;
        
            m_RaycastResultCache.Clear();
        
            HandlePointerExitAndEnter(_data, _currentObject);
        
            if(clickAction.GetStateDown(targetSource))
                ProcessPress(_data);
        
            if(clickAction.GetStateUp(targetSource))
                ProcessRelease(_data);
        }

        public PointerEventData GetData()
        {
            return _data;
        }

        private void ProcessPress(PointerEventData data)
        {
            data.pointerPressRaycast = data.pointerCurrentRaycast;

            var newPointerPress = ExecuteEvents.ExecuteHierarchy(_currentObject, data, ExecuteEvents.pointerDownHandler);

            if (newPointerPress == null)
                newPointerPress = ExecuteEvents.GetEventHandler<IPointerClickHandler>(_currentObject);

            data.pressPosition = data.position;
            data.pointerPress = newPointerPress;
            data.rawPointerPress = _currentObject;
        }

        private void ProcessRelease(PointerEventData data)
        {
            ExecuteEvents.Execute(data.pointerPress, data, ExecuteEvents.pointerUpHandler);

            var pointerUpHandler = ExecuteEvents.GetEventHandler<IPointerClickHandler>(_currentObject);

            if (data.pointerPress == pointerUpHandler)
                ExecuteEvents.Execute(data.pointerPress, data, ExecuteEvents.pointerClickHandler);
        
            eventSystem.SetSelectedGameObject(null);
        
            data.pressPosition = Vector2.zero;
            data.pointerPress = null;
            data.rawPointerPress = null;
        }
    }
}
