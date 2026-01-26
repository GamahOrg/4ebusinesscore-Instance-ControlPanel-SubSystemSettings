namespace UserManagement.Helpers.DomainModel
{
    public class UserDto
    {
        public UserInformationDto UserInformation { get; set; }
        public UserInformationDto ManagerInformation { get; set; }
    }
    public class UserInformationDto
    {
        public string FullNameEn { get; set; }
        public string FullNameAr { get; set; }
        public string Email { get; set; }
        public string Department { get; set; }
        public string Position { get; set; }
        public string Company { get; set; }
        public string Mobile { get; set; }

    }
}
