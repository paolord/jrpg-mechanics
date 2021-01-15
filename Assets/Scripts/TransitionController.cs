using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionController : MonoBehaviour
{
    [SerializeField] private float startingPixelation;
    [SerializeField] private float endingPixelation;
    [SerializeField] private float transitionSpeed;

    private float _pixelation;
    private bool _isTransitioning;
    private Material mosaicMaterial;

    public void StartTransition()
    {
        _isTransitioning = true;
        _pixelation = startingPixelation;
    }

    // Start is called before the first frame update
    private void Start()
    {
        mosaicMaterial = GetComponent<SpriteRenderer>().material;
    }

    void OnEnable()
    {
        _isTransitioning = false;
        _pixelation = startingPixelation;
    }

    // Update is called once per frame
    void Update()
    {
        if (_isTransitioning)
        {
            if (_pixelation <= endingPixelation)
            {
                _isTransitioning = false;
                //SceneManager.LoadScene("BattleScene");
            } else
            {
                _pixelation += (transitionSpeed * -1);
                mosaicMaterial.SetFloat("Pixelate", _pixelation);
            }
        }
    }
}
