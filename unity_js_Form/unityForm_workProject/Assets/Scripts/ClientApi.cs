using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class ClientApi : MonoBehaviour
{
    public string url;
    public EnemyViewController enemyViewController;

    public string getUrl = "localhost:3000/enemy";
    public string postUrl = "localhost:3000/enemy/create";

    void Start()
    {
        var enemy = new Enemy()
        {
            id = 100,
            name = "Balrog",
            health = 1000,
            attack = 2500
        };

        //StartCoroutine(Get(url));
        //StartCoroutine(Post(url, enemy));

        
    }

    public void GetRequest(string url, System.Action<string> callback)
    {
        StartCoroutine(Get(url, callback));
    }

    public IEnumerator Get(string url, System.Action<string> callback)
    {
        using (UnityWebRequest www = UnityWebRequest.Get(url))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.ConnectionError)
            {
                Debug.Log(www.error);
            }
            else
            {
                if (www.isDone)
                {
                    var result = System.Text.Encoding.UTF8.GetString(www.downloadHandler.data);
                    //Debug.Log(result);
                    result = "{\"result\":" + result + "}";

                    callback(result);
                    //var enemy = JsonUtility.FromJson<Enemy>(result);
                    //enemyViewController.displayEnemyData(enemy.id.ToString(), enemy.name, enemy.health.ToString(), enemy.attack.ToString());
                    //Debug.Log("name:" + enemy.name + " ,health:" + enemy.health + " ,attack:" + enemy.attack);
                }
                else
                {
                    Debug.Log("Cannot get the Data");
                }
            }
        }
    }

    public void PostRequest(string url,EnemyRequestData data, System.Action<string> callback)
    {
        StartCoroutine(Post(url, data, callback));
    }

    public IEnumerator Post(string url, EnemyRequestData data, System.Action<string> callback)
    {
        var jsonData = JsonUtility.ToJson(data);
        Debug.Log(jsonData);

        using (UnityWebRequest www = UnityWebRequest.Post(url, jsonData))
        {
            www.SetRequestHeader("content-type", "application/json");
            www.uploadHandler.contentType = "application/json";
            www.uploadHandler = new UploadHandlerRaw(System.Text.Encoding.UTF8.GetBytes(jsonData));

            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.ConnectionError)
            {
                Debug.Log(www.error);
            }
            else
            {
                if (www.isDone)
                {
                    var result = System.Text.Encoding.UTF8.GetString(www.downloadHandler.data);
                    result = "{\"result\":" + result + "}";
                    //var resultEnemyList = JsonHelper.FromJson<Enemy>(result);

                    //foreach (var item in resultEnemyList)
                    //{
                    //    Debug.Log(item.name);
                    //}
                    callback(result);
                }
                else
                {
                    Debug.Log("data cannot be reached");
                }
            }
        }
    }
}
