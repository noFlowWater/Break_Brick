using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;


public class DataManager : MonoBehaviour
{
    static GameObject container;


    static DataManager instance;
    public static DataManager Instance
    {
        get
        {
            if (!instance)
            {
                container = new GameObject();
                container.name = "DataMAnager";
                instance = container.AddComponent(typeof(DataManager)) as DataManager;
                DontDestroyOnLoad(container);
            }
            return instance;
        }
    }

    string GameDataFileName = "GameData.json";

    public GameData data = new GameData();

    // 불러오기
    public void LoadGameData()
    {
        GameManager.instance.loading = true;
        string filePath = Application.persistentDataPath + "/" + GameDataFileName;

        // 저장된 게임이 있을시
        if (File.Exists(filePath))
        {
            // 저장된 파일 읽어오고 Json을 클래스 형식으로 전환해서 할당
            string FromJsonData = File.ReadAllText(filePath);
            data = JsonUtility.FromJson<GameData>(FromJsonData);
            Spawner spawner = GameObject.Find("Spawner").GetComponent<Spawner>();


            GameManager.instance.level = data.level;
            GameManager.instance.score = data.score;
            GameManager.instance.ballNumber = data.ballNumber;
            GameManager.instance.color = data.color;



            // for (int i = 0; i < data.bricks.Count; ++i)
            if (data.bricks != null)
            {
                foreach (BrickData brickData in data.bricks)
                {
                    if (brickData.type == 0 || brickData.life == 0) { break; }
                    GameObject brick = spawner.Get_(brickData.color, brickData.type);
                    brick.GetComponent<Brick>().life = brickData.life;
                    brick.GetComponent<Brick>().posX = brickData.posX;
                    brick.GetComponent<Brick>().posY = brickData.posY;
                    brick.GetComponent<Brick>().isMove = brickData.isMove;
                    if (brick.GetComponent<Brick>() is NormalBrick)
                    {
                        brick.GetComponent<NormalBrick>().maxLife = brickData.maxLife;
                    }
                    brick.name = "(" + brickData.posX + ", " + brickData.posY + ")";
                }
            }
            GameManager.instance.isPlayerTurn = true;
            print("불러오기 완료");

        }

        GameManager.instance.loading = false;
    }

    // 저장하기
    public void SaveGameData()
    {
        // 데이터 저장
        data.level = GameManager.instance.level;
        data.score = GameManager.instance.score;

        data.ballNumber = GameManager.instance.ballNumber;
        data.color = GameManager.instance.color;

        // (-9.5, -5.5) ~ (9.5, 5.5)
        // data.bricks = new ArrayList();
        data.bricks = new BrickData[20 * 12];

        int i = 0;
        for (float x = -9.5f; x <= 9.5; ++x)
        {
            for (float y = -5.5f; y <= 5.5f; ++y)
            {
                GameObject brick = GameObject.Find("(" + x + ", " + y + ")");
                if (brick != null)
                {
                    BrickData brickData = new BrickData();
                    if (brick.GetComponent<Brick>() is NormalBrick)
                    {
                        // print("normal");
                        brickData.type = 1;
                        brickData.maxLife = brick.GetComponent<NormalBrick>().maxLife;
                    }
                    else if (brick.GetComponent<Brick>() is BallNumberIncreaseBrick)
                    {
                        brickData.type = 2;
                    }
                    //red
                    if (brick.layer == 6)
                    {
                        brickData.color = 1;
                    }
                    //blue
                    else if (brick.layer == 7)
                    {
                        brickData.color = 0;
                        // print("blue");
                    }

                    brickData.life = brick.GetComponent<Brick>().life;
                    brickData.posX = brick.GetComponent<Brick>().posX;
                    brickData.posY = brick.GetComponent<Brick>().posY;
                    brickData.isMove = brick.GetComponent<Brick>().isMove;

                    // data.bricks.Add(brickData);
                    data.bricks[i++] = brickData;
                }
            }
        }

        // 클래스를 Json 형식으로 전환 (true: 가독성 좋게 작성)
        string ToJsonData = JsonUtility.ToJson(data, true);
        string filePath = Application.persistentDataPath + "/" + GameDataFileName;
        // print(filePath);

        // 이미 저장된 파일이 있다면 덮어쓰고, 없다면 새로 만들어서 저장
        File.WriteAllText(filePath, ToJsonData);

        print("저장완료");
    }

    public void DataDelete()
    {
        string filePath = Application.persistentDataPath + "/" + GameDataFileName;
        if (File.Exists(filePath))
        {
            File.Delete(filePath);
            print("데이터 삭제");
        }
    }
}
