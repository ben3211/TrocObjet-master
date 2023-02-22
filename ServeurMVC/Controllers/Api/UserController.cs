using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api;

[Microsoft.AspNetCore.Mvc.Route("api/{controller}")]

public class UserController : Controller
{
    private readonly MaDal db;
    private readonly IMapper mapper;

    public UserController(MaDal db, IMapper mapper)
    {
        this.db = db;
        this.mapper = mapper;
    }

    //Get /api/user
    [HttpGet]
    public IEnumerable<SearchResult> Get(string searchText)
    {
        if (searchText == null)
        {
            searchText = "";
        }
        var daos = db.AppUsers.Where(c => c.FirstName.Contains(searchText)).ToArray();
        return daos.Select(dao => new SearchResult()
        {
            Id = dao.IdUser,
            Label = dao.FirstName,
            Description = dao.LastName
        });

    }

    //GET api/user/"guid"
    [HttpGet("{id:guid}")]
    public object GetUser(Guid id)
    {
        var dao = db.AppUsers.FirstOrDefault(c => c.IdUser == id);
        var model = mapper.Map<UserModel>(dao);
        return model;
    }

    //POST /api/user
    [HttpPost]
    public async Task<object> PostUser([FromBody] UserModel postUser)
    {
        if (!ModelState.IsValid)
        {
            var messagesErreur = ModelState.SelectMany(c => c.Value.Errors).Select(c => c.ErrorMessage).ToArray();
            return BadRequest();
        }
        var dao = mapper.Map<AppUserDAO>(postUser);
        //////////////////////////////////////////////////////////////////////////// A changer 
        dao.IdUser = Guid.Parse("3542B7FA-DD8B-4930-B13B-3ED8ECC63FBA"); // Mis en attendant l'authentification
        ////////////////////////////////////////////////////////////////////////////
        db.AppUsers.Add(dao);
        await db.SaveChangesAsync();
        return Ok(true);
    }

    //DELETE /api/user/ "guid"
    [HttpDelete("{id:guid}")]
    public async Task<object> DeleteUser(Guid id)
    {
        var dao = db.AppUsers.Find(id);
        if (dao == null)
        {
            return NotFound();
        }
        db.AppUsers.Remove(dao);
        await db.SaveChangesAsync();
        return Ok(true);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> PutUser(Guid id, [FromBody] UserModel putUser)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        // On recherche l'objet avec l'ID dans la DB
        var existingUser = await db.AppUsers.FindAsync(id);
        if (existingUser == null)
        {
            return NotFound();
        }

        // On met a jour l'objet existant avec les valeur du putObject
        existingUser.FirstName = putUser.FirstName;
        existingUser.LastName = putUser.LastName;
        existingUser.City = putUser.City;
        existingUser.PhoneNumber = putUser.PhoneNumber;

        try
        {
            await db.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!UserExists(id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return Ok(true);
    }

    // Regarde si un object avec l'ID donné éxiste déja dans la DB
    private bool UserExists(Guid id)
    {
        return db.AppUsers.Any(e => e.IdUser == id);
    }
}