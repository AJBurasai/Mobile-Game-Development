using UnityEngine;

public class ClickSpawner : MonoBehaviour
{
    public GameObject frogPrefab;
    int score = 0;
    private Camera cam;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            score += 1;
            Instantiate(frogPrefab, (Vector2)mousePos, Quaternion.identity);
        }
    }
}
