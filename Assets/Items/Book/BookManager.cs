using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BookManager : MonoBehaviour
{
    // Load the book data from a JSON file
    public TextAsset jsonFile;
    public TextMeshPro bookTitleOpen;
    public TextMeshPro bookTitleClosed;
    public TextMeshProUGUI pageTitle;
    public TextMeshProUGUI pageContent;
    public Image pageImage;

    private BookData activeBook;
    private int currentPageIndex = 0;

    void Start()
    {
        if (jsonFile != null)
        {
            LoadBook(jsonFile.text);
        }
        else
        {
            Debug.LogError("Book JSON file not found!");
        }
    }

    public void LoadBook(string jsonText)
    {
        activeBook = JsonUtility.FromJson<BookData>(jsonText);
        bookTitleOpen.text = activeBook.bookTitle;
        bookTitleClosed.text = activeBook.bookTitle;
        ShowPage(0);
    }

    private void ShowPage(int index)
    {
        BookPage page = activeBook.pages[index];
        
        pageTitle.text = page.title;
        pageContent.text = page.content;
        
        Sprite[] allSprites = Resources.LoadAll<Sprite>(page.imagePath);

        if (allSprites != null && allSprites.Length > 0)
        {
            pageImage.sprite = allSprites[0];
        }
    }

    public void nextPage()
    {
        if (currentPageIndex < activeBook.pages.Count - 1)
        {
            currentPageIndex++;
            ShowPage(currentPageIndex);
        }
    }

    public void previousPage()
    {
        if (currentPageIndex > 0)
        {
            currentPageIndex--;
            ShowPage(currentPageIndex);
        }
    }
}
