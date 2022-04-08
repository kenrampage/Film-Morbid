using UnityEngine;
using UnityEngine.Events;

public class Bookcase : MonoBehaviour
{
    [SerializeField] GameObject painting;
    public Book[] books = new Book[60];
    bool won;
    float timer;
    bool fs;
    [SerializeField] private UnityEvent onPaintingMove;

    void Start()
    {
        fs = false;
        books = GetComponentsInChildren<Book>();
        won = false;
        timer = 0;
    }
    void Update()
    {
        if (won)
        {
            if (fs == false)
            {
                onPaintingMove?.Invoke();
                fs = true;
            }
            timer += Time.deltaTime;
            if(timer < 2.3f)
            {
                painting.transform.position += new Vector3(0, Time.deltaTime / 1.05f, 0);
            }
        }
    }
    /// <summary>
    /// Check if the correct books are pulled out.
    /// </summary>
    public bool checkBooks()
    {
        bool toReturn = true;
        for(int i = 0; i < books.Length; i++)
        {
            if(books[i].isCorrectBook && books[i].pulledOut)
            {
                won = true;
                //This is for correct books
            }
            else if(books[i].isCorrectBook && !books[i].pulledOut)
            {
                //Wrong book
                won = false;
                return false;
            }
            else if (!books[i].isCorrectBook && books[i].pulledOut)
            {
                //Wrong book
                won = false;
                return false;
            }
        }
        
        return toReturn;
    }
}
