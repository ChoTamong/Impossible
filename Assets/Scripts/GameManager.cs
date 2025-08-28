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
            savePath = Path.Combine(Application.persistentDataPath, "userdata.json");
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    private void Start()
    {
        userData = new UserData("조경표", 100000, 50000);

        if (popupBank != null)
        {
            popupBank.Refresh();
        }
    }
    public void SaveUserData()
    {
        string json = JsonUtility.ToJson(userData, true); //저장
        File.WriteAllText(savePath, json);
        Debug.Log("저장 경로: " + Application.persistentDataPath);
    }

    //JSON 로드
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
        }
    }
}
