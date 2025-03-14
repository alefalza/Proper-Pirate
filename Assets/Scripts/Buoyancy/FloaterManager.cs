using UnityEngine;

namespace Buoyancy
{
    [RequireComponent(typeof(Rigidbody))]
    public class FloaterManager : MonoBehaviour
    {
        [SerializeField] private Floater[] floaters = null;

        private Rigidbody _rigidbody = null;
        
        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _rigidbody.useGravity = false;
            _rigidbody.isKinematic = false;
        }

        private void Start()
        {
            InitializeFloaters();
        }

        private void FixedUpdate()
        {
            UpdateFloaters();
        }

        private void InitializeFloaters()
        {
            foreach (Floater floater in floaters)
            {
                floater.Initialize(_rigidbody, floaters.Length);
            }
        }

        private void UpdateFloaters()
        {
            foreach (Floater floater in floaters)
            {
                floater.Float();
            }
        }
    }
}