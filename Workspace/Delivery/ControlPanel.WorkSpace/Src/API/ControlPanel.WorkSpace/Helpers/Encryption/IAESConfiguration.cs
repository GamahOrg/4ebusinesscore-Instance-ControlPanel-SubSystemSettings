namespace UserManagement.Helpers.Encryption
{
    public interface IAESConfiguration
    {
        public string Key { get; set; }
        public string IV { get; set; }
    }
}

