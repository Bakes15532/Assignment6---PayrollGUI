// Professor.cs
//
// Joshua Baker
// 
// Professor class that extends FacultyMember.
using System;
[Serializable]
public class Professor : FacultyMember
{
    // public enumeration with constants that represent facultyMember's rank
    public enum RankEnum { ASSISTANT, ASSOCIATE, FULL };

    // private string array that stores standings
    private static string[] ranksValue = { "Assistant", "Associate", "Full" };

    // private instance variable for storing rank
    private int rankValue;

    // parameter-less constructor
    public Professor() : base()
    {
    }

    // constructor
    public Professor(string first, string last, bool married, string gender, DateTime startDate, decimal salary, Address homeAddress, PhoneNumber homePhone, PhoneNumber workPhone, int rank)
        : base(first, last, married, gender, startDate, salary, homeAddress, homePhone, workPhone)
    {
        this.Rank = rank;
    } // end Professor constructor

    // property that gets and sets facultyMember's rank
    public int Rank
    {
        get
        {
            return rankValue;
        } // end get
        set
        {
            if (value > (int)RankEnum.FULL || value < (int)RankEnum.ASSISTANT)
            {
                throw new ApplicationException("Professor rank ranges from " +
                    ((int)RankEnum.ASSISTANT).ToString() + " to " +
                    ((int)RankEnum.FULL).ToString());
            }
            rankValue = value;
        } // end set
    } // end property Rank

    // property that gets and sets facultyMember's rank as a string
    public string RankName
    {
        get
        {
            return ranksValue[rankValue];
        } // end get
        set
        {
            switch (value.Trim().ToUpper())
            {
                case "ASSISTANT":
                    rankValue = (int)RankEnum.ASSISTANT;
                    break;
                case "ASSOCIATE":
                    rankValue = (int)RankEnum.ASSOCIATE;
                    break;
                case "FULL":
                    rankValue = (int)RankEnum.FULL;
                    break;
                default:
                    throw new ApplicationException("Professor rank must be Assistant, Associate, or Full!");
            }
        } // end set
    } // end property RankName

    // returns facultyMember's academic title; override FacultyMember's abstract property
    public override string AcademicTitle
    {
        get
        {
            return ranksValue[Rank] + " Professor";
        }
    }

    // override FacultyMember's abstract property RetirementPercentage 
    public override double RetirementPercentage
    {
        get
        {
            const double RETIREMENT_RATE = 10;
            return RETIREMENT_RATE;
        }
    }
} // end class Professor
