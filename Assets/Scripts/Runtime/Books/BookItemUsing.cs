using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookItemUsing : ItemUsing
{
    [SerializeField] Book bookModel;

    public override void UseItem(Unit userUnit)
    {
        Game.WindowManagerView.ShowWindow(bookModel.BookView.gameObject, bookModel, true, false);
    }
}
