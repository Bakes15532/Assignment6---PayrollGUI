// FacultyMember.cs
//
// Joshua Baker
// 
// FacultyMember abstract base class.

using System;
[Serializable]
public abstract class FacultyMember
{
    // private instance variable for storing first name
    private string firstNameValue;

    // private instance variable for storing last name
    private string lastNameValue;

    // private instance variable for storing start date
    private DateTime startDateValue;

    // private instance variable for storing salary
    private decimal salaryValue;

    // parameter-less constructor
    public FacultyMember()
    {
    }

    // constructor
    public FacultyMember(string first, string last, bool married, string gender, DateTime startDate, decimal salary, Address homeAddress, PhoneNumber homePhone, PhoneNumber workPhone)
    {
        this.FirstName = first;
        this.LastName = last;
        this.Married = married;
        this.Gender = gender;
        this.StartDate = startDate;
        this.Salary = salary;
        this.HomeAddress = homeAddress;
        this.HomePhone = homePhone;
        this.WorkPhone = workPhone;
    } // end FacultyMember constructor

    // property to get and set facultyMember's first name
    public string FirstName
    {
        get
        {
            return Util.Capitalize(firstNameValue);
        } // end get
        set
        {
            value = value.Trim().ToUpper();
            if (value.Length < 1)
                throw new ApplicationException("First name is empty!");
            // check for letters
            foreach (char c in value)
                if (c < 'A' || c > 'Z')
                    throw new ApplicationException("First name must consist of letters only!");
            firstNameValue = value;
        } // end set
    } // end property FirstName

    // property to get and set facultyMember's last name
    public string LastName
    {
        get
        {
            return Util.Capitalize(lastNameValue);
        } // end get
        set
        {
            value = value.Trim().ToUpper();
            if (value.Length < 1)
                throw new ApplicationException("Last name is empty!");
            // check for letters
            foreach (char c in value)
                if (c < 'A' || c > 'Z')
                    throw new ApplicationException("Last name must consist of letters only!");
            lastNameValue = value;
        } // end set
    } // end property LastName

    // read-only property to get facultyMember's name
    public string Name
    {
        get
        {
            return LastName + ", " + FirstName;
        } // end get
    } // end property Name

    // property to get and set facultyMember's salary
    public decimal Salary
    {
        get
        {
            return salaryValue;
        } // end get
        set
        {
            if (value < 0)
                throw new ApplicationException("Salary must not be negative!");
            salaryValue = value;
        } // end set
    } // end property LastName

    // property to get and set whether facultyMember is married
    public bool Married { get; set; }

    // read-only property to get facultyMember's marital status
    public string MaritalStatus
    {
        get
        {
            if (Married)
                return "married";
            else
                return "single";
        } // end get
    } // end property Name

    // property to get and set whether facultyMember is male
    public bool IsMale { get; set; }

    // property to get and set whether facultyMember is female
    public bool IsFemale
    {
        get
        {
            return !IsMale;
        } // end get
        set
        {
            IsMale = !value;
        } // end set
    } // end property IsFemale

    // property to get and set facultyMember's gender
    public string Gender
    {
        get
        {
            if (IsMale)
                return "Male";
            else
                return "Female";
        } // end get
        set
        {
            value = value.ToUpper();
            if (value == "MALE" || value == "M")
                IsMale = true;
            else if (value == "FEMALE" || value == "F")
                IsMale = false;
            else
                throw new ApplicationException("Gender should be Male or Female!");
        } // end set
    } // end property Gender

    // read-only property to get facultyMember's title
    public string Title
    {
        get
        {
            if (IsMale)
                return "Mr.";
            else
                return "Ms.";
        } // end get
    } // end property Title

    // read-only property to get facultyMember's title and name
    public string TitleName
    {
        get
        {
            return Title + " " + Name;
        } // end get
    } // end property TitleName

    // property to get and set facultyMember's birth date
    public DateTime StartDate
    {
        get
        {
            return startDateValue;
        } // end get
        set
        {
            if (value > DateTime.Now)
            {
                throw new ApplicationException("FacultyMember start date must be prior to today!");
            }
            startDateValue = value;
        } // end set
    } // end property StartDate

    // read-only property to get facultyMember's age
    public byte YearsWorked
    {
        get
        {
            // Get the birth date value for the current year.

            DateTime thisYearStartDate = new DateTime(DateTime.Now.Year, StartDate.Month, StartDate.Day);

            // Calculate and return the age based on if the birth date has occurred this year or not.

            if (thisYearStartDate <= DateTime.Now)
            {
                return (byte)(DateTime.Now.Year - StartDate.Year);
            }
            else
            {
                return (byte)(DateTime.Now.Year - StartDate.Year - 1);
            }
        }
    }

    // property to get and set facultyMember's home address
    public Address HomeAddress { get; set; }

    // property to get and set facultyMember's home phone
    public PhoneNumber HomePhone { get; set; }

    // property to get and set facultyMember's cell phone
    public PhoneNumber WorkPhone { get; set; }

    // returns string representation of FacultyMember object
    public override string ToString()
    {
        return TitleName + "\t"
        + MaritalStatus + "\t"
        + AcademicTitle + "\t"
        + HomeAddress + "\t"
        + HomePhone.ToString() + ", "
        + WorkPhone.ToString() + ", "
        + YearsWorked + " years, "
        + " Salary: " + Salary.ToString("C")
        + " Retirement: " + RetirementAmount.ToString("C"); ;
    } // end method ToString

    // virtual readonly property for AcademicTitle, will be overridden by derived classes
    public virtual string AcademicTitle
    {
        get
        {
            return "Faculty Member";
        }
    } // no implementation here

    // abstract readonly property for RetirementPercentage, will be overridden by derived classes
    public abstract double RetirementPercentage
    {
        get;
    } // no implementation here

    // read-only property to get facultyMember's RetirementAmount
    public decimal RetirementAmount
    {
        get
        {
            return Salary * (decimal)RetirementPercentage / 100;
        }
    }

} // end abstract class FacultyMember
