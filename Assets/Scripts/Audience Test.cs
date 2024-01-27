using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudienceTest : MonoBehaviour
{
    public int maxApproval = 100;
    public int currentApproval;

    public AudienceApproval audienceapproval;

    // Start is called before the first frame update
    void Start()
    {
        currentApproval = 50;
        audienceapproval.SetMaxApproval(maxApproval);
    }

    // Update is called once per frame
    void Update()
    {
       
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ApprovalChange(-10);
        }
        if (Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            ApprovalChange(10);
        }
    }
    void ApprovalChange(int approval)
    {
        currentApproval += approval;
        audienceapproval.SetApproval(currentApproval);
    }
}
