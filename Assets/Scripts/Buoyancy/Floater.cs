using UnityEngine;

namespace Buoyancy
{
    public class Floater : MonoBehaviour
    {
        [Tooltip("Determines how deep the floater can sink into the water before buoyancy forces are applied.")]
        [SerializeField] private float depthBeforeSubmerged = 1f;
        [Tooltip("Scales the buoyant force applied to the floater based on its immersion depth.")]
        [SerializeField] private float displacementAmount = 1f;
        [Tooltip("Simulates the resistance experienced by the floater due to its motion through the water.")]
        [SerializeField] private float waterDrag = 1f;
        [Tooltip("Affects the rotation of the floater as it moves through the water.")]
        [SerializeField] private float waterAngularDrag = 3f;

        private Rigidbody _rigidbody;
        private int _floaterCount;
        private float _waveHeight;
        
        private static Vector3 Gravity => Physics.gravity;

        public void Initialize(Rigidbody rigidbody, int floaterCount)
        {
            _rigidbody = rigidbody;
            _floaterCount = floaterCount;
        }

        public void Float()
        {
            float gravityForce = Gravity.y / _floaterCount;
            ApplyGravity(gravityForce);

            Vector3 position = transform.position;
            _waveHeight = WaveManager.Instance.GetWaveHeight(position);

            if (position.y < _waveHeight)
            {
                float deltaY = _waveHeight - position.y;
                float displacementMultiplier = Mathf.Clamp01(deltaY / depthBeforeSubmerged) * displacementAmount;

                ApplyGravity(Mathf.Abs(Gravity.y), displacementMultiplier);
                ApplyDrag(waterDrag, displacementMultiplier);
                ApplyAngularDrag(waterAngularDrag, displacementMultiplier);
            }
        }

        private void ApplyGravity(float gravityForce, float displacementMultiplier = 1f)
        {
            Vector3 force = new Vector3(0f, gravityForce * displacementMultiplier, 0f);
            _rigidbody.AddForceAtPosition(force, transform.position, ForceMode.Acceleration);
        }

        private void ApplyDrag(float drag, float displacementMultiplier)
        {
            Vector3 force = -_rigidbody.velocity * (drag * displacementMultiplier * Time.fixedDeltaTime);
            _rigidbody.AddForce(force, ForceMode.VelocityChange);
        }

        private void ApplyAngularDrag(float angularDrag, float displacementMultiplier)
        {
            Vector3 force = -_rigidbody.angularVelocity * (angularDrag * displacementMultiplier * Time.fixedDeltaTime);
            _rigidbody.AddTorque(force, ForceMode.VelocityChange);
        }
    }
}