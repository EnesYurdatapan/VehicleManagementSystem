namespace OnlineAppointmentManagementSystem.Web.Services.Abstract
{
    public interface ITokenProvider
    {
        void SetToken(string token);
        string? GetToken();
        void ClearToken();
    }
}
