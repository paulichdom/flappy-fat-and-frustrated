using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour {
    
    private const float CameraOrthoSize = 50f;
    private const float PipeWidth = 7.8f;
    private const float PipeHeadHeight = 3.75f;
    private const float PipeMoveSpeed = 3f;

    private List<Transform> pipeList;

    private void Awake() {
        pipeList = new List<Transform>();
    }

    public void Start() {
        CreateGapPipes(50f, 20f, 20f);
    }

    public void Update() {
        HandlePipeMovement();
    }

    private void HandlePipeMovement() {
        foreach (Transform pipeTransform in pipeList) {
            pipeTransform.position += new Vector3(-1, 0, 0) * (PipeMoveSpeed * Time.deltaTime);
        }
    }

    private void CreateGapPipes(float gapY, float gapSize, float xPosition) {
        CreatePipe(gapY - gapSize * 0.5f, xPosition, true);
        CreatePipe(CameraOrthoSize * 2f - gapY - gapSize * 0.5f, xPosition, false);
    }
    
    private void CreatePipe(float height, float xPosition, bool createBottom) {
        // Set up Pipe Head
        Transform pipeHead = Instantiate(GameAssets.GetInstance().pfPipeHead);
        float pipeHeadYPosition;
        
        if (createBottom) {
            pipeHeadYPosition = -CameraOrthoSize + height - PipeHeadHeight * 0.5f;
        }
        else {
            pipeHeadYPosition = +CameraOrthoSize - height + PipeHeadHeight * 0.5f;
        }
        
        pipeHead.position = new Vector3(xPosition, pipeHeadYPosition);
        pipeList.Add(pipeHead);
        
        // Set up Pipe Body
        Transform pipeBody = Instantiate(GameAssets.GetInstance().pfPipeBody);
        float pipeBodyYPosition;
        
        if (createBottom) {
            pipeBodyYPosition = -CameraOrthoSize;
        }
        else {
            pipeBodyYPosition = +CameraOrthoSize;
            pipeBody.localScale = new Vector3(1, -1, 1);
        }
        
        pipeBody.position = new Vector3(xPosition, pipeBodyYPosition);
        pipeList.Add(pipeBody);
        
        SpriteRenderer pipeBodySpriteRenderer = pipeBody.GetComponent<SpriteRenderer>();
        pipeBodySpriteRenderer.size = new Vector2(PipeWidth, height);
        
        BoxCollider2D pipeBodyBoxCollider = pipeBody.GetComponent<BoxCollider2D>();
        pipeBodyBoxCollider.size = new Vector2(PipeWidth, height);
        pipeBodyBoxCollider.offset = new Vector2(0f, height * 0.5f);
    }
}
