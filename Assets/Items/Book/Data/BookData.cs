using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BookPage
{
    public int pageNumber;
    public string title;
    
    [TextArea(3, 10)] 
    public string content;
    public string imagePath;
}

[System.Serializable]
public class BookData
{
    public string bookTitle;
    public List<BookPage> pages;
}
