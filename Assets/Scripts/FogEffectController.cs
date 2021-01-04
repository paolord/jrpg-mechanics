using UnityEngine;
using UnityEngine.UI;

public class FogEffectController : MonoBehaviour
{
    [SerializeField] private float minOffset; // 0
    [SerializeField] private float maxOffset; // 100
    [SerializeField] private float speed; // 0.0015

    private Material _noiseMaterial;
    private Vector4 _offset;

    private float _direction;

    void Awake()
    {
        _noiseMaterial = GetComponent<Image>().material;
        _offset = new Vector4(minOffset, 0, 0, 0);
        _direction = 1;
    }

    void Update()
    {
        if (_offset.x < minOffset || _offset.x > maxOffset)
        {
            _direction *= -1;
        }
        _offset.x += (speed * _direction);
        // Note: You have to use the value in the Reference field in the Shader Graph Blackboard
        _noiseMaterial.SetVector("Vector2_95E3719", _offset);
    }
    private void OnEnable()
    {
        _offset = new Vector4(minOffset, 0, 0, 0);
    }

    private void OnDisable()
    {
            
    }
}
