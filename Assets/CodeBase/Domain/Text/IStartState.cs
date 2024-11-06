namespace CodeBase.Domain.Text
{
    public interface IStartState
    {
        public void OnStartGame();
        public void Exit();
    }
}