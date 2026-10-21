using UnityEngine;

public class FrogChaos : MonoBehaviour
{
    public GameObject frogPrefab;
    public float cloneInterval = 2f;
    [Range(0f, 1f)]
    public float cloneChance = 0.5f;
    public int maxFrogs = 200;
    public float noGravityChance = 0.2f;
    public int uselessCalculations = 500;
    public float spinSpeed = 180f;

    public bool spamConsole = true;
    public float logInterval = 1f;

    private float cloneTimer;
    private float logTimer;
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

        if (Random.value < noGravityChance)
        {
            rb.gravityScale = 0f;
            Debug.Log(name + " has escaped gravity!");
        }
        else
        {
            rb.gravityScale = 1f;
        }
    } 

    void Update()
    {
        float result = 0f;

        for (int i = 0; i < uselessCalculations; i++)
        {
            result += Mathf.Sin(i * Time.deltaTime);
            result *= 0.99999f;
        }

        transform.rotation = Quaternion.Euler(
            0f, 0f, result % 360f
        );

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

        logTimer += Time.deltaTime;

        if (spamConsole && logTimer >= logInterval)
        {
            logTimer = 0f;

            Debug.Log(
                name + " is still calculating nonsense. " +
                "Frogs found: " +
                GameObject.FindGameObjectsWithTag("Frog").Length
            );
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

        Debug.Log("ANOTHER FROG HAS BEEN CREATED.");
    }
}