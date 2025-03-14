using UnityEngine;

namespace CustomGeometry
{
    public class Plane : Shape
    {
        [SerializeField] private int length = 1;
        [SerializeField] private int resolution = 1;

        protected override void GenerateVertices()
        {
            int arrayLength = (resolution + 1) * (resolution + 1);
            _vertices = new Vector3[arrayLength];
            
            float halfLength = length / 2f;
            float increment = (1f / resolution) * length;
            int vertexCount = 0;

            for (float z = -halfLength; z <= halfLength; z += increment)
            {
                for (float x = -halfLength; x <= halfLength; x += increment)
                {
                    Vector3 vertex = new Vector3(x, 0f, z);
                    _vertices[vertexCount] = vertex;
                    vertexCount++;
                }
            }
        }

        protected override void GenerateTriangles()
        {
            int arrayLength = resolution * resolution * 6;
            _triangles = new int[arrayLength];

            int tris = 0;
            int k = resolution + 1;

            for (int z = 0; z < k * resolution; z += k)
            {
                for (int x = 0; x < resolution; x++)
                {
                    _triangles[tris + 0] = z + x;
                    _triangles[tris + 1] = z + x + k;
                    _triangles[tris + 2] = z + x + k + 1;

                    _triangles[tris + 3] = z + x;
                    _triangles[tris + 4] = z + x + k + 1;
                    _triangles[tris + 5] = z + x + 1;

                    tris += 6;
                }
            }
        }
    }
}
