using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Gamekit3D;
using Common;
using Assets._3DGamekit.Scripts.Game;
using Gamekit3D.Network;


public class ChatUI : MonoBehaviour
{

    public GameObject messageView;
    public GameObject myMessage;
    public GameObject friendMessage;

    private string friendName;
    private int friendID;
    private List<content> messages = null;
    private List<GameObject> messageObjects = new List<GameObject>();

    // my message info content layout | ----------------- message text | image |
    // friend's info content layout   | image | message text ----------------- |


    private void Awake()
    {
        myMessage.SetActive(false);
        friendMessage.SetActive(false);
    }
    // Use this for initialization
    void Start()
    {
        //Test();
    }

    private void OnEnable()
    {
        PlayerMyController.Instance.EnabledWindowCount++;
      }

    private void OnDisable()
    {
        PlayerMyController.Instance.EnabledWindowCount--;
        friendName = null;
        messages = null;
        foreach (var msgOb in messageObjects)
            msgOb.SetActive(false);
        messageObjects.Clear();
    }

    // Update is called once per frame
    void Update()
    {
        if(!World.Instance.chatHistory.ContainsKey(World.Instance.partner))
        {
            Debug.Log(World.Instance.partner);
            messages = null;
            return;
        }
        else
        {
            messages = World.Instance.chatHistory[World.Instance.partner];
        }
        if(messages.Count > messageObjects.Count)
        {
            for(int i = messageObjects.Count; i < messages.Count; i++)
            {
                var msg = messages[i];
                if (msg.source == 0)
                    ReceiveFriendMessage(msg.message);
                else
                    SendMyMessage(msg.message);
            }
        }

    }

    public void ReceiveFriendMessage(string text)
    {
        if (friendMessage == null)
            return;
        GameObject cloned = GameObject.Instantiate(friendMessage);
        messageObjects.Add(cloned);
        if (cloned == null)
            return;
        cloned.SetActive(true);
        AddElement(cloned, text);
    }

    public void SendMyMessage(string text)
    {
        if (myMessage == null)
            return;

        GameObject cloned = GameObject.Instantiate(myMessage);
        messageObjects.Add(cloned);
        if (cloned == null)
            return;
        cloned.SetActive(true);
        AddElement(cloned, text);
    }

    public void OnSendButtonClick(GameObject inputField)
    {
        InputField input = inputField.GetComponent<InputField>();
        if (input == null)
            return;

        if (input.text.Trim().Length == 0)
            return;

        //SendMyMessage(input.text);
        CChatMessage m = new CChatMessage();
        m.from = World.Instance.selfId;
        m.to = World.Instance.partner;
        m.message = input.text;
        if(World.Instance.chatHistory.ContainsKey(m.to))
        {
            World.Instance.chatHistory[m.to].Add(new content(1, input.text));
        }
        else
        {
            List<content> temp = new List<content>();
            World.Instance.chatHistory.Add(m.to, temp);
            World.Instance.chatHistory[m.to].Add(new content(1, input.text));
        }
        Debug.Log(string.Format("Frontend: sendmessage from {0} to {1} message {2}", m.from, m.to, m.message));
        Client.Instance.Send(m);
        input.text = "";
    }

    void AddElement(GameObject element, string text)
    {
        TextMeshProUGUI textMesh = element.GetComponentInChildren<TextMeshProUGUI>();
        if (textMesh == null)
            return;
        //float width = textMesh.GetPreferredValues().x; // get preferred width before assign text
        textMesh.text = text;
        RectTransform rectTransform = element.GetComponent<RectTransform>();
        if (rectTransform == null)
            return;

        RectTransform parentRect = this.GetComponent<RectTransform>();
        if (parentRect == null)
            return;

        if (textMesh.preferredWidth < parentRect.rect.width)
        {
            ContentSizeFitter filter = textMesh.GetComponent<ContentSizeFitter>();
            if (filter != null)
            {
                filter.horizontalFit = ContentSizeFitter.FitMode.PreferredSize;
                textMesh.rectTransform.sizeDelta = new Vector2(textMesh.preferredWidth, textMesh.preferredHeight);
            }
        }

        element.transform.SetParent(this.transform, false);

        ScrollRect scrollRect = messageView.GetComponent<ScrollRect>();
        if (scrollRect == null)
            return;

        scrollRect.normalizedPosition = new Vector2(0, 0);
    }

    void Test()
    {
        //AddNewMessage(true, "my message send");
        //AddNewMessage(false, "friend message receive");

        SendMyMessage("hello");
        ReceiveFriendMessage("hello");
        SendMyMessage("h2");
        SendMyMessage("H1");
    }

    /*
    void AddNewMessage(bool mine, string message)
    {
        GameObject newContent = GameObject.Instantiate(content);
        if (newContent == null)
            return;
        GameObject newImage = GameObject.Instantiate(image);
        if (newImage == null)
            return;
        GameObject newText = GameObject.Instantiate(text);
        if (newText == null)
            return;

        HorizontalLayoutGroup layout = newContent.GetComponent<HorizontalLayoutGroup>();
        if (mine)
            layout.childAlignment = TextAnchor.UpperRight;
        else
            layout.childAlignment = TextAnchor.UpperLeft;

        TextMeshProUGUI textMesh = text.GetComponentInChildren<TextMeshProUGUI>();
        if (textMesh == null)
            return;

        //float width = textMesh.GetPreferredValues().x; // get preferred width before assign text
        textMesh.text = message;
        RectTransform rectTransform = text.GetComponent<RectTransform>();
        if (rectTransform == null)
            return;

        RectTransform viewRect = messageContent.GetComponent<RectTransform>();
        if (viewRect == null)
            return;

        if (textMesh.preferredWidth < viewRect.rect.width)
        {
            ContentSizeFitter filter = textMesh.GetComponent<ContentSizeFitter>();
            if (filter != null)
            {
                filter.horizontalFit = ContentSizeFitter.FitMode.PreferredSize;
                rectTransform.sizeDelta = new Vector2(textMesh.preferredWidth, textMesh.preferredHeight);
            }
        }

        newImage.transform.SetParent(newContent.transform, false);
        newText.transform.SetParent(newContent.transform, false);
        newContent.transform.SetParent(messageContent.transform, false);
    }
    */
}
