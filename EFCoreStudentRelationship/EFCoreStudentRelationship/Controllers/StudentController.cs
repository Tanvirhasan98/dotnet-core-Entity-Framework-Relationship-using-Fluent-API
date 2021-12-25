
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EFCoreStudentRelationship.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly SchoolContext _context;

        public StudentController(SchoolContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult<List<Student>>> Get(int studentId)
        {
            var students = await _context.Students
                .Include(a => a.Addresses)
                .Include(b => b.Courses)
                .Include(e => e.Grades)
                .Where(c => c.Id == studentId)
                .ToListAsync();

            return students;
        }



        [HttpPost]
        public async Task<ActionResult<List<Student>>> Create(Addressdto staddresses)
        {
            var user = await _context.Students.FindAsync(staddresses.StudentId);
            if (user == null)
                return NotFound();

            var newStudentAddress = new StudentAddress
            {
                Address = staddresses.Address,
                City = staddresses.City,
                State = staddresses.State,
                Country = staddresses.Country,
                Student = user
            };

            _context.StudentAddresses.Add(newStudentAddress);

            await _context.SaveChangesAsync();

            return await Get(staddresses.StudentId);
        }


        [HttpPut("{address}")]
        public async Task<ActionResult<List<Addressdto>>> UpdatePerson(Addressdto staddresses)
        {
            var dbaddress = await _context.StudentAddresses.FindAsync(staddresses.StudentAddressId);
            if (dbaddress == null)
                return BadRequest("Student address not found");

            dbaddress.Address = staddresses.Address;
            dbaddress.City = staddresses.City;
            dbaddress.State = staddresses.State;
            dbaddress.Country = staddresses.Country;
            dbaddress.StudentId=staddresses.StudentId;

            await _context.SaveChangesAsync();

            return Ok(await _context.StudentAddresses.ToListAsync());

        }


        [HttpDelete("{Studentaddress}")]
        public async Task<ActionResult<List<Addressdto>>> Delete(int id)
        {
            var dbaddress = await _context.StudentAddresses.FindAsync(id);
            if (dbaddress == null)
                return BadRequest("Person not found");
            _context.StudentAddresses.Remove(dbaddress);
            await _context.SaveChangesAsync();
            //return Okd(bperson);
            return Ok(await _context.StudentAddresses.ToListAsync());
        }

    }
}
