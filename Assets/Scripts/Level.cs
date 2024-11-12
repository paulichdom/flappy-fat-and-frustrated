using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour {
    
    private const float CameraOrthoSize = 50f;
    private const float PipeWidth = 7.8f;
    private const float PipeHeadHeight = 3.75f;
    private const float PipeMoveSpeed = 3f;
    private const float PipeDestroyXPosition = -100f;
    private const float PipeSpawnXPosition = +100f;
    private const float GapSize = 50f;

    private List<Pipe> _pipeList;
    private float _pipeSpawnTimer;
    private float _pipeSpawnTimerMax;
    
    private void Awake() {
        _pipeList = new List<Pipe>();
        _pipeSpawnTimerMax = 1f;
    }

    public void Start() {
        // CreateGapPipes(50f, 20f, 20f);
    }

    public void Update() {
        HandlePipeMovement();
        HandlePipeSpawning();
    }

    // ReSharper disable Unity.PerformanceAnalysis
    private void HandlePipeSpawning() {
        _pipeSpawnTimer -= Time.deltaTime;
        if (_pipeSpawnTimer < 0) {
            _pipeSpawnTimer += _pipeSpawnTimerMax;

            const float heightEdgeLimit = 10f;
            const float minHeight = GapSize * 0.5f + heightEdgeLimit;
            const float totalHeight = CameraOrthoSize + 2f;
            const float maxHeight = totalHeight - GapSize * 0.5f - heightEdgeLimit;
            
            float height = Random.Range(minHeight, maxHeight);
            CreateGapPipes(height, GapSize, PipeSpawnXPosition);
        }
    }

    private void HandlePipeMovement() {
        for (int i = 0; i < _pipeList.Count; i++) {
            Pipe pipe = _pipeList[i];
            pipe.Move();
            
            if (pipe.GetXPosition() < PipeDestroyXPosition) {
                pipe.DestroySelf();
                _pipeList.Remove(pipe);
                i--;
            }
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
        _pipeList.Add(pipe);
    }

    private class Pipe {
        private readonly Transform _pipeHeadTransform;
        private readonly Transform _pipeBodyTransform;

        public Pipe(Transform pipeHeadTransform, Transform pipeBodyTransform) {
            _pipeHeadTransform = pipeHeadTransform;
            _pipeBodyTransform = pipeBodyTransform;
        }

        public void Move() {
            _pipeHeadTransform.position += new Vector3(-1, 0, 0) * (PipeMoveSpeed * Time.deltaTime);
            _pipeBodyTransform.position += new Vector3(-1, 0, 0) * (PipeMoveSpeed * Time.deltaTime);
        }

        public float GetXPosition() {
            return _pipeHeadTransform.position.x;
        }

        public void DestroySelf() {
            Destroy(_pipeHeadTransform.gameObject);
            Destroy(_pipeBodyTransform.gameObject);
        }
    }
};
