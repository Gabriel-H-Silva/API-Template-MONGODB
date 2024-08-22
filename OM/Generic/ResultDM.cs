namespace Model.Generic
{
    public class ResultDM
    {
        public Information Information { get; set; } = new Information();
        public object? Req { get; set; }
        public object? Res { get; set; }
        public bool Saved { get; set; }
    }

    public class Information
    {
        public int Status { get; set; } = 0;
        public string Message { get; set; } = "Not Defined";
    }
}
