namespace Sample4.StartUp.Models
{
    /// <summary>
    /// ˆ—‚Ìisó‘Ô‚ğ•Û‚·‚éclass
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
