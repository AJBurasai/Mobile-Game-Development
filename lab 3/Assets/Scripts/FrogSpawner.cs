using UnityEngine;

public class FrogSpawner : MonoBehaviour
{
    public GameObject frogPrefab;
    public int score = 0;

    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        Vector2 tapPosition = Vector2.zero;
        bool tapped = false;

        // Phone touchscreen
        if (Input.touchCount > 0 &&
            Input.GetTouch(0).phase == TouchPhase.Began)
        {
            tapPosition = Input.GetTouch(0).position;
            tapped = true;
        }

        // Mouse clicks for testing in the Unity Editor
        if (Input.GetMouseButtonDown(0))
        {
            tapPosition = Input.mousePosition;
            tapped = true;
        }

        if (tapped)
        {
            Vector3 screenPosition = new Vector3(
                tapPosition.x,
                tapPosition.y,
                -mainCamera.transform.position.z
            );

            Vector3 worldPosition =
                mainCamera.ScreenToWorldPoint(screenPosition);

            worldPosition.z = 0f;

            GameObject frog = Instantiate(
                frogPrefab,
                worldPosition,
                Quaternion.identity
            );

            frog.GetComponent<FrogCounter>().spawner = this;

            score++;
        }
    }

    public void ChangeScore(int amount)
    {
        score += amount;
        Debug.Log("Score: " + score);
    }
}