using ECM.Examples.Common;
using UnityEngine;

namespace ECM.Examples.Components
{
    [RequireComponent(typeof(Rigidbody))]
    public class KinematicRotate : MonoBehaviour
    {
        #region FIELDS

        [SerializeField]
        private Vector3 _rotationVector = new Vector3(0,0,0);

        #endregion

        #region PRIVATE FIELDS

        private Rigidbody _rigidbody;
        private Vector3 _angle;
        private Vector3 startRotation;

        #endregion

        #region PROPERTIES

        public Vector3 rotationVector
        {
            get { return _rotationVector; }
            set {
                _rotationVector.x = Mathf.Clamp(value.x, -360.0f, 360.0f);
                _rotationVector.y = Mathf.Clamp(value.y, -360.0f, 360.0f);
                _rotationVector.z = Mathf.Clamp(value.z, -360.0f, 360.0f);
            }
        }

        public Vector3 angle
        {
            get { return _angle; }
            set {
                _angle.x = Utils.WrapAngle(value.x);
                _angle.y = Utils.WrapAngle(value.y);
                _angle.z = Utils.WrapAngle(value.z);
            }
        }

        #endregion

        #region MONOBEHAVIOUR

        public void OnValidate()
        {
            rotationVector = _rotationVector;
        }

        public void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _rigidbody.isKinematic = true;
            startRotation = transform.localEulerAngles;
        }

        public void FixedUpdate()
        {
            angle += rotationVector * Time.deltaTime;
            
            var rotation = Quaternion.Euler(startRotation + angle);

            _rigidbody.MoveRotation(rotation);
        }

        #endregion
    }
}
