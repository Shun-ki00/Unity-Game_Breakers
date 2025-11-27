using UnityEngine;

public class MultiDisplayInitializer : MonoBehaviour
{
    void Start()
    {
        // ディスプレイが2つ以上ある場合、2番目をアクティブ化
        if (Display.displays.Length > 1)
        {
            Display.displays[1].Activate();
        }
    }
}
