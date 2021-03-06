using System.Collections;
using System.Collections.Generic;
using System.IO;
using AL.ALLog;
using UnityEngine;

public class MapLoad : MonoBehaviour
{
    Vector2 basicPos;

    [SerializeField]
    private string _testFileName;

    [SerializeField]
    GameObject tileObj;

    int posX = 0, posY = 0;

    void Start()
    {
        basicPos = new Vector2(0, -710);
        loadMap(string.Format("MapData/{0}", _testFileName));
        //createMap(); createMap(); createMap(); createMap();
    }

    // void Update()
    // {
    //     transform.position -= new Vector3(1.5f * Time.deltaTime, 0);
    // }

    /// <summary>
    /// createMap
    /// </summary>
    /// <desc>
    /// 맵 불러올일 있을때 이거 실행시키면됨
    /// </desc>
    public void createMap()
    {
        Debug.Log(loadMap("MapData/map" + Random.Range(0, 4)));
    }

    /// <summary>
    /// Load Map
    /// </summary>
    /// <param name="fileName"></param>
    /// <returns></returns>
    string loadMap(string fileName)
    {
        TextAsset textAsset = Resources.Load(fileName) as TextAsset;
        string str = textAsset.text;

        basicPos = new Vector2(posX * 300, posY * 300 - 710);

        string[] spString = str.Split('\n');
        string[] code = null;

        int maxCount = 0;
        for (int i = 0; i < spString.Length; i++)
        {
            int tempCount = 0;
            code = spString[i].Split(',');
            for (int j = 0; j < code.Length - 1; j++)
            {
                string s = code[j].Replace(" ", "");
                s = code[j].Replace("\n", "");

                if (s[0].Equals('\n') || s[0].Equals("") || s[0].Equals("\n"))
                    break;
                if (!s.Equals("0"))
                {
                    if (s.Equals("@"))
                        MonsterManager.mm.createMonster(MTYPE.MHUMAN, basicPos + new Vector2(300 * j, 300 * i));
                    else if (s.Equals("#"))
                        MonsterManager.mm.createMonster(MTYPE.MUMBRELLA, basicPos + new Vector2(300 * j, 300 * i));                    
                    else if (s.Equals("$"))
                        MonsterManager.mm.createMonster(MTYPE.MJUMP, basicPos + new Vector2(300 * j, 300 * i));                    
                    else if (s.Equals("%"))
                        MonsterManager.mm.createMonster(MTYPE.MBIRD, basicPos + new Vector2(300 * j, 300 * i));                    
                    else
                    {
                        GameObject obj = Instantiate(tileObj) as GameObject;
                        obj.GetComponent<UnityEngine.UI.Image>().sprite = Resources.Load<Sprite>("Tile/Harlem/" + s);
                        //obj.transform.parent = transform;
                        obj.transform.SetParent(transform);
                        obj.transform.localPosition = basicPos + new Vector2(300 * j, 300 * i);
                        obj.transform.localScale = Vector2.one;

                        if (s.Equals("4"))
                            obj.SendMessage("typeChange", TILE_TYPE.CURVE_RIGHT);
                        else if (s.Equals("3"))
                            obj.SendMessage("typeChange", TILE_TYPE.CURVE_LEFT);
                        else if (s.Equals("1"))
                            obj.SendMessage("typeChange", TILE_TYPE.CURVE_RIGHT2);
                        else if (s.Equals("2"))
                            obj.SendMessage("typeChange", TILE_TYPE.CURVE_LEFT2);
                        else if (s.Equals("5") || s.Equals("8") || s.Equals("y"))
                            obj.SendMessage("directionChange", TILE_DIRECTION.RIGHT);
                        else if (s.Equals("6") || s.Equals("x") || s.Equals("z"))
                            obj.SendMessage("directionChange", TILE_DIRECTION.LEFT);
                    }
                }

                tempCount++;
            }
            if (maxCount < tempCount)
                maxCount = tempCount;
        }
        ALLog.Log(string.Format("Max Count : {0}", maxCount));
        posX += maxCount - 1;
        posY += spString.Length - 1;

        return str;
    }
    
    // 더이상 쓰지 않음 X
    string pathForDocuments(string fileName)
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            string path = Application.persistentDataPath;
            path = "jar:file://" + Application.dataPath + "!/assets/";
            //path = path.Substring(0, path.LastIndexOf('/'));
            return Path.Combine(path, fileName);
        }
        else
        {
            string path = Application.dataPath;
            path = path.Substring(0, path.LastIndexOf('/'));
            return Path.Combine(path, fileName);
        }
    }
}
