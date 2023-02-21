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
                    .Include(c=>c.Photos).ToArray();
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
    public object GetObject(Guid id) {
        // var dao = db.Objects.Find(id);
        var dao = db.Objects.Include(c => c.Photos).FirstOrDefault(c => c.IdObject == id);
        var model=mapper.Map<ObjectModel>(dao);
        //model.Photos = dao.Photos?.Select(p => new PhotoDAO { Path = p.Path }).ToList();
        return model;
    }

    // POST : http://localhost:5088/api/object
    [HttpPost]
    public async Task<object> PostObject([FromBody]ObjectModel postObject){
        if(!ModelState.IsValid){
            var messagesErreur = ModelState.SelectMany(c=>c.Value.Errors).Select(c=>c.ErrorMessage).ToArray();
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
    public async Task<object> DeleteObject(Guid id){
        var dao = db.Objects.Find(id);
        if (dao == null) {
            return NotFound();
        }
        db.Objects.Remove(dao);
        await db.SaveChangesAsync();
        return Ok (true);
    }

    // PUT: api/object/{id}
    [HttpPut("{id:guid}")]
    public async Task<object> PutObject(Guid id, [FromBody]ObjectModel putObject) {
             if(!ModelState.IsValid){
            var messagesErreur = ModelState.SelectMany(c=>c.Value.Errors).Select(c=>c.ErrorMessage).ToArray();
            return BadRequest();
        }
        var dao = mapper.Map<ObjectDAO>(putObject);
        dao.IdOwner = Guid.Parse("07C137A3-4D46-432B-BCE3-11F6D8761372");// Mis en attendant l'authentification
        db.Objects.Add(dao);
        await db.SaveChangesAsync();
        return Ok(true);
        
    }
}