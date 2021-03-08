namespace Interfaces
{
    public enum Intent
    {
        Nothing,
        WantToGoRight,
        WantToGoLeft,
        WantToGoTop,
        WantToGoBot
    }
    
    public interface IPlayerIntent
    {
        Intent GetPlayerIntent();
    }
}