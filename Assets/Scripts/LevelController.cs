using UnityEngine;
using System.Collections;

public class LevelController : MonoBehaviour
{
    [Header("Level Stats")]
    public int height;

    [Header("Level Settings")]
    public float scrollSpeed;
    public float minYPosition;
    public float minXPosition;
    public float screenYLimit;
    private ObjectPooler pool;

    [Header("Platform Settings")]
    public float minXDistance;
    public float maxXDistance;
    public float heightDiffBetweenPlatforms;
    public float doublePlatformThreshold;


    private float prevPlatformX = 0f;
    private float heightOfPreviousPlatform;
    private void Start()
    {
        StartCoroutine(Timer());
        pool = GameObject.FindGameObjectWithTag("ObjectPooler").GetComponent<ObjectPooler>();
    }

    private void Update()
    {
        if (height - heightOfPreviousPlatform > heightDiffBetweenPlatforms)
        {
            CreatePlatform(pool.Pull(0));
        }
    }

    public IEnumerator Timer()
    {
        while (true)
        {
            height++;
            yield return new WaitForSeconds(1f / scrollSpeed);
        }
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    public void CreatePlatform(GameObject _platform)
    {
        heightOfPreviousPlatform = height;
        float _tempXDifference = Random.Range(minXDistance, maxXDistance);
        if (prevPlatformX > 0)
        {
            _tempXDifference = -_tempXDifference;
        }
        Vector2 _tempPosition = new Vector2(Mathf.Clamp((prevPlatformX + _tempXDifference), minXPosition, -minXPosition), screenYLimit);
        prevPlatformX = _tempPosition.x;
        pool.Pull(0).transform.position = _tempPosition;

        if (prevPlatformX < -doublePlatformThreshold || prevPlatformX > doublePlatformThreshold)
        {
            _tempPosition = new Vector2(Mathf.Clamp((prevPlatformX + Random.Range(minXDistance, maxXDistance)), minXPosition, -minXPosition), screenYLimit + (Random.Range((-heightDiffBetweenPlatforms * .5f), (heightDiffBetweenPlatforms * .5f))));
            pool.Pull(0).transform.position = _tempPosition;
        }
    }
}
