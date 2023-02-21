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
        model.Photos = dao.Photos?.Select(p => new PhotoDAO { Path = p.Path }).ToList();
        return model;
    }

    
    [HttpPost]
    public async Task<object> PostObject([FromBody]ObjectModel postObject){
        if(!ModelState.IsValid){
            var messagesErreur = ModelState.SelectMany(c=>c.Value.Errors).Select(c=>c.ErrorMessage).ToArray();
            return BadRequest();
        }
        var dao = mapper.Map<ObjectDAO>(postObject);
        dao.IdOwner = Guid.Parse("A0D08C5D-FA86-4246-983F-0DFFBED13C3D");// Mis en attendant l'authentification
        db.Objects.Add(dao);
        await db.SaveChangesAsync();
        return Ok(true);
    }

}