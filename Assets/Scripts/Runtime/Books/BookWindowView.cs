using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BookWindowView : WindowView
{
    [SerializeField] List<TMP_Text> openedPagesTexts;
    [SerializeField] List<TMP_Text> openedPagesNumbers;
    [SerializeField] int maxCharactersNumberOnPage;
    [SerializeField] GameObject forwardButton;
    [SerializeField] GameObject backwardButton;
    [SerializeField] int currentSheetPosition;

    Book presentation;

    public override void SetPresentation(MonoBehaviour presentation)
    {
        this.presentation = presentation as Book;
        UpdateView();
    }

    public void RotateForward()
    {
        currentSheetPosition++;
        UpdateView();
    }

    public void RotateBackward()
    {
        currentSheetPosition--;
        UpdateView();
    }

    void UpdateView()
    {
        for (int i = 0; i < openedPagesTexts.Count; i++)
        {  
            openedPagesTexts[i].text = presentation.Text;
            int pageNumber = currentSheetPosition * openedPagesTexts.Count + i;
            openedPagesTexts[i].pageToDisplay = pageNumber + 1;
            
            openedPagesNumbers[i].text = (pageNumber + 1).ToString();
        }

        openedPagesTexts[0].ForceMeshUpdate();
        int pageCount = openedPagesTexts[0].textInfo.pageCount;
        int sheetsCount = (pageCount + (openedPagesTexts.Count - 1)) / openedPagesTexts.Count;

        forwardButton.SetActive(currentSheetPosition < sheetsCount - 1);
        backwardButton.SetActive(currentSheetPosition > 0);
    }
}
