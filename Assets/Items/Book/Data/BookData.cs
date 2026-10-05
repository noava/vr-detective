using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BookPage
{
    public int pageNumber;
    public string plantName;
    public string scientificName;
    
    [TextArea(3, 10)] 
    public string symptoms;
    public string cause;
    public string treatment;
    public string imagePath;
}

[System.Serializable]
public class BookData
{
    public string bookTitle;
    public List<BookPage> pages;
}
