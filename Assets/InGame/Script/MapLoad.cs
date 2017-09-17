using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class MapLoad : MonoBehaviour
{
    Vector2 basicPos;
    [SerializeField]
    GameObject tileObj;

    void Start()
    {
        basicPos = new Vector2(0, -710);
        Debug.Log(readStringFromFile("MapData/map0"));
    }

    string readStringFromFile(string fileName)
    {
        TextAsset textAsset = Resources.Load(fileName) as TextAsset;
        string str = textAsset.text;

        string[] spString = str.Split('\n');

        for (int i = 0; i < spString.Length; i++)
        {
            string[] code = spString[i].Split(',');
            for (int j = 0; j < code.Length -1; j++)
            {
                string s = code[j].Replace(" ", "");
                s = code[j].Replace("\n", "");
                
                if (s[0].Equals('\n') || s[0].Equals("") || s[0].Equals("\n"))
                    break;
                if (!s.Equals("0"))
                {
                    GameObject obj = Instantiate(tileObj) as GameObject;
                    obj.GetComponent<UnityEngine.UI.Image>().sprite = Resources.Load<Sprite>("Tile/Harlem/" + s);
                    obj.transform.parent = transform;
                    obj.transform.localPosition = basicPos + new Vector2(300 * j, 300 * i);
                    obj.transform.localScale = Vector2.one;
                }
            }
        }
        return str;
    }

    // X
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
