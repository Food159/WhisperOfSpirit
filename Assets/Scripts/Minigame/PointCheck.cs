using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PointCheck : MonoBehaviour
{
    [Header("UI")]
    public RectTransform pointer;
    public List<RectTransform> greenZonesInScene;
    private RectTransform currentGreenZone;
    [SerializeField] GameObject checkPanel;
    [SerializeField] GameObject menu;
    [SerializeField] TMP_Text successCountText;
    [SerializeField] TMP_Text failCountText;

    [Space]
    [Header("Config")]
    public float moveSpeed = 100f; // ความเร็ว pointer
    private float minAngle = 90f; // องศาสูงสุดที่ pointer ไปได้ทางด้านซ้าย
    private float maxAngle = -90f; // องศาสูงสุดที่ pointer ไปได้ทางด้านขวา
    public KeyCode keyInput = KeyCode.Space;
    private int successCount = 0;
    private int successRequired = 5;
    private int failCount = 0;
    private int maxFail = 3;

    private bool rotating = false; 
    public float currentAngle; // องศาปัจจุบันที่ pointer ชึ้
    private float direction = -1f; // กำหนดทิศทางของมุม -1f คือหมุนตามเข็มนาฬิกา 1f คือหมุนทวนเข็มนาฬิกา
    private Dictionary<string, Vector2> greenZoneSuccessAngles = new Dictionary<string, Vector2>() // เก็บชื่อของ prefabs และ ตำแหน่ง angle(min, max) ที่ success เอาไว้
    {
        { "Green1", new Vector2(-10f, 10f) },
        { "Green2", new Vector2(30f, 50f) },
        { "Green3", new Vector2(-75f, -60f) },
        { "Green4", new Vector2(80f, 90f) },
        { "Green5", new Vector2(-45f, -30f) },
    };
    private List<IPausable> pauseable = new List<IPausable>();

    [SerializeField] private BossCheck bosscheck;
    public bool isSuccess = false;
    public bool pauseGame = true;
    private void Awake()
    {
        MonoBehaviour[] behaviours = FindObjectsOfType<MonoBehaviour>(true);
        foreach (var b in behaviours)
        {
            if (b is IPausable)
            {
                pauseable.Add((IPausable)b);
            }
        }
    }
    private void Update()
    {
        if (!rotating) // ถ้าไม่ได้ rotate ก็ return
            return;

        currentAngle += direction * moveSpeed * Time.unscaledDeltaTime; // คำนวณมุม โดยการใช้ direction * speed เช่น direction = -1f * 100 = -100(คือไปตามเข็มนาฬิกา) แล้วเอาไปคูณกับ Time.deltatime

        successCountText.text = $"Success : {successCount}/{successRequired}";
        failCountText.text = $"Fail {failCount}/{maxFail}";

        if (currentAngle >= minAngle) // ถ้า pointer ไปถึง minangle ให้หมุนไปที่ maxangle
        {
            currentAngle = minAngle;
            direction = -1f;
        }
        else if(currentAngle <= maxAngle) // ถ้า pointer ไปถึง maxangle ให้หมุนไปที่ minangle
        {
            currentAngle = maxAngle;
            direction = 1f;
        }
        pointer.transform.eulerAngles = new Vector3(0, 0, currentAngle); // หมุน pointer ตามค่า currentangle

        if(Input.GetKeyDown(keyInput))
        {
            CheckResult();
        }
    }
    public void StartCheck(bool resetPointer = false)
    {
        if (pauseGame)
        {
            Time.timeScale = 0f;
            menu.SetActive(false);
        }

        checkPanel.SetActive(true);
        rotating = true; // ให้ bool rotating ทำงาน
        if(resetPointer)
        {
            currentAngle = minAngle; // ให้ pointer เริ่มที่ minAngle
            direction = 1f; // เริ่มหมุนตามเข็มนาฬิกา
            pointer.transform.eulerAngles = new Vector3(0, 0, currentAngle);
        }
        //currentAngle = minAngle; // ให้ pointer เริ่มที่ minAngle
        //direction = 1f; // เริ่มหมุนตามเข็มนาฬิกา
        //pointer.transform.eulerAngles = new Vector3(0, 0, maxAngle);

        foreach (var zone in greenZonesInScene) // ปิด greenzone ทุกอัน
        {
            zone.gameObject.SetActive(false);
        }
        int index = Random.Range(0, greenZonesInScene.Count); // สุ่มเปิด greenzone ใน List มา 1 อัน
        currentGreenZone = greenZonesInScene[index];
        currentGreenZone.gameObject.SetActive(true);
        foreach (var p in pauseable)
        {
            p.Pause();
        }    
    }
    void CheckResult()
    {
        if (pauseGame)
        {
            Time.timeScale = 1f;
            menu.SetActive(true);
        }
        rotating = false;
        float angle = pointer.eulerAngles.z;
        if (angle > 180f) angle -= 360f;

        bool success = false;

        if (currentGreenZone != null)
        {
            string zoneName = currentGreenZone.name;

            if (greenZoneSuccessAngles.ContainsKey(zoneName))
            {
                Vector2 range = greenZoneSuccessAngles[zoneName];
                float minRange = range.x;
                float maxRange = range.y;

                Debug.Log($"Pointer Angle: {angle} | {zoneName} | Range: {minRange} ถึง {maxRange}");
                Debug.Log($"{zoneName} ใน Dictionary!");

                if (angle >= minRange && angle <= maxRange)
                {
                    success = true;
                }
            }
        }

        if (success)
        {
            SoundManager.instance.PlaySfx(SoundManager.instance.minigameCorrect);
            successCount++;
            Debug.Log($"Success {successCount}/{successRequired}");
            if(successCount >= successRequired)
            {
                SoundManager.instance.PlaySfx(SoundManager.instance.minigameComplete);
                isSuccess = true;
                EndCheck();
            }
            else
            {
                StartCheck(false);
            }
        }
        else
        {
            SoundManager.instance.PlaySfx(SoundManager.instance.minigameWrong);
            failCount++;
            Debug.Log($"Fail {failCount}/{maxFail}");
            if (failCount >= maxFail)
            {
                SoundManager.instance.PlaySfx(SoundManager.instance.minigameFail);
                successCount = 0;
                isSuccess = false;
                Debug.Log("Fail!");
                EndCheck();
            }
            else
            {
                StartCheck(false);
            }
        }

    }
    private void EndCheck()
    {
        gameObject.SetActive(false);
        foreach (var p in pauseable)
        {
            p.Resume();
        }
    }
}
