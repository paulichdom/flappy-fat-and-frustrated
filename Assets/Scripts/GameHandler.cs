using UnityEngine;

public class GameHandler : MonoBehaviour {
    private void Start()
    {
      Debug.Log("GameHandler.Start");

       GameObject pipeGameObject = new GameObject("Pipe", typeof(SpriteRenderer));
       pipeGameObject.GetComponent<SpriteRenderer>().sprite = GameAssets.GetInstance().pipeHeadSprite;
    }
}
