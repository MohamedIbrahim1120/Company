namespace Company.PL.Services
{
    public class TransentServices : ITransentServices
    {

        public TransentServices()
        {
            Guid = Guid.NewGuid();
        }

        public Guid Guid { get; set; }

        public string GetGuid()
        {
            return Guid.ToString();
        }
    }
}
