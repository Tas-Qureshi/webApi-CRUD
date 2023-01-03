using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("/api/[controller]")]
public class AddressBooksController : ControllerBase
{
    private readonly Db _db;
    public AddressBooksController(Db db)
    {
        _db = db;
    }

    [HttpGet]
    public IActionResult GetData()
    {
        var VM = new List<AddressBookGetData>();
        foreach (var item in _db.AddressBooks)
        {
            var book = new AddressBookGetData()
            {
                Id = item.Id,
                Name = item.Name,
                Street = item.Street,
                StreetNo = item.StreetNo,
                City = item.City,
                Email = item.Email,
                PhoneNumber = item.PhoneNumber
            };
            VM.Add(book);
        }
        return Ok(VM);
    }

    [HttpGet]
    [Route("{id}")]
    public IActionResult GetAddressBookById(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }
        var addressBook = _db.AddressBooks.FirstOrDefault(c => c.Id == id);

        var VM = new AddressBookGetData()
        {
            Id = addressBook.Id,
            Name = addressBook.Name,
            Street = addressBook.Street,
            StreetNo = addressBook.StreetNo,
            City = addressBook.City,
            Email = addressBook.Email,
            PhoneNumber = addressBook.PhoneNumber
        };

        return Ok(VM);
    }

    [HttpPost]
    public IActionResult Create(CreateAddressBook addressBookToAdd)
    {
        var nextId = _db.AddressBooks.Count + 1;
        AddressBook addressBook = new AddressBook(){
            Id = nextId,
            Name = addressBookToAdd.Name,
            Street = addressBookToAdd.Street,
            StreetNo = addressBookToAdd.StreetNo,
            City = addressBookToAdd.City,
            Email = addressBookToAdd.Email,
            PhoneNumber = addressBookToAdd.PhoneNumber
        };
        _db.AddressBooks.Add(addressBook);
        return CreatedAtAction(nameof(GetAddressBookById), new { id = nextId}, addressBook);
    }

    [HttpPut]
    [Route("{id}")]
    public IActionResult Update(int? id, UpdateAddressBook updateAddressBook)
    {
        if (id == null)
        {
            return NotFound();
        }
        var addressBook = _db.AddressBooks.FirstOrDefault(c => c.Id == id);

        if (addressBook != null)
        {
            addressBook.Name = updateAddressBook.Name;
            addressBook.Street = updateAddressBook.Street;
            addressBook.StreetNo = updateAddressBook.StreetNo;
            addressBook.City = updateAddressBook.City;
            addressBook.Email = updateAddressBook.Email;
            addressBook.PhoneNumber = updateAddressBook.PhoneNumber;
            
            return Ok(addressBook);
        }
        return BadRequest();
        
    }

    [HttpDelete]
    [Route("{id}")]
    public IActionResult Delete(int? id)    
    {
        if (id == null)
        {
            return NotFound();
        }
        var addressBook = _db.AddressBooks.FirstOrDefault(c => c.Id == id);
        if(addressBook != null)
        {
            _db.AddressBooks.Remove(addressBook);
            return Ok(addressBook);
        }
        return NotFound();
    }

}