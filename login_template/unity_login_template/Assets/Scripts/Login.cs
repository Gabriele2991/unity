using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Login : MonoBehaviour
{
    [SerializeField] private string loginEndpoint = "http://localhost:3000/account/login";
    [SerializeField] private string createEndpoint = "http://localhost:3000/account/create";

    [SerializeField] private TextMeshProUGUI alertText;
    [SerializeField] private Button loginButton;
    [SerializeField] private Button createButton;
    [SerializeField] private TMP_InputField usernameInputField;
    [SerializeField] private TMP_InputField passwordInputField;

    public void OnLoginClick()
    {
        alertText.text = "Signing in...";
        ActivateButton(false);
        StartCoroutine(TryLogin());
    }
    public void OnCreateClick()
    {
        alertText.text = "Creating Account...";
        ActivateButton(false);
        StartCoroutine(TryCreate());
    }

    private IEnumerator TryLogin()
    {
        string username = usernameInputField.text;
        string password = passwordInputField.text;

        if(username.Length<3 || username.Length > 24)
        {
            alertText.text = "Invalid username";
            ActivateButton(true);
            yield break;
        }
        
        if(password.Length<3 || password.Length > 24)
        {
            alertText.text = "Invalid password";
            ActivateButton(true);
            yield break;
        }

        WWWForm form = new WWWForm();
        form.AddField("rUsername", username);
        form.AddField("rPassword", password);

        //Starting the request
        UnityWebRequest request = UnityWebRequest.Post(loginEndpoint, form);
        var handler = request.SendWebRequest();

        float startTime = 0.0f;
        while (!handler.isDone)
        {
            startTime += Time.deltaTime;
            
            if (startTime > 10.0f) break;

            yield return null;
        }

        if(request.result == UnityWebRequest.Result.Success)
        {
            //Login success?
            if (request.downloadHandler.text != "Invalid credentials")
            {
                ActivateButton(false);
                GameAccount returnedAccount = JsonUtility.FromJson<GameAccount>(request.downloadHandler.text);
                alertText.text = $"{returnedAccount._id} Welcome" + returnedAccount.username +((returnedAccount.adminFlag ==1)?"Admin":"");
            }
            else
            {
                alertText.text = "Invalid credentials";
                ActivateButton(true);
            }
        }
        else
        {
            alertText.text = "Error connecting to the server";
            ActivateButton(true);
        }

        Debug.Log($"{username}:{password}");

        yield return null;
    }
    
    private IEnumerator TryCreate()
    {


        string username = usernameInputField.text;
        string password = passwordInputField.text;

        if(username.Length<3 || username.Length > 24)
        {
            alertText.text = "Invalid username";
            ActivateButton(true);
            yield break;
        }
        
        if(password.Length<3 || password.Length > 24)
        {
            alertText.text = "Invalid password";
            ActivateButton(true);
            yield break;
        }

        WWWForm form = new WWWForm();
        form.AddField("rUsername", username);
        form.AddField("rPassword", password);

        //Starting the request
        UnityWebRequest request = UnityWebRequest.Post(createEndpoint, form);
        var handler = request.SendWebRequest();

        float startTime = 0.0f;
        while (!handler.isDone)
        {
            startTime += Time.deltaTime;
            
            if (startTime > 10.0f) break;

            yield return null;
        }

        if(request.result == UnityWebRequest.Result.Success)
        {
            //Login success?
            if (request.downloadHandler.text != "Invalid credentials" && request.downloadHandler.text != "Username is already taken")
            {
                GameAccount returnedAccount = JsonUtility.FromJson<GameAccount>(request.downloadHandler.text);
                alertText.text = "Account has been created";
            }
            else
            {
                alertText.text = "Invalid credentials";
            }
        }
        else
        {
            alertText.text = "Error connecting to the server";
        }
        ActivateButton(true);

        Debug.Log($"{username}:{password}");

        yield return null;
    }

    private void ActivateButton(bool toggle)
    {
        loginButton.interactable = toggle;
        createButton.interactable = toggle;
    }
}
 