using UnityEngine;

public class BackgroundSpawner : MonoBehaviour
{
    public GameObject backgroundPrefab;
    public Transform parentTransform;
    public float spawnInterval = 10f;
    public float nextSpawnX = 20f;
    private float timer = 0f;

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= spawnInterval)
        {
            Vector3 spawnPos = new Vector3(nextSpawnX, 0f, 0f);
            GameObject newBg = Instantiate(backgroundPrefab, parentTransform);

            RectTransform rect = newBg.GetComponent<RectTransform>();
            if (rect != null)
                rect.anchoredPosition = new Vector2(spawnPos.x, spawnPos.y);
            else
                newBg.transform.localPosition = spawnPos;

            newBg.transform.SetSiblingIndex(0);

            timer = 0f;
        }
    }
}