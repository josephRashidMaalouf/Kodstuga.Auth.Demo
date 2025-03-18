namespace Blazor.Events;

public static class AuthPublisher
{
    public static event Action AuthenticationStateChanged;

    public static async Task OnAuthenticationStateChanged()
    {
        AuthenticationStateChanged?.Invoke();
    }
}