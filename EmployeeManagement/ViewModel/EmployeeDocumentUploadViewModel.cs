namespace EmployeeManagement.ViewModel
{
    public class DocumentUpload
    {
        public IFormFile File { get; set; }
        public string Remark { get; set; }
    }
    public class EmployeeDocumentUploadViewModel
    {
            public int EmployeeId { get; set; }
            public List<DocumentUpload> Documents { get; set; }
        
    }
}
