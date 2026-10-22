using UnityEngine;

public class FrogCounter : MonoBehaviour
{
    public FrogSpawner spawner;

    private bool wasOnScreen = true;

    void Update()
    {
        if (spawner == null || Camera.main == null)
            return;

        Vector3 viewPosition =
            Camera.main.WorldToViewportPoint(transform.position);

        bool isOnScreen =
            viewPosition.x >= 0f &&
            viewPosition.x <= 1f &&
            viewPosition.y >= 0f &&
            viewPosition.y <= 1f &&
            viewPosition.z > 0f;

        if (isOnScreen && !wasOnScreen)
        {
            spawner.ChangeScore(1);
        }
        else if (!isOnScreen && wasOnScreen)
        {
            spawner.ChangeScore(-1);
            Destroy(this.gameObject);
        }

        wasOnScreen = isOnScreen;
    }
}