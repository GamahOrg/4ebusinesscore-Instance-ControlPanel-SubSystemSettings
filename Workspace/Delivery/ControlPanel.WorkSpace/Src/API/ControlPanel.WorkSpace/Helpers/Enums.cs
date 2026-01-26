namespace UserManagement.Helpers
{
    public enum State
    {
        NotDeleted,
        Deleted
    }
    public enum StatusEnum
    {
        FailedToSave,
        SavedSuccessfully,
        Exception,
        Success,
        Failed,
        FailedToFindTheObject,
        AlreadyExisting
    }
    public enum UserType
    {
        Local,
        Domain
    }
    public enum UserGender
    {
        Male,
        Female
    }
    public enum UserKind
    {
        Manager,
        Employee
    }
    public enum KafkaEvent
    {
        Post,
        Put,
        Delete
    }
    public enum MaritalStatus
    {
        Single,
        married
    }
    public enum NoteTypeId
    {
        Tools,
        System,
        CommanServices
    }
    public enum Table
    {
        User,
        Job,
        Card,
        Role
    }

    public enum TableColumn
    {
        UserTypeId = 1,
        JobDegreeId = 2,
        CardSetting = 3,
        CategoryRoleId = 4,
        UserJobTitle = 5

    }
    public enum JobTime
    {
        
        Basic , Responsible
    }
}