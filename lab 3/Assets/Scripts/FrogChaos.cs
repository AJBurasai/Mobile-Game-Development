using UnityEngine;

public class FrogChaos : MonoBehaviour
{
    public GameObject frogPrefab;
    public float cloneInterval = 2f;
    [Range(0f, 1f)]
    public float cloneChance = 0.5f;
    public int maxFrogs = 200;
    public float noGravityChance = 0.2f;
    public float spinSpeed = 180f;

    private float cloneTimer;
    private FrogCounter frogCounter;

    void Start()
    {
        frogCounter = GetComponent<FrogCounter>();

        if (frogPrefab == null)
        {
            Debug.LogError(
                "FrogChaos needs a Frog Prefab assigned!", this
            );
        }
        ApplyRandomGravity();
    }
    void ApplyRandomGravity()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();

        if (rb == null)
            return;
    } 

    void Update()
    {
        transform.Rotate(
            0f, 0f, spinSpeed * Time.deltaTime
        );

        transform.localScale = Vector3.one *
            Random.Range(0.5f, 1.5f);

        SpriteRenderer sprite =
            GetComponent<SpriteRenderer>();

        if (sprite != null)
        {
            sprite.color = new Color(
                Random.value,
                Random.value,
                Random.value
            );
        }

        cloneTimer += Time.deltaTime;

        if (cloneTimer >= cloneInterval)
        {
            cloneTimer = 0f;

            GameObject[] frogs =
                GameObject.FindGameObjectsWithTag("Frog");

            if (frogs.Length < maxFrogs &&
                Random.value < cloneChance)
            {
                SpawnClone();
            }
        }
    }

    void SpawnClone()
    {
        if (frogPrefab == null)
            return;

        Vector2 offset = Random.insideUnitCircle * 2f;

        GameObject clone = Instantiate(
            frogPrefab,
            transform.position + (Vector3)offset,
            Quaternion.identity
        );

        FrogCounter cloneCounter =
            clone.GetComponent<FrogCounter>();

        if (cloneCounter != null && frogCounter != null)
        {
            cloneCounter.spawner = frogCounter.spawner;
        }
    }
}