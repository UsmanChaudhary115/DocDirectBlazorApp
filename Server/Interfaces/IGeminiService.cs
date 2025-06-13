namespace Server.Interfaces
{
    public interface IGeminiService
    {
        Task<string> AskGeminiAsync(string prompt);
    }
}