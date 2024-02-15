using EmployeeManagement.Data;
using EmployeeManagement.Data.Repository;
using EmployeeManagement.Models;
using EmployeeManagement.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeRepository _employeeRepository;
        private IWebHostEnvironment _hostingEnvironment;
        private EmployeeDBContext _dbContext;
        private readonly ILogger<EmployeeController> _logger;
        public EmployeeController(IEmployeeRepository employeeRepository, IWebHostEnvironment hostingEnvironment, EmployeeDBContext dBContext, ILogger<EmployeeController> logger)
        {
            _employeeRepository = employeeRepository;
            _hostingEnvironment = hostingEnvironment;
            _dbContext = dBContext;
            _logger = logger;
        }
        [HttpGet]
        public async Task<IEnumerable<Employee>> GetEmployees()
        {
            return await _employeeRepository.GetEmployeesAsync();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmployee([FromRoute] int id)
        {
            var employee = await _employeeRepository.GetEmployeeByIdAsync(id);

            if (employee == null)
            {
                return NotFound();
            }
            return Ok(employee);
        }
        [HttpPost]

        public async Task<IActionResult> PostEmployee([FromBody] Employee employee)
        {
            await _employeeRepository.AddEmployeeAsync(employee);
            return CreatedAtAction("GetEmployee", new { id = employee.Id }, employee);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee([FromRoute] int id)
        {
            var employee = await _employeeRepository.DeleteEmployeeAsync(id);
            if (employee == null)
            {
                return NotFound();
            }
            return Ok(employee);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmployee([FromRoute] int id, [FromBody] Employee employee)
        {
            if (id != employee.Id)
            {
                return BadRequest();
            }
            var updatedEmployee = await _employeeRepository.UpdateEmployeeAsync(id, employee);
            if (updatedEmployee == null)
            {
                return NotFound();
            }
            return Ok(updatedEmployee);
        }




        [HttpPost("UploadFiles")]
        public async Task<IActionResult> UploadFiles([FromForm] EmployeeDocumentUploadViewModel model)
        {
            if (model.Documents == null || model.Documents.Count == 0)
            {
                return BadRequest("No files received in the request");
            }
            var webRootPath = _hostingEnvironment.WebRootPath;
            if (string.IsNullOrWhiteSpace(webRootPath))
            {
                return NotFound("Web root path is not set.");
            }

            if (!Directory.Exists(webRootPath))
            {
                return NotFound("Web root directory does not exist.");
            }

            if (string.IsNullOrWhiteSpace(_hostingEnvironment.WebRootPath))
            {
                return BadRequest("Web root path is not configured");
            }
            var uploads = Path.Combine(_hostingEnvironment.WebRootPath, "uploads");
            if (!Directory.Exists(uploads))
            {
                Directory.CreateDirectory(uploads);
            }

            var uploadedFiles = new List<string>();
            foreach (var documentUpload in model.Documents)
            {
                var file = documentUpload.File;
                var remark = documentUpload.Remark;
                if (string.IsNullOrWhiteSpace(file.FileName))
                {
                    return BadRequest("One of the files doesn't have a name");
                }

                if (file.Length > 0)
                {
                    var filePath = Path.Combine(uploads, file.FileName);
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(fileStream);
                    }

                    var document = new EmployeeDocument
                    {
                        FileName = file.FileName,
                        FilePath = filePath,
                        Remarks = remark,
                        EmployeeId = model.EmployeeId
                    };
                    _dbContext.EmployeeDocuments.Add(document);

                    var fileUrl = Url.Content(Path.Combine("~/uploads", file.FileName));
                    uploadedFiles.Add(fileUrl);
                }
            }

            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // Log the exception and return a 500 response
                return StatusCode(500, "An error occurred while saving the files to the database");
            }

            return Ok(new { message = "Files uploaded successfully.", files = uploadedFiles });
        }
        [HttpGet("GetEmployeeDocuments/{employeeId}")]
        public async Task<IActionResult> GetEmployeeDocuments(int employeeId)
        {
            var documents = await _dbContext.EmployeeDocuments
                .Where(d => d.EmployeeId == employeeId)
                .ToListAsync();

            if (documents == null || documents.Count == 0)
            {
                return NotFound("No documents found for the specified employee ID.");
            }

            var documentData = documents.Select(d => new
            {
                Url = Url.Content(Path.Combine("~/uploads", d.FileName).Replace("\\", "/")),
                Remarks = d.Remarks
            }).ToList();

            return Ok(documentData);
        }
        [HttpPost("{id}/qualifications")]
        
        public async Task<ActionResult<IEnumerable<Qualification>>> AddQualifications(int id, QualificationsModel model)
            {
                var employee = await _dbContext.Employees.FindAsync(id);

                if (employee == null)
                {
                    return NotFound($"Employee with id {id} not found.");
                }

                foreach (var qualificationDto in model.Qualifications)
                {
                var qualification = new Qualification
                {
                    Id = qualificationDto.Id,
                    QualificationName = qualificationDto.QualificationName,
                    Institution = qualificationDto.Institution,
                    YearOfPassing = qualificationDto.YearOfPassing,
                    Percentage = qualificationDto.Percentage,
                    Stream = qualificationDto.Stream,
                    EmployeeId = id
                };


                _dbContext.Qualifications.Add(qualification);
                }

                await _dbContext.SaveChangesAsync();
                return Ok();
        }
        [HttpGet("{id}/qualifications")]
        public async Task<ActionResult<IEnumerable<Qualification>>> GetQualifications(int id)
        {
            var employee = await _dbContext.Employees.FindAsync(id);

            if (employee == null)
            {
                return NotFound($"Employee with id {id} not found.");
            }

            var qualifications = await _dbContext.Qualifications
                .Where(q => q.EmployeeId == id)
                .Select(q => new QualificationDTO
                {
                    Id = q.Id,
                    QualificationName = q.QualificationName,
                    Institution = q.Institution,
                    YearOfPassing = q.YearOfPassing,
                    Percentage = q.Percentage,
                    Stream = q.Stream,
                    EmployeeId = q.EmployeeId
                })
                .ToListAsync();

            return Ok(qualifications);

        }
    }
}
public class QualificationsModel
{
    public IEnumerable<Qualification> Qualifications { get; set; }
}