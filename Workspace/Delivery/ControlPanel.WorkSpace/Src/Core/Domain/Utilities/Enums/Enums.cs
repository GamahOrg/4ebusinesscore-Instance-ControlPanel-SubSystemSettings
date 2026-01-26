namespace Domain.Enums
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
    public enum ActionNameEnum  
    {
        Get,
        Post,
        Put,
        Delete,
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
        SubSystem,
        Component,
        Part
    }
    public enum Color
    {
        Red,
        Green,
        Yellow,
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
        UserJobTitle = 5,
        JobPositionId = 6

    }
    public enum JobTime
    {
        
        Basic , Responsible
    }

    public enum SubsystemType
    {
        IsExternal,
        IsInternal,
        IsExternalComponent,
        IsSinglSignOn
    }
    public enum SSOSecuritySettingType
    {
        password,
        saml,
        clientcred,
        authorizationcode,
        m2m
    }   
}