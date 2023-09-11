namespace ServiceAuth_API.DTO
{
    public class DocumentDTO
    {
        public string? Id { get; set; }
        public string? DocumentName { get; set; }
        public byte[]? FileContent { get; set; }
        public DateTime UploadedDate { get; set; }
        public string? UploadedBy { get; set; }
    }
}
