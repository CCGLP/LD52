using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; 
public class ReviewPanel : Singleton<ReviewPanel>
{
    [SerializeField]
    protected TextMeshProUGUI reportedPostsNumber, correctlyReportedNumber, wrongReportedNumber, extraHoursNumber, evaluationText, endProbationText;



    public void SetInformation(int reportedPosts, int correctlyReported, int wronglyReported, int extraHoursBonus, bool goodEvaluation)
    {
        reportedPostsNumber.text = reportedPosts.ToString(); 
        correctlyReportedNumber.text = correctlyReported.ToString();
        wrongReportedNumber.text = wronglyReported.ToString();
        extraHoursNumber.text = extraHoursBonus.ToString();
        if (!goodEvaluation)
        {
            evaluationText.color = Color.red;
            evaluationText.text = "..."; 
        }

        if (GameController.Instance.dayIndex == 5)
        {
            endProbationText.gameObject.SetActive(true); 
        }
    }
    void Start()
    {
        
    }

    
}
