using UnityEngine;

public class Level : MonoBehaviour
{
    public void Start() {
        CreatePipe(0f,0f);
    }
    private void CreatePipe(float height, float xPosition) {
        Transform pipeHead = Instantiate(GameAssets.GetInstance().pfPipeHead);
        Transform pipeBody = Instantiate(GameAssets.GetInstance().pfPipeBody);
    }
}
