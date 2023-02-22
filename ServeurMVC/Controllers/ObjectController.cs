using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

public class ObjectController : Controller
{
    private readonly MaDal db;
    private readonly IMapper mapper;

    public ObjectController(MaDal db, IMapper mapper)
    {
        this.db = db;
        this.mapper = mapper;
    }

    // GET /Object
    public IActionResult Index(string searchText)
    {
        if (searchText == null)
        {
            searchText = "";
        }
        var daos = db.Objects.Where(c => c.Label.Contains(searchText));
        var objectModels = mapper.Map<IEnumerable<ObjectModel>>(daos);
        return View(objectModels);
    }

    // GET /Object/Details/F7C8F7E3-16CA-423C-AD29-01C6FE697F65
    public IActionResult Details(Guid id)
    {
        var obj = db.Objects.Include(c=>c.Owner).Include(c=>c.Photos).FirstOrDefault(c=>c.IdObject == id);

             
        // var obj = db.Objects.Find(id);
        if (obj == null)
        {
            // return NotFound(); // Si je souhaite que l'utilisateur reçoive une erreur 404
            return RedirectToAction("Index");
        }
        var modelObject = mapper.Map<ObjectModel>(obj);
        return View(modelObject);
    }
}