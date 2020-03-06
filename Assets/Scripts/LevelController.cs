using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class LevelController : MonoBehaviour
{
    [Header("Level Stats")]
    public int height;
    public float prevPlatformX;
    public float heightOfPreviousPlatform;

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
    [Range(0, 100)]
    public int collectibleProbability;
    public float collectibleYOffset;


    public List<GameObject> player;
    private void Start()
    {
        StartCoroutine(Timer());
        pool = GameObject.FindGameObjectWithTag("ObjectPooler").GetComponent<ObjectPooler>();
    }

    private void Update()
    {
        //print(height);
        Restart();
        if (height - heightOfPreviousPlatform > heightDiffBetweenPlatforms)
        {
            CreatePlatform(pool.Pull(0));
        }
    }

    public IEnumerator Timer()
    {
        while (true)
        {
            
            if (scrollSpeed>0)
            {
                height++;
                yield return new WaitForSeconds(1f / scrollSpeed);
            }
            else
            {
                yield return new WaitForEndOfFrame();
            }
            
        }
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

        if (prevPlatformX < -doublePlatformThreshold)
        {
            _tempPosition = new Vector2(Mathf.Clamp((prevPlatformX + Random.Range(minXDistance, maxXDistance)), minXPosition, -minXPosition), screenYLimit + (Random.Range((-heightDiffBetweenPlatforms * .5f), (heightDiffBetweenPlatforms * .5f))));
            pool.Pull(0).transform.position = _tempPosition;
        }
        else if (prevPlatformX > doublePlatformThreshold)
        {
            _tempPosition = new Vector2(Mathf.Clamp((prevPlatformX - Random.Range(minXDistance, maxXDistance)), minXPosition, -minXPosition), screenYLimit + (Random.Range((-heightDiffBetweenPlatforms * .5f), (heightDiffBetweenPlatforms * .5f))));
            pool.Pull(0).transform.position = _tempPosition;
        }

        if (Random.Range(0,100)<=collectibleProbability)
        {
            pool.Pull(2).transform.position = new Vector2(_tempPosition.x, _tempPosition.y+collectibleYOffset);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            scrollSpeed = .3f;
            GetComponent<BoxCollider2D>().enabled = false;
        }
    }

    private void Restart()
    {
        for (int i = player.Count - 1; i >= 0; i--)
        {
            if (player[i] == null)
            {
                player.Remove(player[i]);
            }
        }
        //player = GameObject.FindGameObjectsWithTag("Player");
        if (player.Count <= 0)
        {
            //Go to next scene
           SceneManager.LoadScene(2, LoadSceneMode.Single);
        }
    }
}
