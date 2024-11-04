using UnityEngine;

public class Level : MonoBehaviour {
    
    private const float CameraOrthoSize = 50f;
    private const float PipeWidth = 7.8f;
    private const float PipeHeadHeight = 3.75f;
    
    public void Start() {
        CreatePipe(50f,0f);
        CreatePipe(40f,20f);
        CreatePipe(30f,40f);
        CreatePipe(20f,60f);
    }
    
    private void CreatePipe(float height, float xPosition) {
        // Set up Pipe Head
        Transform pipeHead = Instantiate(GameAssets.GetInstance().pfPipeHead);
        pipeHead.position = new Vector3(xPosition, -CameraOrthoSize + height - PipeHeadHeight * 0.5f);
        
        // Set up Pipe Body
        Transform pipeBody = Instantiate(GameAssets.GetInstance().pfPipeBody);
        pipeBody.position = new Vector3(xPosition, -CameraOrthoSize);
        
        SpriteRenderer pipeBodySpriteRenderer = pipeBody.GetComponent<SpriteRenderer>();
        pipeBodySpriteRenderer.size = new Vector2(PipeWidth, height);
        
        BoxCollider2D pipeBodyBoxCollider = pipeBody.GetComponent<BoxCollider2D>();
        pipeBodyBoxCollider.size = new Vector2(PipeWidth, height);
        pipeBodyBoxCollider.offset = new Vector2(0f, height * 0.5f);
    }
}
