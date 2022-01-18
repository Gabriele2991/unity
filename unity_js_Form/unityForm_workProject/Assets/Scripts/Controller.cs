using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Controller : MonoBehaviour
{
    public Transform canvasParent;
    public ClientApi client;

    private Transform contentParent;
    private EnemyFormView formView;

    // Start is called before the first frame update
    private void Start()
    {
        CreateListView();
        CreateFormView();
    }

    public void CreateFormView()
    {
        var formPanelPrefab = Resources.Load("Prefabs/EnemyFormPanel");
        var formPanelGo = Instantiate(formPanelPrefab, canvasParent) as GameObject;
        formView = formPanelGo.GetComponent<EnemyFormView>();
        formView.InitFormView(SendCreateRequest);
    }

    private void CreateListView()
    {
        var listpanelprefab = Resources.Load("Prefabs/EnemyListPanel");
        var listPanelGo = Instantiate(listpanelprefab, canvasParent) as GameObject;
        contentParent = listPanelGo.GetComponentInChildren<ScrollRect>().content;
    }

    private void SendCreateRequest(EnemyRequestData data)
    {
        client.PostRequest(client.postUrl, data, result =>
        { 
              Debug.Log(result);
        });
    }
}
