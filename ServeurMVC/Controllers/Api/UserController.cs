using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api;

[Microsoft.AspNetCore.Mvc.Route("api/{controller}")]

public class UserController : Controller
{
    private readonly MaDal db;
    private readonly IMapper mapper;
    private readonly UserManager<AccountDAO> userManager;

    public UserController(MaDal db, IMapper mapper, UserManager<AccountDAO> userManager)
    {
        this.db = db;
        this.mapper = mapper;
        this.userManager = userManager;
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

       //Get /api/user
    [HttpGet("{id}/objects")]
    public IEnumerable<ObjectModel> GetObjectsForUser(Guid id)
    {     var daos = db.Objects.Include(c=>c.Photos).Where(c => c.IdOwner == id);
        var models=this.mapper.Map<IEnumerable<ObjectModel>>(daos);
        return models.ToArray();
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
         var ac1 = new AccountDAO() { UserName = postUser.LastName };
        await  this.userManager.CreateAsync(ac1,password:"kikoo");

        var dao = mapper.Map<AppUserDAO>(postUser);
        //////////////////////////////////////////////////////////////////////////// A changer 
        dao.IdUser =ac1.Id; // Mis en attendant l'authentification
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