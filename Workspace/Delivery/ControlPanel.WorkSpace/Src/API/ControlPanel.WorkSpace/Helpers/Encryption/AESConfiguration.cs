namespace UserManagement.Helpers.Encryption
{
    public class AESConfiguration : IAESConfiguration
    {
        public AESConfiguration()
        {
        }

        public string Key { get; set; }
        public string IV { get; set; }
    }
}

