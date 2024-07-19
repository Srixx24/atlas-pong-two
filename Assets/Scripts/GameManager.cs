using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace ZPong
{

    public class GameManager : MonoBehaviour
    {
        [SerializeField] private float startDelay = 3f;
        [SerializeField] private GameObject ballPrefab;
        [SerializeField] private GameObject canvasParent;
        [SerializeField] private Vector3 entryPoint = new Vector3(0f, 920f, 0f);

        public Ball activeBall;

        public static GameManager Instance { get; private set; }

        private Goal[] goals;
        public Image explosionImage;
        private float explosionDuration = 20f;

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(this);
            }
            else
            {
                Instance = this;
            }

            goals = new Goal[2];
        }

        void SetGame()
        {
            // Clones the ball from the prefab at the start of the game
            if (activeBall == null)
            {
                activeBall = Instantiate(ballPrefab, entryPoint, this.transform.rotation, canvasParent.transform)
                    .GetComponent<Ball>();
                activeBall.GetComponent<RectTransform>().anchoredPosition = new Vector2(0f, 920f);
            }

            StartCoroutine(FallIntoBall());
        }

        IEnumerator FallIntoBall()
        {
            activeBall.SetBallActive(false);
            float fallDuration = 1f;
            float startTime = Time.time;

            while (Time.time - startTime < fallDuration)
            {
                float t = (Time.time - startTime) / fallDuration;
                activeBall.transform.position = Vector3.Lerp(entryPoint, Vector3.zero, t);
                activeBall.GetComponent<RectTransform>().anchoredPosition = new Vector2(0f, Mathf.Lerp(920f, 0f, t));
                yield return null;
            }

            activeBall.transform.position = Vector3.zero;
            activeBall.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
            activeBall.SetBallActive(true);

            // Display the explosion effect
            if (explosionImage != null)
            {
                explosionImage.gameObject.SetActive(true);
                yield return new WaitForSeconds(explosionDuration);
                explosionImage.gameObject.SetActive(false);
            }
        }

        void StartGame()
        {
            //Debug.Log("Starting game!");
            activeBall.SetBallActive(true);
        }

        public void Reset()
        {
            StartCoroutine(StartTimer());
        }

        private void Start()
        {
            Reset();
        }

        IEnumerator StartTimer()
        {
            SetGame();
            yield return new WaitForSeconds(startDelay);

            SetBounds();

            StartGame();
        }

        void SetBounds()
        {
            activeBall.SetHeightBounds();
            foreach (var g in goals)
            {
                g.SetHeightBounds();
            }
        }

        public void ResetBall()
        {
            StartCoroutine(ResetBallCoroutine());
        }

        private IEnumerator ResetBallCoroutine()
        {
            // Simply reset the ball's position and state instead of destroying it
            activeBall.transform.position = Vector3.zero;
            activeBall.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
            activeBall.SetBallActive(false);

            yield return null;

            StartCoroutine(StartTimer());
        }

        public void SetGoalObj(Goal g)
        {
            if (goals[0])
            {
                goals[1] = g;
            }
            else
            {
                goals[0] = g;
            }
        }
    }
}
