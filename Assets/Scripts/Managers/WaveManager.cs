using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [SerializeField] private Material material;

    private WaveData _waveData;
    private float _currentTime;

    public static WaveManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(this);
        }
    }

    private void Start()
    {
        _waveData = new WaveData()
        {
            direction = material.GetVector("_Direction"),
            steepness = material.GetFloat("_Steepness"),
            wavelength = material.GetFloat("_Wavelength")
        };
    }

    private void Update()
    {
        _currentTime += Time.deltaTime;
        material.SetFloat("_CurrentTime", _currentTime);
    }

    public float GetWaveHeight(Vector3 position)
    {
        float waveHeight = 0f;

        Vector2 d = _waveData.direction.normalized;
        float k = 2 * Mathf.PI / _waveData.wavelength;
        float c = Mathf.Sqrt(9.8f / k);
        float f = k * (Vector2.Dot(d, new Vector2(position.x, position.z)) - c * _currentTime);
        float a = _waveData.steepness / k;

        waveHeight += a * Mathf.Sin(f);

        return waveHeight;
    }
}

public struct WaveData
{
    public Vector2 direction;
    public float steepness;
    public float wavelength;
}