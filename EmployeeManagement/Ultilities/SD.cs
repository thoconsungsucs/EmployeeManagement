namespace EmployeeManagement.Ultilities
{
    public static class SD
    {
        public static class ValidationMessages
        {
            public static class DiplomaMessage
            {
                public const string DateInvalid = "Issued date must be before expiry date";
            }

            public static class CityMessage
            {
                public const string NameUnique = "City name already exists";
                public const string NameRequired = "City name is required";
                public const string NameLength = "City name must be between 2 and 50 characters";
                public const string NameRegex = "City name can only contain letters";
            }

            public static class DistrictMessage
            {
                public const string NameUnique = "District name already exists";
                public const string NameRequired = "District name is required";
                public const string NameLength = "District name must be between 2 and 50 characters";
                public const string CityRequired = "City is required";
                public const string CityInvalid = "City is invalid";
                public const string DistrictInvalid = "District is invalid";
            }

            public static class WardMessage
            {
                public const string NameUnique = "Ward name already exists";
                public const string NameRequired = "Ward name is required";
                public const string NameLength = "Ward name must be between 2 and 50 characters";
                public const string DistrictRequired = "District is required";
                public const string DistrictInvalid = "District is invalid";
                public const string WardInvalid = "Ward is invalid";
            }

            public static class JobMessage
            {
                public const string JobInvalid = "Job is invalid";
            }
            public static class EthicMessage
            {
                public const string EthicInvalid = "Ethic is invalid";
            }

            public static class EmployeeMessage
            {
                public const string NameRequired = "Name is required";
                public const string NameInvalid = "Name can only contain letters";
                public const string NameLength = "Name must be between 2 and 50 characters";
                public const string DateOfBirthRequired = "Date of birth is required";
                public const string DateOfBirthInvalid = "Date of birth is invalid";
                public const string PhoneNumberInvalid = "Phone number is invalid";
                public const string IdentityIdInvalid = "Identity ID is invalid";
                public const string IdentityIdUnique = "Identity ID already exists";
            }
        }

    }
}
