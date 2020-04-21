namespace Sample4.Models
{
    /// <summary>
    /// 処理の進行状態を保持するclass
    /// </summary>
    public class ProgressInfo
    {
        public ProgressInfo(int value, string message)
        {
            this.Value = value;
            this.Message = message;
        }

        public int Value { get; private set; }
        public string Message { get; private set; }
    }
}
