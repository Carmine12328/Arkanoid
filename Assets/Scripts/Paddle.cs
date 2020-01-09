using UnityEngine;

public class Paddle : MonoBehaviour
{
    private Camera mainCamera;
    private float paddleInitialy;
    private float defaultPaddleWidthInPixels = 200;
    private float defaultLeftClamp = 140;
    private float defaultRighttClamp = 400;
    private SpriteRenderer sr;

    // Start is called before the first frame update
    private void Start()
    {
        mainCamera = FindObjectOfType<Camera>();
        paddleInitialy = this.transform.position.y;
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    private void Update()
    {
        PaddleMovement();
    }

    public void PaddleMovement()
    {
        float paddleShift = (defaultPaddleWidthInPixels - ((defaultPaddleWidthInPixels / 2) * this.sr.size.x)) / 2;
        float leftClamp = defaultLeftClamp - paddleShift;
        float rightClamp = defaultRighttClamp + paddleShift;
        float mousePositionPixels = Mathf.Clamp(Input.mousePosition.x, leftClamp, rightClamp);
        var mousePosition = mainCamera.ScreenToWorldPoint(new Vector3(mousePositionPixels, 0, 0)).x;
        this.transform.position = new Vector3(mousePosition, paddleInitialy, 0);
    }
}