using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public UserData userData;
    public PopupBank popupBank;

    private string savePath;

    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        savePath = Path.Combine(Application.persistentDataPath, "userdata.json");
    }

    // Update is called once per frame
    private void Start()
    {
        LoadUserData(); // 데이터 로드

        if ( popupBank != null)
        {
            popupBank.Refresh();
        }

        if (popupBank != null)
        {
            popupBank.Refresh();
        }
    }

    //저장
    public void SaveUserData()
    {
        string json = JsonUtility.ToJson(userData, true); //저장
        File.WriteAllText(savePath, json);
        //Debug.Log("저장 경로: " + Application.persistentDataPath);
    }

    //로드
    public void LoadUserData()
    {
        if (File.Exists(savePath))
        {
            string json = File.ReadAllText(savePath);
            userData = JsonUtility.FromJson<UserData>(json);
            Debug.Log("로드 완료: " + json);
        }
        else
        {
            Debug.Log("저장된 파일이 없어 기본값 사용");
            userData = new UserData("조경표", 100000, 50000);

            SaveUserData();
        }
    }
}
