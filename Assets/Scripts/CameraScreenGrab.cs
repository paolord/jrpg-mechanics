using UnityEngine;
using UnityEngine.Rendering; // see Note-1

/**
 * Note-1
 * When using the URP you need to trigger the OnPostRender method through the RenderPipelineManager
 * Source: https://answers.unity.com/questions/1545858/onpostrender-is-not-called.html
 * */

public class CameraScreenGrab : MonoBehaviour
{
    private PlayerControls _playerControls;

    private bool _grab;

    [SerializeField] public Renderer m_Display;

    private void Awake()
    {
        _playerControls = new PlayerControls();
        _grab = false;
    }

    void Update()
    {
        Vector2 movementInput = _playerControls.Player.Move.ReadValue<Vector2>();
        
        if (movementInput.x != 0)
        {
            _grab = true;
        }
    }

    private void OnPostRender()
    {
        if (_grab)
        {
            Texture2D texture = new Texture2D(Camera.main.pixelWidth, Camera.main.pixelHeight, TextureFormat.RGB24, false);
            //Debug.Log(Screen.width + " " + Screen.height);
            //Debug.Log(Camera.main.pixelRect);

            texture.ReadPixels(Camera.main.pixelRect, 0, 0, false);
            texture.Apply();

            if (m_Display != null)
            {
                //m_Display.material.mainTexture = texture;

                m_Display.GetComponent<SpriteRenderer>().sprite = Sprite.Create(texture, Camera.main.pixelRect, new Vector2(0.5f, 0.5f), 16f);
                
                //m_Display.transform.localScale = new Vector3(10, 10, 0);
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