using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GradeSpawner : MonoBehaviour {


    //public Texture2D[] images;
    public ObjectPool[] gradePools = new ObjectPool[6];
    public float spawnRateTime;

    [Header("Chances")]
    [Space(20f)]
    public float gradeAChance;
    public float gradeBChance;
    public float gradeCChance;
    public float gradeDChance;
    public float gradeFChance;

    private List<float> gradeChances = new List<float>();

    private float _time = 0f;



    private void Start() {
        _time = spawnRateTime;
        gradeChances.AddRange(new float[] { gradeAChance, gradeBChance, gradeCChance, gradeDChance, gradeFChance });
    }


    private void Update() {
        _time += Time.deltaTime;

        if (_time > spawnRateTime && !GameManager.instance.IsGameOver) {
            int randomCount = Random.Range(5, 10);
            spawnRateTime = (2.1f * randomCount) / GameManager.instance.GradeSpeed;
            Spawn(randomCount);

            _time = 0f;
        }
    }


    private void Spawn(int count) {
        for (int i = 0; i < count; i++) {
            Vector3 pos = new Vector3(GameManager.instance.ScreenRight.x + (i * 2f), 0f, 0f);
            SpawnWithChance(pos);
        }
    }


    private void SpawnWithChance(Vector3 pos) {
        int randomValue = Random.Range(0, 100);

        for (int i = 0; i < gradeChances.Count; i++) {
            if (randomValue <= gradeChances[i]) {
                SpawnObject(gradePools[i], pos);
                break;
            }

            randomValue -= (int)gradeChances[i];
        }

    }

    private void SpawnObject(ObjectPool pool, Vector3 pos) {
        GameObject grade = pool.GetObject();
        grade.transform.position = new Vector2(pos.x, Random.Range(1.5f, 5f));
        grade.GetComponent<Grade>().SetSpeed(GameManager.instance.GradeSpeed);
        grade.SetActive(true);
    }


}
