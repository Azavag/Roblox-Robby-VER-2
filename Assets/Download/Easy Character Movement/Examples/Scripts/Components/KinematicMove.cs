using ECM.Examples.Common;
using UnityEngine;

namespace ECM.Examples.Components
{
    [RequireComponent(typeof(Rigidbody))]
    public class KinematicMove : MonoBehaviour
    {
        #region FIELDS

        [SerializeField]
        public float _moveTime = 3.0f;

        [SerializeField]
        private Transform targetPosition;
        [SerializeField]
        private float stopDuration = 1f;
        #endregion

        #region PRIVATE FIELDS

       private Rigidbody _rigidbody;

        private Vector3 _startPosition;
        private Vector3 _targetPosition;
        private float stopTimer;
        private bool isStopped = false;
        private float _time;

        #endregion

        #region PROPERTIES
        
        public float moveTime
        {
            get { return _moveTime; }
            set { _moveTime = Mathf.Max(1.0f, value); }
        }

        #endregion

        #region MONOBEHAVIOUR

        public void OnValidate()
        {
            moveTime = _moveTime;
        }

        public void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _rigidbody.isKinematic = true;

            _startPosition = transform.position;
            _targetPosition = targetPosition.position;
        }


        public void FixedUpdate()
        {
            
        }
        private void Update()
        {
            if (isStopped)
            {
                StopTimer();
                return;
            }
            _time += Time.deltaTime;
            var t = Utils.EaseInOut(Mathf.PingPong(_time, _moveTime), _moveTime);
            var p = Vector3.Lerp(_startPosition, _targetPosition, t);
            if (p == _targetPosition || p == _startPosition)
            {
                isStopped = true;
                stopTimer = stopDuration;
                if (p == _startPosition)
                    _time = 0;
            }
            _rigidbody.MovePosition(p);
        }
        private void LateUpdate()
        {
        }

        void StopTimer()
        {
            stopTimer -= Time.deltaTime;
            if (stopTimer <= 0)
                isStopped = false;
        }

        #endregion
    }
}
