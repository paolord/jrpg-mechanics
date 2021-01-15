using UnityEngine;
using UnityEngine.Rendering; // see Note-1
using UnityEngine.Events;
/**
 * Note-1
 * When using the URP you need to trigger the OnPostRender method through the RenderPipelineManager
 * Source: https://answers.unity.com/questions/1545858/onpostrender-is-not-called.html
 * */

public class CameraScreenGrab : MonoBehaviour
{
    private PlayerControls _playerControls;

    private bool _grab;

    [SerializeField] private Renderer _transitionOverlay;

    [SerializeField] public UnityEvent TriggerTransition;

    private void Awake()
    {
        _playerControls = new PlayerControls();
        _grab = false;
    }

    public void GrabThenTriggerTransition()
    {
        _grab = true;
    }

    private void OnPostRender()
    {
        if (_grab)
        {
            Texture2D texture = new Texture2D(Camera.main.pixelWidth, Camera.main.pixelHeight, TextureFormat.RGB24, false);
            //Debug.Log(Camera.main.pixelWidth + " " + Camera.main.pixelHeight);
            //Debug.Log(Camera.main.pixelRect);

            texture.ReadPixels(Camera.main.pixelRect, 0, 0, false);
            texture.Apply();

            if (_transitionOverlay != null)
            {
                //m_Display.material.mainTexture = texture;
                Vector2 v = new Vector2(0.5f, 0.5f);
                _transitionOverlay.GetComponent<SpriteRenderer>().sprite = Sprite.Create(texture, Camera.main.pixelRect, v, 16f);

                //m_Display.transform.localScale = new Vector3(10, 10, 0);
                TriggerTransition.Invoke();
            }

            _grab = false;
        }
    }

    // see Note-1 
    private void RenderPipelineManager_endCameraRendering(ScriptableRenderContext context, Camera camera)
    {
        //Debug.Log(camera.pixelRect);
        //Debug.Log(Screen.);
        OnPostRender();
    }

    private void OnEnable()
    {
        // see Note-1
        RenderPipelineManager.endCameraRendering += RenderPipelineManager_endCameraRendering;
        _playerControls.Enable();
    }

    private void OnDisable()
    {
        // see Note-1
        RenderPipelineManager.endCameraRendering += RenderPipelineManager_endCameraRendering;
        _playerControls.Disable();
    }
}