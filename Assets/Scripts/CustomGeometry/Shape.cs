using UnityEngine;

namespace CustomGeometry
{
    [RequireComponent(typeof(MeshFilter))]
    [RequireComponent(typeof(MeshRenderer))]
    public abstract class Shape : MonoBehaviour
    {
        [SerializeField] private bool displayGizmos = true;
        
        private MeshFilter _meshFilter = null;
        private MeshRenderer _meshRenderer = null;

        protected Vector3[] _vertices = null;
        protected int[] _triangles = null;
        protected Vector2[] _uv = null;

        public Mesh Mesh { get; private set; } = null;

        private void Awake()
        {
            _meshFilter = GetComponent<MeshFilter>();
            _meshRenderer = GetComponent<MeshRenderer>();
        }

        private void Start()
        {
            CreateShape();
        }

        private void OnEnable()
        {
            Mesh = new Mesh { name = $"{GetType().Name}" };
            _meshFilter.mesh = Mesh;

            if (_meshRenderer.material == null)
            {
                Shader shader = Shader.Find("Universal Render Pipeline/Lit");
                _meshRenderer.material = new Material(shader);
            }
        }
        
        protected virtual void CreateShape()
        {
            GenerateVertices();
            GenerateTriangles();
            UpdateMesh();
        }
        
        protected abstract void GenerateVertices();

        protected abstract void GenerateTriangles();

        public void UpdateMesh(Vector3[] vertices = null, int[] triangles = null, Vector2[] uv = null, bool clearMesh = true)
        {
            if (clearMesh)
            {
                Mesh.Clear();
            }
            
            Mesh.vertices = vertices ?? _vertices;
            Mesh.triangles = triangles ?? _triangles;
            Mesh.uv = uv ?? _uv;
            Mesh.RecalculateNormals();
        }

        private void OnDrawGizmos()
        {
            if (displayGizmos == false || _vertices == null) return;
            
            Gizmos.color = Color.red;
            
            foreach (Vector3 vertex in _vertices)
            {
                Vector3 center = transform.TransformPoint(vertex);
                float radius = 0.1f;
                Gizmos.DrawSphere(center, radius);
            }
        }
    }
}
