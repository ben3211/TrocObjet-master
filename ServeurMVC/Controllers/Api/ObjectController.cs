using AutoMapper;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api;


[Microsoft.AspNetCore.Mvc.Route("api/{controller}")]
public class ObjectController : Controller
{

    private readonly MaDal db;
    private readonly IMapper mapper;
    public ObjectController(MaDal db, IMapper mapper)
    {
        this.db = db;
        this.mapper = mapper;
    }

    // GET api/object
    [HttpGet]
    public IEnumerable<SearchResult> Get(string searchText)
    {
        if (searchText == null)
        {
            searchText = "";
        }
        var daos = db.Objects.Where(c => c.Label.Contains(searchText))
                    .ToArray();
        return daos.Select(dao => new SearchResult()
        {
            Id = dao.IdObject,
            Label = dao.Label,
            Description = dao.Description,
            //Photos = dao.Photos.Select(p => p.Url).ToList()
        });
    }

    // GET api/object/ "guid"
    [HttpGet("{id:guid}")]
    public object GetObject(Guid id)
    {
        // var dao = db.Objects.Find(id);
        var dao = db.Objects.FirstOrDefault(c => c.IdObject == id);
        var model = mapper.Map<ObjectModel>(dao);
        //model.Photos = dao.Photos?.Select(p => new PhotoDAO { Path = p.Path }).ToList();
        return model;
    }

    // POST : http://localhost:5088/api/object
    [HttpPost]
    public async Task<object> PostObject([FromBody] ObjectModel postObject)
    {
        if (!ModelState.IsValid)
        {
            var messagesErreur = ModelState.SelectMany(c => c.Value.Errors).Select(c => c.ErrorMessage).ToArray();
            return BadRequest();
        }
        var dao = mapper.Map<ObjectDAO>(postObject);
        dao.IdObject = Guid.NewGuid();
        dao.IdOwner = Guid.Parse("D6726FD9-ED2D-44D3-90B9-DCFEFF1E3B75");// Mis en attendant l'authentification
        db.Objects.Add(dao);
        await db.SaveChangesAsync();
        return Ok(true);
    }

    // DELETE :  http://localhost:5088/api/object/id
    [HttpDelete("{id:guid}")]
    public async Task<object> DeleteObject(Guid id)
    {
        var dao = db.Objects.Find(id);
        if (dao == null)
        {
            return NotFound();
        }
        db.Objects.Remove(dao);
        await db.SaveChangesAsync();
        return Ok(true);
    }

    // PUT: api/object/{id}
    // [HttpPut("{id:guid}")]
    // public async Task<object> PutObject(Guid id, [FromBody]ObjectModel putObject) {
    //          if(!ModelState.IsValid){
    //         var messagesErreur = ModelState.SelectMany(c=>c.Value.Errors).Select(c=>c.ErrorMessage).ToArray();
    //         return BadRequest();
    //     }
    /////////////////////// crétation d'une nouvelle instence donc nouveau guig au lieu d'update!!!!!!!!!!!
    //     var dao = mapper.Map<ObjectDAO>(putObject);
    /////////////////////////////////////////////
    //     dao.IdOwner = Guid.Parse("07C137A3-4D46-432B-BCE3-11F6D8761372");// Mis en attendant l'authentification
    //     db.Objects.Add(dao);
    //     await db.SaveChangesAsync();
    //     return Ok(true);
    // }

    // PUT: api/object/{id}
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> PutObject(Guid id, [FromBody] ObjectModel putObject)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        // On recherche l'objet avec l'ID dans la DB
        var existingObject = await db.Objects.FindAsync(id);
        if (existingObject == null)
        {
            return NotFound();
        }

        // On met a jour l'objet existant avec les valeur du putObject
        existingObject.Label = putObject.Label;
        existingObject.Description = putObject.Description;
        existingObject.EstimatedPrice = putObject.EstimatedPrice;
        // existingObject.Photos = mapper.Map<List<PhotoDAO>>(putObject.Photos);

        try
        {
            await db.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!ObjectExists(id))
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
    private bool ObjectExists(Guid id)
    {
        return db.Objects.Any(e => e.IdObject == id);
    }
}