namespace EmployeeManagement.Ultilities
{
    public static class SD
    {
        public static class ValidationMessages
        {
            public static class CityMessage
            {
                public const string NameUnique = "City name already exists";
                public const string NameRequired = "City name is required";
                public const string NameLength = "City name must be between 2 and 50 characters";
                public const string NameRegex = "City name can only contain letters";
            }


        }
    }
}
