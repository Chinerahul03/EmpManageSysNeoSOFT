namespace EmpManageSys2.Services.Validation
{
    public class CreateUpdateValidationCheck : ICreateUpdateValidationCheck
    {
        public string checkException(string errorCode)
        {
            string? message = "";
            if (errorCode.Contains("45809E714CA6C6B3"))
            {
                message = "Passport number already exists!";
            }
            else if (errorCode.Contains("49A14740FC073C00"))
            {
                message = "Email Id already exists!";
            }
            else if (errorCode.Contains("250375B10A6A060D"))
            {
                message = "Mobile number already exists!";
            }
            else if (errorCode.Contains("7C38BFC803C9067D"))
            {
                message = "Pan card number already exists!";
            }
            else
            {
                message = "Please enter correct data!";
            }
            return message;
        }
    }
}
