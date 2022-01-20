using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.Networking;

public class Login : MonoBehaviour
{
    [SerializeField] private string authenticationEndpoint = "http://localhost:3000/account";

    [SerializeField] private TMP_InputField usernameInputField;
    [SerializeField] private TMP_InputField passwordInputField;

    public void OnLoginClick()
    {
        StartCoroutine(TryLogin());
    }

    private IEnumerator TryLogin()
    {
        string username = usernameInputField.text;
        string password = passwordInputField.text;

        //Starting the request
        UnityWebRequest request = UnityWebRequest.Get(authenticationEndpoint);
        var handler = request.SendWebRequest();

        float startTime = 0.0f;
        while (!handler.isDone)
        {
            startTime += Time.deltaTime;
            
            if (startTime > 10.0f) break;

            yield return null;
        }

        Debug.Log($"{username}:{password}");

        yield return null;
    }
}
 