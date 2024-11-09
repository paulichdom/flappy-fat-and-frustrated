using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour {
    
    private const float CameraOrthoSize = 50f;
    private const float PipeWidth = 7.8f;
    private const float PipeHeadHeight = 3.75f;
    private const float PipeMoveSpeed = 3f;

    private List<Pipe> pipeList;

    private void Awake() {
        pipeList = new List<Pipe>();
    }

    public void Start() {
        CreateGapPipes(50f, 20f, 20f);
    }

    public void Update() {
        HandlePipeMovement();
    }

    private void HandlePipeMovement() {
        foreach (Pipe pipeTransform in pipeList) {
            pipeTransform.Move();
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
        
        SpriteRenderer pipeBodySpriteRenderer = pipeBody.GetComponent<SpriteRenderer>();
        pipeBodySpriteRenderer.size = new Vector2(PipeWidth, height);
        
        BoxCollider2D pipeBodyBoxCollider = pipeBody.GetComponent<BoxCollider2D>();
        pipeBodyBoxCollider.size = new Vector2(PipeWidth, height);
        pipeBodyBoxCollider.offset = new Vector2(0f, height * 0.5f);
        
        Pipe pipe = new Pipe(pipeHead, pipeBody);
        pipeList.Add(pipe);
    }

    private class Pipe {
        private readonly Transform pipeHeadTransform;
        private readonly Transform pipeBodyTransform;

        public Pipe(Transform pipeHeadTransform, Transform pipeBodyTransform) {
            this.pipeHeadTransform = pipeHeadTransform;
            this.pipeBodyTransform = pipeBodyTransform;
        }

        public void Move() {
            pipeHeadTransform.position += new Vector3(-1, 0, 0) * PipeMoveSpeed * Time.deltaTime;
            pipeBodyTransform.position += new Vector3(-1, 0, 0) * PipeMoveSpeed * Time.deltaTime;
        }
    }
};
