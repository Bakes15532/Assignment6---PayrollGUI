// Lecturer.cs
//
// Joshua Baker
// 
// Lecturer class that extends FacultyMember.
using System;
[Serializable]
public class Lecturer : FacultyMember
{
    // parameter-less constructor
    public Lecturer()
        : base()
    {
    }

    // constructor
    public Lecturer(string first, string last, bool married, string gender, DateTime startDate, decimal salary, Address homeAddress, PhoneNumber homePhone, PhoneNumber workPhone,
        bool isAdjunct)
       : base(first, last, married, gender, startDate, salary, homeAddress, homePhone, workPhone)
    {
        this.IsAdjunct = isAdjunct;
    } // end Lecturer constructor

    // property that gets and sets lecturer's isAdjust
    public bool IsAdjunct { get; set; }

    // returns lecturer's academic title; override FacultyMember's abstract property
    public override string AcademicTitle
    {
        get
        {
            if (IsAdjunct)
                return "Adjunct";
            else
                return "Lecturer";
        }
    }

    // override FacultyMember's abstract property RetirementPercentage 
    public override double RetirementPercentage
    {
        get
        {
            const double RETIREMENT_RATE = 8;
            if (IsAdjunct)
                return 0;
            else
                return RETIREMENT_RATE;
        }
    }
} // end class Lecturer

