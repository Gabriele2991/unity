using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Controller : MonoBehaviour
{
    private int counter = 0;
    public Transform canvasParent;
    public ClientApi client;
    public EnemyDatabase enemyDatabase;

    private Transform contentParent;
    private GameObject enemyViewPrefab;
    private EnemyFormView formView;

    private List<EnemyView> enemyViews = new List<EnemyView>();

    // Start is called before the first frame update
    private void Start()
    {
        CreateListView();
        CreateFormView();
        enemyViewPrefab = Resources.Load<GameObject>("Prefabs/EnemyView");

        RequestEnemies();
    }

    public void CreateFormView()
    {
        var formPanelPrefab = Resources.Load("Prefabs/EnemyFormPanel");
        var formPanelGO = Instantiate(formPanelPrefab, canvasParent) as GameObject;
        formView = formPanelGO.GetComponent<EnemyFormView>();
        formView.InitFormView(SendCreateRequest);
    }

    private void CreateListView()
    {
        var listpanelprefab = Resources.Load("Prefabs/EnemyListPanel");
        var listPanelGO = Instantiate(listpanelprefab, canvasParent) as GameObject;
        contentParent = listPanelGO.GetComponentInChildren<ScrollRect>().content;
    }

    private void SendCreateRequest(EnemyRequestData data)
    {
        client.PostRequest(client.postUrl, data, result =>
        { 
             Debug.Log(result);
            OnDataRecieved(result);
        });
    }

    private void RequestEnemies()
    {
        client.GetRequest(client.getUrl, result => {
            Debug.Log(result);
            OnDataRecieved(result);
        });
    }

    private void OnDataRecieved(string json)
    {
        var recievedEnemies = JsonHelper.FromJson<Enemy>(json);
        enemyDatabase.ClearInventory();

        foreach (var enemy in recievedEnemies)
        {
            enemyDatabase.Add(enemy);
        }

        CreateEnemyViews();
    }

    private void CreateEnemyViews()
    {
        var currentEnemies = enemyDatabase.GetEnemies();
        
        //destroy old views
        if (counter == 0 && enemyViews.Count > 0)
        {
            foreach (var enemy in enemyViews)
            {
                Destroy(enemy.gameObject);
            }
            counter++;
        }
       


        //create new enemy views
        foreach (var enemy in currentEnemies)
        {
            var enemyViewGO = Instantiate(enemyViewPrefab, contentParent) as GameObject;
            var enemyView = enemyViewGO.GetComponent<EnemyView>();
            enemyView.InitView(enemy);
            enemyViews.Add(enemyView);
        }
    }
}
